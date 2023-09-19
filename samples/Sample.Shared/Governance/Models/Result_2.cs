using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Result_2Tag))]
	public class Result_2
	{
		[VariantTagProperty()]
		public Result_2Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public Result_2(Result_2Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result_2()
		{
		}

		public static Result_2 Ok(Neuron info)
		{
			return new Result_2(Result_2Tag.Ok, info);
		}

		public static Result_2 Err(GovernanceError info)
		{
			return new Result_2(Result_2Tag.Err, info);
		}

		public Neuron AsOk()
		{
			this.ValidateTag(Result_2Tag.Ok);
			return (Neuron)this.Value!;
		}

		public GovernanceError AsErr()
		{
			this.ValidateTag(Result_2Tag.Err);
			return (GovernanceError)this.Value!;
		}

		private void ValidateTag(Result_2Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum Result_2Tag
	{
		[VariantOptionType(typeof(Neuron))]
		Ok,
		[VariantOptionType(typeof(GovernanceError))]
		Err
	}
}