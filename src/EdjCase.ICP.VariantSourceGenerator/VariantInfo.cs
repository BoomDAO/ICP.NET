using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.VariantSourceGenerator
{
	internal class VariantInfo
	{
		public ClassDeclarationSyntax Class { get;  }
		public Dictionary<string, Type?> Options { get; }
		public VariantInfo(ClassDeclarationSyntax @class, Dictionary<string, Type?> options)
		{
			this.Class = @class ?? throw new ArgumentNullException(nameof(@class));
			this.Options = options ?? new Dictionary<string, Type?>();
		}
	}
}