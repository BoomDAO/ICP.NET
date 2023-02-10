using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.ClientGenerator
{
	internal class TypeName
	{
		private string? @namespace { get; }
		private string? prefix { get; } // used for parent classes
		private string name { get; }
		private List<TypeName> genericTypes { get; }
		private string? nameCache { get; set; }
		private string? namespacedNameCache { get; set; }

		public TypeName(string name, string? @namespace, string? prefix, List<TypeName> genericTypes)
		{
			this.name = name;
			this.@namespace = @namespace;
			this.prefix = prefix;
			this.genericTypes = genericTypes ?? new List<TypeName>();
		}
		public TypeName(string name, string? @namespace, string? prefix, params TypeName[] genericTypes) : this(name, @namespace, prefix, genericTypes.ToList())
		{

		}

		public bool HasPrefix => this.prefix != null;

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
				if (this.prefix != null)
				{
					builder.Append(this.prefix);
					builder.Append('.');
				}
			}
			builder.Append(this.prefix);
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

		public override string ToString()
		{
			return this.GetNamespacedName();
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
			return new TypeName(name, type.Namespace, prefix: null); // TODO prefix
		}

	}

}
