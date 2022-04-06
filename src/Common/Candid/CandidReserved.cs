namespace ICP.Common.Candid
{
	public class CandidReserved : CandidToken
	{
		public override CandidTokenType Type { get; } = CandidTokenType.Reserved;
	}
}