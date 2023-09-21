using EdjCase.ICP.Candid.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.ClientGenerator
{
	internal class AliasedTypeName : TypeName
	{
		public TypeName Aliased { get; set; }
		public TypeName NotAliased { get; set; }

		public AliasedTypeName(TypeName aliased, TypeName notAliased)
		{
			this.Aliased = aliased ?? throw new ArgumentNullException(nameof(aliased));
			this.NotAliased = notAliased ?? throw new ArgumentNullException(nameof(notAliased));
		}
		public override string BuildName(bool includeNamespace, bool resolveAliases)
		{
			if (resolveAliases)
			{
				return this.NotAliased.BuildName(includeNamespace, true);
			}
			return this.Aliased.BuildName(includeNamespace, false);
		}

		public override TypeSyntax ToTypeSyntax(bool resolveAliases)
		{
			if (resolveAliases)
			{
				return this.NotAliased.ToTypeSyntax(false);
			}
			return this.Aliased.ToTypeSyntax(false);
		}
	}
	internal class NestedTypeName : TypeName
	{
		public TypeName ParentType { get; }
		public string Name { get; }
		public NestedTypeName(TypeName parentType, string name)
		{
			this.ParentType = parentType ?? throw new ArgumentNullException(nameof(parentType));
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public override string BuildName(bool includeNamespace, bool resolveAliases)
		{
			if (!includeNamespace)
			{
				return this.Name;
			}
			return this.ParentType.BuildName(true, resolveAliases) + "." + this.Name;
		}

		public override TypeSyntax ToTypeSyntax(bool resolveAliases)
		{
			string typeName = this.BuildName(includeNamespace: true, resolveAliases);
			return SyntaxFactory.IdentifierName(typeName);
		}
	}

	internal class SimpleTypeName : TypeName
	{
		public string Name { get; }
		public string? Namespace { get; }

		public SimpleTypeName(string name, string? @namespace)
		{
			this.Name = name;
			this.Namespace = @namespace;
		}

		public override string BuildName(bool includeNamespace, bool resolveAliases)
		{
			if (!includeNamespace || this.Namespace == null)
			{
				return this.Name;
			}
			return this.Namespace + "." + this.Name;
		}

		public override TypeSyntax ToTypeSyntax(bool resolveAliases)
		{
			string typeName = this.BuildName(includeNamespace: true, resolveAliases);
			return SyntaxFactory.IdentifierName(typeName);
		}

		public static TypeName FromType<T>(bool isNullable = false)
		{
			return FromType(typeof(T), isNullable);
		}

		public static TypeName FromType(Type type, bool isNullable = false)
		{
			TypeName typeName;
			if (type.IsNested)
			{
				TypeName parentType = FromType(type.DeclaringType!);
				typeName = new NestedTypeName(parentType, type.Name);
			}
			else
			{
				typeName = new SimpleTypeName(type.Name, type.Namespace);
			}
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
		public ListTypeName(TypeName innerType)
		{
			this.InnerType = innerType;
		}

		public override TypeSyntax ToTypeSyntax(bool resolveAliases)
		{
			return SyntaxFactory.GenericName(
					SyntaxFactory.Identifier("System.Collections.Generic.List")
				)
				.WithTypeArgumentList(
					SyntaxFactory.TypeArgumentList(
						SyntaxFactory.SingletonSeparatedList(
							this.InnerType.ToTypeSyntax(resolveAliases)
						)
					)
				);
		}

		public override string BuildName(bool includeNamespace, bool resolveAliases)
		{
			return $"System.Collections.Generic.List<{this.InnerType.BuildName(includeNamespace, resolveAliases)}>";
		}
	}

	internal class DictionaryTypeName : TypeName
	{
		public TypeName KeyType { get; set; }
		public TypeName ValueType { get; set; }
		public DictionaryTypeName(TypeName keyType, TypeName valueType)
		{
			this.KeyType = keyType;
			this.ValueType = valueType;
		}

		public override TypeSyntax ToTypeSyntax(bool resolveAliases)
		{
			return SyntaxFactory.GenericName(
					SyntaxFactory.Identifier("System.Collections.Generic.Dictionary")
				)
				.WithTypeArgumentList(
					SyntaxFactory.TypeArgumentList(
						SyntaxFactory.SeparatedList(
							new[] {
								this.KeyType.ToTypeSyntax(resolveAliases),
								this.ValueType.ToTypeSyntax(resolveAliases)
							}
						)
					)
				);
		}

		public override string BuildName(bool includeNamespace, bool resolveAliases)
		{
			string key = this.KeyType.BuildName(includeNamespace, resolveAliases);
			string value = this.ValueType.BuildName(includeNamespace, resolveAliases);
			return $"System.Collections.Generic.Dictionary<{key}, {value}>";
		}
	}

	internal class OptionalValueTypeName : TypeName
	{
		public TypeName InnerType { get; set; }
		public OptionalValueTypeName(TypeName innerType)
		{
			this.InnerType = innerType;
		}

		public override TypeSyntax ToTypeSyntax(bool resolveAliases)
		{
			return SyntaxFactory.GenericName(
					SyntaxFactory.Identifier(typeof(OptionalValue<>).Namespace + ".OptionalValue")
				)
				.WithTypeArgumentList(
					SyntaxFactory.TypeArgumentList(
						SyntaxFactory.SingletonSeparatedList(
							this.InnerType.ToTypeSyntax(resolveAliases)
						)
					)
				);
		}

		public override string BuildName(bool includeNamespace, bool resolveAliases)
		{
			return $"{typeof(OptionalValue<>).Namespace}.OptionalValue<{this.InnerType.BuildName(includeNamespace, resolveAliases)}>";
		}
	}

	internal class NullableTypeName : TypeName
	{
		public TypeName InnerType { get; set; }
		public NullableTypeName(TypeName innerType)
		{
			this.InnerType = innerType;
		}

		public override TypeSyntax ToTypeSyntax(bool resolveAliases)
		{
			return SyntaxFactory.NullableType(this.InnerType.ToTypeSyntax(resolveAliases));
		}

		public override string BuildName(bool includeNamespace, bool resolveAliases)
		{
			return this.InnerType.BuildName(includeNamespace, resolveAliases) + "?";
		}
	}

	internal class TupleTypeName : TypeName
	{
		public List<TypeName> ElementTypeNameList { get; }

		public TupleTypeName(List<TypeName> elementTypeNameList)
		{
			this.ElementTypeNameList = elementTypeNameList;
		}
		public override string BuildName(bool includeNamespace, bool resolveAliases)
		{
			string elements = string.Join(", ", this.ElementTypeNameList.Select(e => e.BuildName(includeNamespace, resolveAliases)));
			if (resolveAliases)
			{
				// Alias statements don't like (..,..) syntax
				return $"System.ValueTuple<{elements}>";
			}
			return $"({elements})";
		}

		public override TypeSyntax ToTypeSyntax(bool resolveAliases)
		{
			return SyntaxFactory.TupleType(
				SyntaxFactory.SeparatedList(
					this.ElementTypeNameList
					.Select(e => SyntaxFactory.TupleElement(e.ToTypeSyntax(resolveAliases)))
				)
			);
		}
	}

	internal class ArrayTypeName : TypeName
	{
		public TypeName? ElementTypeName { get; }

		public ArrayTypeName(TypeName? elementTypeName)
		{
			this.ElementTypeName = elementTypeName;
		}

		public override string BuildName(bool includeNamespace, bool resolveAliases)
		{
			if (this.ElementTypeName == null)
			{
				return "System.Array";
			}
			return this.ElementTypeName.BuildName(includeNamespace, resolveAliases) + "[]";
		}

		public override TypeSyntax ToTypeSyntax(bool resolveAliases)
		{
			if (this.ElementTypeName == null)
			{
				return SyntaxFactory.IdentifierName("System.Array");
			}
			return SyntaxFactory.ArrayType(this.ElementTypeName.ToTypeSyntax(resolveAliases))
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
		public abstract string BuildName(bool includeNamespace, bool resolveAliases = false);
		public abstract TypeSyntax ToTypeSyntax(bool resolveAliases = false);


		public override string ToString()
		{
			return this.BuildName(true, false);
		}
	}

}
