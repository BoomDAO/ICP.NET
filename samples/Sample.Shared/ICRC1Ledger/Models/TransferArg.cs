using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;
using TxIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using QueryArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using Map = System.Collections.Generic.List<(System.String, Sample.Shared.ICRC1Ledger.Models.Value)>;
using Block = Sample.Shared.ICRC1Ledger.Models.Value;
using QueryBlockArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.ICRC1Ledger.Models;
using System.Collections.Generic;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class TransferArg
	{
		[CandidName("from_subaccount")]
		public OptionalValue<Subaccount> FromSubaccount { get; set; }

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

		public TransferArg(OptionalValue<Subaccount> fromSubaccount, Account to, Tokens amount, OptionalValue<Tokens> fee, OptionalValue<List<byte>> memo, OptionalValue<Timestamp> createdAtTime)
		{
			this.FromSubaccount = fromSubaccount;
			this.To = to;
			this.Amount = amount;
			this.Fee = fee;
			this.Memo = memo;
			this.CreatedAtTime = createdAtTime;
		}

		public TransferArg()
		{
		}
	}
}