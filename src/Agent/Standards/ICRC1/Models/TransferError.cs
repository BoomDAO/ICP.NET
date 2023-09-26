using Timestamp = System.UInt64;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{
	/// <summary>
	/// This class represents an error that can occur during a transfer
	/// </summary>
	[Variant]
	public class TransferError
	{
		/// <summary>
		/// The tag that indicates the type of transfer error
		/// </summary>
		[VariantTagProperty]
		public TransferErrorTag Tag { get; set; }

		/// <summary>
		/// The value that contains the error information, represented as an object
		/// </summary>
		[VariantValueProperty]
		private object? Value { get; set; }

		private TransferError(TransferErrorTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		/// <summary>
		/// Constructor for reflection
		/// </summary>
		protected TransferError()
		{
		}

		/// <summary>
		/// Creates a new instance of TransferError with a BadFeeError object as the value
		/// </summary>
		/// <param name="info">The BadFeeError object containing the error information</param>
		public static TransferError BadFee(BadFeeError info)
		{
			return new TransferError(TransferErrorTag.BadFee, info);
		}

		/// <summary>
		/// Creates a new instance of TransferError with a BadBurnError object as the value
		/// </summary>
		/// <param name="info">The BadBurnError object containing the error information</param>
		public static TransferError BadBurn(BadBurnError info)
		{
			return new TransferError(TransferErrorTag.BadBurn, info);
		}

		/// <summary>
		/// Creates a new instance of TransferError with an InsufficientFundsError object as the value
		/// </summary>
		/// <param name="info">The InsufficientFundsError object containing the error information</param>
		public static TransferError InsufficientFunds(InsufficientFundsError info)
		{
			return new TransferError(TransferErrorTag.InsufficientFunds, info);
		}

		/// <summary>
		/// Creates a new instance of TransferError with a TooOld tag and null value
		/// </summary>
		public static TransferError TooOld()
		{
			return new TransferError(TransferErrorTag.TooOld, null);
		}

		/// <summary>
		/// Creates a new instance of TransferError with a CreatedInFutureError object as the value
		/// </summary>
		/// <param name="info">The CreatedInFutureError object containing the error information</param>
		public static TransferError CreatedInFuture(CreatedInFutureError info)
		{
			return new TransferError(TransferErrorTag.CreatedInFuture, info);
		}

		/// <summary>
		/// Creates a new instance of TransferError with a DuplicateError object as the value
		/// </summary>
		/// <param name="info">The DuplicateError object containing the error information</param>
		public static TransferError Duplicate(DuplicateError info)
		{
			return new TransferError(TransferErrorTag.Duplicate, info);
		}

		/// <summary>
		/// Creates a new instance of TransferError with a TemporarilyUnavailable tag and null value
		/// </summary>
		public static TransferError TemporarilyUnavailable()
		{
			return new TransferError(TransferErrorTag.TemporarilyUnavailable, null);
		}

		/// <summary>
		/// Creates a new instance of TransferError with a GenericError object as the value
		/// </summary>
		/// <param name="info">The GenericError object containing the error information</param>
		public static TransferError GenericError(GenericError info)
		{
			return new TransferError(TransferErrorTag.GenericError, info);
		}

		/// <summary>
		/// Gets the value of this TransferError object as a BadFeeError object
		/// </summary>
		/// <returns>The BadFeeError object representing the error information</returns>
		public BadFeeError AsBadFee()
		{
			this.ValidateTag(TransferErrorTag.BadFee);
			return (BadFeeError)this.Value!;
		}

		/// <summary>
		/// Gets the value of this TransferError object as a BadBurnError object
		/// </summary>
		/// <returns>The BadBurnError object representing the error information</returns>
		public BadBurnError AsBadBurn()
		{
			this.ValidateTag(TransferErrorTag.BadBurn);
			return (BadBurnError)this.Value!;
		}

		/// <summary>
		/// Gets the value of this TransferError object as an InsufficientFundsError object
		/// </summary>
		/// <returns>The InsufficientFundsError object representing the error information</returns>
		public InsufficientFundsError AsInsufficientFunds()
		{
			this.ValidateTag(TransferErrorTag.InsufficientFunds);
			return (InsufficientFundsError)this.Value!;
		}

		/// <summary>
		/// Gets the value of this TransferError object as a CreatedInFutureError object
		/// </summary>
		/// <returns>The CreatedInFutureError object representing the error information</returns>
		public CreatedInFutureError AsCreatedInFuture()
		{
			this.ValidateTag(TransferErrorTag.CreatedInFuture);
			return (CreatedInFutureError)this.Value!;
		}

		/// <summary>
		/// Gets the value of this TransferError object as a DuplicateError object
		/// </summary>
		/// <returns>The DuplicateError object representing the error information</returns>
		public DuplicateError AsDuplicate()
		{
			this.ValidateTag(TransferErrorTag.Duplicate);
			return (DuplicateError)this.Value!;
		}

		/// <summary>
		/// Gets the value of this TransferError object as a GenericError object
		/// </summary>
		/// <returns>The GenericError object representing the error information</returns>
		public GenericError AsGenericError()
		{
			this.ValidateTag(TransferErrorTag.GenericError);
			return (GenericError)this.Value!;
		}

		/// <summary>
		/// Throws an exception if the current tag of this TransferError object does not match the given tag
		/// </summary>
		/// <param name="tag">The expected tag</param>
		private void ValidateTag(TransferErrorTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new System.InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	/// <summary>
	/// This class represents an error that occurs when a fee is incorrect
	/// </summary>
	public class BadFeeError
	{
		/// <summary>
		/// The expected fee for the transaction, represented as an UnboundedUInt object
		/// </summary>
		[CandidName("expected_fee")]
		public UnboundedUInt ExpectedFee { get; set; }

		/// <summary>
		/// Primary constructor for the BadFeeError class
		/// </summary>
		/// <param name="expectedFee">The expected fee for the transaction, represented as an UnboundedUInt object</param>
		public BadFeeError(UnboundedUInt expectedFee)
		{
			// check for null value before setting property
			this.ExpectedFee = expectedFee ?? throw new ArgumentNullException(nameof(expectedFee));
		}
	}

	/// <summary>
	/// This class represents an error that occurs when a burn amount is incorrect
	/// </summary>
	public class BadBurnError
	{
		/// <summary>
		/// The minimum burn amount for the transaction, represented as an UnboundedUInt object
		/// </summary>
		[CandidName("min_burn_amount")]
		public UnboundedUInt MinBurnAmount { get; set; }

		/// <summary>
		/// Primary constructor for the BadBurnError class
		/// </summary>
		/// <param name="minBurnAmount">The minimum burn amount for the transaction, represented as an UnboundedUInt object</param>
		public BadBurnError(UnboundedUInt minBurnAmount)
		{
			// check for null value before setting property
			this.MinBurnAmount = minBurnAmount ?? throw new ArgumentNullException(nameof(minBurnAmount));
		}
	}

	/// <summary>
	/// This class represents an error that occurs when there are insufficient funds for a transaction
	/// </summary>
	public class InsufficientFundsError
	{
		/// <summary>
		/// The balance of the account, represented as an UnboundedUInt object
		/// </summary>
		[CandidName("balance")]
		public UnboundedUInt Balance { get; set; }

		/// <summary>
		/// Primary constructor for the InsufficientFundsError class
		/// </summary>
		/// <param name="balance">The balance of the account, represented as an UnboundedUInt object</param>
		public InsufficientFundsError(UnboundedUInt balance)
		{
			// check for null value before setting property
			this.Balance = balance ?? throw new ArgumentNullException(nameof(balance));
		}
	}

	/// <summary>
	/// This class represents an error that occurs when a transaction is created in the future
	/// </summary>
	public class CreatedInFutureError
	{
		/// <summary>
		/// The ledger time, represented as a Timestamp object
		/// </summary>
		[CandidName("ledger_time")]
		public Timestamp LedgerTime { get; set; }

		/// <summary>
		/// Primary constructor for the CreatedInFutureError class
		/// </summary>
		/// <param name="ledgerTime">The ledger time, represented as a Timestamp object</param>
		public CreatedInFutureError(Timestamp ledgerTime)
		{
			this.LedgerTime = ledgerTime;
		}
	}

	/// <summary>
	/// This class represents an error that occurs when a transaction is a duplicate
	/// </summary>
	public class DuplicateError
	{
		/// <summary>
		/// The ID of the transaction that this transaction is a duplicate
		/// </summary>
		[CandidName("duplicate_of")]
		public UnboundedUInt DuplicateOf { get; set; }

		/// <summary>
		/// Primary constructor for the DuplicateError class
		/// </summary>
		/// <param name="duplicateOf">The ID of the transaction that this transaction is a duplicate of, represented as an UnboundedUInt object</param>
		public DuplicateError(UnboundedUInt duplicateOf)
		{
			// check for null value before setting property
			this.DuplicateOf = duplicateOf ?? throw new ArgumentNullException(nameof(duplicateOf));
		}

	}

	/// <summary>
	/// This class represents a generic error that can occur during a transaction
	/// </summary>
	public class GenericError
	{
		/// <summary>
		/// The error code, represented as an UnboundedUInt object
		/// </summary>
		[CandidName("error_code")]
		public UnboundedUInt ErrorCode { get; set; }

		/// <summary>
		/// The error message, represented as a string
		/// </summary>
		[CandidName("message")]
		public string Message { get; set; }

		/// <summary>
		/// Primary constructor for the GenericError class
		/// </summary>
		/// <param name="errorCode">The error code, represented as an UnboundedUInt object</param>
		/// <param name="message">The error message, represented as a string</param>
		public GenericError(UnboundedUInt errorCode, string message)
		{
			// check for null values before setting properties
			this.ErrorCode = errorCode ?? throw new ArgumentNullException(nameof(errorCode));
			this.Message = message ?? throw new ArgumentNullException(nameof(message));
		}

	}
	/// <summary>
	/// This enum represents the possible types of errors that can occur during a transfer
	/// </summary>
	public enum TransferErrorTag
	{
		/// <summary>
		/// Indicates an error due to an incorrect fee
		/// </summary>
		BadFee,

		/// <summary>
		/// Indicates an error due to an incorrect burn amount
		/// </summary>
		BadBurn,

		/// <summary>
		/// Indicates an error due to insufficient funds for the transaction
		/// </summary>
		InsufficientFunds,

		/// <summary>
		/// Indicates an error due to a transaction being too old
		/// </summary>
		TooOld,

		/// <summary>
		/// Indicates an error due to a transaction being created in the future
		/// </summary>
		CreatedInFuture,

		/// <summary>
		/// Indicates an error due to a transaction being a duplicate
		/// </summary>
		Duplicate,

		/// <summary>
		/// Indicates that the service is temporarily unavailable
		/// </summary>
		TemporarilyUnavailable,

		/// <summary>
		/// Indicates a generic error that can occur during a transfer
		/// </summary>
		GenericError
	}
}