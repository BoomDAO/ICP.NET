using EdjCase.ICP.Candid.Mapping.Mappers;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
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
	}

}