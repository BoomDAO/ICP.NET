using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;
using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Timestamp = System.UInt64;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant()]
	public class ApproveError
	{
		[VariantTagProperty()]
		public ApproveErrorTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public ApproveError.GenericErrorInfo? GenericError { get => this.Tag == ApproveErrorTag.GenericError ? (ApproveError.GenericErrorInfo)this.Value : default; set => (this.Tag, this.Value) = (ApproveErrorTag.GenericError, value); }

		public ApproveError.DuplicateInfo? Duplicate { get => this.Tag == ApproveErrorTag.Duplicate ? (ApproveError.DuplicateInfo)this.Value : default; set => (this.Tag, this.Value) = (ApproveErrorTag.Duplicate, value); }

		public ApproveError.BadFeeInfo? BadFee { get => this.Tag == ApproveErrorTag.BadFee ? (ApproveError.BadFeeInfo)this.Value : default; set => (this.Tag, this.Value) = (ApproveErrorTag.BadFee, value); }

		public ApproveError.AllowanceChangedInfo? AllowanceChanged { get => this.Tag == ApproveErrorTag.AllowanceChanged ? (ApproveError.AllowanceChangedInfo)this.Value : default; set => (this.Tag, this.Value) = (ApproveErrorTag.AllowanceChanged, value); }

		public ApproveError.CreatedInFutureInfo? CreatedInFuture { get => this.Tag == ApproveErrorTag.CreatedInFuture ? (ApproveError.CreatedInFutureInfo)this.Value : default; set => (this.Tag, this.Value) = (ApproveErrorTag.CreatedInFuture, value); }

		public ApproveError.ExpiredInfo? Expired { get => this.Tag == ApproveErrorTag.Expired ? (ApproveError.ExpiredInfo)this.Value : default; set => (this.Tag, this.Value) = (ApproveErrorTag.Expired, value); }

		public ApproveError.InsufficientFundsInfo? InsufficientFunds { get => this.Tag == ApproveErrorTag.InsufficientFunds ? (ApproveError.InsufficientFundsInfo)this.Value : default; set => (this.Tag, this.Value) = (ApproveErrorTag.InsufficientFunds, value); }

		public ApproveError(ApproveErrorTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected ApproveError()
		{
		}

		public class GenericErrorInfo
		{
			[CandidName("message")]
			public string Message { get; set; }

			[CandidName("error_code")]
			public UnboundedUInt ErrorCode { get; set; }

			public GenericErrorInfo(string message, UnboundedUInt errorCode)
			{
				this.Message = message;
				this.ErrorCode = errorCode;
			}

			public GenericErrorInfo()
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

		public class BadFeeInfo
		{
			[CandidName("expected_fee")]
			public UnboundedUInt ExpectedFee { get; set; }

			public BadFeeInfo(UnboundedUInt expectedFee)
			{
				this.ExpectedFee = expectedFee;
			}

			public BadFeeInfo()
			{
			}
		}

		public class AllowanceChangedInfo
		{
			[CandidName("current_allowance")]
			public UnboundedUInt CurrentAllowance { get; set; }

			public AllowanceChangedInfo(UnboundedUInt currentAllowance)
			{
				this.CurrentAllowance = currentAllowance;
			}

			public AllowanceChangedInfo()
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

		public class ExpiredInfo
		{
			[CandidName("ledger_time")]
			public Timestamp LedgerTime { get; set; }

			public ExpiredInfo(Timestamp ledgerTime)
			{
				this.LedgerTime = ledgerTime;
			}

			public ExpiredInfo()
			{
			}
		}

		public class InsufficientFundsInfo
		{
			[CandidName("balance")]
			public UnboundedUInt Balance { get; set; }

			public InsufficientFundsInfo(UnboundedUInt balance)
			{
				this.Balance = balance;
			}

			public InsufficientFundsInfo()
			{
			}
		}
	}

	public enum ApproveErrorTag
	{
		GenericError,
		TemporarilyUnavailable,
		Duplicate,
		BadFee,
		AllowanceChanged,
		CreatedInFuture,
		TooOld,
		Expired,
		InsufficientFunds
	}
}