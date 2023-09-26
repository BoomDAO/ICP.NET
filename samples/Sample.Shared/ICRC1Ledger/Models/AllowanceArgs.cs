using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class AllowanceArgs
	{
		[CandidName("account")]
		public Account Account { get; set; }

		[CandidName("spender")]
		public Account Spender { get; set; }

		public AllowanceArgs(Account account, Account spender)
		{
			this.Account = account;
			this.Spender = spender;
		}

		public AllowanceArgs()
		{
		}
	}
}