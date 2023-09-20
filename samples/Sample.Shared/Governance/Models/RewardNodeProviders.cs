using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class RewardNodeProviders
	{
		[CandidName("use_registry_derived_rewards")]
		public OptionalValue<bool> UseRegistryDerivedRewards { get; set; }

		[CandidName("rewards")]
		public List<RewardNodeProvider> Rewards { get; set; }

		public RewardNodeProviders(OptionalValue<bool> useRegistryDerivedRewards, List<RewardNodeProvider> rewards)
		{
			this.UseRegistryDerivedRewards = useRegistryDerivedRewards;
			this.Rewards = rewards;
		}

		public RewardNodeProviders()
		{
		}
	}
}