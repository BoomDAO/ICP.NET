using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;

namespace ICP.ClientGenerator
{
	internal class TypeSourceGenerator
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

		public string GenerateClientSourceCode(TypeName clientName, string baseNamespace, ServiceSourceCodeType desc, List<string>? importedNamespaces = null)
		{
			IndentedStringBuilder builder = new();

			WriteNamespace(builder, baseNamespace, () =>
			{
				this.WriteService(builder, clientName, desc);
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
			builder.AppendLine("using EdjCase.ICP.Candid.Models;");
			builder.AppendLine($"using {baseNamespace}.Models;");
			builder.AppendLine("");
			builder.AppendLine(source);
			return builder.ToString();
		}
		public static (string FileName, string SourceCode) GenerateTypeSourceCode(
			ValueName id,
			string baseNamespace,
			Action<IndentedStringBuilder> writeType,
			List<string>? importedNamespaces = null)
		{
			IndentedStringBuilder builder = new();

			WriteNamespace(builder, baseNamespace + ".Models", () =>
			{
				writeType(builder);
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

			return (id.PascalCaseValue, source);
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

		private void WriteService(IndentedStringBuilder builder, TypeName serviceName, ServiceSourceCodeType service)
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
				foreach ((ValueName methodName, ServiceSourceCodeType.Func funcDesc) in service.Methods)
				{
					List<TypedValueName> resolvedArgTypes = this.ResolveTypes(builder, funcDesc.ArgTypes);

					List<TypedValueName> resolvedReturnTypes = funcDesc.IsFireAndForget
						? new() // TODO confirm no return types, or even not async?
						: this.ResolveTypes(builder, funcDesc.ReturnTypes);

					List<TypedParam> argsForMethod = resolvedArgTypes
						.Select((a, i) => TypedParam.FromType(a.Type, a.Value))
						.ToList();

					WriteMethod(
						builder,
						() =>
						{
							builder.AppendLine($"string method = \"{methodName.CandidName}\";");

							var parameterVariables = new List<string>();
							foreach (TypedValueName param in resolvedArgTypes)
							{
								int index = parameterVariables.Count;
								string variableName = "p" + index;
								string valueWithType;
								if (param.Type != null)
								{
									valueWithType = $"CandidTypedValue.FromObject({param.Value.CamelCaseValue})";
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
								builder.AppendLine("QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg);");
								builder.AppendLine("CandidArg reply = response.ThrowOrGetReply();");
								argVariableName = "reply";
							}
							else
							{
								builder.AppendLine("CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg);");
								argVariableName = "responseArg";
							}

							if (!funcDesc.IsFireAndForget && resolvedReturnTypes.Any())
							{
								var returnParamVariables = new List<string>();
								int i = 0;
								foreach (TypedValueName parameter in resolvedReturnTypes)
								{
									// Only include non null/empty/reserved params
									if (parameter.Type != null)
									{
										string variableName = "r" + i;
										string? optionalString = null;
										string variableTypeName = parameter.Type.GetNamespacedName();
										builder.AppendLine($"{variableTypeName} {variableName} = {argVariableName}.Values[{i}].To{optionalString}Object<{parameter.Type.GetNamespacedName()}>();");
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
						returnTypes: funcDesc.IsFireAndForget ? new() : resolvedReturnTypes,
						name: methodName.PascalCaseValue,
						baseConstructorParams: null,
						argsForMethod.ToArray()
					);

				}
			});
		}

		private List<TypedValueName> ResolveTypes(IndentedStringBuilder builder, List<(ValueName Name, SourceCodeType Type)> argTypes)
		{
			List<TypedValueName> resolvedTypes = new();
			int argIndex = 0;
			foreach ((ValueName argName, SourceCodeType argType) in argTypes)
			{
				string nameContext = "Arg" + argIndex;
				(TypeName? type, _) = this.ResolveType(argType, nameContext, out Action<IndentedStringBuilder>? argTypeBuilder);
				if (argTypeBuilder != null)
				{
					argTypeBuilder(builder);
				}
				// Skip null types, means they are null, empty or reserved
				if (type != null)
				{
					resolvedTypes.Add(new TypedValueName(type, argName));
				}
			}
			return resolvedTypes;
		}

		internal void WriteRecord(IndentedStringBuilder builder, TypeName recordName, RecordSourceCodeType record)
		{
			WriteClass(builder, recordName, () =>
			{
				int i = 0;
				foreach ((ValueName fieldName, SourceCodeType fieldType) in record.Fields)
				{
					(TypeName? fieldTypeName, _) = this.ResolveType(fieldType, "R" + i, out Action<IndentedStringBuilder>? typeBuilder);
					i++;
					if (typeBuilder != null)
					{
						typeBuilder(builder);
					}

					string propertyName = fieldName.PascalCaseValue;
					if (propertyName == recordName.GetName())
					{
						// Cant match the class name
						propertyName = propertyName + "_"; // TODO best way to escape it. @ does not work
					}
					builder.AppendLine($"[{typeof(CandidNameAttribute).FullName}(\"{fieldName.CandidName}\")]");
					builder.AppendLine($"public {fieldTypeName!.GetNamespacedName()} {propertyName} {{ get; set; }}");
					builder.AppendLine("");
				}
			});

		}


		internal void WriteVariant(IndentedStringBuilder builder, TypeName variantName, VariantSourceCodeType variant)
		{
			TypeName enumName = new(variantName.GetName() + "Tag", null);
			var implementationTypes = new List<TypeName>();
			var enumOptions = new List<(ValueName Name, TypeName? Type)>();

			builder.AppendLine($"[{typeof(VariantAttribute).FullName}(typeof({enumName.GetNamespacedName()}))]");
			WriteClass(builder, variantName, () =>
			{
				// Type Property
				builder.AppendLine($"[{typeof(VariantTagPropertyAttribute).FullName}]");
				builder.AppendLine($"public {enumName.GetNamespacedName()} Tag {{ get; set; }}");

				// Value Property
				builder.AppendLine($"[{typeof(VariantValuePropertyAttribute).FullName}]");
				builder.AppendLine($"public object? Value {{ get; set; }}");

				// Constrcutor
				WriteMethod(
					builder,
					inner: () =>
					{
						builder.AppendLine("this.Tag = tag;");
						builder.AppendLine("this.Value = value;");
					},
					access: "private",
					isStatic: false,
					isAsync: false,
					isConstructor: true,
					returnType: null,
					name: variantName.GetName(),
					baseConstructorParams: null,
					TypedParam.FromType(enumName, ValueName.Default("tag")),
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


				bool anyOptionsWithType = false;
				int i = 0;
				foreach ((ValueName optionName, SourceCodeType optionType) in variant.Options)
				{
					string backupOptionName = "O" + i;
					i++;
					(TypeName? optionTypeName, bool customType) = this.ResolveType(optionType, backupOptionName, out Action<IndentedStringBuilder>? optionTypeBuilder);

					if (optionTypeBuilder != null)
					{
						optionTypeBuilder(builder);
					}
					if (optionTypeName != null && customType)
					{
						// Prefix with parent name if subtype
						optionTypeName = optionTypeName.WithParentType(variantName);
					}
					enumOptions.Add((optionName, optionTypeName));
					if (optionTypeName == null)
					{
						WriteMethod(
							builder,
							inner: () =>
							{
								builder.AppendLine($"return new {variantName.GetNamespacedName()}({enumName.GetNamespacedName()}.{optionName.PascalCaseValue}, null);");
							},
							access: "public",
							isStatic: true,
							isAsync: false,
							isConstructor: false,
							returnType: new TypedValueName(variantName, optionName),
							name: optionName.PascalCaseValue
						);
					}
					else
					{
						anyOptionsWithType = true;
						ValueName paramName = ValueName.Default("info");
						WriteMethod(
							builder,
							inner: () =>
							{
								builder.AppendLine($"return new {variantName.GetNamespacedName()}({enumName.GetNamespacedName()}.{optionName.PascalCaseValue}, {paramName.CamelCaseValue});");
							},
							access: "public",
							isStatic: true,
							isAsync: false,
							isConstructor: false,
							returnType: new TypedValueName(variantName, ValueName.Default("type")),
							name: optionName.PascalCaseValue,
							baseConstructorParams: null,
							TypedParam.FromType(optionTypeName, paramName)
						);
						builder.AppendLine("");

						WriteMethod(
							builder,
							inner: () =>
							{
								builder.AppendLine($"this.ValidateTag({enumName.GetNamespacedName()}.{optionName.PascalCaseValue});");
								builder.AppendLine($"return ({optionTypeName.GetNamespacedName()})this.Value!;");
							},
							access: "public",
							isStatic: false,
							isAsync: false,
							isConstructor: false,
							returnType: new TypedValueName(optionTypeName, optionName),
							name: "As" + optionName.PascalCaseValue
						);
					}
					builder.AppendLine("");

				}

				if (anyOptionsWithType)
				{
					WriteMethod(
						builder,
						inner: () =>
						{
							builder.AppendLine($"if (!this.Tag.Equals(tag))");
							builder.AppendLine("{");
							builder.AppendLine("	throw new InvalidOperationException($\"Cannot cast '{this.Tag}' to type '{tag}'\");");
							builder.AppendLine("}");
						},
						access: "private",
						isStatic: false,
						isAsync: false,
						isConstructor: false,
						returnType: null,
						name: "ValidateTag",
						baseConstructorParams: null,
						new TypedParam(enumName.GetNamespacedName(), "tag")
					);
				}

			}, implementationTypes);



			WriteEnum(builder, enumName, enumOptions);
		}

		private readonly Dictionary<string, (TypeName? Type, bool CustomType, Action<IndentedStringBuilder>? TypeBuilder)> _resolvedTypes = new();

		internal (TypeName? Type, bool CustomType) ResolveTypeDeclaration(ValueName typeName, SourceCodeType type, out Action<IndentedStringBuilder>? typeBuilder)
		{
			// note that this only works for only one level of type nesting, so type aliases to generics whose argument is a user-defined type
			// will fail, for example:
			//    type A<T> = record { left : A, right : B };
			//    type X = blob;
			//    type F = A<X>;

			var typeNameStr = typeName.PascalCaseValue;

			if (this._resolvedTypes.TryGetValue(typeNameStr, out var existing))
			{
				typeBuilder = null;
				return (existing.Type, existing.CustomType);
			}
			else
			{
				var res = this.ResolveType_Impl(type, typeNameStr, isDeclaration: true, out typeBuilder);
				this._resolvedTypes[typeNameStr] = (res.Item1, res.Item2, typeBuilder);
				return res;
			}
		}

		internal (TypeName? Type, bool CustomType) ResolveType(SourceCodeType type, string nameContext, out Action<IndentedStringBuilder>? typeBuilder)
		{
			return this.ResolveType_Impl(type, nameContext, isDeclaration: false, out typeBuilder);
		}

		internal (TypeName? Type, bool CustomType) ResolveType_Impl(SourceCodeType type, string nameContext, bool isDeclaration, out Action<IndentedStringBuilder>? typeBuilder)
		{
			switch (type)
			{
				case NullEmptyOrReservedSourceCodeType:
					typeBuilder = null;
					return (null, false);
				case CsharpTypeSourceCodeType c:
					List<(TypeName, Action<IndentedStringBuilder>?)> genericTypes = new();
					if (!c.GenericTypes.Any())
					{
						typeBuilder = null;
					}
					else
					{
						int i = 0;
						foreach (var t in c.GenericTypes)
						{
							(TypeName? n, bool inline) = this.ResolveType(t, nameContext + "V" + i, out Action<IndentedStringBuilder>? genericTypeBuilder);
							i++;
							if (n != null)
							{
								genericTypes.Add((n, genericTypeBuilder));
							}
						}
						typeBuilder = (builder) =>
						{
							// Build each sub type
							foreach ((TypeName t, Action<IndentedStringBuilder>? tBuilder) in genericTypes)
							{
								if (tBuilder != null)
								{
									tBuilder(builder);
								}
							}
						};
					}
					string typeName = c.Type.Name;
					if (c.Type.IsGenericTypeDefinition)
					{
						// Remove the `1 from the end
						typeName = typeName.Substring(0, typeName.Length - 2);
					}
					var cType = new TypeName(typeName, c.Type.Namespace, genericTypes.Select(t => t.Item1).ToArray());
					return (cType, false)
;
				case ReferenceSourceCodeType re:
					{
						string correctedRefId = StringUtil.ToPascalCase(re.Id.ToString()); // TODO casing?
						typeBuilder = null;

						if (isDeclaration)
						{
							if (this._resolvedTypes.TryGetValue(correctedRefId, out var existing))
							{
								typeBuilder = null;
								return (existing.Type, false);
							}
							else
							{
								throw new System.InvalidOperationException("Candid type reference to another user-defined type; but that type is not yet defined");
							}
						}
						else
						{
							return (new TypeName(correctedRefId, null), false);
						}
					}

				case VariantSourceCodeType v:
					TypeName variantName = new TypeName(nameContext, null);
					typeBuilder = (builder) => this.WriteVariant(builder, variantName, v);
					return (variantName, true);
				case RecordSourceCodeType r:
					TypeName recordName = new TypeName(nameContext, null);
					typeBuilder = (builder) => this.WriteRecord(builder, recordName, r);
					return (recordName, true);
				case ServiceSourceCodeType s:
					TypeName serviceName = new TypeName(nameContext, null);
					typeBuilder = (builder) => this.WriteService(builder, serviceName, s);
					return (serviceName, true);
				default:
					throw new NotImplementedException();
			}
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
				return new TypedParam(type.GetNamespacedName(), name.CamelCaseValue);
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
					returnValue = $"({string.Join(", ", returnTypes.Select(r => $"{r.Type.GetNamespacedName()} {r.Value.PascalCaseValue}"))})";
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
					builder.AppendLine(v.PascalCaseValue + ",");
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
