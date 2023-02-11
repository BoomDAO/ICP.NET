using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Subaccount = System.Collections.Generic.List<byte>;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{
	[Variant(typeof(TransferErrorTag))]
	public class TransferError
	{
		[VariantTagProperty()]
		public TransferErrorTag Tag { get; set; }

		[VariantValueProperty()]
		private object? Value { get; set; }

		public TransferError(TransferErrorTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected TransferError()
		{
		}

		public static TransferError BadFee(BadFeeError info)
		{
			return new TransferError(TransferErrorTag.BadFee, info);
		}

		public static TransferError BadBurn(BadBurnError info)
		{
			return new TransferError(TransferErrorTag.BadBurn, info);
		}

		public static TransferError InsufficientFunds(InsufficientFundsError info)
		{
			return new TransferError(TransferErrorTag.InsufficientFunds, info);
		}

		public static TransferError TooOld()
		{
			return new TransferError(TransferErrorTag.TooOld, null);
		}

		public static TransferError CreatedInFuture(CreatedInFutureError info)
		{
			return new TransferError(TransferErrorTag.CreatedInFuture, info);
		}

		public static TransferError Duplicate(DuplicateError info)
		{
			return new TransferError(TransferErrorTag.Duplicate, info);
		}

		public static TransferError TemporarilyUnavailable()
		{
			return new TransferError(TransferErrorTag.TemporarilyUnavailable, null);
		}

		public static TransferError GenericError(GenericError info)
		{
			return new TransferError(TransferErrorTag.GenericError, info);
		}

		public BadFeeError AsBadFee()
		{
			this.ValidateTag(TransferErrorTag.BadFee);
			return (BadFeeError)this.Value!;
		}

		public BadBurnError AsBadBurn()
		{
			this.ValidateTag(TransferErrorTag.BadBurn);
			return (BadBurnError)this.Value!;
		}

		public InsufficientFundsError AsInsufficientFunds()
		{
			this.ValidateTag(TransferErrorTag.InsufficientFunds);
			return (InsufficientFundsError)this.Value!;
		}

		public CreatedInFutureError AsCreatedInFuture()
		{
			this.ValidateTag(TransferErrorTag.CreatedInFuture);
			return (CreatedInFutureError)this.Value!;
		}

		public DuplicateError AsDuplicate()
		{
			this.ValidateTag(TransferErrorTag.Duplicate);
			return (DuplicateError)this.Value!;
		}

		public GenericError AsGenericError()
		{
			this.ValidateTag(TransferErrorTag.GenericError);
			return (GenericError)this.Value!;
		}

		private void ValidateTag(TransferErrorTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new System.InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public class BadFeeError
	{
		[CandidName("expected_fee")]
		public UnboundedUInt ExpectedFee { get; set; }

		public BadFeeError(UnboundedUInt expectedFee)
		{
			this.ExpectedFee = expectedFee;
		}
	}

	public class BadBurnError
	{
		[CandidName("min_burn_amount")]
		public UnboundedUInt MinBurnAmount { get; set; }

		public BadBurnError(UnboundedUInt minBurnAmount)
		{
			this.MinBurnAmount = minBurnAmount;
		}
	}

	public class InsufficientFundsError
	{
		[CandidName("balance")]
		public UnboundedUInt Balance { get; set; }

		public InsufficientFundsError(UnboundedUInt balance)
		{
			this.Balance = balance;
		}
	}

	public class CreatedInFutureError
	{
		[CandidName("ledger_time")]
		public Timestamp LedgerTime { get; set; }

		public CreatedInFutureError(Timestamp ledgerTime)
		{
			this.LedgerTime = ledgerTime;
		}
	}

	public class DuplicateError
	{
		[CandidName("duplicate_of")]
		public UnboundedUInt DuplicateOf { get; set; }

		public DuplicateError(UnboundedUInt duplicateOf)
		{
			this.DuplicateOf = duplicateOf;
		}
	}

	public class GenericError
	{
		[CandidName("error_code")]
		public UnboundedUInt ErrorCode { get; set; }

		[CandidName("message")]
		public string Message { get; set; }

		public GenericError(UnboundedUInt errorCode, string message)
		{
			this.ErrorCode = errorCode;
			this.Message = message;
		}
	}

	public enum TransferErrorTag
	{
		[CandidName("BadFee")]
		[VariantOptionType(typeof(BadFeeError))]
		BadFee,
		[CandidName("BadBurn")]
		[VariantOptionType(typeof(BadBurnError))]
		BadBurn,
		[CandidName("InsufficientFunds")]
		[VariantOptionType(typeof(InsufficientFundsError))]
		InsufficientFunds,
		[CandidName("TooOld")]
		TooOld,
		[CandidName("CreatedInFuture")]
		[VariantOptionType(typeof(CreatedInFutureError))]
		CreatedInFuture,
		[CandidName("Duplicate")]
		[VariantOptionType(typeof(DuplicateError))]
		Duplicate,
		[CandidName("TemporarilyUnavailable")]
		TemporarilyUnavailable,
		[CandidName("GenericError")]
		[VariantOptionType(typeof(GenericError))]
		GenericError
	}
}