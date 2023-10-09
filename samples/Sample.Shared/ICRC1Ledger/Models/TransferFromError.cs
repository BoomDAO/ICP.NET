using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using System;
using EdjCase.ICP.Candid.Models;
using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Timestamp = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant]
	public class TransferFromError
	{
		[VariantTagProperty]
		public TransferFromErrorTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public TransferFromError(TransferFromErrorTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected TransferFromError()
		{
		}

		public static TransferFromError BadFee(TransferFromError.BadFeeInfo info)
		{
			return new TransferFromError(TransferFromErrorTag.BadFee, info);
		}

		public static TransferFromError BadBurn(TransferFromError.BadBurnInfo info)
		{
			return new TransferFromError(TransferFromErrorTag.BadBurn, info);
		}

		public static TransferFromError InsufficientFunds(TransferFromError.InsufficientFundsInfo info)
		{
			return new TransferFromError(TransferFromErrorTag.InsufficientFunds, info);
		}

		public static TransferFromError InsufficientAllowance(TransferFromError.InsufficientAllowanceInfo info)
		{
			return new TransferFromError(TransferFromErrorTag.InsufficientAllowance, info);
		}

		public static TransferFromError TooOld()
		{
			return new TransferFromError(TransferFromErrorTag.TooOld, null);
		}

		public static TransferFromError CreatedInFuture(TransferFromError.CreatedInFutureInfo info)
		{
			return new TransferFromError(TransferFromErrorTag.CreatedInFuture, info);
		}

		public static TransferFromError Duplicate(TransferFromError.DuplicateInfo info)
		{
			return new TransferFromError(TransferFromErrorTag.Duplicate, info);
		}

		public static TransferFromError TemporarilyUnavailable()
		{
			return new TransferFromError(TransferFromErrorTag.TemporarilyUnavailable, null);
		}

		public static TransferFromError GenericError(TransferFromError.GenericErrorInfo info)
		{
			return new TransferFromError(TransferFromErrorTag.GenericError, info);
		}

		public TransferFromError.BadFeeInfo AsBadFee()
		{
			this.ValidateTag(TransferFromErrorTag.BadFee);
			return (TransferFromError.BadFeeInfo)this.Value!;
		}

		public TransferFromError.BadBurnInfo AsBadBurn()
		{
			this.ValidateTag(TransferFromErrorTag.BadBurn);
			return (TransferFromError.BadBurnInfo)this.Value!;
		}

		public TransferFromError.InsufficientFundsInfo AsInsufficientFunds()
		{
			this.ValidateTag(TransferFromErrorTag.InsufficientFunds);
			return (TransferFromError.InsufficientFundsInfo)this.Value!;
		}

		public TransferFromError.InsufficientAllowanceInfo AsInsufficientAllowance()
		{
			this.ValidateTag(TransferFromErrorTag.InsufficientAllowance);
			return (TransferFromError.InsufficientAllowanceInfo)this.Value!;
		}

		public TransferFromError.CreatedInFutureInfo AsCreatedInFuture()
		{
			this.ValidateTag(TransferFromErrorTag.CreatedInFuture);
			return (TransferFromError.CreatedInFutureInfo)this.Value!;
		}

		public TransferFromError.DuplicateInfo AsDuplicate()
		{
			this.ValidateTag(TransferFromErrorTag.Duplicate);
			return (TransferFromError.DuplicateInfo)this.Value!;
		}

		public TransferFromError.GenericErrorInfo AsGenericError()
		{
			this.ValidateTag(TransferFromErrorTag.GenericError);
			return (TransferFromError.GenericErrorInfo)this.Value!;
		}

		private void ValidateTag(TransferFromErrorTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
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