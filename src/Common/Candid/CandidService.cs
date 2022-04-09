namespace ICP.Common.Candid
{
	public class CandidService : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Service;
		public byte[] PrincipalId { get; set; }

		public CandidService(byte[] principalId)
		{
			this.PrincipalId = principalId ?? throw new ArgumentNullException(nameof(principalId));
		}
	}
}