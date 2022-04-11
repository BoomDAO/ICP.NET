namespace ICP.Common.Candid
{
	public class CandidNull : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Null;

		public override byte[] EncodeValue()
		{
			// Empty byte sequence
			return new byte[0];
		}
	}
}