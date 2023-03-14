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
using System.Threading.Tasks;
using EdjCase.ICP.Candid;

namespace EdjCase.ICP.ClientGenerator
{
	internal class RoslynTypeResolver
	{
		private readonly Dictionary<string, ResolvedType> _resolvedTypes = new();

		public string ModelNamespace { get; }
		public HashSet<ValueName> Aliases { get; }
		public bool FeatureNullable { get; }

		public RoslynTypeResolver(
			string modelNamespace,
			HashSet<ValueName> aliases,
			bool featureNullable)
		{
			this.ModelNamespace = modelNamespace;
			this.Aliases = aliases;
			this.FeatureNullable = featureNullable;
		}

		public ResolvedType ResolveTypeDeclaration(
			ValueName typeName,
			SourceCodeType type
		)
		{
			// note that this only works for only one level of type nesting, so type aliases to generics whose argument is a user-defined type
			// will fail, for example:
			//    type A<T> = record { left : A, right : B };
			//    type X = blob;
			//    type F = A<X>;

			string typeNameStr = typeName.PropertyName;

			if (this._resolvedTypes.TryGetValue(typeNameStr, out ResolvedType? existing))
			{
				return existing;
			}
			ResolvedType res = this.ResolveTypeInner(type, typeNameStr, parentType: null);
			this._resolvedTypes[typeNameStr] = res;
			return res;
		}

		public ResolvedType ResolveType(
			SourceCodeType type,
			string nameContext,
			TypeName? parentType
		)
		{
			return this.ResolveTypeInner(type, nameContext, parentType);
		}

		private ResolvedType ResolveTypeInner(
			SourceCodeType type,
			string nameContext,
			TypeName? parentType
		)
		{
			switch (type)
			{
				case CompiledTypeSourceCodeType c:
					{
						ResolvedType? resolvedGenericType = null;
						MemberDeclarationSyntax[]? innerTypes = null;
						if (c.GenericType != null)
						{
							resolvedGenericType = this.ResolveType(c.GenericType, nameContext + "Item", parentType);
							if (resolvedGenericType.GeneratedSyntax != null)
							{
								innerTypes = resolvedGenericType.GeneratedSyntax;
							}
						}

						string typeName = c.Type.Name;
						if (c.Type.IsGenericTypeDefinition)
						{
							// Remove the `1 from the end
							typeName = typeName[..^2];
						}
						var cType = new TypeName(
							typeName,
							c.Type.Namespace,
							prefix: null,
							resolvedGenericType == null ? Array.Empty<TypeName>() : new[]
							{
								resolvedGenericType.Name
							}
						);

						return new ResolvedType(cType, innerTypes);
					}
				case ReferenceSourceCodeType re:
					{
						ValueName correctedRefId = ValueName.Default(re.Id.Value);
						bool isAlias = this.Aliases.Contains(correctedRefId);

						string? @namespace = isAlias ? null : this.ModelNamespace;
						return new ResolvedType(new TypeName(
							correctedRefId.PropertyName,
							@namespace,
							prefix: null
						));

					}
				case VariantSourceCodeType v:
					{
						if (parentType != null)
						{
							nameContext += "Variant";
						}
						TypeName variantName = this.BuildType(nameContext, parentType);
						(ClassDeclarationSyntax? classSyntax, EnumDeclarationSyntax enumSyntax) = this.GenerateVariant(variantName, v, parentType);
						if (classSyntax != null)
						{
							return new ResolvedType(variantName, classSyntax, enumSyntax);
						}
						return new ResolvedType(variantName, enumSyntax);
					}
				case RecordSourceCodeType r:
					{
						if (parentType != null)
						{
							nameContext += "Record";
						}
						TypeName recordName = this.BuildType(nameContext, parentType);
						ClassDeclarationSyntax classSyntax = this.GenerateRecord(recordName, r);
						return new ResolvedType(recordName, classSyntax);
					}
				default:
					throw new NotImplementedException();
			}
		}


		public ClassDeclarationSyntax GenerateClient(TypeName clientName, ServiceSourceCodeType service)
		{
			ValueName candidConverterProperty = ValueName.Default("Converter");
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
				),
			};

			List<ClassProperty> optionalProperties = new()
			{				
				// public CandidConverter? Converter { get; }
				new ClassProperty(
					name: candidConverterProperty,
					type: TypeName.FromType<CandidConverter>(isNullable: this.FeatureNullable),
					access: AccessType.Public,
					hasSetter: false
				)
			};

			List<(MethodDeclarationSyntax Method, List<MemberDeclarationSyntax> SubTypes)> methods = service.Methods
				.Select(method => GenerateFuncMethod(method.Name, method.FuncInfo, clientName, this, candidConverterProperty))
				.ToList();

			return GenerateClass(
				clientName,
				properties,
				optionalProperties: optionalProperties,
				methods: methods.Select(m => m.Method).ToList(),
				subTypes: methods.SelectMany(m => m.SubTypes).ToList()
			);
		}


		internal (ClassDeclarationSyntax? Class, EnumDeclarationSyntax Type) GenerateVariant(
			TypeName variantTypeName,
			VariantSourceCodeType variant,
			TypeName? parentType
		)
		{

			List<(ValueName Name, ResolvedType? Type)> resolvedOptions = variant.Options
				.Select((o, i) => (o.Tag, o.Type == null ? null : this.ResolveType(o.Type, o.Tag.PropertyName, variantTypeName)))
				.ToList();


			List<(ValueName Name, TypeName? Type)> enumOptions = resolvedOptions
				.Select(o => (o.Name, o.Type?.Name))
				.ToList();

			if (enumOptions.All(o => o.Type == null))
			{
				// If there are no types, just create an enum value

				TypeName enumTypeName = this.BuildType(variantTypeName.GetName(), parentType);
				EnumDeclarationSyntax enumSyntax = GenerateEnum(enumTypeName, enumOptions);
				return (null, enumSyntax);
			}
			else
			{
				TypeName enumTypeName = this.BuildType(variantTypeName.GetName() + "Tag", parentType);


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

				// TODO auto change the property values of all class types if it matches the name
				ValueName tagName = ValueName.Default(variantTypeName.GetName() == "Tag" ? "TagValue" : "Tag");
				ValueName valueName = ValueName.Default(variantTypeName.GetName() == "Value" ? "Value_" : "Value");

				// 'As{X}' methods (if has option type)
				methods.AddRange(
					resolvedOptions
					.Where(r => r.Type != null)
					.Select(o => this.GenerateVariantOptionAsMethod(enumTypeName, o.Name, o.Type!, valueName))
				);


				bool anyOptionsWithType = resolvedOptions.Any(o => o.Type != null);
				if (anyOptionsWithType)
				{
					// If there are any types, then create the helper method 'ValidateType' that
					// they all use
					methods.Add(this.GenerateVariantValidateTypeMethod(enumTypeName, tagName));
				}
				List<ClassProperty> properties = new()
			{
				new ClassProperty(
					tagName,
					enumTypeName,
					access: AccessType.Public,
					hasSetter: true,
					AttributeInfo.FromType<VariantTagPropertyAttribute>()
				),
				new ClassProperty(
					valueName,
					TypeName.FromType<object>(isNullable: this.FeatureNullable),
					access: AccessType.Public,
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
					optionalProperties: null,
					methods: methods,
					attributes: attributes,
					emptyConstructorAccess: AccessType.Protected,
					subTypes: resolvedOptions
						.SelectMany(o => o.Type?.GeneratedSyntax ?? Array.Empty<MemberDeclarationSyntax>())
						.ToList()
				);
				EnumDeclarationSyntax enumSyntax = GenerateEnum(enumTypeName, enumOptions);
				return (classSyntax, enumSyntax);
			}
		}

		private TypeName BuildType(string name, TypeName? parentType)
		{
			string @namespace;
			string? prefix = null;
			if (parentType == null)
			{
				@namespace = this.ModelNamespace;
			}
			else
			{
				@namespace = parentType.GetNamespacedName();
				int lastDotIndex = @namespace.LastIndexOf('.');
				prefix = @namespace[(lastDotIndex + 1)..];
				@namespace = @namespace[..lastDotIndex];
			}
			return new TypeName(name, @namespace, prefix);
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
								SyntaxFactory.IdentifierName(tagName.PropertyName)
							),
							SyntaxFactory.IdentifierName(nameof(CandidTag.Equals))
						)
					)
					.WithArgumentList(
						SyntaxFactory.ArgumentList(
							SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
								SyntaxFactory.Argument(
									SyntaxFactory.IdentifierName(tagName.VariableName)
								)
							)
						)
					)
				),
				SyntaxFactory.Block(
					SyntaxFactory.SingletonList<StatementSyntax>(
						SyntaxFactory.ThrowStatement(
							SyntaxFactory.ObjectCreationExpression(
								SyntaxFactory.IdentifierName(typeof(InvalidOperationException).FullName!)
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
																SyntaxFactory.IdentifierName(tagName.PropertyName)
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
															SyntaxFactory.IdentifierName(tagName.VariableName)
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
			ValueName valueName
		)
		{
			// public {VariantOptionType} As{OptionName}()
			// {
			//     this.ValidateTag({EnumType}.{Option});
			//     return ({VariantOptionType})this.Value!;
			// }
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
										SyntaxFactory.IdentifierName(optionName.PropertyName)
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
								SyntaxFactory.IdentifierName(valueName.PropertyName)
							)
						)
					)
				)
			);
			return GenerateMethod(
				body: body,
				access: AccessType.Public,
				isStatic: false,
				isAsync: false,
				returnTypes: new List<TypedValueName> { new TypedValueName(optionType.Name, optionName) },
				name: "As" + optionName.PropertyName
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
				: SyntaxFactory.IdentifierName(optionValueParamName.VariableName);

			var creationParameters = new List<TypedValueName>();
			if (optionType != null)
			{
				creationParameters.Add(new TypedValueName(optionType.Name, optionValueParamName));
			}
			string methodName = optionTypeName.PropertyName == variantTypeName.GetName()
				? optionTypeName.PropertyName + "_" // Escape colliding names
				: optionTypeName.PropertyName;
			return GenerateMethod(
				// return new {VariantType}({VariantEnum}.{Option}, value);
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
												SyntaxFactory.IdentifierName(optionTypeName.PropertyName)
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
				name: methodName,
				parameters: creationParameters?.ToArray() ?? Array.Empty<TypedValueName>()
			);
		}

		internal ClassDeclarationSyntax GenerateRecord(TypeName recordTypeName, RecordSourceCodeType record)
		{
			List<(ValueName Tag, ResolvedType Type)> resolvedFields = record.Fields
				.Select((f, i) => (f.Tag, this.ResolveType(f.Type, f.Tag.PropertyName, parentType: recordTypeName)))
				.ToList();
			List<MemberDeclarationSyntax> subItems = resolvedFields
				.SelectMany(f => f.Type.GeneratedSyntax ?? Array.Empty<MemberDeclarationSyntax>())
				.ToList();

			List<ClassProperty> properties = resolvedFields
				.Select((f, i) =>
				{
					ValueName propertyName = f.Tag;
					if (propertyName.PropertyName == recordTypeName.GetName())
					{
						// Cant match the class name
						propertyName = new ValueName(
							propertyName: propertyName.PropertyName + "_", // TODO best way to escape it. @ does not work
							variableName: propertyName.VariableName,
							candidName: propertyName.CandidName
						);
					}
					AttributeInfo[]? attributes = null;
					if (propertyName.CandidName != propertyName.PropertyName)
					{
						// [CandidName("{fieldCandidName}")]
						// Only add attribute if the name is different
						attributes = new[]
						{
							AttributeInfo.FromType<CandidNameAttribute>(propertyName.CandidName)
						};
					}


					// public {fieldType} {fieldName} {{ get; set; }}
					return new ClassProperty(
						name: propertyName,
						type: f.Type.Name,
						access: AccessType.Public,
						hasSetter: true,
						attributes
					);
				})
				.ToList();

			return GenerateClass(
				name: recordTypeName,
				properties: properties,
				optionalProperties: null,
				subTypes: subItems,
				emptyConstructorAccess: AccessType.Public
			);

		}

		private static PropertyDeclarationSyntax? GenerateProperty(ClassProperty property)
		{
			if (property.Type == null)
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
					SyntaxFactory.Identifier(property.Name.PropertyName)
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
					List<AttributeListSyntax> attributeList = new();

					if (v.Name.CandidName != v.Name.PropertyName)
					{
						// [CandidName({candidName}]
						// Only add if names differ
						attributeList.Add(GenerateAttribute(
							new AttributeInfo(TypeName.FromType<CandidNameAttribute>(), v.Name.CandidName)
						));
					}

					if (v.Type != null)
					{
						// [VariantOptionType(typeof({type}))]
						attributeList.Add(GenerateAttribute(
							new AttributeInfo(TypeName.FromType<VariantOptionTypeAttribute>(), v.Type)
						));

					}
					return SyntaxFactory
						// {optionName},
						.EnumMemberDeclaration(SyntaxFactory.Identifier(v.Name.PropertyName))
						// Add attributes
						.WithAttributeLists(SyntaxFactory.List(attributeList));
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

			string typeName = attribute.Type.GetNamespacedName();
			typeName = typeName[..^"Attribute".Length]; // Remove suffix
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

		private static (MethodDeclarationSyntax Method, List<MemberDeclarationSyntax> SubTypes) GenerateFuncMethod(
			ValueName name,
			ServiceSourceCodeType.Func info,
			TypeName clientName,
			RoslynTypeResolver typeResolver,
			ValueName candidConverterProperty
		)
		{
			List<(ValueName Name, ResolvedType Type)> resolvedArgTypes = info.ArgTypes
				.Select(t => (t.Name, typeResolver.ResolveType(t.Type, t.Name.PropertyName, parentType: clientName)))
				.ToList();
			List<(ValueName Name, ResolvedType Type)> resolvedReturnTypes = info.ReturnTypes
				.Select(t => (t.Name, typeResolver.ResolveType(t.Type, t.Name.PropertyName, parentType: clientName)))
				.ToList();

			List<TypedValueName> argTypes = resolvedArgTypes
				.Select(t => new TypedValueName(t.Type.Name, t.Name))
				.ToList();
			List<TypedValueName> returnTypes = resolvedReturnTypes
				.Select(t => new TypedValueName(t.Type.Name, t.Name))
				.ToList();

			BlockSyntax body = GenerateFuncMethodBody(
				methodName: name,
				argTypes,
				returnTypes,
				isQuery: info.IsQuery,
				typeResolver: typeResolver,
				candidConverterProperty
			);
			List<MemberDeclarationSyntax> subTypes = resolvedArgTypes
				.Where(t => t.Type.GeneratedSyntax != null)
				.SelectMany(t => t.Type.GeneratedSyntax!)
				.Concat(resolvedReturnTypes
					.Where(t => t.Type.GeneratedSyntax != null)
					.SelectMany(t => t.Type.GeneratedSyntax!)
				)
				.ToList();

			MethodDeclarationSyntax method = GenerateMethod(
				body: body,
				access: AccessType.Public,
				isStatic: false,
				isAsync: true,
				returnTypes: returnTypes,
				name: name.PropertyName,
				parameters: argTypes.ToArray()
			);
			return (method, subTypes);
		}

		private static BlockSyntax GenerateFuncMethodBody(
			ValueName methodName,
			List<TypedValueName> argTypes,
			List<TypedValueName> returnTypes,
			bool isQuery,
			RoslynTypeResolver typeResolver,
			ValueName candidConverterProperty
		)
		{
			// Build arguments for conversion to CandidArg
			IEnumerable<ArgumentSyntax> fromCandidArguments = argTypes
				.Select(t =>
				{
					ExpressionSyntax expression;
					if (t == null)
					{
						// `CandidTypedValue.Null();`
						expression = SyntaxFactory.InvocationExpression(
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.IdentifierName(typeof(CandidTypedValue).FullName!),
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
								SyntaxFactory.IdentifierName(typeof(CandidTypedValue).FullName!),
								SyntaxFactory.IdentifierName(nameof(CandidTypedValue.FromObject))
							)
						)
						.WithArgumentList(
							SyntaxFactory.ArgumentList(
								SyntaxFactory.SingletonSeparatedList(
									SyntaxFactory.Argument(SyntaxFactory.IdentifierName(t.Value.VariableName.ToSyntaxIdentifier()))
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
				.VariableDeclaration(SyntaxFactory.IdentifierName(typeof(CandidArg).FullName!))
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
										SyntaxFactory.IdentifierName(typeof(CandidArg).FullName!),
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
				StatementSyntax invokeQueryCall = GenerateQueryCall(methodName.CandidName, argName, responseName);
				statements.Add(invokeQueryCall);

				// `CandidArg reply = response.ThrowOrGetReply();`
				StatementSyntax invokeThrowOrGetReply = GenerateThrowOrGetReply(variableName, responseName);
				statements.Add(invokeThrowOrGetReply);

			}
			else
			{
				// `CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, {methodName}, arg)`
				StatementSyntax invokeCallAndWait = GenerateCallAndWait(methodName.CandidName, argName, variableName);
				statements.Add(invokeCallAndWait);

			}
			if (returnTypes.Any())
			{
				ArgumentSyntax converterArg = SyntaxFactory.Argument(
					SyntaxFactory.MemberAccessExpression(
						SyntaxKind.SimpleMemberAccessExpression,
						SyntaxFactory.ThisExpression(),
						SyntaxFactory.IdentifierName(candidConverterProperty.PropertyName)
					)
				);
				statements.Add(
					// `return reply.ToObjects<{T1}, {T2}, ...>(candidConverter);`
					SyntaxFactory.ReturnStatement(
						SyntaxFactory.InvocationExpression(
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.IdentifierName(variableName),
								SyntaxFactory.GenericName(
									SyntaxFactory.Identifier(nameof(CandidArg.ToObjects))
								)
								.WithTypeArgumentList(
									SyntaxFactory.TypeArgumentList(
										SyntaxFactory.SeparatedList(
											returnTypes
											.Select(t => t.Type.ToTypeSyntax())
										)
									)
								)
							)
						)
						.WithArgumentList(
							SyntaxFactory.ArgumentList(
								SyntaxFactory.SingletonSeparatedList(converterArg)
							)
						)
					)
				);
			}
			return SyntaxFactory.Block(statements);
		}

		private static StatementSyntax GenerateThrowOrGetReply(string variableName, string responseName)
		{
			return SyntaxFactory.LocalDeclarationStatement(
				SyntaxFactory.VariableDeclaration(
					SyntaxFactory.IdentifierName(typeof(CandidArg).FullName!)
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
						SyntaxFactory.IdentifierName(typeof(QueryResponse).FullName!)
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
					SyntaxFactory.IdentifierName(typeof(CandidArg).FullName!)
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
			List<ClassProperty> properties,
			List<ClassProperty>? optionalProperties = null
		)
		{
			optionalProperties ??= new List<ClassProperty>();
			IEnumerable<ExpressionStatementSyntax> propertyAssignments = properties
				.Concat(optionalProperties)
				.Select(p =>
				{
					var value = SyntaxFactory.IdentifierName(p.Name.VariableName);
					return SyntaxFactory.ExpressionStatement(
						SyntaxFactory.AssignmentExpression(
							SyntaxKind.SimpleAssignmentExpression,
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.ThisExpression(),
								SyntaxFactory.IdentifierName(p.Name.PropertyName)
							),
							value
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
							.Select(p =>
							{
								return SyntaxFactory.Parameter(
									SyntaxFactory.Identifier(p.Name.VariableName)
								)
								.WithType(p.Type!.ToTypeSyntax());
							})
							.Concat(optionalProperties
								.Where(p => p.Type != null)
								.Select(p =>
								{
									return SyntaxFactory.Parameter(
										SyntaxFactory.Identifier(p.Name.VariableName)
									)
									.WithType(p.Type!.ToTypeSyntax())
									// Add `= default` to the end
									.WithDefault(SyntaxFactory.EqualsValueClause(
										SyntaxFactory.LiteralExpression(
											SyntaxKind.DefaultLiteralExpression)
										)
									);
								}))
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
						.Select(t => SyntaxFactory.TupleElement(t.Type.ToTypeSyntax(), t.Value.PropertyName.ToSyntaxIdentifier()))
					));
				}
			}

			if (returnType == null)
			{
				if (isAsync)
				{
					// `Task` return type
					returnType = SyntaxFactory.IdentifierName(typeof(Task).FullName!.ToSyntaxIdentifier());
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
					returnType = SyntaxFactory.GenericName(typeof(Task).FullName!.ToSyntaxIdentifier())
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
						.Parameter(p.Value.VariableName.ToSyntaxIdentifier())
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
			List<ClassProperty>? optionalProperties = null,
			List<MethodDeclarationSyntax>? methods = null,
			List<TypeName>? implementTypes = null,
			List<AttributeListSyntax>? attributes = null,
			AccessType? emptyConstructorAccess = null,
			List<MemberDeclarationSyntax>? subTypes = null)
		{
			if (properties.Any(p => p.Type == null))
			{
				// TODO
				throw new NotImplementedException("Null, empty or reserved properties are not yet supported");
			}
			List<ConstructorDeclarationSyntax> constructors = new();
			IEnumerable<PropertyDeclarationSyntax> properySyntaxList = properties
				.Concat(optionalProperties ?? new List<ClassProperty>())
				.Select(GenerateProperty)
				.Where(p => p != null)!;
			if (properties.Any())
			{
				// Only create constructor if there are properties
				constructors.Add(GenerateConstructor(name, AccessType.Public, properties, optionalProperties));
			}
			if (emptyConstructorAccess != null)
			{
				// Empty Constrcutor for reflection
				constructors.Add(GenerateConstructor(
					name: name,
					access: emptyConstructorAccess.Value,
					properties: new List<ClassProperty>()
				));
			}

			ClassDeclarationSyntax classSyntax = SyntaxFactory.ClassDeclaration(name.GetName())
				.WithModifiers(SyntaxFactory.TokenList(
					// Make class public
					SyntaxFactory.Token(SyntaxKind.PublicKeyword)
				))
				.WithMembers(SyntaxFactory.List(
					// Properties
					properySyntaxList.Cast<MemberDeclarationSyntax>()
					// Constructors
					.Concat(constructors.Cast<MemberDeclarationSyntax>())
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
			public bool IsNullable { get; }
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
