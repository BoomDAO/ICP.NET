using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Values
{
	public class CandidOptional : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Optional;
		public CandidValue Value { get; }

		public CandidOptional(CandidValue? value = null)
		{
			this.Value = value ?? CandidPrimitive.Null();
		}

		public override byte[] EncodeValue(CandidType type)
		{
			CandidOptionalType t = (CandidOptionalType)type;
			if(this.Value.Type == CandidValueType.Primitive
				&& this.Value.AsPrimitive().ValueType == PrimitiveType.Null)
			{
				return new byte[] { 0 };
			}
			return new byte[] { 1 }
				.Concat(this.Value.EncodeValue(t.Value))
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
				return this.Value.Equals(o.Value);
			}
			return false;
		}

        public override string ToString()
        {
			return this.Value.ToString();
        }
    }
}