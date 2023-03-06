using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Result4Tag))]
	public class Result4
	{
		[VariantTagProperty()]
		public Result4Tag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

		public Result4(Result4Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result4()
		{
		}

		public static Result4 Ok(NeuronInfo info)
		{
			return new Result4(Result4Tag.Ok, info);
		}

		public static Result4 Err(GovernanceError info)
		{
			return new Result4(Result4Tag.Err, info);
		}

		public NeuronInfo AsOk()
		{
			this.ValidateTag(Result4Tag.Ok);
			return (NeuronInfo)this.Value!;
		}

		public GovernanceError AsErr()
		{
			this.ValidateTag(Result4Tag.Err);
			return (GovernanceError)this.Value!;
		}

		private void ValidateTag(Result4Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum Result4Tag
	{
		[VariantOptionType(typeof(NeuronInfo))]
		Ok,
		[VariantOptionType(typeof(GovernanceError))]
		Err
	}
}