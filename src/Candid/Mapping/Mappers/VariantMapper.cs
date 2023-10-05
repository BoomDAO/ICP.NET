using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class VariantMapper : ICandidValueMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }
		public PropertyInfo TypeProperty { get; }
		public PropertyInfo ValueProperty { get; }
		public Dictionary<CandidTag, Option> Options { get; }
		public Dictionary<Enum, CandidTag> EnumMapping { get; }

		public VariantMapper(
			CandidVariantType candidType,
			Type type,
			PropertyInfo typeProperty,
			PropertyInfo valueProperty,
			Dictionary<CandidTag, Option> options)
		{
			this.CandidType = candidType ?? throw new ArgumentNullException(nameof(candidType));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.TypeProperty = typeProperty;
			this.ValueProperty = valueProperty;
			this.Options = options;
			this.EnumMapping = options.ToDictionary(k => k.Value.EnumValue, k => k.Key);
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			CandidVariant variant = value.AsVariant();
			object obj = Activator.CreateInstance(this.Type, nonPublic: true);

			if (!this.Options.TryGetValue(variant.Tag, out Option optionInfo))
			{
				throw new Exception($"Could not map candid variant to type '{this.Type}'. Type is missing option '{variant.Tag}'");
			}

			object? variantValue;
			if (optionInfo.Type != null)
			{
				variantValue = converter.ToObject(optionInfo.Type, variant.Value);
			}
			else
			{
				variantValue = null;
			}
			this.TypeProperty.SetValue(obj, optionInfo.EnumValue);
			this.ValueProperty.SetValue(obj, variantValue);
			return obj;
		}

		public CandidValue Map(object obj, CandidConverter converter)
		{
			Enum enumValue = (Enum)this.TypeProperty.GetValue(obj);
			CandidTag innerTag = this.EnumMapping[enumValue];
			object? innerObj = this.ValueProperty.GetValue(obj);

			if (!this.Options.TryGetValue(innerTag, out Option optionInfo))
			{
				throw new Exception($"Could not map type '{this.Type}' to a candid variant. Variant is missing option '{innerTag}'");
			}


			CandidValue innerValue;
			if (innerObj == null)
			{
				innerValue = new CandidOptional(null);
			}
			else if (optionInfo.Type != null)
			{
				innerValue = converter.FromObject(innerObj);
			}
			else
			{
				innerValue = CandidValue.Null();
			}
			return new CandidVariant(innerTag, innerValue);
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
		}

		public class Option
		{
			public Enum EnumValue { get; }
			public Type? Type { get; }

			public Option(Enum enumValue, Type? type)
			{
				this.EnumValue = enumValue ?? throw new ArgumentNullException(nameof(enumValue));
				this.Type = type;
			}
		}
	}
}
