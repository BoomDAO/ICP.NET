namespace ICP.Common.Candid
{
	public class CandidOptional : CandidToken
	{
		public override CandidTokenType Type { get; } = CandidTokenType.Optional;
		public CandidToken? Value { get; }

		public CandidOptional(CandidToken? value)
		{
			this.Value = value;
		}
	}
}