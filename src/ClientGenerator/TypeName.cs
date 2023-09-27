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
		public override bool IsDefaultNullable => this.NotAliased.IsDefaultNullable;
		public TypeName Aliased { get; set; }
		public TypeName NotAliased { get; set; }

		public AliasedTypeName(TypeName aliased, TypeName notAliased)
		{
			this.Aliased = aliased ?? throw new ArgumentNullException(nameof(aliased));
			this.NotAliased = notAliased ?? throw new ArgumentNullException(nameof(notAliased));
		}
		public override string BuildName(bool featureNullable, bool useOptionalValue, bool includeNamespace, bool resolveAliases)
		{
			if (resolveAliases)
			{
				return this.NotAliased.BuildName(featureNullable, useOptionalValue, includeNamespace, true);
			}
			return this.Aliased.BuildName(featureNullable, useOptionalValue, includeNamespace, false);
		}

		public override TypeSyntax ToTypeSyntax(bool featureNullable, bool useOptionalValue, bool resolveAliases)
		{
			if (resolveAliases)
			{
				return this.NotAliased.ToTypeSyntax(featureNullable, useOptionalValue, false);
			}
			return this.Aliased.ToTypeSyntax(featureNullable, useOptionalValue, false);
		}
	}
	internal class NestedTypeName : TypeName
	{
		public override bool IsDefaultNullable { get; } = true;
		public TypeName ParentType { get; }
		public string Name { get; }
		public NestedTypeName(TypeName parentType, string name)
		{
			this.ParentType = parentType ?? throw new ArgumentNullException(nameof(parentType));
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public override string BuildName(bool featureNullable, bool useOptionalValue, bool includeNamespace, bool resolveAliases)
		{
			if (!includeNamespace)
			{
				return this.Name;
			}
			return this.ParentType.BuildName(featureNullable, useOptionalValue, true, resolveAliases) + "." + this.Name;
		}

		public override TypeSyntax ToTypeSyntax(bool featureNullable, bool useOptionalValue, bool resolveAliases)
		{
			string typeName = this.BuildName(featureNullable, useOptionalValue, includeNamespace: true, resolveAliases: resolveAliases);
			return SyntaxFactory.IdentifierName(typeName);
		}
	}

	internal class SimpleTypeName : TypeName
	{
		public override bool IsDefaultNullable { get; }
		public string Name { get; }
		public string? Namespace { get; }

		public SimpleTypeName(string name, string? @namespace, bool isDefaultNullable)
		{
			this.Name = name;
			this.Namespace = @namespace;
			this.IsDefaultNullable = isDefaultNullable;
		}

		public override string BuildName(bool featureNullable, bool useOptionalValue, bool includeNamespace, bool resolveAliases)
		{
			if (!includeNamespace || this.Namespace == null)
			{
				return this.Name;
			}
			return this.Namespace + "." + this.Name;
		}

		public override TypeSyntax ToTypeSyntax(bool featureNullable, bool useOptionalValue, bool resolveAliases)
		{
			string typeName = this.BuildName(featureNullable, useOptionalValue, includeNamespace: true, resolveAliases: resolveAliases);
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
				typeName = new SimpleTypeName(type.Name, type.Namespace, type.IsClass);
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
		public override bool IsDefaultNullable { get; } = true;
		public TypeName InnerType { get; set; }
		public ListTypeName(TypeName innerType)
		{
			this.InnerType = innerType;
		}

		public override TypeSyntax ToTypeSyntax(bool featureNullable, bool useOptionalValue, bool resolveAliases)
		{
			return SyntaxFactory.GenericName(
					SyntaxFactory.Identifier("System.Collections.Generic.List")
				)
				.WithTypeArgumentList(
					SyntaxFactory.TypeArgumentList(
						SyntaxFactory.SingletonSeparatedList(
							this.InnerType.ToTypeSyntax(featureNullable, useOptionalValue, resolveAliases)
						)
					)
				);
		}

		public override string BuildName(bool featureNullable, bool useOptionalValue, bool includeNamespace, bool resolveAliases)
		{
			return $"System.Collections.Generic.List<{this.InnerType.BuildName(featureNullable, useOptionalValue, includeNamespace, resolveAliases)}>";
		}
	}

	internal class DictionaryTypeName : TypeName
	{
		public override bool IsDefaultNullable { get; } = true;
		public TypeName KeyType { get; set; }
		public TypeName ValueType { get; set; }
		public DictionaryTypeName(TypeName keyType, TypeName valueType)
		{
			this.KeyType = keyType;
			this.ValueType = valueType;
		}

		public override TypeSyntax ToTypeSyntax(bool featureNullable, bool useOptionalValue, bool resolveAliases)
		{
			return SyntaxFactory.GenericName(
					SyntaxFactory.Identifier("System.Collections.Generic.Dictionary")
				)
				.WithTypeArgumentList(
					SyntaxFactory.TypeArgumentList(
						SyntaxFactory.SeparatedList(
							new[] {
								this.KeyType.ToTypeSyntax(featureNullable, useOptionalValue, resolveAliases),
								this.ValueType.ToTypeSyntax(featureNullable, useOptionalValue, resolveAliases)
							}
						)
					)
				);
		}

		public override string BuildName(bool featureNullable, bool useOptionalValue, bool includeNamespace, bool resolveAliases)
		{
			string key = this.KeyType.BuildName(featureNullable, useOptionalValue, includeNamespace, resolveAliases);
			string value = this.ValueType.BuildName(featureNullable, useOptionalValue, includeNamespace, resolveAliases);
			return $"System.Collections.Generic.Dictionary<{key}, {value}>";
		}
	}

	internal class OptionalValueTypeName : TypeName
	{
		public override bool IsDefaultNullable { get; } = true;
		public TypeName InnerType { get; set; }
		public OptionalValueTypeName(TypeName innerType)
		{
			this.InnerType = innerType;
		}

		public override TypeSyntax ToTypeSyntax(bool featureNullable, bool useOptionalValue, bool resolveAliases)
		{
			if (!useOptionalValue)
			{
				return new NullableTypeName(this.InnerType).ToTypeSyntax(featureNullable, useOptionalValue, resolveAliases);
			}
			return SyntaxFactory.GenericName(
					SyntaxFactory.Identifier(typeof(OptionalValue<>).Namespace + ".OptionalValue")
				)
				.WithTypeArgumentList(
					SyntaxFactory.TypeArgumentList(
						SyntaxFactory.SingletonSeparatedList(
							this.InnerType.ToTypeSyntax(featureNullable, useOptionalValue, resolveAliases)
						)
					)
				);
		}

		public override string BuildName(bool featureNullable, bool useOptionalValue, bool includeNamespace, bool resolveAliases)
		{
			if (!useOptionalValue)
			{
				return new NullableTypeName(this.InnerType).BuildName(featureNullable, useOptionalValue, includeNamespace, resolveAliases);
			}
			return $"{typeof(OptionalValue<>).Namespace}.OptionalValue<{this.InnerType.BuildName(featureNullable, useOptionalValue, includeNamespace, resolveAliases)}>";
		}
	}

	internal class NullableTypeName : TypeName
	{
		public override bool IsDefaultNullable => true;
		public TypeName InnerType { get; set; }
		public NullableTypeName(TypeName innerType)
		{
			this.InnerType = innerType;
		}

		public override TypeSyntax ToTypeSyntax(bool featureNullable, bool useOptionalValue, bool resolveAliases)
		{
			TypeSyntax inner = this.InnerType.ToTypeSyntax(featureNullable, useOptionalValue, resolveAliases);
			if (!featureNullable && this.InnerType.IsDefaultNullable)
			{
				// Dont wrap in nullable
				return inner;
			}
			return SyntaxFactory.NullableType(this.InnerType.ToTypeSyntax(featureNullable, useOptionalValue, resolveAliases));
		}

		public override string BuildName(bool featureNullable, bool useOptionalValue, bool includeNamespace, bool resolveAliases)
		{
			string innerName = this.InnerType.BuildName(featureNullable, useOptionalValue, includeNamespace, resolveAliases);
			return featureNullable  ? innerName + "?" : innerName;
		}
	}

	internal class TupleTypeName : TypeName
	{
		public override bool IsDefaultNullable { get; } = false;
		public List<TypeName> ElementTypeNameList { get; }

		public TupleTypeName(List<TypeName> elementTypeNameList)
		{
			this.ElementTypeNameList = elementTypeNameList;
		}
		public override string BuildName(bool featureNullable, bool useOptionalValue, bool includeNamespace, bool resolveAliases)
		{
			string elements = string.Join(", ", this.ElementTypeNameList.Select(e => e.BuildName(featureNullable, useOptionalValue, includeNamespace, resolveAliases)));
			if (resolveAliases)
			{
				// Alias statements don't like (..,..) syntax
				return $"System.ValueTuple<{elements}>";
			}
			return $"({elements})";
		}

		public override TypeSyntax ToTypeSyntax(bool featureNullable, bool useOptionalValue, bool resolveAliases)
		{
			return SyntaxFactory.TupleType(
				SyntaxFactory.SeparatedList(
					this.ElementTypeNameList
					.Select(e => SyntaxFactory.TupleElement(e.ToTypeSyntax(featureNullable, useOptionalValue, resolveAliases)))
				)
			);
		}
	}

	internal class ArrayTypeName : TypeName
	{
		public override bool IsDefaultNullable { get; } = true;
		public TypeName? ElementTypeName { get; }

		public ArrayTypeName(TypeName? elementTypeName)
		{
			this.ElementTypeName = elementTypeName;
		}

		public override string BuildName(bool featureNullable, bool useOptionalValue, bool includeNamespace, bool resolveAliases)
		{
			if (this.ElementTypeName == null)
			{
				return "System.Array";
			}
			return this.ElementTypeName.BuildName(featureNullable, useOptionalValue, includeNamespace, resolveAliases) + "[]";
		}

		public override TypeSyntax ToTypeSyntax(bool featureNullable, bool useOptionalValue, bool resolveAliases)
		{
			if (this.ElementTypeName == null)
			{
				return SyntaxFactory.IdentifierName("System.Array");
			}
			return SyntaxFactory.ArrayType(this.ElementTypeName.ToTypeSyntax(featureNullable, useOptionalValue, resolveAliases))
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
		public abstract string BuildName(bool featureNullable, bool useOptionalValue, bool includeNamespace, bool resolveAliases = false);
		public abstract TypeSyntax ToTypeSyntax(bool featureNullable, bool useOptionalValue, bool resolveAliases = false);
		public abstract bool IsDefaultNullable { get; }

		public override string ToString()
		{
			return this.BuildName(false, true, true, false);
		}
	}

}
