using System;

namespace ICP.Common.Candid
{
	public class CandidReserved : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Reserved;

		public override byte[] EncodeValue()
		{
			// Empty byte sequence
			return new byte[0];
		}
	}
}