using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Result_3Tag))]
	public class Result_3
	{
		[VariantTagProperty()]
		public Result_3Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public Result_3(Result_3Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result_3()
		{
		}

		public static Result_3 Ok(GovernanceCachedMetrics info)
		{
			return new Result_3(Result_3Tag.Ok, info);
		}

		public static Result_3 Err(GovernanceError info)
		{
			return new Result_3(Result_3Tag.Err, info);
		}

		public GovernanceCachedMetrics AsOk()
		{
			this.ValidateTag(Result_3Tag.Ok);
			return (GovernanceCachedMetrics)this.Value!;
		}

		public GovernanceError AsErr()
		{
			this.ValidateTag(Result_3Tag.Err);
			return (GovernanceError)this.Value!;
		}

		private void ValidateTag(Result_3Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum Result_3Tag
	{
		[VariantOptionType(typeof(GovernanceCachedMetrics))]
		Ok,
		[VariantOptionType(typeof(GovernanceError))]
		Err
	}
}