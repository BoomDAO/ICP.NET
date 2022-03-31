namespace Candid
{
	public class CandidVector : CandidToken
	{
		public override CandidTokenType Type { get; } = CandidTokenType.Vector;

		public CandidToken[] Values { get; }

		public CandidVector(CandidToken[] values)
		{
			this.Values = values;
		}
	}

}
