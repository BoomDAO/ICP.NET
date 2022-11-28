using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Values
{
	public class CandidVector : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Vector;

		public CandidValue[] Values { get; }

		public CandidVector(CandidValue[] values)
		{
			CandidValueType? valueType = values.FirstOrDefault()?.Type;
			if (valueType != null && values.Any(v => v.Type != valueType))
			{
				throw new ArgumentException("All vector values must be the same type");
			}
			this.Values = values;
		}

		public override byte[] EncodeValue(CandidType type)
		{
			CandidVectorType t = (CandidVectorType)type;
			byte[] valueListBytes = this.Values
				.SelectMany(v => v.EncodeValue(t.InnerType))
				.ToArray();
			byte[] valueCountBytes = LEB128.EncodeSigned(this.Values.Length);
			return valueCountBytes
				.Concat(valueListBytes)
				.ToArray();
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Values.Select(v => v.GetHashCode()));
		}

		public override bool Equals(CandidValue? other)
		{
			if (other is CandidVector v)
			{
				if (this.Values.Length == v.Values.Length)
				{
					return this.Values.SequenceEqual(v.Values);
				}
			}
			return false;
		}

        public override string ToString()
        {
			IEnumerable<string> values = this.Values.Select(v => v.ToString());
			return $"[{string.Join(", ", values)}]";
        }
	}

}
