using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{

	internal class VariantMapper : IObjectMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }
		public Dictionary<CandidTag, (Type Type, IObjectMapper? OverrideMapper)?> Options { get; }

		public VariantMapper(CandidVariantType candidType, Type type, Dictionary<CandidTag, (Type Type, IObjectMapper? OverrideMapper)?> options)
		{
			this.CandidType = candidType ?? throw new ArgumentNullException(nameof(candidType));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.Options = options;
		}

		public object Map(CandidValue value, CandidConverterOptions options)
		{
			CandidVariant variant = value.AsVariant();
			ICandidVariantValue obj = (ICandidVariantValue)Activator.CreateInstance(this.Type, nonPublic: true);
			(Type Type, IObjectMapper? Mapper)? optionInfo = this.Options[variant.Tag];
			IObjectMapper? optionMapper = optionInfo?.Mapper ?? (optionInfo == null ? null : options.ResolveMapper(optionInfo.Value.Type));

			object? variantValue;
			if (optionMapper != null)
			{
				variantValue = optionMapper.Map(variant.Value, options);
			}
			else
			{
				variantValue = null;
			}
			obj.SetValue(variant.Tag, variantValue);
			return obj;
		}

		public CandidTypedValue Map(object obj, CandidConverterOptions options)
		{
			ICandidVariantValue v = (ICandidVariantValue)obj;
			(CandidTag innerTag, object? innerObj) = v.GetValue();
			(Type Type, IObjectMapper? Mapper)? optionInfo = this.Options[innerTag];

			IObjectMapper? optionMapper = optionInfo?.Mapper ?? (optionInfo == null ? null : options.ResolveMapper(optionInfo.Value.Type));
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
	}

}
