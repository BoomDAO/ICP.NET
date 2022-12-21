using EdjCase.ICP.Candid;
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

		public static string GenerateClientSourceCode(string baseNamespace, ServiceSourceDescriptor desc, List<string>? importedNamespaces = null)
		{
			IndentedStringBuilder builder = new();

			WriteNamespace(builder, baseNamespace, () =>
			{
				WriteService(builder, desc);
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
			string baseNamespace,
			TypeSourceDescriptor type,
			List<string>? importedNamespaces = null)
		{
			IndentedStringBuilder builder = new();

			WriteNamespace(builder, baseNamespace + ".Models", () =>
			{
				WriteType(builder, type);
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
				source= builder.ToString();
			}

			return (type.Name, source);
		}

		public static string GenerateAliasSourceCode(string baseNamespace, Dictionary<string, string> aliases, bool useGlobal)
		{
			IndentedStringBuilder builder = new();
			string? prefix = useGlobal ? "global " : null;
			foreach ((string id, string aliasedType) in aliases)
			{
				builder.AppendLine($"{prefix}using {id} = {aliasedType};");
			}
			builder.AppendLine("");
			builder.AppendLine($"namespace {baseNamespace}.Models;");

			return builder.ToString();
		}

		private static void WriteType(IndentedStringBuilder builder, TypeSourceDescriptor type)
		{
			switch (type)
			{
				case VariantSourceDescriptor v:
					WriteVariant(builder, v);
					break;
				case RecordSourceDescriptor r:
					WriteRecord(builder, r);
					break;
				case ServiceSourceDescriptor s:
					WriteService(builder, s);
					break;
				case FuncSourceDescriptor f:
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

		private static void WriteService(IndentedStringBuilder builder, ServiceSourceDescriptor service)
		{
			string className = $"{service.Name}ApiClient";
			WriteClass(builder, className, () =>
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
					name: className,
					baseConstructorParams: null,
					("IAgent", "agent"),
					("Principal", "canisterId")
				);
				foreach ((string name, FuncSourceDescriptor func) in service.Methods)
				{
					List<(string TypeName, string VariableName)> args = func.Parameters
						.Where(p => p.TypeName != null) // exclude null/empty/reserved
						.Select((a, i) => (a.TypeName!, a.VariableName))
						.ToList();
					args.Add(("IIdentity?", "identityOverride = null"));
					List<(string Type, string Param)> returnTypes;
					if (func.IsFireAndForget)
					{
						// TODO confirm no return types, or even not async?
						returnTypes = new List<(string Type, string Param)>();
					}
					else
					{
						returnTypes = func.ReturnParameters
							.Where(p => p.TypeName != null) // exclude null/empty/reserved
							.Select(p => (p.TypeName!, p.VariableName))
							.ToList();
					}
					WriteMethod(
						builder,
						() =>
						{
							builder.AppendLine($"string method = \"{func.Name}\";");

							var parameterVariables = new List<string>();
							foreach (FuncSourceDescriptor.ParameterInfo parameter in func.Parameters)
							{
								int index = parameterVariables.Count;
								string variableName = "p" + index;
								string valueWithType;
								if (parameter.TypeName != null)
								{
									string isOptString = parameter.IsOpt ? "true" : "false";
									valueWithType = $"CandidValueWithType.FromObject<{parameter.TypeName}>({parameter.VariableName}, {isOptString})";
								}
								else
								{
									valueWithType = "CandidValueWithType.Null()";
								}
								builder.AppendLine($"CandidValueWithType {variableName} = {valueWithType};");
								parameterVariables.Add(variableName);
							}

							builder.AppendLine("var candidArgs = new List<CandidValueWithType>");
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
							if (func.IsQuery)
							{
								builder.AppendLine("QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);");
								builder.AppendLine("QueryReply reply = response.ThrowOrGetReply();");
								argVariableName = "reply.Arg";
							}
							else
							{
								builder.AppendLine("CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);");
								argVariableName = "responseArg";
							}

							if (returnTypes.Any())
							{
								var returnParamVariables = new List<string>();
								int i = 0;
								foreach (FuncSourceDescriptor.ParameterInfo parameter in func.ReturnParameters)
								{
									// Only include non null/empty/reserved params
									if (parameter.TypeName != null)
									{
										string variableName = "r" + i;
										string? orDefault = parameter.TypeName.EndsWith("?") ? "OrDefault" : null; // TODO better detection of optional
										builder.AppendLine($"{parameter.TypeName} {variableName} = {argVariableName}.Values[{i}].ToObject{orDefault}<{parameter.TypeName}>();");
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
						name: func.Name,
						baseConstructorParams: null,
						args.ToArray()
					);

				}


				foreach (TypeSourceDescriptor paramType in service.SubTypesToCreate)
				{
					WriteType(builder, paramType);
				}
			});
		}



		private static void WriteRecord(IndentedStringBuilder builder, RecordSourceDescriptor record)
		{
			WriteClass(builder, record.Name, () =>
			{
				foreach ((string field, string fieldFullTypeName) in record.Fields)
				{
					EscapeSafeString escapedName = FixNameEscape(field);
					builder.AppendLine($"public {fieldFullTypeName} {escapedName.Get()} {{ get; set; }}");
					builder.AppendLine("");
				}

				foreach (TypeSourceDescriptor paramType in record.SubTypesToCreate)
				{
					WriteType(builder, paramType);
				}

			});

		}

		private class EscapeSafeString
		{
			public bool Escaped { get; }
			private string value { get; }

			public EscapeSafeString(bool escaped, string value)
			{
				this.Escaped = escaped;
				this.value = value;
			}

			public string Get(bool prefixIfEscaped = true)
			{
				if (!this.Escaped)
				{
					return this.value;
				}
				if (prefixIfEscaped)
				{
					return "@" + this.value;
				}
				return this.value;
			}
		}

		private static EscapeSafeString FixNameEscape(string value)
		{
			bool isEscaped = value.StartsWith("\"") && value.EndsWith("\"");
			if (isEscaped)
			{
				value = value.Trim('"');
			}
			return new EscapeSafeString(isEscaped, value);
		}

		private static void WriteVariant(IndentedStringBuilder builder, VariantSourceDescriptor variant)
		{

			string className = variant.Name;
			string enumName = $"{className}Type";
			List<string> enumValues = variant.Options
				.Select(o => FixNameEscape(o.Name).Get())
				.ToList();
			WriteEnum(builder, enumName, enumValues);
			var implementationTypes = new List<string>
			{
				$"EdjCase.ICP.Candid.CandidVariantValueBase<{enumName}>"
			};
			WriteClass(builder, variant.Name, () =>
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
					name: className,
					baseConstructorParams: new List<string> { "type", "value" },
					(enumName, "type"),
					("object?", "value")
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
					name: className,
					baseConstructorParams: null
				);
				builder.AppendLine("");



				foreach ((string option, string? infoFullTypeName) in variant.Options)
				{
					EscapeSafeString escapedOption = FixNameEscape(option);
					if (infoFullTypeName == null)
					{
						WriteMethod(
							builder,
							inner: () =>
							{
								builder.AppendLine($"return new {className}({enumName}.{escapedOption.Get()}, null);");
							},
							access: "public",
							isStatic: true,
							isAsync: false,
							isConstructor: false,
							returnType: className,
							name: escapedOption.Get()
						);
					}
					else
					{
						WriteMethod(
							builder,
							inner: () =>
							{
								builder.AppendLine($"return new {className}({enumName}.{escapedOption.Get()}, info);");
							},
							access: "public",
							isStatic: true,
							isAsync: false,
							isConstructor: false,
							returnType: className,
							name: escapedOption.Get(),
					baseConstructorParams: null,
							(infoFullTypeName, "info")
						);
						builder.AppendLine("");

						WriteMethod(
							builder,
							inner: () =>
							{
								builder.AppendLine($"this.ValidateType({enumName}.{escapedOption.Get()});");
								builder.AppendLine($"return ({infoFullTypeName})this.value!;");
							},
							access: "public",
							isStatic: false,
							isAsync: false,
							isConstructor: false,
							returnType: infoFullTypeName,
							name: "As" + escapedOption.Get(prefixIfEscaped: false)
						);
					}
					builder.AppendLine("");

				}




				foreach (TypeSourceDescriptor paramType in variant.SubTypesToCreate)
				{
					WriteType(builder, paramType);
				}

			}, implementationTypes);
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
			string? returnType,
			string name,
			List<string>? baseConstructorParams = null,
			params (string Type, string Param)[] parameters)
		{
			List<(string, string)> returnTypes = new();
			if (returnType != null)
			{
				returnTypes.Add((returnType, returnType));
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
			List<(string Type, string Param)> returnTypes,
			string name,
			List<string>? baseConstructorParams = null,
			params (string Type, string Param)[] parameters)
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
						.Select(r => r.Type)
						.Single();
				}
				else
				{
					returnValue = $"({string.Join(", ", returnTypes.Select(r => $"{r.Type} {r.Param}"))})";
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
			string parametersString = string.Join(", ", parameters.Select(p => $"{p.Type} {p.Param}"));


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


		private static void WriteEnum(IndentedStringBuilder builder, string name, List<string> values)
		{
			builder.AppendLine($"public enum {name}");
			builder.AppendLine("{");
			using (builder.Indent())
			{
				foreach (string v in values)
				{
					builder.AppendLine(v + ",");
				}
			}
			builder.AppendLine("}");
		}

		private static void WriteClass(IndentedStringBuilder builder, string name, Action inner, List<string>? implementTypes = null)
		{
			string? implementations = null;
			if (implementTypes?.Any() == true)
			{
				implementations = " : " + string.Join(", ", implementTypes);
			}
			builder.AppendLine($"public class {name}{implementations}");
			builder.AppendLine("{");
			using (builder.Indent())
			{
				inner();
			}
			builder.AppendLine("}");
		}
	}
}
