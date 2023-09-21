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
			MemberDeclarationSyntax[]? typeSyntax = null
		)
		{
			this.Name = type;
			this.GeneratedSyntax = typeSyntax;
		}
	}

	internal class AttributeInfo
	{
		public TypeName Type { get; }
		public object[]? Args { get; }

		public AttributeInfo(TypeName type, params object[]? args)
		{
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.Args = args;
		}

		public static AttributeInfo FromType<T>(params object[]? args)
		{
			TypeName type = SimpleTypeName.FromType<T>();
			return new AttributeInfo(type, args);
		}
	}
}
