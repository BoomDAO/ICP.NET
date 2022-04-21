using System;
using System.Linq;

namespace ICP.Common.Candid
{
	public class CandidOptional : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Optional;
		public CandidValue Value { get; }

		public CandidOptional(CandidValue? value = null)
		{
			this.Value = value ?? CandidPrimitive.Null();
		}

		public override byte[] EncodeValue()
		{
			if(this.Value.Type == CandidValueType.Principal
				&& this.Value.AsPrimitive().ValueType == CandidPrimitiveType.Null)
			{
				return new byte[] { 0 };
			}
			return new byte[] { 1 }
				.Concat(this.Value.EncodeValue())
				.ToArray();
		}
	}
}