namespace Candid
{
	public class CandidVariant : CandidToken
	{
		public override CandidTokenType Type { get; } = CandidTokenType.Variant;

		public uint Tag { get; }
		public CandidToken Value { get; }

		public CandidVariant(uint tag, CandidToken value)
		{
			this.Tag = tag;
			this.Value = value;
		}
	}

}
