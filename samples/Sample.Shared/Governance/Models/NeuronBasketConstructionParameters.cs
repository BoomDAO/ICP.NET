using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class NeuronBasketConstructionParameters
	{
		[CandidName("dissolve_delay_interval_seconds")]
		public ulong DissolveDelayIntervalSeconds { get; set; }

		[CandidName("count")]
		public ulong Count { get; set; }

		public NeuronBasketConstructionParameters(ulong dissolveDelayIntervalSeconds, ulong count)
		{
			this.DissolveDelayIntervalSeconds = dissolveDelayIntervalSeconds;
			this.Count = count;
		}

		public NeuronBasketConstructionParameters()
		{
		}
	}
}