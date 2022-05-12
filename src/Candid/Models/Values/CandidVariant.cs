using ICP.Candid.Models;
using ICP.Candid.Encodings;
using System;
using System.Linq;
using ICP.Candid.Models.Types;

namespace ICP.Candid.Models.Values
{
	public class CandidVariant : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Variant;

		public CandidTag Tag { get; }
		public CandidValue Value { get; }

		public CandidVariant(CandidTag tag, CandidValue? value = null)
		{
			this.Tag = tag;
			this.Value = value ?? CandidPrimitive.Null();
		}

		public override byte[] EncodeValue()
		{
			// bytes = index (LEB128) + encoded value
			return LEB128.EncodeUnsigned(this.Tag.Id)
				.Concat(this.Value.EncodeValue())
				.ToArray();
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Tag.Id, this.Value);
		}

		public override bool Equals(CandidValue? other)
		{
			if (other is CandidVariant v)
			{
				return this.Tag.Id == v.Tag.Id && this.Value.Equals(v.Value);
			}
			return false;
		}

        public override string ToString()
        {
			return $"{{{this.Tag}:{this.Value}}}";
        }
    }

}
