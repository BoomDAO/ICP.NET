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
			if(this.Value.Type == CandidValueType.Primitive
				&& this.Value.AsPrimitive().ValueType == CandidPrimitiveType.Null)
			{
				return new byte[] { 0 };
			}
			return new byte[] { 1 }
				.Concat(this.Value.EncodeValue())
				.ToArray();
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Value);
		}

		public override bool Equals(CandidValue? other)
		{
			if (other is CandidOptional o)
			{
				return this.Value == o.Value;
			}
			return false;
		}
	}
}