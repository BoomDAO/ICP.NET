using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public enum OperationType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RemoveHotKey")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(RemoveHotKey))]
		RemoveHotKey,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("AddHotKey")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(AddHotKey))]
		AddHotKey,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("StopDissolving")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Operation))]
		StopDissolving,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("StartDissolving")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Operation))]
		StartDissolving,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("IncreaseDissolveDelay")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(IncreaseDissolveDelay))]
		IncreaseDissolveDelay,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("JoinCommunityFund")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Operation))]
		JoinCommunityFund,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("SetDissolveTimestamp")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(SetDissolveTimestamp))]
		SetDissolveTimestamp,
	}
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
		
		public static Operation StopDissolving(Operation info)
		{
			return new Operation(OperationType.StopDissolving, info);
		}
		
		public Operation AsStopDissolving()
		{
			this.ValidateType(OperationType.StopDissolving);
			return (Operation)this.value!;
		}
		
		public static Operation StartDissolving(Operation info)
		{
			return new Operation(OperationType.StartDissolving, info);
		}
		
		public Operation AsStartDissolving()
		{
			this.ValidateType(OperationType.StartDissolving);
			return (Operation)this.value!;
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
		
		public static Operation JoinCommunityFund(Operation info)
		{
			return new Operation(OperationType.JoinCommunityFund, info);
		}
		
		public Operation AsJoinCommunityFund()
		{
			this.ValidateType(OperationType.JoinCommunityFund);
			return (Operation)this.value!;
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
}

