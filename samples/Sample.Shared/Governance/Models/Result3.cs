using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Result3
	{
		[VariantTagProperty()]
		public Result3Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public GovernanceCachedMetrics? Ok { get => this.Tag == Result3Tag.Ok ? (GovernanceCachedMetrics)this.Value : default; set => (this.Tag, this.Value) = (Result3Tag.Ok, value); }

		public GovernanceError? Err { get => this.Tag == Result3Tag.Err ? (GovernanceError)this.Value : default; set => (this.Tag, this.Value) = (Result3Tag.Err, value); }

		public Result3(Result3Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result3()
		{
		}
	}

	public enum Result3Tag
	{
		Ok,
		Err
	}
}