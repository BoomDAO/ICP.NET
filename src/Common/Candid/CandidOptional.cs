namespace ICP.Common.Candid
{
	public class CandidOptional : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Optional;
		public CandidValue? Value { get; }

		public CandidOptional(CandidValue? value)
		{
			this.Value = value;
		}
	}
}