using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class UpdateNodeProvider
	{
		[CandidName("reward_account")]
		public OptionalValue<AccountIdentifier> RewardAccount { get; set; }

		public UpdateNodeProvider(OptionalValue<AccountIdentifier> rewardAccount)
		{
			this.RewardAccount = rewardAccount;
		}

		public UpdateNodeProvider()
		{
		}
	}
}