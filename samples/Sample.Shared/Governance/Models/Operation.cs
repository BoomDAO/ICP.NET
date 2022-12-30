using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Operation : EdjCase.ICP.Candid.Models.CandidVariantValueBase<OperationType>
	{
		public Operation(OperationType type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Operation()
		{
		}
		
		public static Operation RemoveHotKey(RemoveHotKey info)
		{
			return new Operation(OperationType.RemoveHotKey, info);
		}
		
		public RemoveHotKey AsRemoveHotKey()
		{
			this.ValidateType(OperationType.RemoveHotKey);
			return (RemoveHotKey)this.value!;
		}
		
		public static Operation AddHotKey(AddHotKey info)
		{
			return new Operation(OperationType.AddHotKey, info);
		}
		
		public AddHotKey AsAddHotKey()
		{
			this.ValidateType(OperationType.AddHotKey);
			return (AddHotKey)this.value!;
		}
		
		public class O2
		{
		}
		public static Operation StopDissolving(Operation.O2 info)
		{
			return new Operation(OperationType.StopDissolving, info);
		}
		
		public Operation.O2 AsStopDissolving()
		{
			this.ValidateType(OperationType.StopDissolving);
			return (Operation.O2)this.value!;
		}
		
		public class O3
		{
		}
		public static Operation StartDissolving(Operation.O3 info)
		{
			return new Operation(OperationType.StartDissolving, info);
		}
		
		public Operation.O3 AsStartDissolving()
		{
			this.ValidateType(OperationType.StartDissolving);
			return (Operation.O3)this.value!;
		}
		
		public static Operation IncreaseDissolveDelay(IncreaseDissolveDelay info)
		{
			return new Operation(OperationType.IncreaseDissolveDelay, info);
		}
		
		public IncreaseDissolveDelay AsIncreaseDissolveDelay()
		{
			this.ValidateType(OperationType.IncreaseDissolveDelay);
			return (IncreaseDissolveDelay)this.value!;
		}
		
		public class O5
		{
		}
		public static Operation JoinCommunityFund(Operation.O5 info)
		{
			return new Operation(OperationType.JoinCommunityFund, info);
		}
		
		public Operation.O5 AsJoinCommunityFund()
		{
			this.ValidateType(OperationType.JoinCommunityFund);
			return (Operation.O5)this.value!;
		}
		
		public static Operation SetDissolveTimestamp(SetDissolveTimestamp info)
		{
			return new Operation(OperationType.SetDissolveTimestamp, info);
		}
		
		public SetDissolveTimestamp AsSetDissolveTimestamp()
		{
			this.ValidateType(OperationType.SetDissolveTimestamp);
			return (SetDissolveTimestamp)this.value!;
		}
		
	}
	public enum OperationType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RemoveHotKey")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(RemoveHotKey))]
		RemoveHotKey,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("AddHotKey")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(AddHotKey))]
		AddHotKey,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("StopDissolving")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Operation.O2))]
		StopDissolving,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("StartDissolving")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Operation.O3))]
		StartDissolving,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("IncreaseDissolveDelay")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(IncreaseDissolveDelay))]
		IncreaseDissolveDelay,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("JoinCommunityFund")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Operation.O5))]
		JoinCommunityFund,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("SetDissolveTimestamp")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(SetDissolveTimestamp))]
		SetDissolveTimestamp,
	}
}

