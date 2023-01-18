using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class PrimitiveMapper : IObjectMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }

		public Func<object, CandidConverterOptions, CandidTypedValue> ToCandid { get; }
		public Func<CandidValue, CandidConverterOptions, object> FromCandid { get; }

		public PrimitiveMapper(
			CandidType candidType,
			Type type,
			Func<object, CandidConverterOptions, CandidTypedValue> toCandid,
			Func<CandidValue, CandidConverterOptions, object> fromCandid)
		{
			this.CandidType = candidType ?? throw new ArgumentNullException(nameof(candidType));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.ToCandid = toCandid ?? throw new ArgumentNullException(nameof(toCandid));
			this.FromCandid = fromCandid ?? throw new ArgumentNullException(nameof(fromCandid));
		}

		public object Map(CandidValue value, CandidConverterOptions options)
		{
			return this.FromCandid(value, options);
		}

		public CandidTypedValue Map(object obj, CandidConverterOptions options)
		{
			return this.ToCandid(obj, options);
		}
	}
}