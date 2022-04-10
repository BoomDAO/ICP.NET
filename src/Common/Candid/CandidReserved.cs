using System;

namespace ICP.Common.Candid
{
	public class CandidReserved : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Reserved;

		public override byte[] EncodeValue()
		{
			throw new InvalidOperationException("Reserved values cannot be encoded");
		}
	}
}