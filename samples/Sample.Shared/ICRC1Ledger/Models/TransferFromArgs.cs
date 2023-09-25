using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.ICRC1Ledger.Models;
using System.Collections.Generic;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class TransferFromArgs
	{
		[CandidName("spender_subaccount")]
		public OptionalValue<Subaccount> SpenderSubaccount { get; set; }

		[CandidName("from")]
		public Account From { get; set; }

		[CandidName("to")]
		public Account To { get; set; }

		[CandidName("amount")]
		public Tokens Amount { get; set; }

		[CandidName("fee")]
		public OptionalValue<Tokens> Fee { get; set; }

		[CandidName("memo")]
		public OptionalValue<List<byte>> Memo { get; set; }

		[CandidName("created_at_time")]
		public OptionalValue<Timestamp> CreatedAtTime { get; set; }

		public TransferFromArgs(OptionalValue<Subaccount> spenderSubaccount, Account from, Account to, Tokens amount, OptionalValue<Tokens> fee, OptionalValue<List<byte>> memo, OptionalValue<Timestamp> createdAtTime)
		{
			this.SpenderSubaccount = spenderSubaccount;
			this.From = from;
			this.To = to;
			this.Amount = amount;
			this.Fee = fee;
			this.Memo = memo;
			this.CreatedAtTime = createdAtTime;
		}

		public TransferFromArgs()
		{
		}
	}
}