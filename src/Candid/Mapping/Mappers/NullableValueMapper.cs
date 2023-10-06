using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class NullableValueMapper : ICandidValueMapper
	{
		public ICandidValueMapper InnerValueMapper {  get; }
		public NullableValueMapper(ICandidValueMapper innerValueMapper)
		{
			this.InnerValueMapper = innerValueMapper ?? throw new ArgumentNullException(nameof(innerValueMapper));
		}


		public CandidType? GetMappedCandidType(Type type)
		{
			return this.InnerValueMapper.GetMappedCandidType(type);
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			CandidValue innerValue;
			if (value is CandidOptional o)
			{
				if (o.IsNull())
				{
					return null!;
				}
				innerValue = o.Value;
			}
			else
			{
				innerValue = value;
			}
			return this.InnerValueMapper.Map(innerValue, converter);
		}

		public CandidValue Map(object value, CandidConverter converter)
		{
			if (value == null)
			{
				return CandidValue.Null();
			}
			return this.InnerValueMapper.Map(value, converter);
		}
	}
}
