using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class RewardToAccount
	{
		[CandidName("to_account")]
		public OptionalValue<AccountIdentifier> ToAccount { get; set; }

		public RewardToAccount(OptionalValue<AccountIdentifier> toAccount)
		{
			this.ToAccount = toAccount;
		}

		public RewardToAccount()
		{
		}
	}
}