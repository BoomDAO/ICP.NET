using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class MostRecentMonthlyNodeProviderRewards
	{
		[CandidName("timestamp")]
		public ulong Timestamp { get; set; }

		[CandidName("rewards")]
		public List<RewardNodeProvider> Rewards { get; set; }

		public MostRecentMonthlyNodeProviderRewards(ulong timestamp, List<RewardNodeProvider> rewards)
		{
			this.Timestamp = timestamp;
			this.Rewards = rewards;
		}

		public MostRecentMonthlyNodeProviderRewards()
		{
		}
	}
}