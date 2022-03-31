namespace Candid
{
	public class CandidEmpty : CandidToken
	{
		public override CandidTokenType Type { get; } = CandidTokenType.Empty;
	}
}