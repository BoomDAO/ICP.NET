namespace ICP.Common.Candid
{
	public class CandidVariant : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Variant;

		public uint Tag { get; }
		public CandidValue Value { get; }

		public CandidVariant(uint tag, CandidValue value)
		{
			this.Tag = tag;
			this.Value = value;
		}
	}

}
