using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.ClientGenerator
{
	internal class ClassicTypeName : TypeName
	{
		public string Name { get; }
		public TypeName? GenericType { get; }

		public ClassicTypeName(string name, string? @namespace, string? prefix, TypeName? genericType = null)
			: base(@namespace, prefix)
		{
			this.Name = name;
			this.GenericType = genericType;
		}

		protected override string BuildName(Func<TypeName, string> buildInnerName)
		{
			if (this.GenericType != null)
			{
				return this.Name + $"<{buildInnerName(this.GenericType)}>";
			}
			return this.Name;
		}

		public override TypeSyntax ToTypeSyntax()
		{
			return SyntaxFactory.IdentifierName(this.GetNamespacedName());
		}
		public static TypeName FromType<T>(bool isNullable = false)
		{
			return FromType(typeof(T), isNullable);
		}

		public static TypeName FromType(Type type, bool isNullable = false)
		{
			if (type.IsGenericType)
			{
				// TODO?
				throw new NotImplementedException();
			}
			// TODO handle `fake` nullables like `object?`
			string name = isNullable ? type.Name + "?" : type.Name;
			return new ClassicTypeName(name, type.Namespace, prefix: null); // TODO prefix
		}
	}

	internal class TupleTypeName : TypeName
	{
		public List<TypeName> ElementTypeNameList { get; }

		public TupleTypeName(List<TypeName> elementTypeNameList)
			: base("System", prefix: null)
		{
			this.ElementTypeNameList = elementTypeNameList;
		}
		protected override string BuildName(Func<TypeName, string> buildInnerName)
		{
			string elements = string.Join(", ", this.ElementTypeNameList.Select(e => buildInnerName));
			return $"ValueTuple<{elements}>";
		}

		public override TypeSyntax ToTypeSyntax()
		{
			return SyntaxFactory.TupleType(
				SyntaxFactory.SeparatedList(
					this.ElementTypeNameList
					.Select(e => SyntaxFactory.TupleElement(e.ToTypeSyntax()))
				)
			);
		}
	}

	internal class ArrayTypeName : TypeName
	{
		public TypeName ElementTypeName { get; }

		public ArrayTypeName(TypeName elementTypeName)
			: base("System", prefix: null)
		{
			this.ElementTypeName = elementTypeName;
		}

		protected override string BuildName(Func<TypeName, string> buildInnerName)
		{
			return buildInnerName(this.ElementTypeName) + "[]";
		}

		public override TypeSyntax ToTypeSyntax()
		{
			return SyntaxFactory.ArrayType(this.ElementTypeName.ToTypeSyntax())
				.WithRankSpecifiers(
					SyntaxFactory.SingletonList<ArrayRankSpecifierSyntax>(
						SyntaxFactory.ArrayRankSpecifier(
							SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(
								SyntaxFactory.OmittedArraySizeExpression()
							)
						)
					)
				);
		}
	}

	internal abstract class TypeName
	{
		private string? @namespace { get; }
		private string? prefix { get; } // used for parent classes
		private string? nameCache { get; set; }
		private string? namespacedNameCache { get; set; }

		public TypeName(string? @namespace, string? prefix)
		{
			this.@namespace = @namespace;
			this.prefix = prefix;
		}

		protected abstract string BuildName(Func<TypeName, string> buildInnerName);
		public abstract TypeSyntax ToTypeSyntax();

		public bool HasPrefix => this.prefix != null;

		public string GetName()
		{
			return this.GetName(false);
		}

		public string GetNamespacedName()
		{
			return this.GetName(true);
		}

		internal string GetName(bool includeNamespaces)
		{
			if (includeNamespaces)
			{
				if (this.namespacedNameCache != null)
				{
					return this.namespacedNameCache;
				}
			}
			else
			{
				if (this.nameCache != null)
				{
					return this.nameCache;
				}
			}
			StringBuilder builder = new StringBuilder();
			if (includeNamespaces)
			{
				if (this.@namespace != null)
				{
					builder.Append(this.@namespace);
					builder.Append('.');
				}
				if (this.prefix != null)
				{
					builder.Append(this.prefix);
					builder.Append('.');
				}
			}
			string name = this.BuildName(innerType => innerType.GetName(includeNamespaces));
			builder.Append(name);
			string value = builder.ToString();
			if (includeNamespaces)
			{
				this.namespacedNameCache = value;
			}
			else
			{
				this.nameCache = value;
			}
			return value;
		}

		public override string ToString()
		{
			return this.GetNamespacedName();
		}
	}

}
