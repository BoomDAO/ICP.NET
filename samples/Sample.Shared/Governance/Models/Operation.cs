using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public enum OperationType
	{
		RemoveHotKey,
		AddHotKey,
		StopDissolving,
		StartDissolving,
		IncreaseDissolveDelay,
		JoinCommunityFund,
		SetDissolveTimestamp,
	}
	public class Operation : EdjCase.ICP.Candid.CandidVariantValueBase<OperationType>
	{
		public Operation(OperationType type, object? value)  : base(type, value)
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
		
		public static Operation StopDissolving(StopDissolvingInfo info)
		{
			return new Operation(OperationType.StopDissolving, info);
		}
		
		public StopDissolvingInfo AsStopDissolving()
		{
			this.ValidateType(OperationType.StopDissolving);
			return (StopDissolvingInfo)this.value!;
		}
		
		public static Operation StartDissolving(StartDissolvingInfo info)
		{
			return new Operation(OperationType.StartDissolving, info);
		}
		
		public StartDissolvingInfo AsStartDissolving()
		{
			this.ValidateType(OperationType.StartDissolving);
			return (StartDissolvingInfo)this.value!;
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
		
		public static Operation JoinCommunityFund(JoinCommunityFundInfo info)
		{
			return new Operation(OperationType.JoinCommunityFund, info);
		}
		
		public JoinCommunityFundInfo AsJoinCommunityFund()
		{
			this.ValidateType(OperationType.JoinCommunityFund);
			return (JoinCommunityFundInfo)this.value!;
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
		
		public class StopDissolvingInfo
		{
		}
		public class StartDissolvingInfo
		{
		}
		public class JoinCommunityFundInfo
		{
		}
	}
}

