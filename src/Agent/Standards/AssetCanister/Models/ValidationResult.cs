using EdjCase.ICP.Candid.Mapping;

using System;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a result from calling a validation method
	/// </summary>
	[Variant]
	public class ValidationResult
	{
		/// <summary>
		/// The tag for the validation result variant
		/// </summary>
		[VariantTagProperty]
		public ValidationResultTag Tag { get; set; }

		/// <summary>
		/// The value for the validation result variant
		/// </summary>
		[VariantValueProperty]
		public object? Value { get; set; }


		/// <summary>
		/// Represents a result from calling a validation method
		/// </summary>
		/// <param name="tag">The tag for the validation result variant</param>
		/// <param name="value">The value for the validation result variant</param>
		public ValidationResult(ValidationResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationResult"/> class.
		/// </summary>
		protected ValidationResult()
		{
		}

		/// <summary>
		/// Creates a new validation result with an ok value
		/// </summary>
		/// <param name="message">The validation message</param>
		/// <returns>The OK validation result variant</returns>
		public static ValidationResult Ok(string message)
		{
			return new ValidationResult(ValidationResultTag.Ok, message);
		}

		/// <summary>
		/// Creates a new validation result with an error value
		/// </summary>
		/// <param name="message">The error message</param>
		/// <returns>The error validation result variant</returns>
		public static ValidationResult Err(string message)
		{
			return new ValidationResult(ValidationResultTag.Err, message);
		}

		/// <summary>
		/// Casts the validation result to an ok message
		/// </summary>
		/// <returns>The ok message</returns>
		public string AsOk()
		{
			this.ValidateTag(ValidationResultTag.Ok);
			return (string)this.Value!;
		}

		/// <summary>
		/// Casts the validation result to an error message
		/// </summary>
		/// <returns>The error message</returns>
		public string AsErr()
		{
			this.ValidateTag(ValidationResultTag.Err);
			return (string)this.Value!;
		}

		private void ValidateTag(ValidationResultTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	/// <summary>
	/// The tag for the validation result variant
	/// </summary>
	public enum ValidationResultTag
	{
		/// <summary>
		/// The ok variant
		/// </summary>
		Ok,
		/// <summary>
		/// The error variant
		/// </summary>
		Err
	}
}
