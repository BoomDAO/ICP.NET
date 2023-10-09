using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using System;
using EdjCase.ICP.Candid.Models;
using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Timestamp = System.UInt64;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant]
	public class ApproveError
	{
		[VariantTagProperty]
		public ApproveErrorTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public ApproveError(ApproveErrorTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected ApproveError()
		{
		}

		public static ApproveError GenericError(ApproveError.GenericErrorInfo info)
		{
			return new ApproveError(ApproveErrorTag.GenericError, info);
		}

		public static ApproveError TemporarilyUnavailable()
		{
			return new ApproveError(ApproveErrorTag.TemporarilyUnavailable, null);
		}

		public static ApproveError Duplicate(ApproveError.DuplicateInfo info)
		{
			return new ApproveError(ApproveErrorTag.Duplicate, info);
		}

		public static ApproveError BadFee(ApproveError.BadFeeInfo info)
		{
			return new ApproveError(ApproveErrorTag.BadFee, info);
		}

		public static ApproveError AllowanceChanged(ApproveError.AllowanceChangedInfo info)
		{
			return new ApproveError(ApproveErrorTag.AllowanceChanged, info);
		}

		public static ApproveError CreatedInFuture(ApproveError.CreatedInFutureInfo info)
		{
			return new ApproveError(ApproveErrorTag.CreatedInFuture, info);
		}

		public static ApproveError TooOld()
		{
			return new ApproveError(ApproveErrorTag.TooOld, null);
		}

		public static ApproveError Expired(ApproveError.ExpiredInfo info)
		{
			return new ApproveError(ApproveErrorTag.Expired, info);
		}

		public static ApproveError InsufficientFunds(ApproveError.InsufficientFundsInfo info)
		{
			return new ApproveError(ApproveErrorTag.InsufficientFunds, info);
		}

		public ApproveError.GenericErrorInfo AsGenericError()
		{
			this.ValidateTag(ApproveErrorTag.GenericError);
			return (ApproveError.GenericErrorInfo)this.Value!;
		}

		public ApproveError.DuplicateInfo AsDuplicate()
		{
			this.ValidateTag(ApproveErrorTag.Duplicate);
			return (ApproveError.DuplicateInfo)this.Value!;
		}

		public ApproveError.BadFeeInfo AsBadFee()
		{
			this.ValidateTag(ApproveErrorTag.BadFee);
			return (ApproveError.BadFeeInfo)this.Value!;
		}

		public ApproveError.AllowanceChangedInfo AsAllowanceChanged()
		{
			this.ValidateTag(ApproveErrorTag.AllowanceChanged);
			return (ApproveError.AllowanceChangedInfo)this.Value!;
		}

		public ApproveError.CreatedInFutureInfo AsCreatedInFuture()
		{
			this.ValidateTag(ApproveErrorTag.CreatedInFuture);
			return (ApproveError.CreatedInFutureInfo)this.Value!;
		}

		public ApproveError.ExpiredInfo AsExpired()
		{
			this.ValidateTag(ApproveErrorTag.Expired);
			return (ApproveError.ExpiredInfo)this.Value!;
		}

		public ApproveError.InsufficientFundsInfo AsInsufficientFunds()
		{
			this.ValidateTag(ApproveErrorTag.InsufficientFunds);
			return (ApproveError.InsufficientFundsInfo)this.Value!;
		}

		private void ValidateTag(ApproveErrorTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
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