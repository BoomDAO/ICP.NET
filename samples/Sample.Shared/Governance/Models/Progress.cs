using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class Progress
	{
		[VariantTagProperty]
		public ProgressTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Progress(ProgressTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Progress()
		{
		}

		public static Progress LastNeuronId(NeuronId info)
		{
			return new Progress(ProgressTag.LastNeuronId, info);
		}

		public NeuronId AsLastNeuronId()
		{
			this.ValidateTag(ProgressTag.LastNeuronId);
			return (NeuronId)this.Value!;
		}

		private void ValidateTag(ProgressTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum ProgressTag
	{
		LastNeuronId
	}
}