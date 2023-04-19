using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Result_5Tag))]
	public class Result_5
	{
		[VariantTagProperty()]
		public Result_5Tag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

		public Result_5(Result_5Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result_5()
		{
		}

		public static Result_5 Ok(NeuronInfo info)
		{
			return new Result_5(Result_5Tag.Ok, info);
		}

		public static Result_5 Err(GovernanceError info)
		{
			return new Result_5(Result_5Tag.Err, info);
		}

		public NeuronInfo AsOk()
		{
			this.ValidateTag(Result_5Tag.Ok);
			return (NeuronInfo)this.Value!;
		}

		public GovernanceError AsErr()
		{
			this.ValidateTag(Result_5Tag.Err);
			return (GovernanceError)this.Value!;
		}

		private void ValidateTag(Result_5Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum Result_5Tag
	{
		[VariantOptionType(typeof(NeuronInfo))]
		Ok,
		[VariantOptionType(typeof(GovernanceError))]
		Err
	}
}