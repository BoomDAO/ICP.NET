namespace Sample.Shared.Governance.Models
{
	public class IncreaseDissolveDelay
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("additional_dissolve_delay_seconds")]
		public uint AdditionalDissolveDelaySeconds { get; set; }
		
	}
}

