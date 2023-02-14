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
			Dictionary<ValueName, TypeName> aliases
		)
		{
			// Generate client class
			ClassDeclarationSyntax clientClass = typeResolver.GenerateClient(clientName, service);

			return PostProcessSourceCode(modelNamespace, aliases, clientClass);
		}


		public static CompilationUnitSyntax? GenerateTypeSourceCode(
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

		private static CompilationUnitSyntax PostProcessSourceCode(
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
				.WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(@namespace));



			// Moves all namespaces to usings from the Type declarations
			//SetUsings(ref compilationUnit);
			BuildSourceWithShorthands(ref compilationUnit);

			var namespaceRemover = new NamespacePrefixRemover(modelNamespace);
			compilationUnit = (CompilationUnitSyntax)namespaceRemover.Visit(compilationUnit)!;


			compilationUnit = compilationUnit
				// Add alias using statements
				.AddUsings(aliases
						.Select(a => SyntaxFactory.UsingDirective(
							alias: SyntaxFactory.NameEquals(a.Key.PropertyName),
							name: SyntaxFactory.IdentifierName(a.Value.GetNamespacedName())
						))
						.ToArray()
				)
				// Add namespaces used in files after cleanup
				.AddUsings(
					namespaceRemover.UniqueNamespaces
					.Select(n => SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(n)))
					.ToArray()
				)
				// Format
				.NormalizeWhitespace();

			return compilationUnit;
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
	}
}
