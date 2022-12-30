using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.VariantSourceGenerator
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class VariantAttribute : Attribute
	{

	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class VariantOptionAttribute : Attribute
	{
		public string Name { get; }
		public Type? ValueType { get; }
		public VariantOptionAttribute(string name, Type? valueType)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Name must be specified", nameof(name));
			}
			this.Name = name;
			this.ValueType = valueType;
		}
	}
}
