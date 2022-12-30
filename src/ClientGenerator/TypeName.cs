using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	public class TypeName
	{
		private string? @namespace { get; }
		private string name { get; }
		private List<TypeName> genericTypes { get; }
		private string? nameCache { get; set; }
		private string? namespacedNameCache { get; set; }

		public TypeName(string name, string? @namespace, List<TypeName> genericTypes)
		{
			this.name = name;
			this.@namespace = @namespace;
			this.genericTypes = genericTypes ?? new List<TypeName>();
		}
		public TypeName(string name, string? @namespace, params TypeName[] genericTypes) : this(name, @namespace, genericTypes.ToList())
		{

		}

		public string GetName()
		{
			return this.GetName(false);
		}

		public string GetNamespacedName()
		{
			return this.GetName(true);
		}

		private string GetName(bool includeNamespaces)
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
			}
			builder.Append(this.name);
			if (this.genericTypes.Any())
			{
				IEnumerable<string> typeNames = this.genericTypes
					.Select(t => t.GetName(includeNamespaces));
				builder.Append($"<{string.Join(", ", typeNames)}>");
			}
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

		public static TypeName Optional(TypeName type)
		{
			return new TypeName(
				"OptionalValue",
				"EdjCase.ICP.Candid.Models",
				type
			);
		}

		public static TypeName FromType<T>()
		{
			return FromType(typeof(T));
		}

		public static TypeName FromType(Type type)
		{
			List<TypeName>? genericTypes = null;
			if (type.IsGenericType)
			{
				genericTypes = type.GetGenericArguments()
					.Select(TypeName.FromType)
					.ToList();
			}
			return new TypeName(type.Name, type.Namespace, genericTypes?.ToArray() ?? new TypeName[0]);
		}

		internal TypeName WithParentType(TypeName parent)
		{
			string name = parent.GetName() + "." + this.GetName();
			return new TypeName(name, parent.@namespace, parent.genericTypes);
		}
	}

}
