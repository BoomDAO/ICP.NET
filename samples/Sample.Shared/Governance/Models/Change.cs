using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class Change
	{
		[VariantTagProperty]
		public ChangeTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Change(ChangeTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Change()
		{
		}

		public static Change ToRemove(NodeProvider info)
		{
			return new Change(ChangeTag.ToRemove, info);
		}

		public static Change ToAdd(NodeProvider info)
		{
			return new Change(ChangeTag.ToAdd, info);
		}

		public NodeProvider AsToRemove()
		{
			this.ValidateTag(ChangeTag.ToRemove);
			return (NodeProvider)this.Value!;
		}

		public NodeProvider AsToAdd()
		{
			this.ValidateTag(ChangeTag.ToAdd);
			return (NodeProvider)this.Value!;
		}

		private void ValidateTag(ChangeTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum ChangeTag
	{
		ToRemove,
		ToAdd
	}
}