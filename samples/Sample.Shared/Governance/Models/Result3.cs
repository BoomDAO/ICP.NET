using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Result3Tag))]
	public class Result3
	{
		[VariantTagProperty()]
		public Result3Tag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

		public Result3(Result3Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result3()
		{
		}

		public static Result3 Ok(RewardNodeProviders info)
		{
			return new Result3(Result3Tag.Ok, info);
		}

		public static Result3 Err(GovernanceError info)
		{
			return new Result3(Result3Tag.Err, info);
		}

		public RewardNodeProviders AsOk()
		{
			this.ValidateTag(Result3Tag.Ok);
			return (RewardNodeProviders)this.Value!;
		}

		public GovernanceError AsErr()
		{
			this.ValidateTag(Result3Tag.Err);
			return (GovernanceError)this.Value!;
		}

		private void ValidateTag(Result3Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum Result3Tag
	{
		[VariantOptionType(typeof(RewardNodeProviders))]
		Ok,
		[VariantOptionType(typeof(GovernanceError))]
		Err
	}
}