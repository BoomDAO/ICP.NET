using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public enum ChangeType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ToRemove")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(NodeProvider))]
		ToRemove,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ToAdd")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(NodeProvider))]
		ToAdd,
	}
	public class Change : EdjCase.ICP.Candid.Models.CandidVariantValueBase<ChangeType>
	{
		public Change(ChangeType type, System.Object? value)  : base(type, value)
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

