using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(OperationTag))]
	public class Operation
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public OperationTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Operation(OperationTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Operation()
		{
		}
		
		public static Operation RemoveHotKey(RemoveHotKey info)
		{
			return new Operation(OperationTag.RemoveHotKey, info);
		}
		
		public RemoveHotKey AsRemoveHotKey()
		{
			this.ValidateType(OperationTag.RemoveHotKey);
			return (RemoveHotKey)this.Value!;
		}
		
		public static Operation AddHotKey(AddHotKey info)
		{
			return new Operation(OperationTag.AddHotKey, info);
		}
		
		public AddHotKey AsAddHotKey()
		{
			this.ValidateType(OperationTag.AddHotKey);
			return (AddHotKey)this.Value!;
		}
		
		public class O2
		{
		}
		public static Operation StopDissolving(Operation.O2 info)
		{
			return new Operation(OperationTag.StopDissolving, info);
		}
		
		public Operation.O2 AsStopDissolving()
		{
			this.ValidateType(OperationTag.StopDissolving);
			return (Operation.O2)this.Value!;
		}
		
		public class O3
		{
		}
		public static Operation StartDissolving(Operation.O3 info)
		{
			return new Operation(OperationTag.StartDissolving, info);
		}
		
		public Operation.O3 AsStartDissolving()
		{
			this.ValidateType(OperationTag.StartDissolving);
			return (Operation.O3)this.Value!;
		}
		
		public static Operation IncreaseDissolveDelay(IncreaseDissolveDelay info)
		{
			return new Operation(OperationTag.IncreaseDissolveDelay, info);
		}
		
		public IncreaseDissolveDelay AsIncreaseDissolveDelay()
		{
			this.ValidateType(OperationTag.IncreaseDissolveDelay);
			return (IncreaseDissolveDelay)this.Value!;
		}
		
		public class O5
		{
		}
		public static Operation JoinCommunityFund(Operation.O5 info)
		{
			return new Operation(OperationTag.JoinCommunityFund, info);
		}
		
		public Operation.O5 AsJoinCommunityFund()
		{
			this.ValidateType(OperationTag.JoinCommunityFund);
			return (Operation.O5)this.Value!;
		}
		
		public static Operation SetDissolveTimestamp(SetDissolveTimestamp info)
		{
			return new Operation(OperationTag.SetDissolveTimestamp, info);
		}
		
		public SetDissolveTimestamp AsSetDissolveTimestamp()
		{
			this.ValidateType(OperationTag.SetDissolveTimestamp);
			return (SetDissolveTimestamp)this.Value!;
		}
		
		private void ValidateType(OperationTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum OperationTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RemoveHotKey")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(RemoveHotKey))]
		RemoveHotKey,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("AddHotKey")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(AddHotKey))]
		AddHotKey,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("StopDissolving")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Operation.O2))]
		StopDissolving,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("StartDissolving")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Operation.O3))]
		StartDissolving,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("IncreaseDissolveDelay")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(IncreaseDissolveDelay))]
		IncreaseDissolveDelay,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("JoinCommunityFund")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Operation.O5))]
		JoinCommunityFund,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("SetDissolveTimestamp")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(SetDissolveTimestamp))]
		SetDissolveTimestamp,
	}
}

