using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class TransferFromArgs
	{
		[CandidName("spender_subaccount")]
		public TransferFromArgs.SpenderSubaccountInfo SpenderSubaccount { get; set; }

		[CandidName("from")]
		public Account From { get; set; }

		[CandidName("to")]
		public Account To { get; set; }

		[CandidName("amount")]
		public Tokens Amount { get; set; }

		[CandidName("fee")]
		public TransferFromArgs.FeeInfo Fee { get; set; }

		[CandidName("memo")]
		public OptionalValue<byte[]> Memo { get; set; }

		[CandidName("created_at_time")]
		public TransferFromArgs.CreatedAtTimeInfo CreatedAtTime { get; set; }

		public TransferFromArgs(TransferFromArgs.SpenderSubaccountInfo spenderSubaccount, Account from, Account to, Tokens amount, TransferFromArgs.FeeInfo fee, OptionalValue<byte[]> memo, TransferFromArgs.CreatedAtTimeInfo createdAtTime)
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

		public class SpenderSubaccountInfo : OptionalValue<Subaccount>
		{
			public SpenderSubaccountInfo()
			{
			}

			public SpenderSubaccountInfo(Subaccount value) : base(value)
			{
			}
		}

		public class FeeInfo : OptionalValue<Tokens>
		{
			public FeeInfo()
			{
			}

			public FeeInfo(Tokens value) : base(value)
			{
			}
		}

		public class CreatedAtTimeInfo : OptionalValue<Timestamp>
		{
			public CreatedAtTimeInfo()
			{
			}

			public CreatedAtTimeInfo(Timestamp value) : base(value)
			{
			}
		}
	}
}