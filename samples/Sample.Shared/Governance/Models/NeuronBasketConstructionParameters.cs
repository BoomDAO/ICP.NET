using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class NeuronBasketConstructionParameters
	{
		[CandidName("dissolve_delay_interval")]
		public OptionalValue<Duration> DissolveDelayInterval { get; set; }

		[CandidName("count")]
		public OptionalValue<ulong> Count { get; set; }

		public NeuronBasketConstructionParameters(OptionalValue<Duration> dissolveDelayInterval, OptionalValue<ulong> count)
		{
			this.DissolveDelayInterval = dissolveDelayInterval;
			this.Count = count;
		}

		public NeuronBasketConstructionParameters()
		{
		}
	}
}