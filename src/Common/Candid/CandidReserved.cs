namespace ICP.Common.Candid
{
	public class CandidReserved : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Reserved;
	}
}