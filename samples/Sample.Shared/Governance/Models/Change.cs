using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Change
	{
		[VariantTagProperty()]
		public ChangeTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public NodeProvider? ToRemove { get => this.Tag == ChangeTag.ToRemove ? (NodeProvider)this.Value : default; set => (this.Tag, this.Value) = (ChangeTag.ToRemove, value); }

		public NodeProvider? ToAdd { get => this.Tag == ChangeTag.ToAdd ? (NodeProvider)this.Value : default; set => (this.Tag, this.Value) = (ChangeTag.ToAdd, value); }

		public Change(ChangeTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Change()
		{
		}
	}

	public enum ChangeTag
	{
		ToRemove,
		ToAdd
	}
}