using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class Disburse
	{
		[CandidName("to_account")]
		public OptionalValue<AccountIdentifier> ToAccount { get; set; }

		[CandidName("amount")]
		public OptionalValue<Amount> Amount { get; set; }

		public Disburse(OptionalValue<AccountIdentifier> toAccount, OptionalValue<Amount> amount)
		{
			this.ToAccount = toAccount;
			this.Amount = amount;
		}

		public Disburse()
		{
		}
	}
}