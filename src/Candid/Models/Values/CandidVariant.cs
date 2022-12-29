using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Encodings;
using System;
using System.Linq;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Exceptions;

namespace EdjCase.ICP.Candid.Models.Values
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

		public override byte[] EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType)
		{
			CandidVariantType t;
			if (type is CandidReferenceType r)
			{
				t = (CandidVariantType)getReferencedType(r.Id);
			}
			else
			{
				t = (CandidVariantType)type;
			}
			int index = t.Options.AsEnumerable()
				.OrderBy(f => f.Key)
				.ToList()
				.FindIndex(f => f.Key == this.Tag);
			if (index < 0)
			{
				throw new CandidSerializationEncodingException($"Variant option '{this.Tag}' was not found in the type");
			}
			// bytes = index (LEB128) + encoded value
			return LEB128.EncodeUnsigned((uint)index)
				.Concat(this.Value.EncodeValue(t.Options[this.Tag], getReferencedType))
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
