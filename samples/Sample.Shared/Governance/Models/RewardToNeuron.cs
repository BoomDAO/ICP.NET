using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class RewardToNeuron
	{
		[CandidName("dissolve_delay_seconds")]
		public ulong DissolveDelaySeconds { get; set; }

		public RewardToNeuron(ulong dissolveDelaySeconds)
		{
			this.DissolveDelaySeconds = dissolveDelaySeconds;
		}

		public RewardToNeuron()
		{
		}
	}
}