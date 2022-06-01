namespace Sample.Shared.Governance.Models
{
	public class DisburseToNeuron
	{
		public ulong DissolveDelaySeconds { get; set; }
		
		public bool KycVerified { get; set; }
		
		public ulong AmountE8s { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal? NewController { get; set; }
		
		public ulong Nonce { get; set; }
		
	}
}
