using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Result_4Tag))]
	public class Result_4
	{
		[VariantTagProperty()]
		public Result_4Tag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

		public Result_4(Result_4Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result_4()
		{
		}

		public static Result_4 Ok(RewardNodeProviders info)
		{
			return new Result_4(Result_4Tag.Ok, info);
		}

		public static Result_4 Err(GovernanceError info)
		{
			return new Result_4(Result_4Tag.Err, info);
		}

		public RewardNodeProviders AsOk()
		{
			this.ValidateTag(Result_4Tag.Ok);
			return (RewardNodeProviders)this.Value!;
		}

		public GovernanceError AsErr()
		{
			this.ValidateTag(Result_4Tag.Err);
			return (GovernanceError)this.Value!;
		}

		private void ValidateTag(Result_4Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum Result_4Tag
	{
		[VariantOptionType(typeof(RewardNodeProviders))]
		Ok,
		[VariantOptionType(typeof(GovernanceError))]
		Err
	}
}