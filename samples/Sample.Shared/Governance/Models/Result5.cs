using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Result5
	{
		[VariantTagProperty()]
		public Result5Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public NeuronInfo? Ok { get => this.Tag == Result5Tag.Ok ? (NeuronInfo)this.Value : default; set => (this.Tag, this.Value) = (Result5Tag.Ok, value); }

		public GovernanceError? Err { get => this.Tag == Result5Tag.Err ? (GovernanceError)this.Value : default; set => (this.Tag, this.Value) = (Result5Tag.Err, value); }

		public Result5(Result5Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result5()
		{
		}
	}

	public enum Result5Tag
	{
		Ok,
		Err
	}
}