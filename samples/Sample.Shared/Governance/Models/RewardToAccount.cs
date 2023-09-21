using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class RewardToAccount
	{
		[CandidName("to_account")]
		public OptionalValue<AccountId> ToAccount { get; set; }

		public RewardToAccount(OptionalValue<AccountId> toAccount)
		{
			this.ToAccount = toAccount;
		}

		public RewardToAccount()
		{
		}
	}
}