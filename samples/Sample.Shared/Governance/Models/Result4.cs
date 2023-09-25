using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Result4
	{
		[VariantTagProperty()]
		public Result4Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public RewardNodeProviders? Ok { get => this.Tag == Result4Tag.Ok ? (RewardNodeProviders)this.Value : default; set => (this.Tag, this.Value) = (Result4Tag.Ok, value); }

		public GovernanceError? Err { get => this.Tag == Result4Tag.Err ? (GovernanceError)this.Value : default; set => (this.Tag, this.Value) = (Result4Tag.Err, value); }

		public Result4(Result4Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result4()
		{
		}
	}

	public enum Result4Tag
	{
		Ok,
		Err
	}
}