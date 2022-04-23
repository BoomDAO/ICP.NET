using ICP.Common.Encodings;
using System;
using System.Linq;

namespace ICP.Common.Candid
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

		public override byte[] EncodeValue()
		{
			byte[] valueListBytes = this.Values
				.SelectMany(v => v.EncodeValue())
				.ToArray();
			byte[] encodedByteLength = LEB128.EncodeSigned(valueListBytes.Length);
			return encodedByteLength
				.Concat(valueListBytes)
				.ToArray();
		}
	}

}
