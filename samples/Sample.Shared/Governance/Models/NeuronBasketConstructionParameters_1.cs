using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class NeuronBasketConstructionParameters_1
	{
		[CandidName("dissolve_delay_interval_seconds")]
		public ulong DissolveDelayIntervalSeconds { get; set; }

		[CandidName("count")]
		public ulong Count { get; set; }

		public NeuronBasketConstructionParameters_1(ulong dissolveDelayIntervalSeconds, ulong count)
		{
			this.DissolveDelayIntervalSeconds = dissolveDelayIntervalSeconds;
			this.Count = count;
		}

		public NeuronBasketConstructionParameters_1()
		{
		}
	}
}