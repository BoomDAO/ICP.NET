namespace Sample.Shared.Governance.Models
{
	public class RewardToNeuron
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolve_delay_seconds")]
		public ulong DissolveDelaySeconds { get; set; }
		
	}
}

