using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class RewardEvent
	{
		[CandidName("rounds_since_last_distribution")]
		public OptionalValue<ulong> RoundsSinceLastDistribution { get; set; }

		[CandidName("day_after_genesis")]
		public ulong DayAfterGenesis { get; set; }

		[CandidName("actual_timestamp_seconds")]
		public ulong ActualTimestampSeconds { get; set; }

		[CandidName("total_available_e8s_equivalent")]
		public ulong TotalAvailableE8sEquivalent { get; set; }

		[CandidName("latest_round_available_e8s_equivalent")]
		public OptionalValue<ulong> LatestRoundAvailableE8sEquivalent { get; set; }

		[CandidName("distributed_e8s_equivalent")]
		public ulong DistributedE8sEquivalent { get; set; }

		[CandidName("settled_proposals")]
		public List<NeuronId> SettledProposals { get; set; }

		public RewardEvent(OptionalValue<ulong> roundsSinceLastDistribution, ulong dayAfterGenesis, ulong actualTimestampSeconds, ulong totalAvailableE8sEquivalent, OptionalValue<ulong> latestRoundAvailableE8sEquivalent, ulong distributedE8sEquivalent, List<NeuronId> settledProposals)
		{
			this.RoundsSinceLastDistribution = roundsSinceLastDistribution;
			this.DayAfterGenesis = dayAfterGenesis;
			this.ActualTimestampSeconds = actualTimestampSeconds;
			this.TotalAvailableE8sEquivalent = totalAvailableE8sEquivalent;
			this.LatestRoundAvailableE8sEquivalent = latestRoundAvailableE8sEquivalent;
			this.DistributedE8sEquivalent = distributedE8sEquivalent;
			this.SettledProposals = settledProposals;
		}

		public RewardEvent()
		{
		}
	}
}