namespace Sample.Shared.Governance.Models
{
	public class DisburseToNeuron
	{
		public ulong dissolve_delay_seconds { get; set; }
		
		public bool kyc_verified { get; set; }
		
		public ulong amount_e8s { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal? new_controller { get; set; }
		
		public ulong nonce { get; set; }
		
	}
}
