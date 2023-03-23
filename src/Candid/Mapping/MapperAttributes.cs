using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Candid.Mapping
{
	/// <summary>
	/// An attribute to specify a candid name to use for serialization. If unspecified 
	/// the serializers will use the property names
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class CandidNameAttribute : CandidTagAttribute
	{

		/// <param name="name">The name to use for serialization of candid values</param>
		public CandidNameAttribute(string name) : base(name)
		{
		}
	}
	/// <summary>
	/// An attribute to specify a candid tag to use for serialization. If unspecified 
	/// the serializers will use the property names
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class CandidTagAttribute : Attribute
	{
		/// <summary>
		/// The tag to use for serialization of candid values
		/// </summary>
		public CandidTag Tag { get; }

		/// <param name="tag">The tag to use for serialization of candid values</param>
		public CandidTagAttribute(CandidTag tag)
		{
			this.Tag = tag;
		}

		/// <param name="id">The tag id (name hash) to use for serialization of candid values</param>
		public CandidTagAttribute(uint id)
		{
			this.Tag = id;
		}

		/// <param name="name">The tag name to use for serialization of candid values</param>
		public CandidTagAttribute(string name)
		{
			this.Tag = name;
		}
	}

	/// <summary>
	/// An attribute that specifies a custom mapper for the class, struct, property or field
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field)]
	public class CustomMapperAttribute : Attribute
	{
		/// <summary>
		/// The object mapper to use for the decorated item
		/// </summary>
		public ICandidValueMapper Mapper { get; }

		/// <param name="mapper">The object mapper to use for the decorated item</param>
		public CustomMapperAttribute(ICandidValueMapper mapper)
		{
			this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
	}

	/// <summary>
	/// An attribute to ignore a property/field of a class during serialization
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class CandidIgnoreAttribute : Attribute
	{
	}

	/// <summary>
	/// An attribute to put on a class to identify it as a variant type for serialization.
	/// Requires the use of `VariantTagPropertyAttribute`, `VariantOptionTypeAttribute` and
	/// `VariantValuePropertyAttribute` attributes if used
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class VariantAttribute : Attribute
	{
		/// <summary>
		/// The enum type to use for specifying the tags of the variant
		/// </summary>
		public Type TagType { get; }

		/// <param name="enumType">The enum type to use for specifying the tags of the variant</param>
		/// <exception cref="ArgumentException">Throws if the type is not an enum</exception>
		public VariantAttribute(Type enumType)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("Type must be an enum");
			}
			this.TagType = enumType;
		}
	}

	/// <summary>
	/// An attribute to put on a property/field that indicates where to hold the 
	/// tag enum value. Must match the type passed to the `VariantAttribute`
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class VariantTagPropertyAttribute : Attribute
	{
	}

	/// <summary>
	/// An attribute to put on a property/field that indicates where to hold the
	/// tag value object. The type must be compatible with all value types, recommend using `object?`
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class VariantValuePropertyAttribute : Attribute
	{
	}

	/// <summary>
	/// An attribute to put on an enum option to specify if the tag has an attached
	/// value in the variant, otherwise the attached type will be null
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class VariantOptionTypeAttribute : Attribute
	{
		/// <summary>
		/// The type of the variant option value to use
		/// </summary>
		public Type OptionType { get; }

		/// <param name="optionType">The type of the variant option value to use</param>
		public VariantOptionTypeAttribute(Type optionType)
		{
			this.OptionType = optionType;
		}
	}
}
