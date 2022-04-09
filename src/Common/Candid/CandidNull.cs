namespace ICP.Common.Candid
{
	public class CandidNull : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Null;
	}
}