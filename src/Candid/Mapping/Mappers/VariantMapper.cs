using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{

	internal class VariantMapper : IObjectMapper
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

		public object Map(CandidValue value, CandidConverterOptions options)
		{
			CandidVariant variant = value.AsVariant();
			object obj = Activator.CreateInstance(this.Type, nonPublic: true);
			Option optionInfo = this.Options[variant.Tag];
			IObjectMapper? optionMapper = optionInfo.ValueInfo?.OverrideMapper ?? (optionInfo.ValueInfo == null ? null : options.ResolveMapper(optionInfo.ValueInfo.Type));

			object? variantValue;
			if (optionMapper != null)
			{
				variantValue = optionMapper.Map(variant.Value, options);
			}
			else
			{
				variantValue = null;
			}
			this.TypeProperty.SetValue(obj, optionInfo.EnumValue);
			this.ValueProperty.SetValue(obj, variantValue);
			return obj;
		}

		public CandidTypedValue Map(object obj, CandidConverterOptions options)
		{
			Enum enumValue = (Enum)this.TypeProperty.GetValue(obj);
			CandidTag innerTag = this.EnumMapping[enumValue];
			object? innerObj = this.ValueProperty.GetValue(obj);
			Option optionInfo = this.Options[innerTag];

			IObjectMapper? optionMapper = optionInfo.ValueInfo?.OverrideMapper ?? (optionInfo.ValueInfo == null ? null : options.ResolveMapper(optionInfo.ValueInfo.Type));
			CandidValue innerValue;
			if (optionMapper != null)
			{
				CandidTypedValue t = optionMapper.Map(innerObj!, options);
				innerValue = t.Value;
			}
			else
			{
				innerValue = CandidPrimitive.Null();
			}
			return new CandidTypedValue(new CandidVariant(innerTag, innerValue), this.CandidType);
		}

		public class Option
		{
			public Enum EnumValue { get; }
			public ValueInfo? ValueInfo { get; }

			public Option(Enum enumValue, ValueInfo? valueInfo)
			{
				this.EnumValue = enumValue ?? throw new ArgumentNullException(nameof(enumValue));
				this.ValueInfo = valueInfo;
			}
		}

		public class ValueInfo
		{
			public Type Type { get; }
			public IObjectMapper? OverrideMapper { get; }

			public ValueInfo(Type type, IObjectMapper? overrideMapper)
			{
				this.Type = type ?? throw new ArgumentNullException(nameof(type));
				this.OverrideMapper = overrideMapper;
			}
		}
	}
}
