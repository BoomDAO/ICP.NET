using CommandLine;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Host;
using Microsoft.CodeAnalysis.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EdjCase.ICP.ClientGenerator
{
	internal class RoslynSourceGenerator
	{
		public static string GenerateClientSourceCode(
			TypeName clientName,
			string baseNamespace,
			ServiceSourceCodeType service,
			RoslynTypeResolver typeResolver,
			Dictionary<ValueName, TypeName> aliases
		)
		{
			// Generate client class
			ClassDeclarationSyntax clientClass = typeResolver.GenerateClient(clientName, service);

			return PostProcessSourceCode(baseNamespace, aliases, clientClass);
		}


		public static string? GenerateTypeSourceCode(
			ResolvedType type,
			string modelNamespace,
			Dictionary<ValueName, TypeName> aliases
		)
		{
			// Generate client class

			if (type.GeneratedSyntax?.Any() != true)
			{
				return null;
			}

			return PostProcessSourceCode(modelNamespace, aliases, type.GeneratedSyntax);
		}

		private static string PostProcessSourceCode(
			string modelNamespace,
			Dictionary<ValueName, TypeName> aliases,
			params MemberDeclarationSyntax[] members)
		{
			// Generate namespace with class in it
			NamespaceDeclarationSyntax @namespace = SyntaxFactory
				.NamespaceDeclaration(SyntaxFactory.ParseName(modelNamespace))
				.WithMembers(SyntaxFactory.List(members));

			// Generate file with all code
			CompilationUnitSyntax compilationUnit = SyntaxFactory
				.CompilationUnit()
				.WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(@namespace))
				.WithUsings(SyntaxFactory.List(
					aliases
						.Select(a => SyntaxFactory.UsingDirective(
							alias: SyntaxFactory.NameEquals(a.Key.PascalCaseValue),
							name: SyntaxFactory.IdentifierName(a.Value.GetNamespacedName())
						))
					)
				);
			SyntaxTree tree = SyntaxFactory.SyntaxTree(compilationUnit);
			CSharpCompilation compilation = CSharpCompilation.Create(null).AddSyntaxTrees(tree);
			//ImmutableArray<Diagnostic> diagnostics = compilation.GetDiagnostics();
			//foreach(Diagnostic d in diagnostics)
			//{
			//	Console.WriteLine(d.ToString());
			//}


			// Moves all namespaces to usings from the Type declarations
			//SetUsings(ref compilationUnit);
			BuildSourceWithShorthands(ref compilationUnit);

			// Setup formatting options
			AdhocWorkspace workspace = new();
			OptionSet options = workspace.Options
				.WithChangedOption(FormattingOptions.UseTabs, LanguageNames.CSharp, value: true)
				.WithChangedOption(FormattingOptions.NewLine, LanguageNames.CSharp, value: Environment.NewLine);

			compilationUnit = compilationUnit.NormalizeWhitespace();


			return Formatter.Format(compilationUnit, workspace, options).ToFullString();
		}


		private static void BuildSourceWithShorthands(ref CompilationUnitSyntax compilationUnit)
		{
			// TODO
			//private static Dictionary<Type, string> systemTypeShorthands = new Dictionary<Type, string>
			//{
			//	{ typeof(string), "string" },
			//	{ typeof(byte), "byte" },
			//	{ typeof(ushort), "ushort" },
			//	{ typeof(uint), "uint" },
			//	{ typeof(ulong), "ulong" },
			//	{ typeof(sbyte), "sbyte" },
			//	{ typeof(short), "short" },
			//	{ typeof(int), "int" },
			//	{ typeof(long), "long" },
			//	{ typeof(float), "float" },
			//	{ typeof(double), "double" },
			//	{ typeof(bool), "bool" }
			//};
		}

		private static void SetUsings(ref CompilationUnitSyntax root)
		{
			// TODO this does not work
			var compilation = CSharpCompilation.Create("T")
				.AddSyntaxTrees(root.SyntaxTree);
			var semanticModel = compilation.GetSemanticModel(root.SyntaxTree, ignoreAccessibility: false);


			IEnumerable<SyntaxNode> nodes = root
				.DescendantNodes(descendIntoTrivia: true);

			HashSet<string> uniqueNamespaces = new ();
			List<UsingDirectiveSyntax> usingDirectives = new();
			foreach (SyntaxNode node in nodes)
			{
				INamedTypeSymbol type;
				try
				{
					ITypeSymbol? typeSymbol = semanticModel.GetTypeInfo(node).Type;
					if(typeSymbol is not INamedTypeSymbol namedTypeSymbol)
					{
						continue;
					}
					type = namedTypeSymbol;
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
					continue;
				}
				string typeName = type.ToString()!;
				if (type.IsGenericType)
				{
					typeName = typeName[..typeName.IndexOf('<')];
				}
				if (type.IsNamespace
					//|| type.TypeKind == TypeKind.Error
					// TODO better detection than contains '.'
					|| !typeName.Contains('.'))
				{
					continue;
				}
				string @namespace = typeName[..typeName.LastIndexOf('.')];
				var identifierName = SyntaxFactory.IdentifierName(type.Name);
				root = root.ReplaceNode(node, identifierName);
				uniqueNamespaces.Add(@namespace);

			}
			root = root
				.AddUsings(
					uniqueNamespaces
					.Select(n => SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(n)))
					.ToArray()
				);
		}
	}
}
