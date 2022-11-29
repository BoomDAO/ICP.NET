using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.VariantSourceGenerator
{
	[Generator]
	public class VariantSourceGenerator : ISourceGenerator
	{
		private string VariantAttributeName = GetAttributeName<VariantAttribute>();
		private string VariantOptionAttributeName = GetAttributeName<VariantOptionAttribute>();

		public void Initialize(GeneratorInitializationContext context)
		{

		}

		public void Execute(GeneratorExecutionContext context)
		{
			var syntaxFactory = SyntaxFactory.CompilationUnit();
			IEnumerable<VariantInfo> variants = this.GetVariantClasses(context);


			foreach (VariantInfo variant in variants)
			{
				string className = variant.Class.Identifier.Text;

				IEnumerable<MemberDeclarationSyntax> members = this.GetVariantClassMembers(className, variant.Options);

				// public 
				ClassDeclarationSyntax newClass = SyntaxFactory.ClassDeclaration(className)
					.AddModifiers(
						SyntaxFactory.Token(SyntaxKind.PartialKeyword),
						SyntaxFactory.Token(SyntaxKind.PublicKeyword) // TODO make this match normal class
					)
					.AddMembers(members.ToArray());

				syntaxFactory.AddMembers(newClass);
			}
			string newClassFile = syntaxFactory
				.NormalizeWhitespace()
				.ToFullString();
			context.AddSource("Variants.g.cs", newClassFile);
		}

		private IEnumerable<MemberDeclarationSyntax> GetVariantClassMembers(string className, Dictionary<string, Type?> options)
		{

			// Add Enum for different options
			const string enumTypeName = "Option";
			const string typeMemberName = "Type";
			const string valueMemberName = "value";
			const string typeParameterName = "type";
			const string valueParameterName = "value";
			const string validateTypeMethodName = "ValidateType";
			EnumMemberDeclarationSyntax[] enumValues = options
				.Select(o => SyntaxFactory.EnumMemberDeclaration(o.Key))
				.ToArray();
			EnumDeclarationSyntax optionEnum = SyntaxFactory.EnumDeclaration(enumTypeName)
				.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
				.AddMembers(enumValues);
			yield return optionEnum;

			var optionType = SyntaxFactory.ParseTypeName(enumTypeName);

			// Create a Property: public Option Type { get; }
			PropertyDeclarationSyntax typePropertyDeclaration = SyntaxFactory
				.PropertyDeclaration(optionType, typeMemberName)
				.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
				.AddAccessorListAccessors(
					SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
				);
			yield return typePropertyDeclaration;

			TypeSyntax valueType = SyntaxFactory.ParseTypeName("object?");

			// Create a Property: private object? value { get; }
			PropertyDeclarationSyntax valuePropertyDeclaration = SyntaxFactory
				.PropertyDeclaration(valueType, valueMemberName)
				.AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword))
				.AddAccessorListAccessors(
					SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
				);
			yield return valuePropertyDeclaration;

			// Create constructor: private {ClassName}(Option option, object? value) ...
			ConstructorDeclarationSyntax constructor = SyntaxFactory
				.ConstructorDeclaration(className)
				.AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword))
				.AddParameterListParameters(
					SyntaxFactory.Parameter(SyntaxFactory.ParseToken(typeParameterName)).WithType(optionType),
					SyntaxFactory.Parameter(SyntaxFactory.ParseToken(valueParameterName)).WithType(optionType)
				)
				// Body for setting parameters
				.AddBodyStatements(
					// this.Option = option;
					SyntaxFactory.ExpressionStatement(
						SyntaxFactory.AssignmentExpression(
							SyntaxKind.EqualsToken,
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.ThisExpression(),
								SyntaxFactory.IdentifierName(typeMemberName)
							),
							SyntaxFactory.IdentifierName(typeParameterName)
						)
					),
					// this.value = value;
					SyntaxFactory.ExpressionStatement(
						SyntaxFactory.AssignmentExpression(
							SyntaxKind.EqualsToken,
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.ThisExpression(),
								SyntaxFactory.IdentifierName(valueMemberName)
							),
							SyntaxFactory.IdentifierName(valueParameterName)
						)
					)
				);
			yield return constructor;

			// Empty Constrcutor for reflection: protected {ClassName}() {}
			ConstructorDeclarationSyntax emptyConstructor = SyntaxFactory
				.ConstructorDeclaration(className)
				.AddModifiers(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword))
				.AddParameterListParameters();
			yield return emptyConstructor;


			// Add methods to get the value type from the class
			foreach (var option in options)
			{
				TypeSyntax returnType = SyntaxFactory.ParseTypeName(option.Value?.FullName ?? "void");
				// public {Type} As{Name}() ...
				MethodDeclarationSyntax asMethod = SyntaxFactory.MethodDeclaration(returnType, $"As{option.Key}");
	
				// ValidateType(Option.{Name});
				var callValidateMethod = SyntaxFactory.ExpressionStatement(
					SyntaxFactory.InvocationExpression(
						SyntaxFactory.IdentifierName(validateTypeMethodName),
						SyntaxFactory.ArgumentList(
							SyntaxFactory.SeparatedList(new List<ArgumentSyntax>
							{
								SyntaxFactory.Argument(
									SyntaxFactory.MemberAccessExpression(
										SyntaxKind.SimpleMemberAccessExpression,
										SyntaxFactory.IdentifierName(enumTypeName),
										SyntaxFactory.IdentifierName(valueMemberName)
									)
								)
							})
						)
					)
				);

				// return ({Type})this.value;
				var castAndReturn = SyntaxFactory.ReturnStatement(
					SyntaxFactory.CastExpression(
						returnType,
						SyntaxFactory.MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							SyntaxFactory.ThisExpression(),
							SyntaxFactory.IdentifierName(valueMemberName)
						)
					)
				);


				var body = SyntaxFactory.Block(
					callValidateMethod,
					castAndReturn
				);
				asMethod = asMethod.WithBody(body);
				yield return asMethod;
			}
		}

		private IEnumerable<VariantInfo> GetVariantClasses(GeneratorExecutionContext context)
		{
			foreach (var tree in context.Compilation.SyntaxTrees)
			{
				SemanticModel semantic = context.Compilation.GetSemanticModel(tree);
				foreach (ClassDeclarationSyntax variantClass in tree.GetRoot().DescendantNodesAndSelf().OfType<ClassDeclarationSyntax>())
				{
					ISymbol? classSymbol = semantic.GetDeclaredSymbol(variantClass);
					if (classSymbol == null)
					{
						continue;
					}
					ImmutableArray<AttributeData> allAttributes = classSymbol.GetAttributes();
					AttributeData? variantAttribute = allAttributes
						.FirstOrDefault(attribute => attribute.AttributeClass?.Name == this.VariantAttributeName);
					if (variantAttribute != null)
					{
						IEnumerable<AttributeData> optionAttributes = allAttributes
							.Where(attribute => attribute.AttributeClass?.Name == this.VariantOptionAttributeName);
						Dictionary<string, Type?> options = optionAttributes
							.ToDictionary(a => (string)a.ConstructorArguments[0].Value!, a => (Type?)a.ConstructorArguments[1].Value);
						yield return new VariantInfo(variantClass, options);
					}
				}

			}
		}

		private static string GetAttributeName<T>()
		{
			string name = nameof(T);
			return name
				.Take(name.Length - 9) // Remove 'Attribute' from end
				.ToString();
		}
	}
}
