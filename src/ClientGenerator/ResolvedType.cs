using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	internal class ResolvedType
	{
		public TypeName Name { get; }
		public MemberDeclarationSyntax[]? GeneratedSyntax { get; }


		public ResolvedType(
			TypeName type,
			params MemberDeclarationSyntax[]? typeSyntax
		)
		{
			this.Name = type;
			this.GeneratedSyntax = typeSyntax;
		}
	}
}
