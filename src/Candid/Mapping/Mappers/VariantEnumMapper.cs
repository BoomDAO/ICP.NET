using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{

	internal class VariantEnumMapper : ICandidValueMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }
		public Dictionary<CandidTag, Enum> TagToEnum { get; }
		public Dictionary<Enum, CandidTag> EnumToTag { get; }

		public VariantEnumMapper(
			CandidVariantType candidType,
			Type type,
			Dictionary<CandidTag, Enum> options)
		{
			this.CandidType = candidType ?? throw new ArgumentNullException(nameof(candidType));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.TagToEnum = options ?? throw new ArgumentNullException(nameof(options));
			this.EnumToTag = options.ToDictionary(o => o.Value, o => o.Key);
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			CandidVariant variant = value.AsVariant();
			return this.TagToEnum[variant.Tag];
		}

		public CandidValue Map(object value, CandidConverter converter)
		{
			Enum enumValue = (Enum)value;
			CandidTag tag = this.EnumToTag[enumValue];

			return new CandidVariant(tag, null); // Variant enums never have a value
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
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
			public ICandidValueMapper? OverrideMapper { get; }

			public ValueInfo(Type type, ICandidValueMapper? overrideMapper)
			{
				this.Type = type ?? throw new ArgumentNullException(nameof(type));
				this.OverrideMapper = overrideMapper;
			}
		}
	}
}
