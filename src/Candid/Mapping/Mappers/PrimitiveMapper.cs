using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class PrimitiveMapper : ICandidValueMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }

		public Func<object, CandidConverter, CandidValue> ToCandid { get; }
		public Func<CandidValue, CandidConverter, object> FromCandid { get; }

		public PrimitiveMapper(
			CandidType candidType,
			Type type,
			Func<object, CandidConverter, CandidValue> toCandid,
			Func<CandidValue, CandidConverter, object> fromCandid)
		{
			this.CandidType = candidType ?? throw new ArgumentNullException(nameof(candidType));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.ToCandid = toCandid ?? throw new ArgumentNullException(nameof(toCandid));
			this.FromCandid = fromCandid ?? throw new ArgumentNullException(nameof(fromCandid));
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			try
			{
				return this.FromCandid(value, converter);
			}
			catch (InvalidOperationException) when (value is CandidOptional o)
			{
				return this.FromCandid(o.Value, converter);
			}
		}

		public CandidValue Map(object value, CandidConverter converter)
		{
			return this.ToCandid(value, converter);
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
		}
	}
}