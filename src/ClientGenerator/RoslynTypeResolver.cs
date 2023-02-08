using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models.Values;
using ICP.ClientGenerator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using System.Xml.Linq;
using CommandLine;
using Microsoft.VisualBasic.FileIO;

namespace EdjCase.ICP.ClientGenerator
{
	internal class RoslynTypeResolver
	{
		private readonly Dictionary<string, ResolvedType?> _resolvedTypes = new();

		public ResolvedType? ResolveTypeDeclaration(ValueName typeName, SourceCodeType type)
		{
			// note that this only works for only one level of type nesting, so type aliases to generics whose argument is a user-defined type
			// will fail, for example:
			//    type A<T> = record { left : A, right : B };
			//    type X = blob;
			//    type F = A<X>;

			string typeNameStr = typeName.PascalCaseValue;

			if (this._resolvedTypes.TryGetValue(typeNameStr, out ResolvedType? existing))
			{
				return existing;
			}
			ResolvedType? res = this.ResolveTypeInner(type, typeNameStr, isDeclaration: true);
			this._resolvedTypes[typeNameStr] = res;
			return res;
		}

		public ResolvedType? ResolveType(SourceCodeType type, string nameContext)
		{
			return this.ResolveTypeInner(type, nameContext, isDeclaration: false);
		}

		private ResolvedType? ResolveTypeInner(SourceCodeType type, string nameContext, bool isDeclaration)
		{
			switch (type)
			{
				case NullEmptyOrReservedSourceCodeType:
					return null;
				case CsharpBuiltInTypeSourceCodeType c:
					{
						List<ResolvedType> genericTypes = c.GenericTypes
							.Select((t, i) => this.ResolveType(t, nameContext + "V" + i) ?? throw new InvalidOperationException("Generic types cant be null/empty"))
							.ToList();

						string typeName = c.Type.Name;
						if (c.Type.IsGenericTypeDefinition)
						{
							// Remove the `1 from the end
							typeName = typeName[..^2];
						}
						var cType = new TypeName(
							typeName,
							c.Type.Namespace,
							genericTypes.Select(t => t.Name).ToArray()
						);

						MemberDeclarationSyntax[] innerTypes = genericTypes
							.Where(r => r.GeneratedSyntax != null)
							.SelectMany(t => t.GeneratedSyntax!)
							.ToArray();
						return new ResolvedType(cType, innerTypes);
					}
				case ReferenceSourceCodeType re:
					{
						string correctedRefId = StringUtil.ToPascalCase(re.Id.ToString()); // TODO casing?

						// If type declared directly, then build type. Otherwise just reference it
						if (isDeclaration)
						{
							if (this._resolvedTypes.TryGetValue(correctedRefId, out ResolvedType? existing))
							{
								return new ResolvedType(existing.Name);
							}
							else
							{
								throw new InvalidOperationException("Candid type reference to another user-defined type; but that type is not yet defined");
							}
						}
						else
						{
							return new ResolvedType(new TypeName(correctedRefId, null));
						}
					}

				case VariantSourceCodeType v:
					{
						TypeName variantName = new (nameContext, null);
						(ClassDeclarationSyntax classSyntax, EnumDeclarationSyntax typeSyntax) = this.GenerateVariant(variantName, v);
						return new ResolvedType(variantName, classSyntax, typeSyntax);
					}
				case RecordSourceCodeType r:
					{
						TypeName recordName = new TypeName(nameContext, null);
						ClassDeclarationSyntax classSyntax = this.GenerateRecord(recordName, r);
						return new ResolvedType(recordName, classSyntax);
					}
				case ServiceSourceCodeType s:
					{
						TypeName serviceName = new TypeName(nameContext, null);
						ClassDeclarationSyntax classSyntax = this.GenerateService(serviceName, s);
						return new ResolvedType(serviceName, classSyntax);
					}
				default:
					throw new NotImplementedException();
			}
		}

		public ClassDeclarationSyntax GenerateType(ValueName id)
		{

		}

		public ClassDeclarationSyntax GenerateClient(TypeName clientName, ServiceSourceCodeType service)
		{
			List<MethodDeclarationSyntax> methods = service.Methods
				.Select(method => this.GenerateFuncMethod(method.Name, method.FuncInfo))
				.ToList();


			List<ClassProperty> properties = new()
			{
				// public IAgent Agent { get; }
				new ClassProperty(
					name: ValueName.Default("Agent"),
					type: TypeName.FromType<IAgent>(),
					access: AccessType.Public,
					hasSetter: false
				),
				
				// public Principal CanisterId { get; }
				new ClassProperty(
					name: ValueName.Default("CanisterId"),
					type: TypeName.FromType<Principal>(),
					access: AccessType.Public,
					hasSetter: false
				)
			};
			return GenerateClass(
				clientName,
				properties,
				methods
			);
		}



		private List<TypedValueName> ResolveTypes(
			List<(ValueName Name, SourceCodeType Type)> argTypes,
			out List<ResolvedType>? innerTypes
		)
		{

			List<TypedValueName> resolvedTypes = new();
			innerTypes = null;
			int argIndex = 0;
			foreach ((ValueName argName, SourceCodeType argType) in argTypes)
			{
				string nameContext = "Arg" + argIndex;
				ResolvedType? type = this.ResolveType(argType, nameContext);
				if (type.GeneratedSyntax != null)
				{
					if (innerTypes == null)
					{
						innerTypes = new List<ResolvedType>();
					}
					innerTypes.Add(type);
				}
				// Skip null types, means they are null, empty or reserved
				if (type.Name != null)
				{
					resolvedTypes.Add(new TypedValueName(type.Name, argName));
				}
			}
			return resolvedTypes;
		}





		private ClassDeclarationSyntax GenerateService(TypeName serviceName, ServiceSourceCodeType s)
		{
			// TODO
			throw new NotImplementedException();
		}

		internal (ClassDeclarationSyntax Class, EnumDeclarationSyntax Type) GenerateVariant(
			TypeName variantName,
			VariantSourceCodeType variant
		)
		{
			TypeName enumTypeName = new(variantName.GetName() + "Tag", null);
			var implementationTypes = new List<TypeName>();
			var enumOptions = new List<(ValueName Name, TypeName? Type)>();

			List<MethodDeclarationSyntax> methods = this.GenerateVariantMethods(variantName, variant)
				.ToList();
			List<ClassProperty> properties = new()
			{
				new ClassProperty(
					ValueName.Default("Tag"),
					enumTypeName,
					access: AccessType.Public,
					hasSetter: true,
					AttributeInfo.FromType<VariantTagPropertyAttribute>()
				),
				new ClassProperty(
					ValueName.Default("Value"),
					TypeName.FromType<object>(),
					access: AccessType.Private, // TODO public?
					hasSetter: true,
					AttributeInfo.FromType<VariantValuePropertyAttribute>()
				)
			};

			List<AttributeListSyntax> attributes = new()
			{
				// [Variant(typeof({enumType})]
				GenerateAttribute(new AttributeInfo(typeof(VariantAttribute), enumTypeName))
			};

			ClassDeclarationSyntax classSyntax = GenerateClass(
				name: variantName,
				properties: properties,
				methods: methods,
				attributes: attributes,
				emptyReflectionContructor: true
			);
			EnumDeclarationSyntax enumSyntax = GenerateEnum(enumTypeName, enumOptions);
			return (classSyntax, enumSyntax);
		}

		private IEnumerable<MethodDeclarationSyntax> GenerateVariantMethods(
			TypeName variantTypeName,
			VariantSourceCodeType variant
		)
		{


			bool anyOptionsWithType = false;
			int i = 0;
			foreach ((ValueName optionName, SourceCodeType optionType) in variant.Options)
			{
				string backupOptionName = "O" + i;
				i++;
				ResolvedType resolvedType = this.ResolveType(optionType, backupOptionName);

				if (resolvedType.GeneratedSyntax != null)
				{
					optionTypeBuilder(builder);
				}
				if (optionTypeName != null && customType)
				{
					// Prefix with parent name if subtype
					optionTypeName = optionTypeName.WithParentType(variantTypeName);
				}
				enumOptions.Add((optionName, optionTypeName));
				if (optionTypeName == null)
				{
					WriteMethod(
						builder,
						inner: () =>
						{
							builder.AppendLine($"return new {variantTypeName.GetNamespacedName()}({enumTypeName.GetNamespacedName()}.{optionName.PascalCaseValue}, null);");
						},
						access: "public",
						isStatic: true,
						isAsync: false,
						isConstructor: false,
						returnType: new TypedValueName(variantTypeName, optionName),
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
							builder.AppendLine($"return new {variantTypeName.GetNamespacedName()}({enumTypeName.GetNamespacedName()}.{optionName.PascalCaseValue}, {paramName.CamelCaseValue});");
						},
						access: "public",
						isStatic: true,
						isAsync: false,
						isConstructor: false,
						returnType: new TypedValueName(variantTypeName, ValueName.Default("type")),
						name: optionName.PascalCaseValue,
						baseConstructorParams: null,
						TypedParam.FromType(optionTypeName, paramName)
					);
					builder.AppendLine("");

					WriteMethod(
						builder,
						inner: () =>
						{
							builder.AppendLine($"this.ValidateTag({enumTypeName.GetNamespacedName()}.{optionName.PascalCaseValue});");
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
					new TypedParam(enumTypeName.GetNamespacedName(), "tag")
				);
			}
		}

		internal ClassDeclarationSyntax GenerateRecord(TypeName recordName, RecordSourceCodeType record)
		{

			IEnumerable<TypedValueName> properties = record.Fields.Select((f, i) =>
			{
				ResolvedType resolvedField = this.ResolveType(f.Type, "R" + i);
				i++;
				if (resolvedField.GeneratedSyntax != null)
				{
					typeBuilder(builder);
				}

				string propertyName = f.Tag.PascalCaseValue;
				if (propertyName == recordName.GetName())
				{
					// Cant match the class name
					propertyName += "_"; // TODO best way to escape it. @ does not work
				}
				builder.AppendLine($"[{typeof(CandidNameAttribute).FullName}(\"{fieldName.CandidName}\")]");
				builder.AppendLine($"public {fieldTypeName!.GetNamespacedName()} {propertyName} {{ get; set; }}");
				builder.AppendLine("");
				return new ClassProperty();
			});

			return GenerateClass(recordName, properties, methods, implementTypes);

		}

		private static PropertyDeclarationSyntax GenerateProperty(ClassProperty property)
		{
			List<AccessorDeclarationSyntax> accessors = new ()
			{
				// Add get in `{ get; }
				SyntaxFactory
					.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
					.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
			};
			if (property.HasSetter)
			{
				// Add set in `{ get; set; }`
				accessors.Add(
					SyntaxFactory
						.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
						.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
				);
			}
			PropertyDeclarationSyntax propertySyntax = SyntaxFactory
				.PropertyDeclaration(
					SyntaxFactory.IdentifierName(property.Type.GetNamespacedName()),
					SyntaxFactory.Identifier(property.Name.PascalCaseValue)
				)
				.WithAccessorList(SyntaxFactory.AccessorList(
					SyntaxFactory.List(accessors)
				));

			// Add `public`, `private`, etc...
			SyntaxToken? accessSyntaxToken = GenerateAccessToken(property.Access);
			if(accessSyntaxToken != null)
			{
				propertySyntax = propertySyntax
					.WithModifiers(SyntaxTokenList.Create(accessSyntaxToken.Value));
			}
			if (property.Attributes?.Any() == true)
			{
				propertySyntax = propertySyntax
					.WithAttributeLists(SyntaxFactory.List(property.Attributes));
			}

			return propertySyntax;
		}

		private static EnumDeclarationSyntax GenerateEnum(TypeName enumName, List<(ValueName Name, TypeName? Type)> values)
		{
			// Build enum options based on the values
			IEnumerable<SyntaxNode> enumOptions = values
				.Select(v =>
				{
					List<AttributeListSyntax> attributeLists = new()
					{
						// [CandidName({candidName}]
						GenerateAttribute<CandidNameAttribute>(
							SyntaxFactory.AttributeArgument(
								SyntaxFactory.LiteralExpression(
									SyntaxKind.StringLiteralExpression,
									SyntaxFactory.Literal(v.Name.CandidName)
								)
							)
						)
					};

					if (v.Type != null)
					{
						// [VariantOptionType(typeof({type}))]
						attributeLists.Add(GenerateAttribute<VariantOptionTypeAttribute>(
							SyntaxFactory.AttributeArgument(
								SyntaxFactory.TypeOfExpression(v.Type.ToTypeSyntax())
							)
						));

					}
					return SyntaxFactory
						// {optionName},
						.EnumMemberDeclaration(SyntaxFactory.Identifier(v.Name.PascalCaseValue))
						// Add attributes
						.WithAttributeLists(SyntaxFactory.List(attributeLists));
				});

			// Create comma seperators between the enum options
			IEnumerable<SyntaxToken> enumSeperators = Enumerable.Range(0, values.Count - 1)
				.Select(i => SyntaxFactory.Token(SyntaxKind.CommaToken));

			// public enum {enumName}
			return SyntaxFactory.EnumDeclaration(enumName.GetName())
				.WithModifiers(
					// public
					SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
				)
				.WithMembers(
					// Each enum option
					SyntaxFactory.SeparatedList(enumOptions, enumSeperators)
				);
		}



		private static AttributeListSyntax GenerateAttribute(AttributeInfo attribute)
		{
			IEnumerable<AttributeArgumentSyntax> arguments = attribute.Args?
				.Select<object, ExpressionSyntax>(a =>
				{
					return a switch
					{
						string s => SyntaxFactory.LiteralExpression(
							SyntaxKind.StringLiteralExpression,
							SyntaxFactory.Literal(s)
						),
						TypeName type => SyntaxFactory.TypeOfExpression(type.ToTypeSyntax()),
						_ => throw new NotImplementedException()
					};
				})
				.Select(a => SyntaxFactory.AttributeArgument(a))
				?? Array.Empty<AttributeArgumentSyntax>();

			string typeName = attribute.Type.FullName![..^9]; // Remove `Attribute` suffix
			return SyntaxFactory.AttributeList(
				SyntaxFactory.SingletonSeparatedList(
					SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(typeName))
					.WithArgumentList(
						SyntaxFactory.AttributeArgumentList(
							SyntaxFactory.SeparatedList(arguments)
						)
					)
				)
			);
		}

		private MethodDeclarationSyntax GenerateFuncMethod(ValueName name, ServiceSourceCodeType.Func info)
		{
			BlockSyntax body = GenerateFuncMethodBody(name.PascalCaseValue, info, typeResolver);
			List<TypedValueName> returnTypes = info.ReturnTypes
				.Select(t => typeResolver.ResolveType(t.Type, t.Name.PascalCaseValue))
				.Select(t => new TypedValueName(t.Name, t.Va))
				.ToList();
			TypedParam[] parameters = info.ArgTypes
				.Select(t => typeResolver.ResolveType(t.Type, t.Name))
				.ToArray();
			return GenerateMethod(
				body: body,
				access: AccessType.Public,
				isStatic: false,
				isAsync: true,
				returnTypes: returnTypes,
				name: name.PascalCaseValue,
				parameters: parameters
			);
		}

		private static BlockSyntax GenerateFuncMethodBody(
			string methodName,
			ServiceSourceCodeType.Func info,
			RoslynTypeResolver typeResolver
		)
		{
			// Build arguments for conversion to CandidArg
			IEnumerable<ArgumentSyntax> fromCandidArguments = info.ArgTypes
				.Select(t =>
				{
					// `CandidTypedValue.FromObject({argX});`
					// TODO handle null values with CandidTypedValue.Null<T>()?
					return SyntaxFactory.Argument(
						SyntaxFactory.InvocationExpression(
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.IdentifierName(nameof(CandidTypedValue)),
								SyntaxFactory.IdentifierName(nameof(CandidTypedValue.FromObject))
							)
						)
						.WithArgumentList(
							SyntaxFactory.ArgumentList(
								SyntaxFactory.SingletonSeparatedList(
									SyntaxFactory.Argument(SyntaxFactory.IdentifierName(t.Name.CamelCaseValue.ToSyntaxIdentifier()))
								)
							)
						)
					);
				});

			// Build candid arg
			// `CandidArg arg = CandidArg.FromCandid(arg0, arg1, ...);`
			const string argName = "arg";
			VariableDeclarationSyntax argVariable = SyntaxFactory
				.VariableDeclaration(SyntaxFactory.IdentifierName(nameof(CandidArg)))
				.WithVariables(
					SyntaxFactory.SingletonSeparatedList(
						SyntaxFactory.VariableDeclarator(
							SyntaxFactory.Identifier(argName)
						)
						.WithInitializer(
							SyntaxFactory.EqualsValueClause(
								SyntaxFactory.InvocationExpression(
									SyntaxFactory.MemberAccessExpression(
										SyntaxKind.SimpleMemberAccessExpression,
										SyntaxFactory.IdentifierName(nameof(CandidArg)),
										SyntaxFactory.IdentifierName(nameof(CandidArg.FromCandid))
									)
								)
								.WithArgumentList(
									SyntaxFactory.ArgumentList(
										SyntaxFactory.SeparatedList(fromCandidArguments)
									)
								)
							)
						)
					)
				);

			var statements = new List<StatementSyntax>
			{
				SyntaxFactory.LocalDeclarationStatement(argVariable),
			};

			const string variableName = "reply";
			if (info.IsQuery)
			{
				const string responseName = "response";
				// `QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, {methodName}, arg);`
				StatementSyntax invokeQueryCall = GenerateQueryCall(methodName, argName, responseName);
				statements.Add(invokeQueryCall);

				// `CandidArg reply = response.ThrowOrGetReply();`
				StatementSyntax invokeThrowOrGetReply = GenerateThrowOrGetReply(variableName, responseName);
				statements.Add(invokeThrowOrGetReply);

			}
			else
			{
				// `CandidArg reply = this.Agent.CallAndWaitAsync(this.CanisterId, {methodName}, arg)`
				StatementSyntax invokeCallAndWait = GenerateCallAndWait(methodName, argName, variableName);
				statements.Add(invokeCallAndWait);

			}
			statements.Add(
				// `return reply.ToObject<{T1}, {T2}, ...>();`
				SyntaxFactory.ReturnStatement(
					SyntaxFactory.InvocationExpression(
						SyntaxFactory.MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							SyntaxFactory.IdentifierName(variableName),
							SyntaxFactory.GenericName(
								SyntaxFactory.Identifier(nameof(CandidArg.ToObject))
							)
							.WithTypeArgumentList(
								SyntaxFactory.TypeArgumentList(
									SyntaxFactory.SeparatedList(
										info.ArgTypes.Select(t => t.Type.ToTypeSyntax())
									)
								)
							)
						)
					)
				)
			);
			return SyntaxFactory.Block(statements);
		}

		private static StatementSyntax GenerateThrowOrGetReply(string variableName, string responseName)
		{
			return SyntaxFactory.LocalDeclarationStatement(
				SyntaxFactory.VariableDeclaration(
					SyntaxFactory.IdentifierName(nameof(CandidArg))
				)
				.WithVariables(
					SyntaxFactory.SingletonSeparatedList(
						SyntaxFactory.VariableDeclarator(
							SyntaxFactory.Identifier(variableName)
						)
						.WithInitializer(
							SyntaxFactory.EqualsValueClause(
								SyntaxFactory.InvocationExpression(
									SyntaxFactory.MemberAccessExpression(
										SyntaxKind.SimpleMemberAccessExpression,
										SyntaxFactory.IdentifierName(responseName),
										SyntaxFactory.IdentifierName(nameof(QueryResponse.ThrowOrGetReply))
									)
								)
							)
						)
					)
				)
			);
		}

		private static StatementSyntax GenerateQueryCall(string methodName, string argName, string responseName)
		{
			return SyntaxFactory.LocalDeclarationStatement(
					SyntaxFactory.VariableDeclaration(
						SyntaxFactory.IdentifierName(nameof(QueryResponse))
					)
					.WithVariables(
						SyntaxFactory.SingletonSeparatedList(
							SyntaxFactory.VariableDeclarator(
								SyntaxFactory.Identifier(responseName)
							)
							.WithInitializer(
								SyntaxFactory.EqualsValueClause(
									SyntaxFactory.AwaitExpression(
										SyntaxFactory.InvocationExpression(
											SyntaxFactory.MemberAccessExpression(
												SyntaxKind.SimpleMemberAccessExpression,
												SyntaxFactory.MemberAccessExpression(
													SyntaxKind.SimpleMemberAccessExpression,
													SyntaxFactory.ThisExpression(),
													SyntaxFactory.IdentifierName("Agent")
												),
												SyntaxFactory.IdentifierName(nameof(IAgent.QueryAsync))
											)
										)
										.WithArgumentList(
											SyntaxFactory.ArgumentList(
												SyntaxFactory.SeparatedList<ArgumentSyntax>(
													new SyntaxNodeOrToken[]{
														SyntaxFactory.Argument(
															SyntaxFactory.MemberAccessExpression(
																SyntaxKind.SimpleMemberAccessExpression,
																SyntaxFactory.ThisExpression(),
																SyntaxFactory.IdentifierName("CanisterId")
															)
														),
														SyntaxFactory.Token(SyntaxKind.CommaToken),
														SyntaxFactory.Argument(
															SyntaxFactory.IdentifierName(
																SyntaxFactory.Identifier(methodName)
															)
														),
														SyntaxFactory.Token(SyntaxKind.CommaToken),
														SyntaxFactory.Argument(
															SyntaxFactory.IdentifierName(argName)
														)
													}
												)
											)
										)
									)
								)
							)
						)
					)
				);
		}

		private static StatementSyntax GenerateCallAndWait(string methodName, string argName, string variableName)
		{
			InvocationExpressionSyntax apiCall = SyntaxFactory.InvocationExpression(
					SyntaxFactory.MemberAccessExpression(
						SyntaxKind.SimpleMemberAccessExpression,
						SyntaxFactory.MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							SyntaxFactory.ThisExpression(),
							SyntaxFactory.IdentifierName("Agent")
						),
						SyntaxFactory.IdentifierName("CallAndWaitAsync")
					)
				)
				.WithArgumentList(
					SyntaxFactory.ArgumentList(
						SyntaxFactory.SeparatedList<ArgumentSyntax>(
							new SyntaxNodeOrToken[] {
								SyntaxFactory.Argument(
									SyntaxFactory.MemberAccessExpression(
										SyntaxKind.SimpleMemberAccessExpression,
										SyntaxFactory.ThisExpression(),
										SyntaxFactory.IdentifierName("CanisterId")
									)
								),
								SyntaxFactory.Token(SyntaxKind.CommaToken),
								SyntaxFactory.Argument(
									SyntaxFactory.LiteralExpression(
										SyntaxKind.StringLiteralExpression,
										SyntaxFactory.Literal(methodName)
									)
								),
								SyntaxFactory.Token(SyntaxKind.CommaToken),
								SyntaxFactory.Argument(
									SyntaxFactory.IdentifierName(argName)
								)
							}
						)
					)
				);
			return SyntaxFactory.LocalDeclarationStatement(
				SyntaxFactory.VariableDeclaration(
					SyntaxFactory.IdentifierName(nameof(CandidArg))
				)
				.WithVariables(
					SyntaxFactory.SingletonSeparatedList(
						SyntaxFactory.VariableDeclarator(
							SyntaxFactory.Identifier(variableName)
						)
						.WithInitializer(
							SyntaxFactory.EqualsValueClause(
								SyntaxFactory.AwaitExpression(apiCall)
							)
						)
					)
				)
			);
		}

		private static ConstructorDeclarationSyntax GenerateConstructor(
			TypeName name,
			AccessType access,
			List<ClassProperty> properties
		)
		{

			IEnumerable<ExpressionStatementSyntax> propertyAssignments = properties
				.Select(p =>
				{
					return SyntaxFactory.ExpressionStatement(
						SyntaxFactory.AssignmentExpression(
							SyntaxKind.SimpleAssignmentExpression,
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.ThisExpression(),
								SyntaxFactory.IdentifierName(p.Name.PascalCaseValue)
							),
							SyntaxFactory.BinaryExpression(
								SyntaxKind.CoalesceExpression,
								SyntaxFactory.IdentifierName(p.Name.CamelCaseValue),
								SyntaxFactory.ThrowExpression(
									SyntaxFactory.ObjectCreationExpression(
										SyntaxFactory.IdentifierName(nameof(ArgumentNullException))
									)
									.WithArgumentList(
										SyntaxFactory.ArgumentList(
											SyntaxFactory.SingletonSeparatedList(
												SyntaxFactory.Argument(
													SyntaxFactory.InvocationExpression(
														SyntaxFactory.IdentifierName(
															SyntaxFactory.Identifier(
																SyntaxFactory.TriviaList(),
																SyntaxKind.NameOfKeyword,
																"nameof",
																"nameof",
																SyntaxFactory.TriviaList()
															)
														)
													)
													.WithArgumentList(
														SyntaxFactory.ArgumentList(
															SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
																SyntaxFactory.Argument(
																	SyntaxFactory.IdentifierName(p.Name.CamelCaseValue)
																)
															)
														)
													)
												)
											)
										)
									)
								)
							)
						)
					);
				});

			BlockSyntax body = SyntaxFactory.Block(propertyAssignments);


			ConstructorDeclarationSyntax constructor = SyntaxFactory
				.ConstructorDeclaration(
					SyntaxFactory.Identifier(name.GetName())
				)
				.WithParameterList(
					SyntaxFactory.ParameterList(
						SyntaxFactory.SeparatedList(
							properties.Select(p => SyntaxFactory.Parameter(SyntaxFactory.Identifier(p.Name.CamelCaseValue)).WithType(p.Type.ToTypeSyntax())),
							Enumerable.Range(0, properties.Count - 1).Select(i => SyntaxFactory.Token(SyntaxKind.CommaToken))
						)
					)
				)
				.WithBody(body);

			// Add access (public, private, ...)
			SyntaxToken? accessSyntaxToken = GenerateAccessToken(access);
			if(accessSyntaxToken != null)
			{
				constructor = constructor
					.WithModifiers(
						SyntaxFactory.TokenList(accessSyntaxToken.Value)
					);
			}

			return constructor;
		}

		private static SyntaxToken? GenerateAccessToken(AccessType access)
		{
			SyntaxKind? accessKeyword = access switch
			{
				AccessType.None => null,
				AccessType.Public => SyntaxKind.PublicKeyword,
				AccessType.Private => SyntaxKind.PrivateKeyword,
				AccessType.Protected => SyntaxKind.ProtectedKeyword,
				_ => throw new NotImplementedException(),
			};
			return accessKeyword == null ? null : SyntaxFactory.Token(accessKeyword.Value);
		}

		private static MethodDeclarationSyntax GenerateMethod(
			BlockSyntax body,
			AccessType access,
			bool isStatic,
			bool isAsync,
			TypedValueName? returnType,
			string name,
			params TypedParam[] parameters)
		{
			List<TypedValueName> returnTypes = new();
			if (returnType != null)
			{
				returnTypes.Add(returnType);
			}
			return GenerateMethod(body, access, isStatic, isAsync, returnTypes, name, parameters);
		}

		private static MethodDeclarationSyntax GenerateMethod(
			BlockSyntax body,
			AccessType access,
			bool isStatic,
			bool isAsync,
			List<TypedValueName> returnTypes,
			string name,
			params TypedParam[] parameters)
		{
			TypeSyntax returnType;
			if (returnTypes.Any())
			{
				if (returnTypes.Count == 1)
				{
					// Single return type
					returnType = returnTypes[0].Type.ToTypeSyntax();
				}
				else
				{
					// Tuple of return types
					returnType = SyntaxFactory.TupleType(SyntaxFactory.SeparatedList(
						returnTypes.Select(t => SyntaxFactory.TupleElement(t.Type.ToTypeSyntax(), t.Value.PascalCaseValue.ToSyntaxIdentifier()))
					));
				}
			}
			else
			{
				// `void` return type
				returnType = SyntaxFactory.IdentifierName(SyntaxFactory.Token(SyntaxKind.VoidKeyword));
			}

			if (isAsync)
			{
				// Wrap return type in Task. `Task<{returnType}>`
				returnType = SyntaxFactory.GenericName("Task".ToSyntaxIdentifier())
					.WithTypeArgumentList(
						SyntaxFactory.TypeArgumentList(
							SyntaxFactory.SingletonSeparatedList(
								returnType
							)
						)
					);
			}

			List<SyntaxToken> methodModifiers = new();

			SyntaxToken? methodAccess = access switch
			{
				AccessType.None => null,
				AccessType.Public => SyntaxFactory.Token(SyntaxKind.PublicKeyword),
				AccessType.Private => SyntaxFactory.Token(SyntaxKind.PrivateKeyword),
				AccessType.Protected => SyntaxFactory.Token(SyntaxKind.PropertyKeyword),
				_ => throw new NotImplementedException(),
			};

			if (methodAccess != null)
			{
				methodModifiers.Add(methodAccess.Value);
			}

			if (isAsync)
			{
				// Add `async` keyword to method modifiers
				methodModifiers.Add(SyntaxFactory.Token(SyntaxKind.AsyncKeyword));
			}
			if (isStatic)
			{
				// Add `static` keyword to method modifiers
				methodModifiers.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
			}

			IEnumerable<ParameterSyntax> @params = parameters.Select(p =>
			{
				return SyntaxFactory
					.Parameter(p.Name.ToSyntaxIdentifier())
					.WithType(p.Type.ToTypeSyntax());
			});
			return SyntaxFactory.MethodDeclaration(returnType, SyntaxFactory.Identifier(name))
				.WithModifiers(SyntaxFactory.TokenList(methodModifiers))
				.WithParameterList(SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(@params)))
				.WithBody(body);
		}

		private static ClassDeclarationSyntax GenerateClass(
			TypeName name,
			List<ClassProperty> properties,
			List<MethodDeclarationSyntax> methods,
			List<TypeName>? implementTypes = null,
			List<AttributeListSyntax>? attributes = null,
			bool emptyReflectionContructor = false)
		{
			IEnumerable<PropertyDeclarationSyntax> properySyntxList = properties
				.Select(GenerateProperty);

			List<ConstructorDeclarationSyntax> constructors = new()
			{
				GenerateConstructor(name, AccessType.Public, properties)
			};
			if (emptyReflectionContructor)
			{
				// Empty Constrcutor for reflection
				constructors.Add(GenerateConstructor(
					name: name,
					access: AccessType.Protected,
					properties: new List<ClassProperty>()
				));
			}

			ClassDeclarationSyntax classSyntax = SyntaxFactory.ClassDeclaration(name.GetName())
				.WithModifiers(SyntaxFactory.TokenList(
					// Make class public
					SyntaxFactory.Token(SyntaxKind.PublicKeyword)
				))
				.WithMembers(SyntaxFactory.List(
					// Constructor
					Enumerable.Repeat(constructorMethod, 1).Cast<MemberDeclarationSyntax>()
					// Methods
					.Concat(methods.Cast<MemberDeclarationSyntax>())
				));


			if (implementTypes?.Any() == true)
			{
				classSyntax = classSyntax
					.WithBaseList(
						SyntaxFactory.BaseList(
							SyntaxFactory.SeparatedList(
								implementTypes
									.Select(t => SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName(t.GetNamespacedName())))
									.Cast<BaseTypeSyntax>()
									.ToArray()
							)
						)
					);
			}
			if(attributes?.Any() == true)
			{
				classSyntax = classSyntax
					.WithAttributeLists(
						SyntaxFactory.List(attributes)
					);
			}
			return classSyntax;

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

		private enum AccessType
		{
			None,
			Public,
			Private,
			Protected
		}

		private class ClassProperty
		{
			public AccessType Access { get; }
			public ValueName Name { get; }
			public TypeName Type { get; }
			public bool HasSetter { get; }
			public AttributeInfo[]? Attributes { get; }

			public ClassProperty(
				ValueName name,
				TypeName type,
				AccessType access,
				bool hasSetter,
				params AttributeInfo[]? attributes
			)
			{
				this.Access = access;
				this.Name = name ?? throw new ArgumentNullException(nameof(name));
				this.Type = type ?? throw new ArgumentNullException(nameof(type));
				this.HasSetter = hasSetter;
				this.Attributes = attributes;
			}


		}

		private class AttributeInfo
		{
			public Type Type { get; }
			public object[]? Args { get; }

			public AttributeInfo(Type type, params object[]? args)
			{
				this.Type = type ?? throw new ArgumentNullException(nameof(type));
				this.Args = args;
			}

			public static AttributeInfo FromType<T>(params object[]? args)
			{
				TypeName type = TypeName.FromType<T>();
				return new AttributeInfo(type, args);
			}
		}
	}
}
