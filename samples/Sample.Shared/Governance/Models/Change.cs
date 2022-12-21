using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public enum ChangeType
	{
		ToRemove,
		ToAdd,
	}
	public class Change : EdjCase.ICP.Candid.CandidVariantValueBase<ChangeType>
	{
		public Change(ChangeType type, object? value)  : base(type, value)
		{
		}
		
		protected Change()
		{
		}
		
		public static Change ToRemove(NodeProvider info)
		{
			return new Change(ChangeType.ToRemove, info);
		}
		
		public NodeProvider AsToRemove()
		{
			this.ValidateType(ChangeType.ToRemove);
			return (NodeProvider)this.value!;
		}
		
		public static Change ToAdd(NodeProvider info)
		{
			return new Change(ChangeType.ToAdd, info);
		}
		
		public NodeProvider AsToAdd()
		{
			this.ValidateType(ChangeType.ToAdd);
			return (NodeProvider)this.value!;
		}
		
	}
}

