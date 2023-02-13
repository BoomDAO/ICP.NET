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
	public class ClientCode
	{
		public string Name { get; }
		public CompilationUnitSyntax ClientFile { get; }
		public List<(string Name, CompilationUnitSyntax SourceCode)> DataModelFiles { get; }
		public CompilationUnitSyntax? AliasFile { get; }

		public ClientCode(string name, CompilationUnitSyntax clientFile, List<(string Name, CompilationUnitSyntax SourceCode)> typeFiles, CompilationUnitSyntax? aliasFile)
		{
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
			this.ClientFile = clientFile ?? throw new ArgumentNullException(nameof(clientFile));
			this.DataModelFiles = typeFiles ?? throw new ArgumentNullException(nameof(typeFiles));
			this.AliasFile = aliasFile;
		}
	}

}