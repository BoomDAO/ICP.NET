using System;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(ChangeTag))]
	public class Change
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public ChangeTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Change(ChangeTag tag, System.Object? value)
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
		
		public NodeProvider AsToRemove()
		{
			this.ValidateTag(ChangeTag.ToRemove);
			return (NodeProvider)this.Value!;
		}
		
		public static Change ToAdd(NodeProvider info)
		{
			return new Change(ChangeTag.ToAdd, info);
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
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ToRemove")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(NodeProvider))]
		ToRemove,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ToAdd")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(NodeProvider))]
		ToAdd,
	}
}

