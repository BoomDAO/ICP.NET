using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Result_1Tag))]
	public class Result_1
	{
		[VariantTagProperty()]
		public Result_1Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public Result_1(Result_1Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result_1()
		{
		}

		public static Result_1 Error(GovernanceError info)
		{
			return new Result_1(Result_1Tag.Error, info);
		}

		public static Result_1 NeuronId(NeuronId info)
		{
			return new Result_1(Result_1Tag.NeuronId, info);
		}

		public GovernanceError AsError()
		{
			this.ValidateTag(Result_1Tag.Error);
			return (GovernanceError)this.Value!;
		}

		public NeuronId AsNeuronId()
		{
			this.ValidateTag(Result_1Tag.NeuronId);
			return (NeuronId)this.Value!;
		}

		private void ValidateTag(Result_1Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum Result_1Tag
	{
		[VariantOptionType(typeof(GovernanceError))]
		Error,
		[VariantOptionType(typeof(NeuronId))]
		NeuronId
	}
}