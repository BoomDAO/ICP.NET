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
		public string ModelNamespace { get; }

		public NamespacePrefixRemover(string modelNamespace)
		{
			this.UniqueNamespaces = new();
			this.ModelNamespace = modelNamespace;
		}

		[return: NotNullIfNotNull("node")]
		public override SyntaxNode? Visit(SyntaxNode? node)
		{
			string? a = node?.ToString();
			if (a != null && a.StartsWith("EdjCase.ICP.Agent.Agents"))
			{
				int b = 2;
			}
			return base.Visit(node);
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

		private NameSyntax StripNamespace(NameSyntax node)
		{
			switch (node)
			{
				case GenericNameSyntax g:
					IEnumerable<NameSyntax> genericTypeNames = g.TypeArgumentList.Arguments
						.Select(t => this.StripNamespace((NameSyntax)t));

					return g
						.WithTypeArgumentList(
							SyntaxFactory.TypeArgumentList(
								SyntaxFactory.SeparatedList(
									genericTypeNames
										.Select(a => SyntaxFactory.ParseTypeName(a.ToString()))
								)
							)
						);
				case IdentifierNameSyntax i:
					{
						bool isOptional = i.Identifier.Text.EndsWith('?');
						if (isOptional)
						{
							// TODO when parsed, the ? is removed
							return i;
						}

						// TODO? Not sure whats up with this, but parse it into its real type
						NameSyntax name = SyntaxFactory.ParseName(i.Identifier.Text)
							.WithTrailingTrivia(i.Identifier.TrailingTrivia)
							.WithLeadingTrivia(i.Identifier.LeadingTrivia)
							.WithAdditionalAnnotations(i.Identifier.GetAnnotations());
						if(name is IdentifierNameSyntax n)
						{
							// No namepsace, avoid stack overflow
							return n;
						}
						return this.StripNamespace(name);
					}
				case QualifiedNameSyntax q:
					{
						NameSyntax @namespace = q.Left;
						NameSyntax type = this.StripNamespace(q.Right);

						// Handle any nested types
						while (@namespace is QualifiedNameSyntax nQ
							&& nQ.ToString().StartsWith(this.ModelNamespace + "."))
						{
							type = SyntaxFactory.ParseName(nQ.Right.ToString() + "." + type);
							@namespace = nQ.Left;
						}
						this.UniqueNamespaces.Add(@namespace.ToString());
						return type;
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
			return node.Parent switch
			{
				ParameterSyntax
				or TypeOfExpressionSyntax
				or NullableTypeSyntax
				or MethodDeclarationSyntax
				or MemberAccessExpressionSyntax
				or CastExpressionSyntax
				or TypeArgumentListSyntax
				or AttributeSyntax
				or PropertyDeclarationSyntax
				or ObjectCreationExpressionSyntax
				or VariableDeclarationSyntax => this.StripNamespace(node),

				NamespaceDeclarationSyntax
				or QualifiedNameSyntax
				or AssignmentExpressionSyntax
				or ArgumentSyntax
				or InterpolationSyntax => baseFunc(node), // Nodes where to not touch because they are just namespaces without types

				_ => baseFunc(node) // Catch all do nothing
			};
		}
	}
}
