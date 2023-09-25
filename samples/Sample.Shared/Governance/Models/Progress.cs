using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Progress
	{
		[VariantTagProperty()]
		public ProgressTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public NeuronId? LastNeuronId { get => this.Tag == ProgressTag.LastNeuronId ? (NeuronId)this.Value : default; set => (this.Tag, this.Value) = (ProgressTag.LastNeuronId, value); }

		public Progress(ProgressTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Progress()
		{
		}
	}

	public enum ProgressTag
	{
		LastNeuronId
	}
}