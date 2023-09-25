using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;
using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Timestamp = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant()]
	public class TransferFromError
	{
		[VariantTagProperty()]
		public TransferFromErrorTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public TransferFromError.BadFeeInfo? BadFee { get => this.Tag == TransferFromErrorTag.BadFee ? (TransferFromError.BadFeeInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferFromErrorTag.BadFee, value); }

		public TransferFromError.BadBurnInfo? BadBurn { get => this.Tag == TransferFromErrorTag.BadBurn ? (TransferFromError.BadBurnInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferFromErrorTag.BadBurn, value); }

		public TransferFromError.InsufficientFundsInfo? InsufficientFunds { get => this.Tag == TransferFromErrorTag.InsufficientFunds ? (TransferFromError.InsufficientFundsInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferFromErrorTag.InsufficientFunds, value); }

		public TransferFromError.InsufficientAllowanceInfo? InsufficientAllowance { get => this.Tag == TransferFromErrorTag.InsufficientAllowance ? (TransferFromError.InsufficientAllowanceInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferFromErrorTag.InsufficientAllowance, value); }

		public TransferFromError.CreatedInFutureInfo? CreatedInFuture { get => this.Tag == TransferFromErrorTag.CreatedInFuture ? (TransferFromError.CreatedInFutureInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferFromErrorTag.CreatedInFuture, value); }

		public TransferFromError.DuplicateInfo? Duplicate { get => this.Tag == TransferFromErrorTag.Duplicate ? (TransferFromError.DuplicateInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferFromErrorTag.Duplicate, value); }

		public TransferFromError.GenericErrorInfo? GenericError { get => this.Tag == TransferFromErrorTag.GenericError ? (TransferFromError.GenericErrorInfo)this.Value : default; set => (this.Tag, this.Value) = (TransferFromErrorTag.GenericError, value); }

		public TransferFromError(TransferFromErrorTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected TransferFromError()
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

		public class InsufficientAllowanceInfo
		{
			[CandidName("allowance")]
			public Tokens Allowance { get; set; }

			public InsufficientAllowanceInfo(Tokens allowance)
			{
				this.Allowance = allowance;
			}

			public InsufficientAllowanceInfo()
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

	public enum TransferFromErrorTag
	{
		BadFee,
		BadBurn,
		InsufficientFunds,
		InsufficientAllowance,
		TooOld,
		CreatedInFuture,
		Duplicate,
		TemporarilyUnavailable,
		GenericError
	}
}