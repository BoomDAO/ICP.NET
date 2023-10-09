using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class TransferArg
	{
		[CandidName("from_subaccount")]
		public TransferArg.FromSubaccountInfo FromSubaccount { get; set; }

		[CandidName("to")]
		public Account To { get; set; }

		[CandidName("amount")]
		public Tokens Amount { get; set; }

		[CandidName("fee")]
		public TransferArg.FeeInfo Fee { get; set; }

		[CandidName("memo")]
		public OptionalValue<List<byte>> Memo { get; set; }

		[CandidName("created_at_time")]
		public TransferArg.CreatedAtTimeInfo CreatedAtTime { get; set; }

		public TransferArg(TransferArg.FromSubaccountInfo fromSubaccount, Account to, Tokens amount, TransferArg.FeeInfo fee, OptionalValue<List<byte>> memo, TransferArg.CreatedAtTimeInfo createdAtTime)
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

		public class FromSubaccountInfo : OptionalValue<Subaccount>
		{
			protected FromSubaccountInfo()
			{
			}
		}

		public class FeeInfo : OptionalValue<Tokens>
		{
			protected FeeInfo()
			{
			}
		}

		public class CreatedAtTimeInfo : OptionalValue<Timestamp>
		{
			protected CreatedAtTimeInfo()
			{
			}
		}
	}
}