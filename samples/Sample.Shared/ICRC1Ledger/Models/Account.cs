using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Subaccount = System.Collections.Generic.List<System.Byte>;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class Account
	{
		[CandidName("owner")]
		public Principal Owner { get; set; }

		[CandidName("subaccount")]
		public OptionalValue<Subaccount> Subaccount { get; set; }

		public Account(Principal owner, OptionalValue<Subaccount> subaccount)
		{
			this.Owner = owner;
			this.Subaccount = subaccount;
		}

		public Account()
		{
		}
	}
}