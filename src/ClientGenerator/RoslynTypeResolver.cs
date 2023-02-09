using EdjCase.ICP.ClientGenerator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Agent.Responses;

namespace EdjCase.ICP.ClientGenerator
{
	internal class RoslynTypeResolver
	{
		private readonly Dictionary<string, ResolvedType> _resolvedTypes = new();

		public ResolvedType ResolveTypeDeclaration(ValueName typeName, SourceCodeType type)
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
			ResolvedType res = this.ResolveTypeInner(type, typeNameStr, isDeclaration: true);
			this._resolvedTypes[typeNameStr] = res;
			return res;
		}

		public ResolvedType ResolveType(SourceCodeType type, string nameContext)
		{
			return this.ResolveTypeInner(type, nameContext, isDeclaration: false);
		}

		private ResolvedType ResolveTypeInner(SourceCodeType type, string nameContext, bool isDeclaration)
		{
			switch (type)
			{
				case CompiledTypeSourceCodeType c:
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
								return new ResolvedType(existing!.Name);
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
						TypeName variantName = new(nameContext, null);
						(ClassDeclarationSyntax classSyntax, EnumDeclarationSyntax typeSyntax) = this.GenerateVariant(variantName, v);
						return new ResolvedType(variantName, classSyntax, typeSyntax);
					}
				case RecordSourceCodeType r:
					{
						TypeName recordName = new(nameContext, null);
						ClassDeclarationSyntax classSyntax = this.GenerateRecord(recordName, r);
						return new ResolvedType(recordName, classSyntax);
					}
				case ServiceSourceCodeType s:
					{
						TypeName serviceName = new(nameContext, null);
						ClassDeclarationSyntax classSyntax = this.GenerateService(serviceName, s);
						return new ResolvedType(serviceName, classSyntax);
					}
				default:
					throw new NotImplementedException();
			}
		}


		public ClassDeclarationSyntax GenerateClient(TypeName clientName, ServiceSourceCodeType service)
		{
			List<MethodDeclarationSyntax> methods = service.Methods
				.Select(method => GenerateFuncMethod(method.Name, method.FuncInfo, this))
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

				if (type == null)
				{
					// Skip null types, means they are null, empty or reserved
					continue;
				}
				resolvedTypes.Add(new TypedValueName(type.Name, argName));

				if (type.GeneratedSyntax != null)
				{
					innerTypes ??= new List<ResolvedType>();
					innerTypes.Add(type);
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
			TypeName variantTypeName,
			VariantSourceCodeType variant
		)
		{
			TypeName enumTypeName = new(variantTypeName.GetName() + "Tag", null);

			List<(ValueName Name, ResolvedType? Type)> resolvedOptions = variant.Options
				.Select((o, i) => (o.Tag, o.Type == null ? null : this.ResolveType(o.Type, "O" + i)))
				.ToList();

			List<(ValueName Name, TypeName? Type)> enumOptions = resolvedOptions
				.Select(o => (o.Name, o.Type?.Name))
				.ToList();

			List<MethodDeclarationSyntax> methods = new();

			ValueName optionValueParamName = ValueName.Default("info");

			// Creation methods
			// public static {VariantType} {OptionName}({VariantOptionType} value)
			// or if there is no type:
			// public static {VariantType} {OptionName}()
			methods.AddRange(
				resolvedOptions
					.Select(o => this.GenerateVariantOptionCreationMethod(
						variantTypeName,
						enumTypeName,
						o.Name,
						o.Type,
						optionValueParamName
					))
			);

			// 'As{X}' methods (if has option type)
			methods.AddRange(
				resolvedOptions
				.Where(r => r.Type != null)
				.Select(o => this.GenerateVariantOptionAsMethod(enumTypeName, o.Name, o.Type!, optionValueParamName))
			);

			// TODO
			//if (resolvedType.Name != null && customType)
			//{
			//	// Prefix with parent name if subtype
			//	optionTypeName = optionTypeName.WithParentType(variantTypeName);
			//}

			bool anyOptionsWithType = resolvedOptions.Any(o => o.Type != null);
			if (anyOptionsWithType)
			{
				// If there are any types, then create the helper method 'ValidateType' that
				// they all use
				methods.Add(this.GenerateVariantValidateTypeMethod(enumTypeName, ValueName.Default("Tag")));
			}
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
				GenerateAttribute(AttributeInfo.FromType<VariantAttribute>(enumTypeName))
			};

			ClassDeclarationSyntax classSyntax = GenerateClass(
				name: variantTypeName,
				properties: properties,
				methods: methods,
				attributes: attributes,
				emptyReflectionContructor: true
			);
			EnumDeclarationSyntax enumSyntax = GenerateEnum(enumTypeName, enumOptions);
			return (classSyntax, enumSyntax);
		}

		private MethodDeclarationSyntax GenerateVariantValidateTypeMethod(TypeName enumTypeName, ValueName tagName)
		{
			// Generate helper function to validate the variant 'As_' methods
			//private void ValidateTag({VariantEnum} tag)
			//{
			//	if (!this.Tag.Equals(tag))
			//	{
			//		throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			//	}
			//}
			BlockSyntax body = SyntaxFactory.Block(
				SyntaxFactory.IfStatement(
				SyntaxFactory.PrefixUnaryExpression(
					SyntaxKind.LogicalNotExpression,
					SyntaxFactory.InvocationExpression(
						SyntaxFactory.MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.ThisExpression(),
								SyntaxFactory.IdentifierName(tagName.PascalCaseValue)
							),
							SyntaxFactory.IdentifierName(nameof(CandidTag.Equals))
						)
					)
					.WithArgumentList(
						SyntaxFactory.ArgumentList(
							SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
								SyntaxFactory.Argument(
									SyntaxFactory.IdentifierName(tagName.CamelCaseValue)
								)
							)
						)
					)
				),
				SyntaxFactory.Block(
					SyntaxFactory.SingletonList<StatementSyntax>(
						SyntaxFactory.ThrowStatement(
							SyntaxFactory.ObjectCreationExpression(
								SyntaxFactory.IdentifierName(nameof(InvalidOperationException))
							)
							.WithArgumentList(
								SyntaxFactory.ArgumentList(
									SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
										SyntaxFactory.Argument(
											SyntaxFactory.InterpolatedStringExpression(
												SyntaxFactory.Token(SyntaxKind.InterpolatedStringStartToken)
											)
											.WithContents(
												SyntaxFactory.List<InterpolatedStringContentSyntax>(
													new InterpolatedStringContentSyntax[]{
														SyntaxFactory.InterpolatedStringText()
														.WithTextToken(
															SyntaxFactory.Token(
																SyntaxFactory.TriviaList(),
																SyntaxKind.InterpolatedStringTextToken,
																"Cannot cast '",
																"Cannot cast '",
																SyntaxFactory.TriviaList()
															)
														),
														SyntaxFactory.Interpolation(
															SyntaxFactory.MemberAccessExpression(
																SyntaxKind.SimpleMemberAccessExpression,
																SyntaxFactory.ThisExpression(),
																SyntaxFactory.IdentifierName(tagName.PascalCaseValue)
															)
														),
														SyntaxFactory.InterpolatedStringText()
														.WithTextToken(
															SyntaxFactory.Token(
																SyntaxFactory.TriviaList(),
																SyntaxKind.InterpolatedStringTextToken,
																"' to type '",
																"' to type '",
																SyntaxFactory.TriviaList()
															)
														),
														SyntaxFactory.Interpolation(
															SyntaxFactory.IdentifierName(tagName.CamelCaseValue)
														),
														SyntaxFactory.InterpolatedStringText()
														.WithTextToken(
															SyntaxFactory.Token(
																SyntaxFactory.TriviaList(),
																SyntaxKind.InterpolatedStringTextToken,
																"'",
																"'",
																SyntaxFactory.TriviaList()
															)
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
				)
			));
			return GenerateMethod(
				body: body,
				access: AccessType.Private,
				isStatic: false,
				isAsync: false,
				returnTypes: null,
				name: "ValidateTag",
				new TypedValueName(enumTypeName, ValueName.Default("tag"))
			);
		}

		private MethodDeclarationSyntax GenerateVariantOptionAsMethod(
			TypeName enumType,
			ValueName optionName,
			ResolvedType optionType,
			ValueName optionValueParamName
		)
		{
			// 
			BlockSyntax body = SyntaxFactory.Block(
				SyntaxFactory.ExpressionStatement(
					SyntaxFactory.InvocationExpression(
						SyntaxFactory.MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							SyntaxFactory.ThisExpression(),
							SyntaxFactory.IdentifierName("ValidateTag")
						)
					)
					.WithArgumentList(
						SyntaxFactory.ArgumentList(
							SyntaxFactory.SingletonSeparatedList(
								SyntaxFactory.Argument(
									SyntaxFactory.MemberAccessExpression(
										SyntaxKind.SimpleMemberAccessExpression,
										enumType.ToTypeSyntax(),
										SyntaxFactory.IdentifierName(optionName.PascalCaseValue)
									)
								)
							)
						)
					)
				),
				SyntaxFactory.ReturnStatement(
					SyntaxFactory.CastExpression(
						optionType.Name.ToTypeSyntax(),
						SyntaxFactory.PostfixUnaryExpression(
							SyntaxKind.SuppressNullableWarningExpression,
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.ThisExpression(),
								SyntaxFactory.IdentifierName("Value")
							)
						)
					)
				)
			);
			// public {VariantOptionType} As{OptionName}()
			return GenerateMethod(
				body: body,
				access: AccessType.Public,
				isStatic: false,
				isAsync: false,
				returnTypes: new List<TypedValueName> { new TypedValueName(optionType.Name, optionName) },
				name: "As" + optionName.PascalCaseValue
			);
		}

		private MethodDeclarationSyntax GenerateVariantOptionCreationMethod(
			TypeName variantTypeName,
			TypeName enumTypeName,
			ValueName optionTypeName,
			ResolvedType? optionType,
			ValueName optionValueParamName
		)
		{
			ExpressionSyntax arg = optionType == null
						// If option type is not specified, then use `null`
						? SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)
						// If option type is specified, then use param
						: SyntaxFactory.IdentifierName(optionValueParamName.CamelCaseValue);

			var creationParameters = new List<TypedValueName>();
			if (optionType != null)
			{
				creationParameters.Add(new TypedValueName(optionType.Name, optionValueParamName));
			}
			return GenerateMethod(
				// return new VariantType(VariantEnum.Option, value);
				body: SyntaxFactory.Block(
				SyntaxFactory.ReturnStatement(
					SyntaxFactory.ObjectCreationExpression(
						SyntaxFactory.IdentifierName(variantTypeName.GetNamespacedName())
					)
					.WithArgumentList(
						SyntaxFactory.ArgumentList(
							SyntaxFactory.SeparatedList<ArgumentSyntax>(
								new SyntaxNodeOrToken[]{
										SyntaxFactory.Argument(
											SyntaxFactory.MemberAccessExpression(
												SyntaxKind.SimpleMemberAccessExpression,
												SyntaxFactory.IdentifierName(enumTypeName.GetNamespacedName()),
												SyntaxFactory.IdentifierName(optionTypeName.PascalCaseValue)
											)
										),
										SyntaxFactory.Token(SyntaxKind.CommaToken),
										SyntaxFactory.Argument(arg)
								}
							)
						)
					)
				)
				),
				access: AccessType.Public,
				isStatic: true,
				isAsync: false,
				returnTypes: new List<TypedValueName> { new TypedValueName(variantTypeName, optionTypeName) },
				name: optionTypeName.PascalCaseValue,
				parameters: creationParameters?.ToArray() ?? Array.Empty<TypedValueName>()
			);
		}

		internal ClassDeclarationSyntax GenerateRecord(TypeName recordName, RecordSourceCodeType record)
		{
			List<(ValueName Tag, ResolvedType Type)> resolvedFields = record.Fields
				.Select((f, i) => (f.Tag, this.ResolveType(f.Type, "R" + i)))
				.ToList();
			List<MemberDeclarationSyntax> subItems = resolvedFields
				.SelectMany(f => f.Type.GeneratedSyntax ?? Array.Empty<MemberDeclarationSyntax>())
				.ToList();

			List<ClassProperty> properties = resolvedFields
				.Select((f, i) =>
				{
					string propertyName = f.Tag.PascalCaseValue;
					if (propertyName == recordName.GetName())
					{
						// Cant match the class name
						propertyName += "_"; // TODO best way to escape it. @ does not work
					}

					// [CandidName("{fieldCandidName}")]
					// public {fieldType} {fieldName} {{ get; set; }}
					return new ClassProperty(
						name: ValueName.Default(propertyName),
						type: f.Type.Name,
						access: AccessType.Public,
						hasSetter: true,
						AttributeInfo.FromType<CandidNameAttribute>(f.Tag.CandidName)
					);
				})
				.ToList();

			return GenerateClass(
				name: recordName,
				properties: properties,
				subTypes: subItems
			);

		}

		private static PropertyDeclarationSyntax? GenerateProperty(ClassProperty property)
		{
			if(property.Type == null)
			{
				return null;
			}
			List<AccessorDeclarationSyntax> accessors = new()
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
			if (accessSyntaxToken != null)
			{
				propertySyntax = propertySyntax
					.WithModifiers(SyntaxTokenList.Create(accessSyntaxToken.Value));
			}
			if (property.Attributes?.Any() == true)
			{
				IEnumerable<AttributeListSyntax> attributes = property.Attributes
					.Select(a => GenerateAttribute(a));
				propertySyntax = propertySyntax
					.WithAttributeLists(SyntaxFactory.List(attributes));
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
						GenerateAttribute(
							new AttributeInfo(TypeName.FromType<CandidNameAttribute>(), v.Name.CandidName)
						)
					};

					if (v.Type != null)
					{
						// [VariantOptionType(typeof({type}))]
						attributeLists.Add(GenerateAttribute(
							new AttributeInfo(TypeName.FromType<VariantOptionTypeAttribute>(), v.Type)
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
			// TODO // Remove `Attribute` suffix?
			string typeName = attribute.Type.GetNamespacedName();
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

		private static MethodDeclarationSyntax GenerateFuncMethod(
			ValueName name,
			ServiceSourceCodeType.Func info,
			RoslynTypeResolver typeResolver
		)
		{
			List<(ValueName Name, ResolvedType Type)> resolvedArgTypes = info.ArgTypes
				.Select(t => (t.Name, typeResolver.ResolveType(t.Type, t.Name.PascalCaseValue)))
				.ToList();
			List<(ValueName Name, ResolvedType Type)> resolvedReturnTypes = info.ReturnTypes
				.Select(t => (t.Name, typeResolver.ResolveType(t.Type, t.Name.PascalCaseValue)))
				.ToList();

			List<TypedValueName> argTypes = resolvedArgTypes
				.Select(t => new TypedValueName(t.Type.Name, t.Name))
				.ToList();
			List<TypedValueName> returnTypes = resolvedReturnTypes
				.Select(t => new TypedValueName(t.Type.Name, t.Name))
				.ToList();

			BlockSyntax body = GenerateFuncMethodBody(
				methodName: name.PascalCaseValue,
				argTypes,
				returnTypes,
				isQuery: info.IsQuery,
				typeResolver: typeResolver
			);

			return GenerateMethod(
				body: body,
				access: AccessType.Public,
				isStatic: false,
				isAsync: true,
				returnTypes: returnTypes,
				name: name.PascalCaseValue,
				parameters: argTypes.ToArray()
			);
		}

		private static BlockSyntax GenerateFuncMethodBody(
			string methodName,
			List<TypedValueName> argTypes,
			List<TypedValueName> returnTypes,
			bool isQuery,
			RoslynTypeResolver typeResolver
		)
		{
			// Build arguments for conversion to CandidArg
			IEnumerable<ArgumentSyntax> fromCandidArguments = argTypes
				.Select(t =>
				{
					ExpressionSyntax expression;
					if(t == null)
					{
						// `CandidTypedValue.Null();`
						expression = SyntaxFactory.InvocationExpression(
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.IdentifierName(nameof(CandidTypedValue)),
								SyntaxFactory.IdentifierName(nameof(CandidTypedValue.Null))
							)
						);
					}
					else
					{
						// `CandidTypedValue.FromObject({argX});`
						expression = SyntaxFactory.InvocationExpression(
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.IdentifierName(nameof(CandidTypedValue)),
								SyntaxFactory.IdentifierName(nameof(CandidTypedValue.FromObject))
							)
						)
						.WithArgumentList(
							SyntaxFactory.ArgumentList(
								SyntaxFactory.SingletonSeparatedList(
									SyntaxFactory.Argument(SyntaxFactory.IdentifierName(t.Value.CamelCaseValue.ToSyntaxIdentifier()))
								)
							)
						);
					}
					return SyntaxFactory.Argument(expression);
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
			if (isQuery)
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
										argTypes
										.Select(t => t.Type.ToTypeSyntax())
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
							properties
							.Where(p => p.Type != null)
							.Select(p => SyntaxFactory.Parameter(SyntaxFactory.Identifier(p.Name.CamelCaseValue)).WithType(p.Type!.ToTypeSyntax()))
						)
					)
				)
				.WithBody(body);

			// Add access (public, private, ...)
			SyntaxToken? accessSyntaxToken = GenerateAccessToken(access);
			if (accessSyntaxToken != null)
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
			List<TypedValueName>? returnTypes,
			string name,
			params TypedValueName[] parameters)
		{
			TypeSyntax? returnType = null;
			if (returnTypes?.Any() == true)
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
						returnTypes
						.Where(t => t != null)
						.Select(t => SyntaxFactory.TupleElement(t.Type.ToTypeSyntax(), t.Value.PascalCaseValue.ToSyntaxIdentifier()))
					));
				}
			}

			if (returnType == null)
			{
				if (isAsync)
				{
					// `Task` return type
					returnType = SyntaxFactory.GenericName("Task".ToSyntaxIdentifier());
				}
				else
				{
					// `void` return type
					returnType = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword));
				}
			}
			else
			{
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

			IEnumerable<ParameterSyntax> @params = parameters
				.Select(p =>
				{
					return SyntaxFactory
						.Parameter(p.Value.CamelCaseValue.ToSyntaxIdentifier())
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
			List<MethodDeclarationSyntax>? methods = null,
			List<TypeName>? implementTypes = null,
			List<AttributeListSyntax>? attributes = null,
			bool emptyReflectionContructor = false,
			List<MemberDeclarationSyntax>? subTypes = null)
		{
			if (properties.Any(p => p.Type == null))
			{
				// TODO
				throw new NotImplementedException("Null, empty or reserved properties are not yet supported");
			}
			IEnumerable<PropertyDeclarationSyntax> properySyntxList = properties
				.Select(GenerateProperty)
				.Where(p => p != null)!;

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
					// Constructors
					constructors.Cast<MemberDeclarationSyntax>()
					// Methods
					.Concat(methods?.Cast<MemberDeclarationSyntax>() ?? Array.Empty<MemberDeclarationSyntax>())
					// Subtypes
					.Concat(subTypes?.Cast<MemberDeclarationSyntax>() ?? Array.Empty<MemberDeclarationSyntax>())
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
			if (attributes?.Any() == true)
			{
				classSyntax = classSyntax
					.WithAttributeLists(
						SyntaxFactory.List(attributes)
					);
			}
			return classSyntax;

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
			public TypeName? Type { get; }
			public bool HasSetter { get; }
			public AttributeInfo[]? Attributes { get; }

			public ClassProperty(
				ValueName name,
				TypeName? type,
				AccessType access,
				bool hasSetter,
				params AttributeInfo[]? attributes
			)
			{
				this.Access = access;
				this.Name = name ?? throw new ArgumentNullException(nameof(name));
				this.Type = type;
				this.HasSetter = hasSetter;
				this.Attributes = attributes;
			}


		}

		private class AttributeInfo
		{
			public TypeName Type { get; }
			public object[]? Args { get; }

			public AttributeInfo(TypeName type, params object[]? args)
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
