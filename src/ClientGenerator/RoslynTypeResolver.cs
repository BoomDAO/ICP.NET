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
using System.Xml.Linq;
using System.Reflection;
using Org.BouncyCastle.Asn1.Cms;
using EdjCase.ICP.Agent.Models;

namespace EdjCase.ICP.ClientGenerator
{
	internal class RoslynTypeResolver
	{
		private readonly Dictionary<string, ResolvedType> _resolvedTypes = new();

		public string ModelNamespace { get; }
		public HashSet<string> Aliases { get; }
		public bool FeatureNullable { get; }
		public NameHelper NameHelper { get; }
		public Dictionary<string, (string Name, SourceCodeType Type)> DeclaredTypes { get; }

		public RoslynTypeResolver(
			string modelNamespace,
			HashSet<string> aliases,
			bool featureNullable,
			NameHelper nameHelper,
			Dictionary<string, (string Name, SourceCodeType Type)> declaredTypes
		)
		{
			this.ModelNamespace = modelNamespace;
			this.Aliases = aliases;
			this.FeatureNullable = featureNullable;
			this.NameHelper = nameHelper;
			this.DeclaredTypes = declaredTypes;
		}

		public ResolvedType ResolveTypeDeclaration(
			string typeName,
			SourceCodeType type
		)
		{
			// note that this only works for only one level of type nesting, so type aliases to generics whose argument is a user-defined type
			// will fail, for example:
			//    type A<T> = record { left : A, right : B };
			//    type X = blob;
			//    type F = A<X>;


			if (this._resolvedTypes.TryGetValue(typeName, out ResolvedType? existing))
			{
				return existing;
			}
			ResolvedType res = this.ResolveTypeInner(type, typeName, parentType: null);
			this._resolvedTypes[typeName] = res;
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
				case NonGenericSourceCodeType c:
					{
						var cType = new SimpleTypeName(
							c.Type.Name,
							c.Type.Namespace
						);
						return new ResolvedType(cType);
					}
				case ListSourceCodeType l:
					{
						ResolvedType resolvedGenericType = this.ResolveType(l.GenericType, nameContext + "Item", parentType);

						var name = new ListTypeName(resolvedGenericType.Name);
						return new ResolvedType(name, resolvedGenericType.GeneratedSyntax);
					}
				case DictionarySourceCodeType d:
					{
						ResolvedType resolvedKeyType = this.ResolveType(d.KeyType, nameContext + "Key", parentType);
						ResolvedType resolvedValueType = this.ResolveType(d.ValueType, nameContext + "Value", parentType);
						MemberDeclarationSyntax[] generatedSyntax = (resolvedKeyType.GeneratedSyntax ?? Array.Empty<MemberDeclarationSyntax>())
							.Concat(resolvedValueType.GeneratedSyntax ?? Array.Empty<MemberDeclarationSyntax>())
							.ToArray(); ;
						var name = new DictionaryTypeName(resolvedKeyType.Name, resolvedValueType.Name);
						return new ResolvedType(name, generatedSyntax);
					}
				case TupleSourceCodeType t:
					{
						List<ResolvedType> resolvedGenericTypes = t.Fields
							.Select((f, i) => this.ResolveType(f, nameContext + "Value_" + i, parentType))
							.ToList();
						List<TypeName> elementTypeNames = resolvedGenericTypes
							.Select(f => f.Name)
							.ToList();
						MemberDeclarationSyntax[] generatedSyntax = resolvedGenericTypes
							.SelectMany(t => t.GeneratedSyntax ?? Array.Empty<MemberDeclarationSyntax>())
							.ToArray();
						var name = new TupleTypeName(elementTypeNames);
						return new ResolvedType(name, generatedSyntax);
					}
				case OptionalValueSourceCodeType v:
					{
						ResolvedType resolvedGenericType = this.ResolveType(v.GenericType, nameContext + "Value", parentType);

						var name = new OptionalValueTypeName(resolvedGenericType.Name);
						return new ResolvedType(name, resolvedGenericType.GeneratedSyntax);
					}
				case ArraySourceCodeType a:
					{
						if (a.GenericType == null)
						{
							return new ResolvedType(new ArrayTypeName(null));
						}
						ResolvedType resolvedGenericType = this.ResolveType(a.GenericType, nameContext + "Item", parentType);

						var name = new ArrayTypeName(resolvedGenericType.Name);
						return new ResolvedType(name, resolvedGenericType.GeneratedSyntax);
					}
				case ReferenceSourceCodeType re:
					{
						bool isAlias = this.Aliases.Contains(re.Id.Value);
						(string name, SourceCodeType sourceCodeType) = this.DeclaredTypes[re.Id.Value];

						string? @namespace = isAlias ? null : this.ModelNamespace;
						TypeName typeName = new SimpleTypeName(
							name,
							@namespace
						);
						if (isAlias)
						{
							ResolvedType notAliased = this.ResolveType(sourceCodeType, nameContext, parentType);
							typeName = new AliasedTypeName(typeName, notAliased.Name);
						}
						return new ResolvedType(typeName);

					}
				case VariantSourceCodeType v:
					{
						TypeName variantName = this.BuildType(nameContext, parentType);
						(ClassDeclarationSyntax? ClassSyntax, EnumDeclarationSyntax EnumSyntax) result = this.GenerateVariant(variantName, v, parentType);
						if (result.ClassSyntax != null)
						{
							return new ResolvedType(variantName, new MemberDeclarationSyntax[] { result.ClassSyntax, result.EnumSyntax });
						}
						return new ResolvedType(variantName, new MemberDeclarationSyntax[] { result.EnumSyntax });
					}
				case RecordSourceCodeType r:
					{
						TypeName recordName = this.BuildType(nameContext, parentType);
						ClassDeclarationSyntax classSyntax = this.GenerateRecord(recordName, r);
						return new ResolvedType(recordName, new MemberDeclarationSyntax[] { classSyntax });
					}
				default:
					throw new NotImplementedException();
			}
		}


		public ClassDeclarationSyntax GenerateClient(
			TypeName clientName,
			ServiceSourceCodeType service)
		{
			string candidConverterProperty = "Converter";
			List<ClassProperty> properties = new()
			{
				// public IAgent Agent { get; }
				new ClassProperty(
					name: "Agent",
					type: SimpleTypeName.FromType<IAgent>(),
					access: AccessType.Public,
					hasSetter: false
				),
				
				// public Principal CanisterId { get; }
				new ClassProperty(
					name: "CanisterId",
					type: SimpleTypeName.FromType<Principal>(),
					access: AccessType.Public,
					hasSetter: false
				),
			};

			List<ClassProperty> optionalProperties = new()
			{				
				// public CandidConverter? Converter { get; }
				new ClassProperty(
					name: candidConverterProperty,
					type: SimpleTypeName.FromType<CandidConverter>(isNullable: this.FeatureNullable),
					access: AccessType.Public,
					hasSetter: false
				)
			};

			List<(MethodDeclarationSyntax Method, List<MemberDeclarationSyntax> SubTypes)> methods = service.Methods
				.Select(method => this.GenerateFuncMethod(method.CsharpName, method.CandidName, method.FuncInfo, clientName, this, candidConverterProperty))
				.ToList();

			return this.GenerateClass(
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
			(ResolvedName Name, ResolvedType? Type) ResolveOption((ResolvedName Tag, SourceCodeType? Type) option, int i)
			{
				ResolvedType? resolvedType;
				if (option.Type == null)
				{
					resolvedType = null;
				}
				else
				{
					string nameContext = option.Type.IsPredefinedType
						? option.Tag.Name
						: option.Tag.Name + "Info"; // If need to generate sub type, add suffix to avoid name collision
					resolvedType = this.ResolveType(option.Type, nameContext, variantTypeName);
				}
				return (option.Tag, resolvedType);
			}


			List<(ResolvedName Name, ResolvedType? Type)> resolvedOptions = variant.Options
				.Select(ResolveOption)
				.ToList();

			List<(ResolvedName Name, TypeName? Type)> enumOptions = resolvedOptions
				.Select(o => (o.Name, o.Type?.Name))
				.ToList();

			if (enumOptions.All(o => o.Type == null))
			{
				// If there are no types, just create an enum value

				TypeName enumTypeName = this.BuildType(variantTypeName.BuildName(false), parentType);
				EnumDeclarationSyntax enumSyntax = this.GenerateEnum(enumTypeName, enumOptions);
				return (null, enumSyntax);
			}
			else
			{
				TypeName enumTypeName = this.BuildType(variantTypeName.BuildName(false) + "Tag", parentType);

				// TODO auto change the property values of all class types if it matches the name
				bool containsClashingTag = variantTypeName.BuildName(false) == "Tag"
					|| variant.Options.Any(o => o.Tag.Name == "Tag");
				string tagName = containsClashingTag ? "Tag_" : "Tag";

				bool containsClashingValue = variantTypeName.BuildName(false) == "Value"
					|| variant.Options.Any(o => o.Tag.Name == "Value");
				string valueName = containsClashingValue ? "Value_" : "Value";

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
						SimpleTypeName.FromType<object>(isNullable: this.FeatureNullable),
						access: AccessType.Public,
						hasSetter: true,
						AttributeInfo.FromType<VariantValuePropertyAttribute>()
					)
				};
				List<MethodDeclarationSyntax> methods;
				List<PropertyDeclarationSyntax>? customProperties;
				if (false) // TODO
				{
					methods = this.GenerateVariantMethods(
						variantTypeName,
						enumTypeName,
						valueName,
						tagName,
						resolvedOptions
					);
					customProperties = null;
				}
				else
				{
					methods = new List<MethodDeclarationSyntax>();

					customProperties = this.GenerateVariantProperties(
						variantTypeName,
						enumTypeName,
						valueName,
						tagName,
						resolvedOptions
					);
				}

				List<AttributeListSyntax> attributes = new()
				{
					// [Variant]
					this.GenerateAttribute(AttributeInfo.FromType<VariantAttribute>())
				};
				ClassDeclarationSyntax classSyntax = this.GenerateClass(
					name: variantTypeName,
					properties: properties,
					optionalProperties: null,
					customProperties: customProperties,
					methods: methods,
					attributes: attributes,
					emptyConstructorAccess: AccessType.Protected,
					subTypes: resolvedOptions
						.SelectMany(o => o.Type?.GeneratedSyntax ?? Array.Empty<MemberDeclarationSyntax>())
						.ToList()
				);
				EnumDeclarationSyntax enumSyntax = this.GenerateEnum(enumTypeName, enumOptions);
				return (classSyntax, enumSyntax);
			}
		}

		private List<PropertyDeclarationSyntax> GenerateVariantProperties(
			TypeName variantTypeName,
			TypeName enumTypeName,
			string valueName,
			string tagName,
			List<(ResolvedName Name, ResolvedType? Type)> resolvedOptions
		)
		{
			// Properties with types
			// public {OptionType}? {OptionName} {
			//   get => this.Tag == {EnumName}.{OptionName} ? ({OptionType})this.Value : default;
			//   set => (this.Tag, this.Value) = ({EnumName}.{OptionName}, value);
			// }
			return resolvedOptions
				.Where(o => o.Type != null)
				.Select(o =>
				{
					List<AccessorDeclarationSyntax> accessors = new()
					{
						// Add getter
						SyntaxFactory
							.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
							.WithExpressionBody(SyntaxFactory.ArrowExpressionClause(
								SyntaxFactory.ConditionalExpression(
									// this.Tag == {EnumName}.{OptionName}
									SyntaxFactory.BinaryExpression(
										SyntaxKind.EqualsExpression,
										SyntaxFactory.MemberAccessExpression(
											SyntaxKind.SimpleMemberAccessExpression,
											SyntaxFactory.ThisExpression(),
											SyntaxFactory.IdentifierName(tagName)
										),
										SyntaxFactory.MemberAccessExpression(
											SyntaxKind.SimpleMemberAccessExpression,
											enumTypeName.ToTypeSyntax(),
											SyntaxFactory.IdentifierName(o.Name.Name)
										)
									),
									// ({OptionType}) this.Value
									SyntaxFactory.CastExpression(
										o.Type!.Name.ToTypeSyntax(),
										SyntaxFactory.MemberAccessExpression(
											SyntaxKind.SimpleMemberAccessExpression,
											SyntaxFactory.ThisExpression(),
											SyntaxFactory.IdentifierName(valueName)
										)
									),
									// default
									SyntaxFactory.LiteralExpression(
										SyntaxKind.DefaultLiteralExpression,
										SyntaxFactory.Token(SyntaxKind.DefaultKeyword)
									)
								)
							)),
						// Add setter
						SyntaxFactory
							.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
							.WithExpressionBody(SyntaxFactory.ArrowExpressionClause(
								SyntaxFactory.AssignmentExpression(
									SyntaxKind.SimpleAssignmentExpression,
									SyntaxFactory.TupleExpression(
										SyntaxFactory.SeparatedList<ArgumentSyntax>(
											new SyntaxNodeOrToken[]{
												SyntaxFactory.Argument(
													SyntaxFactory.MemberAccessExpression(
														SyntaxKind.SimpleMemberAccessExpression,
														SyntaxFactory.ThisExpression(),
														SyntaxFactory.IdentifierName(tagName)
													)
												),
												SyntaxFactory.Token(SyntaxKind.CommaToken),
												SyntaxFactory.Argument(
													SyntaxFactory.MemberAccessExpression(
														SyntaxKind.SimpleMemberAccessExpression,
														SyntaxFactory.ThisExpression(),
														SyntaxFactory.IdentifierName(valueName)
													)
												)
											}
										)
									),
									SyntaxFactory.TupleExpression(
										SyntaxFactory.SeparatedList<ArgumentSyntax>(
											new SyntaxNodeOrToken[]{
												SyntaxFactory.Argument(
													SyntaxFactory.MemberAccessExpression(
														SyntaxKind.SimpleMemberAccessExpression,
														enumTypeName.ToTypeSyntax(),
														SyntaxFactory.IdentifierName(o.Name.Name)
													)
												),
												SyntaxFactory.Token(SyntaxKind.CommaToken),
												SyntaxFactory.Argument(
													SyntaxFactory.IdentifierName("value")
												)
											}
										)
									)
								)
							))
					};
					return SyntaxFactory.PropertyDeclaration(
						o.Type!.Name.ToTypeSyntax(),
						o.Name.Name
					)
					.WithAccessorList(SyntaxFactory.AccessorList(
						SyntaxFactory.List(accessors)
					));
				})
				.ToList();

		}

		private List<MethodDeclarationSyntax> GenerateVariantMethods(
			TypeName variantTypeName,
			TypeName enumTypeName,
			string valueName,
			string tagName,
			List<(ResolvedName Name, ResolvedType? Type)> resolvedOptions
		)
		{
			// Creation methods
			// public static {VariantType} {OptionName}({VariantOptionType} value)
			// or if there is no type:
			// public static {VariantType} {OptionName}()
			List<MethodDeclarationSyntax> methods = resolvedOptions
				.Select(o => this.GenerateVariantOptionCreationMethod(
					variantTypeName,
					enumTypeName,
					o.Name,
					o.Type,
					"info"
				))
				.ToList();


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
			return methods;
		}

		private TypeName BuildType(string name, TypeName? parentType)
		{
			string @namespace;
			if (parentType == null)
			{
				@namespace = this.ModelNamespace;
				return new SimpleTypeName(name, @namespace);
			}
			return new NestedTypeName(parentType, name);
		}

		private MethodDeclarationSyntax GenerateVariantValidateTypeMethod(
			TypeName enumTypeName,
			string tagName
		)
		{
			// Generate helper function to validate the variant 'As_' methods
			//private void ValidateTag({VariantEnum} tag)
			//{
			//	if (!this.Tag.Equals(tag))
			//	{
			//		throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			//	}
			//}
			string argName = this.NameHelper.ToCamelCase(tagName);
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
								SyntaxFactory.IdentifierName(tagName)
							),
							SyntaxFactory.IdentifierName(nameof(CandidTag.Equals))
						)
					)
					.WithArgumentList(
						SyntaxFactory.ArgumentList(
							SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
								SyntaxFactory.Argument(
									SyntaxFactory.IdentifierName(argName)
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
																SyntaxFactory.IdentifierName(tagName)
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
															SyntaxFactory.IdentifierName(argName)
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
			return this.GenerateMethod(
				body: body,
				access: AccessType.Private,
				isStatic: false,
				isAsync: false,
				returnTypes: null,
				name: "ValidateTag",
				(enumTypeName, "tag")
			);
		}

		private MethodDeclarationSyntax GenerateVariantOptionAsMethod(
			TypeName enumType,
			ResolvedName optionName,
			ResolvedType optionType,
			string valueName
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
										SyntaxFactory.IdentifierName(optionName.Name)
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
								SyntaxFactory.IdentifierName(valueName)
							)
						)
					)
				)
			);

			return this.GenerateMethod(
				body: body,
				access: AccessType.Public,
				isStatic: false,
				isAsync: false,
				returnTypes: new List<TypedValueName> { new TypedValueName(optionType.Name, optionName) },
				name: "As" + optionName.Name
			);
		}

		private MethodDeclarationSyntax GenerateVariantOptionCreationMethod(
			TypeName variantTypeName,
			TypeName enumTypeName,
			ResolvedName optionTypeName,
			ResolvedType? optionType,
			string optionValueParamName
		)
		{
			ExpressionSyntax arg = optionType == null
				// If option type is not specified, then use `null`
				? SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)
				// If option type is specified, then use param
				: SyntaxFactory.IdentifierName(optionValueParamName);

			var creationParameters = new List<(TypeName, string)>();
			if (optionType != null)
			{
				creationParameters.Add((optionType.Name, optionValueParamName));
			}
			string methodName = optionTypeName.Name == variantTypeName.BuildName(false)
				? optionTypeName.Name + "_" // Escape colliding names
				: optionTypeName.Name;
			return this.GenerateMethod(
				// return new {VariantType}({VariantEnum}.{Option}, value);
				body: SyntaxFactory.Block(
				SyntaxFactory.ReturnStatement(
					SyntaxFactory.ObjectCreationExpression(
						variantTypeName.ToTypeSyntax()
					)
					.WithArgumentList(
						SyntaxFactory.ArgumentList(
							SyntaxFactory.SeparatedList<ArgumentSyntax>(
								new SyntaxNodeOrToken[]{
										SyntaxFactory.Argument(
											SyntaxFactory.MemberAccessExpression(
												SyntaxKind.SimpleMemberAccessExpression,
												enumTypeName.ToTypeSyntax(),
												SyntaxFactory.IdentifierName(optionTypeName.Name)
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
				parameters: creationParameters?.ToArray() ?? Array.Empty<(TypeName, string)>()
			);
		}

		internal ClassDeclarationSyntax GenerateRecord(TypeName recordTypeName, RecordSourceCodeType record)
		{
			(ResolvedName Name, ResolvedType Type) ResolveField((ResolvedName Tag, SourceCodeType Type) option, int i)
			{
				string nameContext = option.Type.IsPredefinedType
					? option.Tag.Name
					: option.Tag.Name + "Info"; // If need to generate sub type, add suffix to avoid name collision
				ResolvedType resolvedType = this.ResolveType(option.Type, nameContext, recordTypeName);
				return (option.Tag, resolvedType);
			}
			List<(ResolvedName Tag, ResolvedType Type)> resolvedFields = record.Fields
				.Select(ResolveField)
				.ToList();
			List<MemberDeclarationSyntax> subItems = resolvedFields
				.SelectMany(f => f.Type.GeneratedSyntax ?? Array.Empty<MemberDeclarationSyntax>())
				.ToList();

			List<ClassProperty> properties = resolvedFields
				.Select((f, i) =>
				{
					ResolvedName propertyName = f.Tag;
					if (propertyName.Name == recordTypeName.BuildName(false))
					{
						// Cant match the class name
						propertyName = new ResolvedName(
							name: propertyName.Name + "_", // TODO best way to escape it. @ does not work
							candidTag: propertyName.CandidTag
						);
					}
					List<AttributeInfo> attributes = new List<AttributeInfo>();
					if (propertyName.CandidTag.Name == null)
					{
						// [CandidTag({candidTag})]
						// Indicate there is no associated name, just an id
						// Usually with tuples like 'record { text; nat; }'
						attributes.Add(AttributeInfo.FromType<CandidTagAttribute>(propertyName.CandidTag));
					}
					else if (propertyName.CandidTag != propertyName.Name)
					{
						// [CandidName("{fieldCandidName}")]
						// Only add attribute if the name is different
						attributes.Add(AttributeInfo.FromType<CandidNameAttribute>(propertyName.CandidTag.Name!));
					}


					// public {fieldType} {fieldName} {{ get; set; }}
					return new ClassProperty(
						name: propertyName.Name,
						type: f.Type.Name,
						access: AccessType.Public,
						hasSetter: true,
						attributes.ToArray()
					);
				})
				.ToList();

			return this.GenerateClass(
				name: recordTypeName,
				properties: properties,
				optionalProperties: null,
				subTypes: subItems,
				emptyConstructorAccess: AccessType.Public
			);

		}

		private PropertyDeclarationSyntax? GenerateProperty(ClassProperty property)
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
					property.Type.ToTypeSyntax(),
					SyntaxFactory.Identifier(property.Name)
				)
				.WithAccessorList(SyntaxFactory.AccessorList(
					SyntaxFactory.List(accessors)
				));

			// Add `public`, `private`, etc...
			SyntaxToken? accessSyntaxToken = this.GenerateAccessToken(property.Access);
			if (accessSyntaxToken != null)
			{
				propertySyntax = propertySyntax
					.WithModifiers(SyntaxTokenList.Create(accessSyntaxToken.Value));
			}
			if (property.Attributes?.Any() == true)
			{
				IEnumerable<AttributeListSyntax> attributes = property.Attributes
					.Select(a => this.GenerateAttribute(a));
				propertySyntax = propertySyntax
					.WithAttributeLists(SyntaxFactory.List(attributes));
			}

			return propertySyntax;
		}

		private EnumDeclarationSyntax GenerateEnum(TypeName enumName, List<(ResolvedName Name, TypeName? Type)> values)
		{
			// Build enum options based on the values
			IEnumerable<SyntaxNode> enumOptions = values
				.Select(v =>
				{
					List<AttributeListSyntax> attributeList = new();

					if (v.Name.CandidTag.Name == null)
					{
						// [CandidTagId({candidTagId})]
						// Indicate there is no associated name, just an id
						// Usually with tuples like 'record { text; nat; }'
						attributeList.Add(this.GenerateAttribute(
							new AttributeInfo(SimpleTypeName.FromType<CandidTagAttribute>(), v.Name.CandidTag.Id)
						));
					}
					else if (v.Name.CandidTag.Name != v.Name.Name)
					{
						// [CandidName({candidName}]
						// Only add if names differ
						attributeList.Add(this.GenerateAttribute(
							new AttributeInfo(SimpleTypeName.FromType<CandidNameAttribute>(), v.Name.CandidTag.Name!)
						));
					}
					return SyntaxFactory
						// {optionName},
						.EnumMemberDeclaration(SyntaxFactory.Identifier(v.Name.Name))
						// Add attributes
						.WithAttributeLists(SyntaxFactory.List(attributeList));
				});

			// Create comma seperators between the enum options
			IEnumerable<SyntaxToken> enumSeperators = Enumerable.Range(0, values.Count - 1)
				.Select(i => SyntaxFactory.Token(SyntaxKind.CommaToken));

			// public enum {enumName}
			return SyntaxFactory.EnumDeclaration(enumName.BuildName(false))
				.WithModifiers(
					// public
					SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
				)
				.WithMembers(
					// Each enum option
					SyntaxFactory.SeparatedList(enumOptions, enumSeperators)
				);
		}



		private AttributeListSyntax GenerateAttribute(AttributeInfo attribute)
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
						uint i => SyntaxFactory.LiteralExpression(
							SyntaxKind.NumericLiteralExpression,
							SyntaxFactory.Literal(i)
						),
						CandidTag t => t.Name != null
							? SyntaxFactory.LiteralExpression(
								SyntaxKind.StringLiteralExpression,
								SyntaxFactory.Literal(t.Name)
							)
							: SyntaxFactory.LiteralExpression(
								SyntaxKind.NumericLiteralExpression,
								SyntaxFactory.Literal(t.Id)
							),
						TypeName type => SyntaxFactory.TypeOfExpression(type.ToTypeSyntax()),
						_ => throw new NotImplementedException()
					};
				})
				.Select(a => SyntaxFactory.AttributeArgument(a))
				?? Array.Empty<AttributeArgumentSyntax>();

			string typeName = attribute.Type.BuildName(true);
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

		private (MethodDeclarationSyntax Method, List<MemberDeclarationSyntax> SubTypes) GenerateFuncMethod(
			string csharpName,
			string candidName,
			ServiceSourceCodeType.Func info,
			TypeName clientName,
			RoslynTypeResolver typeResolver,
			string candidConverterProperty
		)
		{
			(ResolvedName Name, ResolvedType Type) ResolveType((ResolvedName Name, SourceCodeType Type) type, int i)
			{
				string nameContext = StringUtil.ToPascalCase(csharpName) + StringUtil.ToPascalCase(type.Name.Name);
				ResolvedType resolvedType = typeResolver.ResolveType(type.Type, nameContext, parentType: clientName);
				return (type.Name, resolvedType);
			}

			List<(ResolvedName Name, ResolvedType Type)> resolvedArgTypes = info.ArgTypes
				.Select(ResolveType)
				.ToList();
			List<(ResolvedName Name, ResolvedType Type)> resolvedReturnTypes = info.ReturnTypes
				.Select(ResolveType)
				.ToList();

			List<(TypeName, string)> argTypes = resolvedArgTypes
				.Select(t => (t.Type.Name, t.Name.ToCamelCase()))
				.ToList();
			List<TypedValueName> returnTypes = resolvedReturnTypes
				.Select(t => new TypedValueName(t.Type.Name, t.Name))
				.ToList();

			BlockSyntax body = this.GenerateFuncMethodBody(
				candidName: candidName,
				argTypes,
				returnTypes,
				isOneway: info.IsOneway,
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

			MethodDeclarationSyntax method = this.GenerateMethod(
				body: body,
				access: AccessType.Public,
				isStatic: false,
				isAsync: true,
				returnTypes: returnTypes,
				name: csharpName,
				parameters: argTypes.ToArray()
			);
			return (method, subTypes);
		}

		private BlockSyntax GenerateFuncMethodBody(
			string candidName,
			List<(TypeName Type, string Name)> argTypes,
			List<TypedValueName> returnTypes,
			bool isOneway,
			bool isQuery,
			RoslynTypeResolver typeResolver,
			string candidConverterProperty
		)
		{
			// Build arguments for conversion to CandidArg
			IEnumerable<ArgumentSyntax> fromCandidArguments = argTypes
				.Select(t =>
				{
					// `CandidTypedValue.FromObject({argX});`
					string argName = this.NameHelper.ToCamelCase(t.Name);
					ExpressionSyntax expression = SyntaxFactory.InvocationExpression(
						SyntaxFactory.MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							SyntaxFactory.IdentifierName(typeof(CandidTypedValue).FullName!),
							SyntaxFactory.IdentifierName(nameof(CandidTypedValue.FromObject))
						)
					)
					.WithArgumentList(
						SyntaxFactory.ArgumentList(
							SyntaxFactory.SingletonSeparatedList(
								SyntaxFactory.Argument(SyntaxFactory.IdentifierName(argName.ToSyntaxIdentifier()))
							)
						)
					);
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
			if (isOneway)
			{
				// Fire and forget
				// No return args
				// Query and Oneway are exclusive annotations in the IC
				// `await this.Agent.CallAsync(this.CanisterId, {methodName}, arg)`
				StatementSyntax invokeCall = this.GenerateCall(candidName, argName);
				statements.Add(invokeCall);
				return SyntaxFactory.Block(statements);
			}
			if (isQuery)
			{
				const string responseName = "response";
				// `QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, {methodName}, arg);`
				StatementSyntax invokeQueryCall = this.GenerateQueryCall(candidName, argName, responseName);
				statements.Add(invokeQueryCall);

				// `CandidArg reply = response.ThrowOrGetReply();`
				StatementSyntax invokeThrowOrGetReply = this.GenerateThrowOrGetReply(variableName, responseName);
				statements.Add(invokeThrowOrGetReply);

			}
			else
			{
				// `CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, {methodName}, arg)`
				string? replyVariableName = returnTypes.Any() ? variableName : null; // Dont include reply variable if its not used
				StatementSyntax invokeCallAndWait = this.GenerateCallAndWait(candidName, argName, replyVariableName);
				statements.Add(invokeCallAndWait);

			}
			if (returnTypes.Any())
			{
				ArgumentSyntax converterArg = SyntaxFactory.Argument(
					SyntaxFactory.MemberAccessExpression(
						SyntaxKind.SimpleMemberAccessExpression,
						SyntaxFactory.ThisExpression(),
						SyntaxFactory.IdentifierName(candidConverterProperty)
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

		private StatementSyntax GenerateThrowOrGetReply(string variableName, string responseName)
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

		private StatementSyntax GenerateQueryCall(string methodName, string argName, string responseName)
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

		private StatementSyntax GenerateCallAndWait(string methodName, string argName, string? variableName)
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
			if (variableName != null)
			{
				// `CandidArg reply = await ...`
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
			// No variable, just await 
			// `await ...`

			return SyntaxFactory.ExpressionStatement(
				SyntaxFactory.AwaitExpression(apiCall)
			);
		}
		private StatementSyntax GenerateCall(string methodName, string argName)
		{
			InvocationExpressionSyntax apiCall = SyntaxFactory.InvocationExpression(
					SyntaxFactory.MemberAccessExpression(
						SyntaxKind.SimpleMemberAccessExpression,
						SyntaxFactory.MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							SyntaxFactory.ThisExpression(),
							SyntaxFactory.IdentifierName("Agent")
						),
						SyntaxFactory.IdentifierName("CallAsync")
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
			return SyntaxFactory.ExpressionStatement(
				SyntaxFactory.AwaitExpression(apiCall)
			);
		}

		private ConstructorDeclarationSyntax GenerateConstructor(
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
					string argName = this.NameHelper.ToCamelCase(p.Name);
					var value = SyntaxFactory.IdentifierName(argName);
					return SyntaxFactory.ExpressionStatement(
						SyntaxFactory.AssignmentExpression(
							SyntaxKind.SimpleAssignmentExpression,
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.ThisExpression(),
								SyntaxFactory.IdentifierName(p.Name)
							),
							value
						)
					);
				});

			BlockSyntax body = SyntaxFactory.Block(propertyAssignments);


			ConstructorDeclarationSyntax constructor = SyntaxFactory
				.ConstructorDeclaration(
					SyntaxFactory.Identifier(name.BuildName(false))
				)
				.WithParameterList(
					SyntaxFactory.ParameterList(
						SyntaxFactory.SeparatedList(
							properties
							.Where(p => p.Type != null)
							.Select(p =>
							{
								string argName = this.NameHelper.ToCamelCase(p.Name);
								return SyntaxFactory.Parameter(
									SyntaxFactory.Identifier(argName)
								)
								.WithType(p.Type!.ToTypeSyntax());
							})
							.Concat(optionalProperties
								.Where(p => p.Type != null)
								.Select(p =>
								{
									string argName = this.NameHelper.ToCamelCase(p.Name);
									return SyntaxFactory.Parameter(
										SyntaxFactory.Identifier(argName)
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
			SyntaxToken? accessSyntaxToken = this.GenerateAccessToken(access);
			if (accessSyntaxToken != null)
			{
				constructor = constructor
					.WithModifiers(
						SyntaxFactory.TokenList(accessSyntaxToken.Value)
					);
			}

			return constructor;
		}

		private SyntaxToken? GenerateAccessToken(AccessType access)
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

		private MethodDeclarationSyntax GenerateMethod(
			BlockSyntax body,
			AccessType access,
			bool isStatic,
			bool isAsync,
			List<TypedValueName>? returnTypes,
			string name,
			params (TypeName Type, string Name)[] parameters)
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
						.Select(t => SyntaxFactory.TupleElement(t.Type.ToTypeSyntax(), (" " + t.Value.Name).ToSyntaxIdentifier()))
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
						.Parameter(p.Name.ToSyntaxIdentifier())
						.WithType(p.Type.ToTypeSyntax());
				});
			return SyntaxFactory.MethodDeclaration(returnType, SyntaxFactory.Identifier(name))
				.WithModifiers(SyntaxFactory.TokenList(methodModifiers))
				.WithParameterList(SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(@params)))
				.WithBody(body);
		}

		private ClassDeclarationSyntax GenerateClass(
			TypeName name,
			List<ClassProperty> properties,
			List<ClassProperty>? optionalProperties = null,
			List<MethodDeclarationSyntax>? methods = null,
			List<PropertyDeclarationSyntax>? customProperties = null,
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
			var uniquePropertyNames = new HashSet<string>();
			ClassProperty FixDuplicates(ClassProperty property)
			{
				string name = property.Name;
				int i = 0;
				while (!uniquePropertyNames.Add(name))
				{
					// Add number suffix till there are no duplicates
					i++;
					name = property.Name + i.ToString();
				}
				if (i > 0)
				{
					return new ClassProperty(name, property.Type, property.Access, property.HasSetter, property.Attributes);
				}
				return property;
			}
			properties = properties
				.Select(FixDuplicates) // Fix if there are duplicate names
				.ToList();
			optionalProperties = optionalProperties
				?.Select(FixDuplicates) // Fix if there are duplicate names
				.ToList();
			List<ConstructorDeclarationSyntax> constructors = new();
			IEnumerable<PropertyDeclarationSyntax> properySyntaxList = properties
				.Concat(optionalProperties ?? new List<ClassProperty>())
				.Select(this.GenerateProperty)
				.Where(p => p != null)!;
			if (customProperties != null)
			{
				properySyntaxList = properySyntaxList.Concat(customProperties);
			}
			if (properties.Any())
			{
				// Only create constructor if there are properties
				constructors.Add(this.GenerateConstructor(name, AccessType.Public, properties, optionalProperties));
			}

			if (emptyConstructorAccess != null)
			{
				// Empty Constrcutor for reflection
				constructors.Add(this.GenerateConstructor(
					name: name,
					access: emptyConstructorAccess.Value,
					properties: new List<ClassProperty>()
				));
			}

			ClassDeclarationSyntax classSyntax = SyntaxFactory.ClassDeclaration(name.BuildName(false))
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
									.Select(t => SyntaxFactory.SimpleBaseType(t.ToTypeSyntax()))
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
			public string Name { get; }
			public TypeName? Type { get; }
			public bool IsNullable { get; }
			public bool HasSetter { get; }
			public AttributeInfo[]? Attributes { get; }

			public ClassProperty(
				string name,
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

	}
}
