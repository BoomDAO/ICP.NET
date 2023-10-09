using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class Result5
	{
		[VariantTagProperty]
		public Result5Tag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Result5(Result5Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result5()
		{
		}

		public static Result5 Ok(NeuronInfo info)
		{
			return new Result5(Result5Tag.Ok, info);
		}

		public static Result5 Err(GovernanceError info)
		{
			return new Result5(Result5Tag.Err, info);
		}

		public NeuronInfo AsOk()
		{
			this.ValidateTag(Result5Tag.Ok);
			return (NeuronInfo)this.Value!;
		}

		public GovernanceError AsErr()
		{
			this.ValidateTag(Result5Tag.Err);
			return (GovernanceError)this.Value!;
		}

		private void ValidateTag(Result5Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum Result5Tag
	{
		Ok,
		Err
	}
}