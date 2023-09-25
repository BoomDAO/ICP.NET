using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{

	/// <summary>
	/// Represents the result of a transfer operation, which can either be Ok with a value or Err with an error object
	/// </summary>
	[Variant]
	public class TransferResult
	{
		/// <summary>
		/// The tag indicating whether the transfer operation was successful or resulted in an error
		/// </summary>
		[VariantTagProperty]
		public TransferResultTag Tag { get; set; }

		/// <summary>
		/// The value of this TransferResult object, which can be either an UnboundedUInt or a TransferError object
		/// </summary>
		[VariantValueProperty]
		public object? Value { get; set; }

		private TransferResult(TransferResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		/// <summary>
		/// Private default constructor used for reflection
		/// </summary>
		private TransferResult()
		{
		}

		/// <summary>
		/// Creates a new instance of TransferResult with the Ok tag and the given UnboundedUInt value
		/// </summary>
		/// <param name="info">The value associated with this Ok TransferResult object</param>
		/// <returns>A new instance of TransferResult with the Ok tag and the given value</returns>
		public static TransferResult Ok(UnboundedUInt info)
		{
			return new TransferResult(TransferResultTag.Ok, info);
		}

		/// <summary>
		/// Creates a new instance of TransferResult with the Err tag and the given TransferError object as the value
		/// </summary>
		/// <param name="info">The TransferError object containing the error information</param>
		/// <returns>A new instance of TransferResult with the Err tag and the given value</returns>
		public static TransferResult Err(TransferError info)
		{
			return new TransferResult(TransferResultTag.Err, info);
		}

		/// <summary>
		/// Gets the value of this TransferResult object as an UnboundedUInt
		/// </summary>
		/// <returns>The UnboundedUInt value associated with this TransferResult object</returns>
		public UnboundedUInt AsOk()
		{
			this.ValidateTag(TransferResultTag.Ok);
			return (UnboundedUInt)this.Value!;
		}

		/// <summary>
		/// Gets the value of this TransferResult object as a TransferError object
		/// </summary>
		/// <returns>The TransferError object associated with this TransferResult object</returns>
		public TransferError AsErr()
		{
			this.ValidateTag(TransferResultTag.Err);
			return (TransferError)this.Value!;
		}

		/// <summary>
		/// Throws an exception if the current tag of this TransferResult object does not match the given tag
		/// </summary>
		/// <param name="tag">The expected tag</param>
		private void ValidateTag(TransferResultTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	/// <summary>
	/// An enumeration of possible tags for a TransferResult object, which can either be Ok with an UnboundedUInt value or Err with a TransferError object
	/// </summary>
	public enum TransferResultTag
	{
		/// <summary>
		/// Indicates a successful transfer operation with the associated UnboundedUInt value
		/// </summary>
		Ok,
		/// <summary>
		/// Indicates a failed transfer operation with the associated TransferError object containing information about the error
		/// </summary>
		Err
	}


}
