using EdjCase.ICP.Candid.Mapping;

using System;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	[Variant]
	public class ValidationResult
	{
		[VariantTagProperty]
		public ValidationResultTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public ValidationResult(ValidationResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected ValidationResult()
		{
		}

		public static ValidationResult Ok(string info)
		{
			return new ValidationResult(ValidationResultTag.Ok, info);
		}

		public static ValidationResult Err(string info)
		{
			return new ValidationResult(ValidationResultTag.Err, info);
		}

		public string AsOk()
		{
			this.ValidateTag(ValidationResultTag.Ok);
			return (string)this.Value!;
		}

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

	public enum ValidationResultTag
	{
		Ok,
		Err
	}
}