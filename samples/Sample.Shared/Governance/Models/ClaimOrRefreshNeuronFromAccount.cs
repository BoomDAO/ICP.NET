namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshNeuronFromAccount
	{
		public EdjCase.ICP.Candid.Models.Principal? Controller { get; set; }
		
		public ulong Memo { get; set; }
		
	}
}
