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
	public class Transaction
	{
		[CandidName("kind")]
		public string Kind { get; set; }

		[CandidName("mint")]
		public OptionalValue<Transaction.MintValue> Mint { get; set; }

		[CandidName("burn")]
		public OptionalValue<Transaction.BurnValue> Burn { get; set; }

		[CandidName("transfer")]
		public OptionalValue<Transaction.TransferValue> Transfer { get; set; }

		[CandidName("timestamp")]
		public ulong Timestamp { get; set; }

		public Transaction(string kind, OptionalValue<Transaction.MintValue> mint, OptionalValue<Transaction.BurnValue> burn, OptionalValue<Transaction.TransferValue> transfer, ulong timestamp)
		{
			this.Kind = kind;
			this.Mint = mint;
			this.Burn = burn;
			this.Transfer = transfer;
			this.Timestamp = timestamp;
		}

		public Transaction()
		{
		}

		public class MintValue
		{
			[CandidName("amount")]
			public UnboundedUInt Amount { get; set; }

			[CandidName("to")]
			public Account To { get; set; }

			[CandidName("memo")]
			public OptionalValue<List<byte>> Memo { get; set; }

			[CandidName("created_at_time")]
			public OptionalValue<ulong> CreatedAtTime { get; set; }

			public MintValue(UnboundedUInt amount, Account to, OptionalValue<List<byte>> memo, OptionalValue<ulong> createdAtTime)
			{
				this.Amount = amount;
				this.To = to;
				this.Memo = memo;
				this.CreatedAtTime = createdAtTime;
			}

			public MintValue()
			{
			}
		}

		public class BurnValue
		{
			[CandidName("amount")]
			public UnboundedUInt Amount { get; set; }

			[CandidName("from")]
			public Account From { get; set; }

			[CandidName("memo")]
			public OptionalValue<List<byte>> Memo { get; set; }

			[CandidName("created_at_time")]
			public OptionalValue<ulong> CreatedAtTime { get; set; }

			public BurnValue(UnboundedUInt amount, Account from, OptionalValue<List<byte>> memo, OptionalValue<ulong> createdAtTime)
			{
				this.Amount = amount;
				this.From = from;
				this.Memo = memo;
				this.CreatedAtTime = createdAtTime;
			}

			public BurnValue()
			{
			}
		}

		public class TransferValue
		{
			[CandidName("amount")]
			public UnboundedUInt Amount { get; set; }

			[CandidName("from")]
			public Account From { get; set; }

			[CandidName("to")]
			public Account To { get; set; }

			[CandidName("memo")]
			public OptionalValue<List<byte>> Memo { get; set; }

			[CandidName("created_at_time")]
			public OptionalValue<ulong> CreatedAtTime { get; set; }

			[CandidName("fee")]
			public OptionalValue<UnboundedUInt> Fee { get; set; }

			public TransferValue(UnboundedUInt amount, Account from, Account to, OptionalValue<List<byte>> memo, OptionalValue<ulong> createdAtTime, OptionalValue<UnboundedUInt> fee)
			{
				this.Amount = amount;
				this.From = from;
				this.To = to;
				this.Memo = memo;
				this.CreatedAtTime = createdAtTime;
				this.Fee = fee;
			}

			public TransferValue()
			{
			}
		}
	}
}