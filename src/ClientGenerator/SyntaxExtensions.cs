using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdjCase.ICP.Candid.Models;
using Microsoft.CodeAnalysis;

namespace EdjCase.ICP.ClientGenerator
{
	internal static class SyntaxExtensions
	{
		public static SyntaxToken ToSyntaxIdentifier(this string name)
		{
			return SyntaxFactory.Identifier(name);
		}

		public static BaseParameterSyntax WithType<T>(this BaseParameterSyntax paramSyntax)
		{
			return paramSyntax.WithType(FromType(typeof(T)));
		}

		private static TypeSyntax FromType(Type type)
		{
			string name = type.Name.Replace('+', '.');

			if (!type.IsGenericType)
			{
				return SyntaxFactory.ParseTypeName(name);
			}
			// Get the C# representation of the generic type minus its type arguments.
			name = name[..name.IndexOf("`")];

			// Generate the name of the generic type.
			var genericArgs = type.GetGenericArguments();
			return SyntaxFactory.GenericName(
				SyntaxFactory.Identifier(name),
				SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(genericArgs.Select(FromType)))
			);
		}
	}
}
