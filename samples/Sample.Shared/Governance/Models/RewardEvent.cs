using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class RewardEvent
	{
		[CandidName("day_after_genesis")]
		public ulong DayAfterGenesis { get; set; }

		[CandidName("actual_timestamp_seconds")]
		public ulong ActualTimestampSeconds { get; set; }

		[CandidName("distributed_e8s_equivalent")]
		public ulong DistributedE8sEquivalent { get; set; }

		[CandidName("settled_proposals")]
		public List<NeuronId> SettledProposals { get; set; }

		public RewardEvent(ulong dayAfterGenesis, ulong actualTimestampSeconds, ulong distributedE8sEquivalent, List<NeuronId> settledProposals)
		{
			this.DayAfterGenesis = dayAfterGenesis;
			this.ActualTimestampSeconds = actualTimestampSeconds;
			this.DistributedE8sEquivalent = distributedE8sEquivalent;
			this.SettledProposals = settledProposals;
		}

		public RewardEvent()
		{
		}
	}
}