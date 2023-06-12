using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;
using TxIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using QueryArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using Map = System.Collections.Generic.List<Sample.Shared.ICRC1Ledger.Models.MapItem>;
using Block = Sample.Shared.ICRC1Ledger.Models.Value;
using QueryBlockArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

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