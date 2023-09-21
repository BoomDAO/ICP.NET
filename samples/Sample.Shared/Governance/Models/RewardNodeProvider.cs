using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class RewardNodeProvider
	{
		[CandidName("node_provider")]
		public OptionalValue<NodeProvider> NodeProvider { get; set; }

		[CandidName("reward_mode")]
		public OptionalValue<RewardMode> RewardMode { get; set; }

		[CandidName("amount_e8s")]
		public ulong AmountE8s { get; set; }

		public RewardNodeProvider(OptionalValue<NodeProvider> nodeProvider, OptionalValue<RewardMode> rewardMode, ulong amountE8s)
		{
			this.NodeProvider = nodeProvider;
			this.RewardMode = rewardMode;
			this.AmountE8s = amountE8s;
		}

		public RewardNodeProvider()
		{
		}
	}
}