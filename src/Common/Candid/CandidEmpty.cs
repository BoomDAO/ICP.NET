namespace ICP.Common.Candid
{
	public class CandidEmpty : CandidToken
	{
		public override CandidTokenType Type { get; } = CandidTokenType.Empty;

		public override byte[] EncodeType()
		{
			return new byte[]
			{
				0b0110_1111 // Empty Type
			};
		}

		public override byte[] EncodeValue()
		{
			throw new InvalidOperationException("Emtpy values cannot appear as a function argument");
		}
	}
}