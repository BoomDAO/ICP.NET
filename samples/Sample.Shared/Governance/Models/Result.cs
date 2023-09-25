using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Result
	{
		[VariantTagProperty()]
		public ResultTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public GovernanceError? Err { get => this.Tag == ResultTag.Err ? (GovernanceError)this.Value : default; set => (this.Tag, this.Value) = (ResultTag.Err, value); }

		public Result(ResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result()
		{
		}
	}

	public enum ResultTag
	{
		Ok,
		Err
	}
}