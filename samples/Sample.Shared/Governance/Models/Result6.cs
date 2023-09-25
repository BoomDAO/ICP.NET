using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Result6
	{
		[VariantTagProperty()]
		public Result6Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public NodeProvider? Ok { get => this.Tag == Result6Tag.Ok ? (NodeProvider)this.Value : default; set => (this.Tag, this.Value) = (Result6Tag.Ok, value); }

		public GovernanceError? Err { get => this.Tag == Result6Tag.Err ? (GovernanceError)this.Value : default; set => (this.Tag, this.Value) = (Result6Tag.Err, value); }

		public Result6(Result6Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result6()
		{
		}
	}

	public enum Result6Tag
	{
		Ok,
		Err
	}
}