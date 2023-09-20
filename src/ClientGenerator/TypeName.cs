using EdjCase.ICP.Candid.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.ClientGenerator
{
	internal class SimpleTypeName : TypeName
	{
		public string Name { get; }

		public SimpleTypeName(string name, string? @namespace, string? prefix)
			: base(@namespace, prefix)
		{
			this.Name = name;
		}

		protected override string BuildName(Func<TypeName, string> buildInnerName)
		{
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
			var typeName = new SimpleTypeName(type.Name, type.Namespace, prefix: null); // TODO prefix
			if (isNullable)
			{
				// Wrap in nullable type
				return new NullableTypeName(typeName);
			}
			return typeName;
		}
	}

	internal class ListTypeName : TypeName
	{
		public TypeName InnerType { get; set; }
		public ListTypeName(TypeName innerType) : base("System.Collections.Generic", null)
		{
			this.InnerType = innerType;
		}

		public override TypeSyntax ToTypeSyntax()
		{
			return SyntaxFactory.GenericName(
					SyntaxFactory.Identifier("System.Collections.Generic.List")
				)
				.WithTypeArgumentList(
					SyntaxFactory.TypeArgumentList(
						SyntaxFactory.SingletonSeparatedList(
							this.InnerType.ToTypeSyntax()
						)
					)
				);
		}

		protected override string BuildName(Func<TypeName, string> buildInnerName)
		{
			return $"List<{buildInnerName(this.InnerType)}>";
		}
	}

	internal class DictionaryTypeName : TypeName
	{
		public TypeName KeyType { get; set; }
		public TypeName ValueType { get; set; }
		public DictionaryTypeName(TypeName keyType, TypeName valueType) : base("System.Collections.Generic", null)
		{
			this.KeyType = keyType;
			this.ValueType = valueType;
		}

		public override TypeSyntax ToTypeSyntax()
		{
			return SyntaxFactory.GenericName(
					SyntaxFactory.Identifier("System.Collections.Generic.Dictionary")
				)
				.WithTypeArgumentList(
					SyntaxFactory.TypeArgumentList(
						SyntaxFactory.SeparatedList(
							new[] {
								this.KeyType.ToTypeSyntax(),
								this.ValueType.ToTypeSyntax()
							}
						)
					)
				);
		}

		protected override string BuildName(Func<TypeName, string> buildInnerName)
		{
			return $"Dictionary<{buildInnerName(this.KeyType)}, {buildInnerName(this.ValueType)}>";
		}
	}

	internal class OptionalValueTypeName : TypeName
	{
		public TypeName InnerType { get; set; }
		public OptionalValueTypeName(TypeName innerType) : base(typeof(OptionalValue<>).Namespace, null)
		{
			this.InnerType = innerType;
		}

		public override TypeSyntax ToTypeSyntax()
		{
			return SyntaxFactory.GenericName(
					SyntaxFactory.Identifier(typeof(OptionalValue<>).Namespace + ".OptionalValue")
				)
				.WithTypeArgumentList(
					SyntaxFactory.TypeArgumentList(
						SyntaxFactory.SingletonSeparatedList(
							this.InnerType.ToTypeSyntax()
						)
					)
				);
		}

		protected override string BuildName(Func<TypeName, string> buildInnerName)
		{
			return $"OptionalValue<{buildInnerName(this.InnerType)}>";
		}
	}

	internal class NullableTypeName : TypeName
	{
		public TypeName InnerType { get; set; }
		public NullableTypeName(TypeName innerType) : base(null, null)
		{
			this.InnerType = innerType;
		}

		public override TypeSyntax ToTypeSyntax()
		{
			return SyntaxFactory.NullableType(this.InnerType.ToTypeSyntax());
		}

		protected override string BuildName(Func<TypeName, string> buildInnerName)
		{
			return buildInnerName(this.InnerType) + "?";
		}
	}

	internal class TupleTypeName : TypeName
	{
		public List<TypeName> ElementTypeNameList { get; }

		public TupleTypeName(List<TypeName> elementTypeNameList)
			: base(null, prefix: null)
		{
			this.ElementTypeNameList = elementTypeNameList;
		}
		protected override string BuildName(Func<TypeName, string> buildInnerName)
		{
			string elements = string.Join(", ", this.ElementTypeNameList.Select(buildInnerName));
			return $"({elements})";
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
		public TypeName? ElementTypeName { get; }

		public ArrayTypeName(TypeName? elementTypeName)
			: base(elementTypeName == null ? "System" : null, prefix: null)
		{
			this.ElementTypeName = elementTypeName;
		}

		protected override string BuildName(Func<TypeName, string> buildInnerName)
		{
			if(this.ElementTypeName == null)
			{
				return "Array";
			}
			return buildInnerName(this.ElementTypeName) + "[]";
		}

		public override TypeSyntax ToTypeSyntax()
		{
			if (this.ElementTypeName == null)
			{
				return SyntaxFactory.IdentifierName("System.Array");
			}
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
