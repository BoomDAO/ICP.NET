using ICP.Common.Encodings;
using System.Linq;

namespace ICP.Common.Candid
{
	public class CandidVariant : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Variant;

		public UnboundedUInt Index { get; }
		public CandidValue Value { get; }

		public CandidVariant(UnboundedUInt tag, CandidValue value)
		{
			this.Index = tag;
			this.Value = value;
		}

		public override byte[] EncodeValue()
		{
			// bytes = index (LEB128) + encoded value
			return LEB128.EncodeUnsigned(this.Index)
				.Concat(this.Value.EncodeValue())
				.ToArray();
		}
	}

}
