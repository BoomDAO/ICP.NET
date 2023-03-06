using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class IncreaseDissolveDelay
	{
		[CandidName("additional_dissolve_delay_seconds")]
		public uint AdditionalDissolveDelaySeconds { get; set; }

		public IncreaseDissolveDelay(uint additionalDissolveDelaySeconds)
		{
			this.AdditionalDissolveDelaySeconds = additionalDissolveDelaySeconds;
		}

		public IncreaseDissolveDelay()
		{
		}
	}
}