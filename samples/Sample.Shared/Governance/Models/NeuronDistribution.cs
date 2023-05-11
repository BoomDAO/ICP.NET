using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class NeuronDistribution
	{
		[CandidName("controller")]
		public OptionalValue<Principal> Controller { get; set; }

		[CandidName("dissolve_delay")]
		public OptionalValue<Duration> DissolveDelay { get; set; }

		[CandidName("memo")]
		public OptionalValue<ulong> Memo { get; set; }

		[CandidName("vesting_period")]
		public OptionalValue<Duration> VestingPeriod { get; set; }

		[CandidName("stake")]
		public OptionalValue<Tokens> Stake { get; set; }

		public NeuronDistribution(OptionalValue<Principal> controller, OptionalValue<Duration> dissolveDelay, OptionalValue<ulong> memo, OptionalValue<Duration> vestingPeriod, OptionalValue<Tokens> stake)
		{
			this.Controller = controller;
			this.DissolveDelay = dissolveDelay;
			this.Memo = memo;
			this.VestingPeriod = vestingPeriod;
			this.Stake = stake;
		}

		public NeuronDistribution()
		{
		}
	}
}