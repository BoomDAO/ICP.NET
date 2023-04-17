using EdjCase.ICP.Candid.Mapping.Mappers;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EdjCase.ICP.ClientGenerator
{
	/// <summary>
	/// A model containing the client code to be rendered
	/// </summary>
	public class ClientSyntax
	{
		/// <summary>
		/// The name of the client
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// The syntax of the client file
		/// </summary>
		public CompilationUnitSyntax ClientFile { get; }

		/// <summary>
		/// The syntax of different declared types for the client
		/// </summary>
		public List<(string Name, CompilationUnitSyntax Syntax)> TypeFiles { get; }

		/// <param name="name">The name of the client</param>
		/// <param name="clientFile">The syntax of the client file</param>
		/// <param name="typeFiles">The syntax of different declared types for the client</param>
		public ClientSyntax(string name, CompilationUnitSyntax clientFile, List<(string Name, CompilationUnitSyntax Syntax)> typeFiles)
		{
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
			this.ClientFile = clientFile ?? throw new ArgumentNullException(nameof(clientFile));
			this.TypeFiles = typeFiles ?? throw new ArgumentNullException(nameof(typeFiles));
		}

		/// <summary>
		/// Rewrites the syntax with the specified `CSharpSyntaxRewriter`
		/// </summary>
		/// <param name="rewriter">A `CSharpSyntaxRewriter` to rewrite the csharp syntax</param>
		/// <returns>Updated client syntax</returns>
		public ClientSyntax Rewrite(CSharpSyntaxRewriter rewriter)
		{
			CompilationUnitSyntax updatedClientFile = (CompilationUnitSyntax)rewriter.Visit(this.ClientFile);

			List<(string Name, CompilationUnitSyntax updatedSyntax)> updatedTypeFiles = this.TypeFiles
				.Select((typeFile) =>
				{
					CompilationUnitSyntax updatedSyntax = (CompilationUnitSyntax)rewriter.Visit(typeFile.Syntax);
					return (typeFile.Name, updatedSyntax);
				})
				.ToList();

			return new ClientSyntax(this.Name, updatedClientFile, updatedTypeFiles);
		}

		/// <summary>
		/// Converts the file syntax objects into source code string values to use as a file
		/// </summary>
		/// <returns>Client and type source code file contents</returns>
		public (string ClientFile, List<(string Name, string Contents)> TypeFiles) GenerateFileContents()
		{
			string clientFile = GenerateFileContents(this.ClientFile);

			List<(string Name, string Contents)> typeFileContents = this.TypeFiles
				.Select((typeFile) =>
				{
					string contents = GenerateFileContents(typeFile.Syntax);
					return (typeFile.Name, contents);
				})
				.ToList();
			return (clientFile, typeFileContents);
		}

		/// <summary>
		/// Helper function to turn a client or type into a string of file contents
		/// </summary>
		/// <param name="syntax">The client or type file to convert to a string</param>
		/// <returns>String source code of the specified syntax</returns>
		public static string GenerateFileContents(CompilationUnitSyntax syntax)
		{
			// Setup formatting options
			AdhocWorkspace workspace = new();
			OptionSet options = workspace.Options
				.WithChangedOption(FormattingOptions.UseTabs, LanguageNames.CSharp, value: true)
				.WithChangedOption(FormattingOptions.NewLine, LanguageNames.CSharp, value: Environment.NewLine);

			return Formatter.Format(syntax, workspace, options).ToFullString();
		}
	}

}