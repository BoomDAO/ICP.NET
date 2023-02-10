using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class EmptyStructMapper<T> : IObjectMapper
		where T : struct
	{
		public CandidType CandidType { get; }
		public Type Type { get; }

		public T Value { get; }

		public Func<CandidValue> CandidValueGetter { get; }

		public EmptyStructMapper(
			CandidType candidType,
			T value,
			Func<CandidValue> candidValueGetter
		)
		{
			this.CandidType = candidType ?? throw new ArgumentNullException(nameof(candidType));
			this.Type = typeof(T);
			this.Value = value;
			this.CandidValueGetter = candidValueGetter;
		}

		public object Map(CandidValue value, CandidConverterOptions options)
		{
			return this.Value;
		}

		public CandidTypedValue Map(object obj, CandidConverterOptions options)
		{
			// TODO implement clone vs this?
			CandidValue value = this.CandidValueGetter();
			return CandidTypedValue.FromValueAndType(value, this.CandidType);
		}
	}
}