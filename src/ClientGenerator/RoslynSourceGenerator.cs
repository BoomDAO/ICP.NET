using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Values;
using ICP.ClientGenerator;
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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	internal class RoslynSourceGenerator
	{
		public static string GenerateClientSourceCode(
			TypeName clientName,
			string baseNamespace,
			ServiceSourceCodeType desc,
			RoslynTypeResolver typeResolver
		)
		{
			// Generate client class
			ClassDeclarationSyntax clientClass = typeResolver.GenerateClient(clientName, desc);

			return PostProcessSourceCode(baseNamespace, clientClass);
		}


		public static (string FileName, string SourceCode) GenerateTypeSourceCode(
			ValueName id,
			string baseNamespace,
			RoslynTypeResolver typeResolver
		)
		{

			// Generate client class
			ClassDeclarationSyntax modelClass = typeResolver.GenerateType(id);

			string source = PostProcessSourceCode(baseNamespace + ".Models", modelClass);

			return (id.PascalCaseValue, source);
		}

		private static string PostProcessSourceCode(string baseNamespace, MemberDeclarationSyntax member)
		{
			// Generate namespace with class in it
			NamespaceDeclarationSyntax @namespace = SyntaxFactory
				.NamespaceDeclaration(SyntaxFactory.ParseName(baseNamespace))
				.WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(member));

			// Generate file with all code
			CompilationUnitSyntax compilationUnit = SyntaxFactory
				.CompilationUnit()
				.WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(@namespace));

			// Moves all namespaces to usings from the Type declarations
			SetUsings(ref compilationUnit);
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

		private static void SetUsings(ref CompilationUnitSyntax compilationUnit)
		{
			IEnumerable<IdentifierNameSyntax> identifiers = compilationUnit
				.DescendantNodes()
				.OfType<IdentifierNameSyntax>();
			List<UsingDirectiveSyntax> usingDirectives = new();
			foreach(IdentifierNameSyntax id in identifiers)
			{
				// TODO
				//if(id.)
				//compilationUnit.ReplaceNode(id, newId);
				usingDirectives.Add(SyntaxFactory.UsingDirective(id));
			}
			compilationUnit = compilationUnit
				.AddUsings(usingDirectives.ToArray());
		}
	}
}
