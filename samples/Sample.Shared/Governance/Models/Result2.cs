using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Result2
	{
		[VariantTagProperty()]
		public Result2Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public Neuron? Ok { get => this.Tag == Result2Tag.Ok ? (Neuron)this.Value : default; set => (this.Tag, this.Value) = (Result2Tag.Ok, value); }

		public GovernanceError? Err { get => this.Tag == Result2Tag.Err ? (GovernanceError)this.Value : default; set => (this.Tag, this.Value) = (Result2Tag.Err, value); }

		public Result2(Result2Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result2()
		{
		}
	}

	public enum Result2Tag
	{
		Ok,
		Err
	}
}