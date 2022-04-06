namespace Candid
{
	public class CandidService : CandidToken
	{
		public override CandidTokenType Type { get; } = CandidTokenType.Service;
		public byte[] PrincipalId { get; set; }

		public CandidService(byte[] principalId)
		{
			this.PrincipalId = principalId ?? throw new ArgumentNullException(nameof(principalId));
		}
	}
}