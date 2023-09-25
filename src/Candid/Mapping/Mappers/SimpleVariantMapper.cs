using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class SimpleVariantMapper : ICandidValueMapper
	{
		public CandidType CandidType { get; }
		public Dictionary<CandidTag, Option> Options { get; }

		public SimpleVariantMapper(
			CandidVariantType candidType,
			Dictionary<CandidTag, Option> options
		)
		{
			this.CandidType = candidType ?? throw new ArgumentNullException(nameof(candidType));
			this.Options = options;
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			CandidVariant variant = value.AsVariant();

			if (!this.Options.TryGetValue(variant.Tag, out Option optionInfo))
			{
				throw new Exception($"Could not map candid variant. Type is missing option '{variant.Tag}'");
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
			return new SimpleVariant(optionInfo.Name, variantValue);
		}

		public CandidValue Map(object obj, CandidConverter converter)
		{
			SimpleVariant variant = (SimpleVariant)obj;

			if (!this.Options.TryGetValue(variant.Tag, out Option optionInfo))
			{
				throw new Exception($"Could not map a candid variant. Variant is missing option '{variant.Tag}'");
			}


			CandidValue innerValue;
			if (optionInfo.Type != null)
			{
				innerValue = converter.FromObject(variant.Value!);
			}
			else
			{
				innerValue = CandidValue.Null();
			}
			return new CandidVariant(variant.Tag, innerValue);
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
		}

		public class Option
		{
			public string Name { get; }
			public Type? Type { get; }

			public Option(string name, Type? type)
			{
				this.Name = name ?? throw new ArgumentNullException(nameof(name));
				this.Type = type;
			}
		}
	}
}
