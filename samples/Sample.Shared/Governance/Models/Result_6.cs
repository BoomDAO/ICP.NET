using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Result_6Tag))]
	public class Result_6
	{
		[VariantTagProperty()]
		public Result_6Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public Result_6(Result_6Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result_6()
		{
		}

		public static Result_6 Ok(NodeProvider info)
		{
			return new Result_6(Result_6Tag.Ok, info);
		}

		public static Result_6 Err(GovernanceError info)
		{
			return new Result_6(Result_6Tag.Err, info);
		}

		public NodeProvider AsOk()
		{
			this.ValidateTag(Result_6Tag.Ok);
			return (NodeProvider)this.Value!;
		}

		public GovernanceError AsErr()
		{
			this.ValidateTag(Result_6Tag.Err);
			return (GovernanceError)this.Value!;
		}

		private void ValidateTag(Result_6Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum Result_6Tag
	{
		[VariantOptionType(typeof(NodeProvider))]
		Ok,
		[VariantOptionType(typeof(GovernanceError))]
		Err
	}
}