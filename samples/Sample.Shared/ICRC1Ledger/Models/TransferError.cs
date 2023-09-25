using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;
using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Timestamp = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant()]
	public class TransferError
	{
		[VariantTagProperty()]
		public TransferErrorTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public TransferError.BadFeeInfo? BadFee { get => this.Tag == TransferErrorTag.BadFee ? (TransferError.BadFeeInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferErrorTag.BadFee, value); }

		public TransferError.BadBurnInfo? BadBurn { get => this.Tag == TransferErrorTag.BadBurn ? (TransferError.BadBurnInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferErrorTag.BadBurn, value); }

		public TransferError.InsufficientFundsInfo? InsufficientFunds { get => this.Tag == TransferErrorTag.InsufficientFunds ? (TransferError.InsufficientFundsInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferErrorTag.InsufficientFunds, value); }

		public TransferError.CreatedInFutureInfo? CreatedInFuture { get => this.Tag == TransferErrorTag.CreatedInFuture ? (TransferError.CreatedInFutureInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferErrorTag.CreatedInFuture, value); }

		public TransferError.DuplicateInfo? Duplicate { get => this.Tag == TransferErrorTag.Duplicate ? (TransferError.DuplicateInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferErrorTag.Duplicate, value); }

		public TransferError.GenericErrorInfo? GenericError { get => this.Tag == TransferErrorTag.GenericError ? (TransferError.GenericErrorInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferErrorTag.GenericError, value); }

		public TransferError(TransferErrorTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected TransferError()
		{
		}

		public class BadFeeInfo
		{
			[CandidName("expected_fee")]
			public Tokens ExpectedFee { get; set; }

			public BadFeeInfo(Tokens expectedFee)
			{
				this.ExpectedFee = expectedFee;
			}

			public BadFeeInfo()
			{
			}
		}

		public class BadBurnInfo
		{
			[CandidName("min_burn_amount")]
			public Tokens MinBurnAmount { get; set; }

			public BadBurnInfo(Tokens minBurnAmount)
			{
				this.MinBurnAmount = minBurnAmount;
			}

			public BadBurnInfo()
			{
			}
		}

		public class InsufficientFundsInfo
		{
			[CandidName("balance")]
			public Tokens Balance { get; set; }

			public InsufficientFundsInfo(Tokens balance)
			{
				this.Balance = balance;
			}

			public InsufficientFundsInfo()
			{
			}
		}

		public class CreatedInFutureInfo
		{
			[CandidName("ledger_time")]
			public Timestamp LedgerTime { get; set; }

			public CreatedInFutureInfo(Timestamp ledgerTime)
			{
				this.LedgerTime = ledgerTime;
			}

			public CreatedInFutureInfo()
			{
			}
		}

		public class DuplicateInfo
		{
			[CandidName("duplicate_of")]
			public BlockIndex DuplicateOf { get; set; }

			public DuplicateInfo(BlockIndex duplicateOf)
			{
				this.DuplicateOf = duplicateOf;
			}

			public DuplicateInfo()
			{
			}
		}

		public class GenericErrorInfo
		{
			[CandidName("error_code")]
			public UnboundedUInt ErrorCode { get; set; }

			[CandidName("message")]
			public string Message { get; set; }

			public GenericErrorInfo(UnboundedUInt errorCode, string message)
			{
				this.ErrorCode = errorCode;
				this.Message = message;
			}

			public GenericErrorInfo()
			{
			}
		}
	}

	public enum TransferErrorTag
	{
		BadFee,
		BadBurn,
		InsufficientFunds,
		TooOld,
		CreatedInFuture,
		TemporarilyUnavailable,
		Duplicate,
		GenericError
	}
}