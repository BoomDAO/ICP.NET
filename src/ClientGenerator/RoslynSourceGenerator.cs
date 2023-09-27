using CommandLine;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using EdjCase.ICP.ClientGenerator.SyntaxRewriters;
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
using static EdjCase.ICP.ClientGenerator.Tool;

namespace EdjCase.ICP.ClientGenerator
{
	internal class RoslynSourceGenerator
	{
		public static CompilationUnitSyntax GenerateClientSourceCode(
			TypeName clientName,
			string modelNamespace,
			ServiceSourceCodeType service,
			RoslynTypeResolver typeResolver,
			Dictionary<string, string> aliases
		)
		{
			// Generate client class
			ClassDeclarationSyntax clientClass = typeResolver.GenerateClient(clientName, service);

			return PostProcessSourceCode(modelNamespace, aliases, clientClass);
		}


		public static CompilationUnitSyntax? GenerateTypeSourceCode(
			ResolvedType type,
			string modelNamespace,
			Dictionary<string, string> aliases
		)
		{
			// Generate client class

			if (type.GeneratedSyntax?.Any() != true)
			{
				return null;
			}

			return PostProcessSourceCode(modelNamespace, aliases, type.GeneratedSyntax);
		}

		private static CompilationUnitSyntax PostProcessSourceCode(
			string modelNamespace,
			Dictionary<string, string> aliases,
			params MemberDeclarationSyntax[] members
		)
		{
			// Generate namespace with class in it
			NamespaceDeclarationSyntax @namespace = SyntaxFactory
				.NamespaceDeclaration(SyntaxFactory.ParseName(modelNamespace))
				.WithMembers(SyntaxFactory.List(members));

			// Generate file with all code
			CompilationUnitSyntax compilationUnit = SyntaxFactory
				.CompilationUnit()
				.WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(@namespace));

			// Moves all namespaces to usings from the Type declarations
			var namespaceRemover = new NamespacePrefixRemover(modelNamespace);
			compilationUnit = (CompilationUnitSyntax)namespaceRemover.Visit(compilationUnit)!;

			IEnumerable<(string Key, string Value)> usedAliases = aliases
				.Where(a => namespaceRemover.UniqueTypes.Contains(a.Key))
				.Select(x => (x.Key, x.Value));

			compilationUnit = compilationUnit
				// Add namespaces used in files after cleanup
				.AddUsings(
					namespaceRemover.UniqueNamespaces
					.Select(n => SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(n)))
					.ToArray()
				)
				// Add alias using statements
				.AddUsings(usedAliases
						.Select(a => SyntaxFactory.UsingDirective(
							alias: SyntaxFactory.NameEquals(a.Key),
							name: SyntaxFactory.IdentifierName(a.Value)
						))
						.ToArray()
				)
				// Format
				.NormalizeWhitespace();

			return compilationUnit;
		}

	}
}
