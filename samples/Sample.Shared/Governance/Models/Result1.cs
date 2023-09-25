using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Result1
	{
		[VariantTagProperty()]
		public Result1Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public GovernanceError? Error { get => this.Tag == Result1Tag.Error ? (GovernanceError)this.Value : default; set => (this.Tag, this.Value) = (Result1Tag.Error, value); }

		public NeuronId? NeuronId { get => this.Tag == Result1Tag.NeuronId ? (NeuronId)this.Value : default; set => (this.Tag, this.Value) = (Result1Tag.NeuronId, value); }

		public Result1(Result1Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result1()
		{
		}
	}

	public enum Result1Tag
	{
		Error,
		NeuronId
	}
}