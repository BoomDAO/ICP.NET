using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class Result1
	{
		[VariantTagProperty]
		public Result1Tag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Result1(Result1Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result1()
		{
		}

		public static Result1 Error(GovernanceError info)
		{
			return new Result1(Result1Tag.Error, info);
		}

		public static Result1 NeuronId(NeuronId info)
		{
			return new Result1(Result1Tag.NeuronId, info);
		}

		public GovernanceError AsError()
		{
			this.ValidateTag(Result1Tag.Error);
			return (GovernanceError)this.Value!;
		}

		public NeuronId AsNeuronId()
		{
			this.ValidateTag(Result1Tag.NeuronId);
			return (NeuronId)this.Value!;
		}

		private void ValidateTag(Result1Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum Result1Tag
	{
		Error,
		NeuronId
	}
}