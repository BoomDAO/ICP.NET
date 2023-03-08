using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class EmptyStructMapper<T> : CandidValueMapper<T>
		where T : struct
	{

		public T EmptyValue { get; }

		public Func<CandidValue> CandidValueGetter { get; }

		public EmptyStructMapper(
			CandidType candidType,
			T value,
			Func<CandidValue> candidValueGetter
		) : base(candidType)
		{
			this.EmptyValue = value;
			this.CandidValueGetter = candidValueGetter;
		}

		public override T MapGeneric(CandidValue value, CandidConverter converter)
		{
			return this.EmptyValue;
		}

		public override CandidValue MapGeneric(T obj, CandidConverter converter)
		{
			// TODO implement clone vs this?
			return this.CandidValueGetter();
		}
	}
}