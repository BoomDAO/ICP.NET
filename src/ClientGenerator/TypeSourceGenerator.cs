using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.ClientGenerator
{
	internal static class TypeSourceGenerator
	{
		private static Dictionary<Type, string> systemTypeShorthands = new Dictionary<Type, string>
		{
			{ typeof(string), "string" },
			{ typeof(byte), "byte" },
			{ typeof(ushort), "ushort" },
			{ typeof(uint), "uint" },
			{ typeof(ulong), "ulong" },
			{ typeof(sbyte), "sbyte" },
			{ typeof(short), "short" },
			{ typeof(int), "int" },
			{ typeof(long), "long" },
			{ typeof(float), "float" },
			{ typeof(double), "double" },
			{ typeof(bool), "bool" }
		};

		public static string GenerateClientSourceCode(TypeName clientName, string baseNamespace, ServiceSourceDescriptor desc, List<string>? importedNamespaces = null)
		{
			IndentedStringBuilder builder = new();

			WriteNamespace(builder, baseNamespace, () =>
			{
				WriteService(builder, clientName, desc);
			});
			string source = BuildSourceWithShorthands(builder);

			// Dont apply shorthands to namespaces by adding them to top after
			builder = new();
			if (importedNamespaces != null)
			{
				foreach (string n in importedNamespaces)
				{
					builder.AppendLine($"using {n};");
				}
			}
			builder.AppendLine("using EdjCase.ICP.Agent.Agents;");
			builder.AppendLine("using EdjCase.ICP.Agent.Responses;");
			builder.AppendLine("using EdjCase.ICP.Agent.Auth;");
			builder.AppendLine("using EdjCase.ICP.Candid.Models;");
			builder.AppendLine($"using {baseNamespace}.Models;");
			builder.AppendLine("");
			builder.AppendLine(source);
			return builder.ToString();
		}
		public static (string FileName, string SourceCode) GenerateTypeSourceCode(
			TypeName typeName,
			string baseNamespace,
			TypeSourceDescriptor type,
			List<string>? importedNamespaces = null)
		{
			IndentedStringBuilder builder = new();

			WriteNamespace(builder, baseNamespace + ".Models", () =>
			{
				WriteType(builder, typeName, type);
			});
			string source = BuildSourceWithShorthands(builder);


			if (importedNamespaces != null)
			{
				// Append before source to avoid shorthand replacement
				builder = new();
				foreach (string n in importedNamespaces)
				{
					builder.AppendLine($"using {n};");
				}
				builder.AppendLine("");
				builder.AppendLine(source);
				source = builder.ToString();
			}

			return (typeName.GetName(), source);
		}

		public static string GenerateAliasSourceCode(string baseNamespace, List<(string Alias, TypeName Type)> aliases, bool useGlobal)
		{
			IndentedStringBuilder builder = new();
			string? prefix = useGlobal ? "global " : null;
			foreach ((string id, TypeName aliasedType) in aliases)
			{
				builder.AppendLine($"{prefix}using {id} = {aliasedType.GetNamespacedName()};");
			}
			builder.AppendLine("");
			builder.AppendLine($"namespace {baseNamespace}.Models;");

			return builder.ToString();
		}

		private static void WriteType(IndentedStringBuilder builder, TypeName typeName, TypeSourceDescriptor type)
		{
			switch (type)
			{
				case VariantSourceDescriptor v:
					WriteVariant(builder, typeName, v);
					break;
				case RecordSourceDescriptor r:
					WriteRecord(builder, typeName, r);
					break;
				case ServiceSourceDescriptor s:
					WriteService(builder, typeName, s);
					break;
				case FuncSourceDescriptor:
					// Func is CandidFunc, no need to create another definition
					break;
				default:
					throw new NotImplementedException();
			};
		}

		private static string BuildSourceWithShorthands(IndentedStringBuilder builder)
		{
			string source = builder.ToString();
			foreach ((Type systemType, string shortHand) in systemTypeShorthands)
			{
				// Convert system types to shorten versions
				string fullTypeName = systemType.FullName ?? throw new Exception($"Type '{systemType}' has no full name");
				source = source.Replace(fullTypeName, shortHand);
			}
			return source;
		}

		private static void WriteService(IndentedStringBuilder builder, TypeName serviceName, ServiceSourceDescriptor service)
		{
			WriteClass(builder, serviceName, () =>
			{
				builder.AppendLine("public IAgent Agent { get; }");
				builder.AppendLine("public Principal CanisterId { get; }");
				// Constrcutor
				WriteMethod(
					builder,
					inner: () =>
					{
						builder.AppendLine("this.Agent = agent ?? throw new ArgumentNullException(nameof(agent));");
						builder.AppendLine("this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));");
					},
					access: "public",
					isStatic: false,
					isAsync: false,
					isConstructor: true,
					returnType: null,
					name: serviceName.GetName(),
					baseConstructorParams: null,
					TypedParam.FromType(new TypeName("IAgent", "EdjCase.ICP.Agent.Agents"), ValueName.Default("agent")),
					TypedParam.FromType(new TypeName("Principal", "EdjCase.ICP.Candid.Models"), ValueName.Default("canisterId"))
				);
				foreach ((string methodName, TypeName funcName, FuncSourceDescriptor funcDesc) in service.Methods)
				{
					List<TypedParam> args = funcDesc.Parameters
						.Where(p => p.Type != null) // exclude null/empty/reserved
						.Select((a, i) => TypedParam.FromType(a.Type!, a.Name))
						.ToList();
					args.Add(new TypedParam($"{typeof(IIdentity).FullName!}?", "identityOverride", "null"));
					List<TypedValueName> returnTypes;
					if (funcDesc.IsFireAndForget)
					{
						// TODO confirm no return types, or even not async?
						returnTypes = new();
					}
					else
					{
						returnTypes = funcDesc.ReturnParameters
							.Where(p => p.Type != null) // exclude null/empty/reserved
							.Select((p, i) => new TypedValueName(p.Type!, p.Name ?? ValueName.Default($"R{i}")))
							.ToList();
					}
					WriteMethod(
						builder,
						() =>
						{
							builder.AppendLine($"string method = \"{methodName}\";");

							var parameterVariables = new List<string>();
							foreach (FuncSourceDescriptor.ParameterInfo param in funcDesc.Parameters)
							{
								int index = parameterVariables.Count;
								string variableName = "p" + index;
								string valueWithType;
								if (param.Type != null)
								{
									valueWithType = $"CandidTypedValue.FromObject({param.Name.VariableName})";
								}
								else
								{
									valueWithType = "CandidTypedValue.Null()";
								}
								builder.AppendLine($"CandidTypedValue {variableName} = {valueWithType};");
								parameterVariables.Add(variableName);
							}

							builder.AppendLine("var candidArgs = new List<CandidTypedValue>");
							builder.AppendLine("{");
							using (builder.Indent())
							{
								foreach (string v in parameterVariables)
								{
									builder.AppendLine(v + ",");
								}
							}
							builder.AppendLine("};");
							builder.AppendLine("CandidArg arg = CandidArg.FromCandid(candidArgs);");
							string argVariableName;
							if (funcDesc.IsQuery)
							{
								builder.AppendLine("QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);");
								builder.AppendLine("QueryReply reply = response.ThrowOrGetReply();");
								argVariableName = "reply.Arg";
							}
							else
							{
								builder.AppendLine("CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);");
								argVariableName = "responseArg";
							}

							if (returnTypes.Any())
							{
								var returnParamVariables = new List<string>();
								int i = 0;
								foreach (FuncSourceDescriptor.ParameterInfo parameter in funcDesc.ReturnParameters)
								{
									// Only include non null/empty/reserved params
									if (parameter.Type != null)
									{
										string variableName = "r" + i;
										string? optionalString = parameter.IsOpt ? "Optional" : null;
										builder.AppendLine($"{parameter.Type.GetNamespacedName()} {variableName} = {argVariableName}.Values[{i}].To{optionalString}Object<{parameter.Type.GetNamespacedName()}>();");
										returnParamVariables.Add(variableName);
									}
									i++;
								}
								string returnString = string.Join(", ", returnParamVariables);
								builder.AppendLine($"return ({returnString});");
							}
						},
						access: "public",
						isStatic: false,
						isAsync: true,
						isConstructor: false,
						returnTypes: returnTypes,
						name: methodName,
						baseConstructorParams: null,
						args.ToArray()
					);

				}
			});
		}



		private static void WriteRecord(IndentedStringBuilder builder, TypeName recordName, RecordSourceDescriptor record)
		{
			WriteClass(builder, recordName, () =>
			{
				foreach ((ValueName field, TypeName fieldFullTypeName) in record.Fields)
				{
					builder.AppendLine($"[{typeof(CandidNameAttribute).FullName}(\"{field.CandidName}\")]");
					builder.AppendLine($"public {fieldFullTypeName.GetNamespacedName()} {field.PropertyName} {{ get; set; }}");
					builder.AppendLine("");
				}
			});

		}


		private static void WriteVariant(IndentedStringBuilder builder, TypeName variantName, VariantSourceDescriptor variant)
		{
			TypeName enumName = new TypeName(variantName.GetName() + "Type", null);
			WriteEnum(builder, enumName, variant.Options);
			var implementationTypes = new List<TypeName>
			{
				new TypeName("CandidVariantValueBase", "EdjCase.ICP.Candid.Models", enumName)
			};
			WriteClass(builder, variantName, () =>
			{
				// Constrcutor
				WriteMethod(
					builder,
					inner: () =>
					{
					},
					access: "public",
					isStatic: false,
					isAsync: false,
					isConstructor: true,
					returnType: null,
					name: variantName.GetName(),
					baseConstructorParams: new List<string> { "type", "value" },
					TypedParam.FromType(enumName, ValueName.Default("type")),
					TypedParam.FromType(new TypeName("Object?", "System"), ValueName.Default("value"))
				);
				builder.AppendLine("");

				// Empty Constrcutor for reflection
				WriteMethod(
					builder,
					inner: () =>
					{
					},
					access: "protected",
					isStatic: false,
					isAsync: false,
					isConstructor: true,
					returnType: null,
					name: variantName.GetName(),
					baseConstructorParams: null
				);
				builder.AppendLine("");



				foreach ((ValueName valueName, TypeName? typeName) in variant.Options)
				{
					if (typeName == null)
					{
						WriteMethod(
							builder,
							inner: () =>
							{
								builder.AppendLine($"return new {variantName.GetNamespacedName()}({enumName.GetNamespacedName()}.{valueName.PropertyName}, null);");
							},
							access: "public",
							isStatic: true,
							isAsync: false,
							isConstructor: false,
							returnType: new TypedValueName(variantName, valueName),
							name: valueName.PropertyName
						);
					}
					else
					{
						ValueName paramName = ValueName.Default("info");
						WriteMethod(
							builder,
							inner: () =>
							{
								builder.AppendLine($"return new {variantName.GetNamespacedName()}({enumName.GetNamespacedName()}.{valueName.PropertyName}, {paramName.VariableName});");
							},
							access: "public",
							isStatic: true,
							isAsync: false,
							isConstructor: false,
							returnType: new TypedValueName(variantName, ValueName.Default("type")),
							name: valueName.PropertyName,
							baseConstructorParams: null,
							TypedParam.FromType(typeName, paramName)
						);
						builder.AppendLine("");

						WriteMethod(
							builder,
							inner: () =>
							{
								builder.AppendLine($"this.ValidateType({enumName.GetNamespacedName()}.{valueName.PropertyName});");
								builder.AppendLine($"return ({typeName.GetNamespacedName()})this.value!;");
							},
							access: "public",
							isStatic: false,
							isAsync: false,
							isConstructor: false,
							returnType: new TypedValueName(typeName, valueName),
							name: "As" + valueName.PropertyName
						);
					}
					builder.AppendLine("");

				}

			}, implementationTypes);
		}


		private class TypedParam
		{
			public string Type { get; }
			public string Name { get; }
			public string? OptType { get; }

			public TypedParam(string type, string name, string? optType = null)
			{
				this.Type = type ?? throw new ArgumentNullException(nameof(type));
				this.Name = name ?? throw new ArgumentNullException(nameof(name));
				this.OptType = optType;
			}

			public static TypedParam FromType(TypeName type, ValueName name)
			{
				return new TypedParam(type.GetNamespacedName(), name.VariableName);
			}
		}


		private static void WriteNamespace(IndentedStringBuilder builder, string name, Action inner)
		{
			builder.AppendLine($"namespace {name}");
			builder.AppendLine("{");
			using (builder.Indent())
			{
				inner();
			}
			builder.AppendLine("}");
		}

		private static void WriteMethod(
			IndentedStringBuilder builder,
			Action inner,
			string? access,
			bool isStatic,
			bool isAsync,
			bool isConstructor,
			TypedValueName? returnType,
			string name,
			List<string>? baseConstructorParams = null,
			params TypedParam[] parameters)
		{
			List<TypedValueName> returnTypes = new();
			if (returnType != null)
			{
				returnTypes.Add(returnType);
			}
			WriteMethod(builder, inner, access, isStatic, isAsync, isConstructor, returnTypes, name, baseConstructorParams, parameters);
		}

		private static void WriteMethod(
			IndentedStringBuilder builder,
			Action inner,
			string? access,
			bool isStatic,
			bool isAsync,
			bool isConstructor,
			List<TypedValueName> returnTypes,
			string name,
			List<string>? baseConstructorParams = null,
			params TypedParam[] parameters)
		{
			var methodItems = new List<string>();
			if (access != null)
			{
				methodItems.Add(access);
			}
			if (!isConstructor)
			{
				if (isStatic)
				{
					methodItems.Add("static");
				}

				string returnValue;
				if (!returnTypes.Any())
				{
					returnValue = "void";
				}
				else if (returnTypes.Count == 1)
				{
					returnValue = returnTypes
						.Select(r => r.Type.GetNamespacedName())
						.Single();
				}
				else
				{
					returnValue = $"({string.Join(", ", returnTypes.Select(r => $"{r.Type.GetNamespacedName()} {r.Value.PropertyName}"))})";
				}
				if (isAsync)
				{
					if (!returnTypes.Any())
					{
						returnValue = "async Task";
					}
					else
					{
						returnValue = $"async Task<{returnValue}>";
					}
				}

				methodItems.Add(returnValue);
			}
			string parametersString = string.Join(", ", parameters.Select(p => $"{p.Type} {p.Name}{(p.OptType == null ? null : $" = {p.OptType}")}"));


			methodItems.Add($"{name}({parametersString})");

			if (isConstructor)
			{
				if (baseConstructorParams != null)
				{
					methodItems.Add($" : base({string.Join(", ", baseConstructorParams)})");
				}
			}
			builder.AppendLine($"{string.Join(" ", methodItems)}");
			builder.AppendLine("{");
			using (builder.Indent())
			{
				inner();
			}
			builder.AppendLine("}");
		}


		private static void WriteEnum(IndentedStringBuilder builder, TypeName enumName, List<(ValueName Name, TypeName? Type)> values)
		{
			builder.AppendLine($"public enum {enumName.GetName()}");
			builder.AppendLine("{");
			using (builder.Indent())
			{
				foreach ((ValueName v, TypeName? type) in values)
				{
					builder.AppendLine($"[{typeof(CandidNameAttribute).FullName}(\"{v.CandidName}\")]");
					if (type != null)
					{
						builder.AppendLine($"[{typeof(VariantOptionTypeAttribute).FullName}(typeof({type.GetNamespacedName()}))]");
					}
					builder.AppendLine(v.PropertyName + ",");
				}
			}
			builder.AppendLine("}");
		}

		private static void WriteClass(IndentedStringBuilder builder, TypeName name, Action inner, List<TypeName>? implementTypes = null)
		{
			string? implementations = null;
			if (implementTypes?.Any() == true)
			{
				implementations = " : " + string.Join(", ", implementTypes.Select(t => t.GetNamespacedName()));
			}
			builder.AppendLine($"public class {name.GetName()}{implementations}");
			builder.AppendLine("{");
			using (builder.Indent())
			{
				inner();
			}
			builder.AppendLine("}");
		}
	}
}
