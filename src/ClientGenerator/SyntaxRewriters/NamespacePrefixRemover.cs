using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace EdjCase.ICP.ClientGenerator.SyntaxRewriters
{
	internal class NamespacePrefixRemover : CSharpSyntaxRewriter
	{
		public HashSet<string> UniqueNamespaces { get; }
		public HashSet<string> UniqueTypes { get; }
		public string ModelNamespace { get; }

		public NamespacePrefixRemover(string modelNamespace)
		{
			this.UniqueNamespaces = new();
			this.UniqueTypes = new();
			this.ModelNamespace = modelNamespace;
		}

		public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
		{
			return this.VisitInternal(node, base.VisitIdentifierName);
		}

		public override SyntaxNode? VisitGenericName(GenericNameSyntax node)
		{
			return this.VisitInternal(node, base.VisitGenericName);
		}


		public override SyntaxNode? VisitQualifiedName(QualifiedNameSyntax node)
		{
			return this.VisitInternal(node, base.VisitQualifiedName);
		}

		private TypeSyntax StripNamespace(TypeSyntax typeSyntax)
		{
			switch (typeSyntax)
			{
				case NameSyntax n:
					return this.StripNamespace(n);
				case ArrayTypeSyntax a:
					return a.WithElementType(this.StripNamespace(a.ElementType));
				case TupleTypeSyntax tt:
					return tt.WithElements(
						SyntaxFactory.SeparatedList(
							tt.Elements.Select(e =>
							{
								TypeSyntax type = this.StripNamespace(e.Type);
								return SyntaxFactory.TupleElement(type, e.Identifier);
							})
						)
					);
				default:
					return typeSyntax;
			}
		}

		private string StripNamespace(string fullName)
		{
			string? builtInType = fullName switch
			{
				"System.String" => "string",
				"System.Byte" => "byte",
				"System.UInt16" => "ushort",
				"System.UInt32" => "uint",
				"System.UInt64" => "ulong",
				"System.SByte" => "sbyte",
				"System.Int16" => "short",
				"System.Int32" => "int",
				"System.Int64" => "long",
				"System.Single" => "float",
				"System.Double" => "double",
				"System.Boolean" => "bool",
				"System.Decimal" => "decimal",
				"System.Char" => "char",
				"System.Object" => "object",
				_ => null,
			};
			if (builtInType != null)
			{
				return builtInType;
			}
			string[] components = fullName.Split('.', StringSplitOptions.RemoveEmptyEntries);
			string @namespace = string.Join(".", components[..^1]);
			string type = components.Last();

			// Handle any custom nested types 
			if (@namespace.StartsWith(this.ModelNamespace + "."))
			{
				// Add parent class name to type
				type = @namespace.Substring(this.ModelNamespace.Length + 1) + "." + type;
				// Remove parent class name from namespace
				@namespace = this.ModelNamespace;
			}
			if (!string.IsNullOrWhiteSpace(@namespace))
			{
				this.UniqueNamespaces.Add(@namespace.ToString());
			}
			this.UniqueTypes.Add(type);
			return type;
		}

		private NameSyntax StripNamespace(NameSyntax node)
		{
			switch (node)
			{
				case GenericNameSyntax g:
					{
						IEnumerable<TypeSyntax> genericTypeNames = g.TypeArgumentList.Arguments
							.Select(this.StripNamespace);

						string newId = this.StripNamespace(g.Identifier.Text);
						return g
							.WithIdentifier(SyntaxFactory.ParseToken(newId))
							.WithTypeArgumentList(
								SyntaxFactory.TypeArgumentList(
									SyntaxFactory.SeparatedList(
										genericTypeNames
											.Select(a => SyntaxFactory.ParseTypeName(a.ToString()))
									)
								)
							);
					}
				case IdentifierNameSyntax i:
					{
						string newId = this.StripNamespace(i.Identifier.Text);
						return SyntaxFactory.IdentifierName(newId);
					}
				case QualifiedNameSyntax q:
					{
						string t = this.StripNamespace(q.ToString());
						return SyntaxFactory.IdentifierName(t);
					}
				default:
					throw new NotImplementedException();
			};
		}


		private SyntaxNode? VisitInternal<T>(
			T node,
			Func<T, SyntaxNode?> baseFunc)
			where T : NameSyntax
		{
			bool shouldStrip = node.Parent switch
			{
				ParameterSyntax
				or TypeOfExpressionSyntax
				or NullableTypeSyntax
				or MethodDeclarationSyntax
				or MemberAccessExpressionSyntax
				or CastExpressionSyntax
				or TypeArgumentListSyntax
				or TupleTypeSyntax
				or TupleElementSyntax
				or ArrayTypeSyntax
				or PredefinedTypeSyntax
				or SimpleBaseTypeSyntax
				or AttributeSyntax
				or PropertyDeclarationSyntax
				or ObjectCreationExpressionSyntax
				or VariableDeclarationSyntax => true,

				NamespaceDeclarationSyntax
				or AssignmentExpressionSyntax
				or ArgumentSyntax
				or QualifiedNameSyntax
				or InterpolationSyntax => false, // Nodes where to not touch because they are just namespaces without types

				_ => false// Catch all do nothing
			};
			if (shouldStrip)
			{
				return this.StripNamespace(node);
			}
			return baseFunc(node);
		}
	}
}
