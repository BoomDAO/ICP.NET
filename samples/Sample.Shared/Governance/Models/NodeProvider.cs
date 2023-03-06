using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class NodeProvider
	{
		[CandidName("id")]
		public OptionalValue<Principal> Id { get; set; }

		[CandidName("reward_account")]
		public OptionalValue<AccountIdentifier> RewardAccount { get; set; }

		public NodeProvider(OptionalValue<Principal> id, OptionalValue<AccountIdentifier> rewardAccount)
		{
			this.Id = id;
			this.RewardAccount = rewardAccount;
		}

		public NodeProvider()
		{
		}
	}
}