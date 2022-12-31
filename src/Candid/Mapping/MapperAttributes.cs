using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EdjCase.ICP.Candid.Mapping
{

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class CandidNameAttribute : Attribute
	{
		public string Name { get; }
		public CandidNameAttribute(string name)
		{
			this.Name = name;
		}
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property)]
	public class CustomMapperAttribute : Attribute
	{
		public IObjectMapper Mapper { get; }
		public CustomMapperAttribute(IObjectMapper mapper)
		{
			this.Mapper = mapper ?? throw new NotImplementedException();
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class CandidIgnoreAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class VariantAttribute : Attribute
	{
		public Type TagType { get; }
		public VariantAttribute(Type enumType)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("Type must be an enum");
			}
			this.TagType = enumType;
		}
	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class VariantTagPropertyAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class VariantValuePropertyAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class VariantOptionTypeAttribute : Attribute
	{
		public Type OptionType { get; }
		public VariantOptionTypeAttribute(Type optionType)
		{
			this.OptionType = optionType;
		}
	}
}
