using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class Operation
	{
		[VariantTagProperty]
		public OperationTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Operation(OperationTag tag, object? value)
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

		public static Operation AddHotKey(AddHotKey info)
		{
			return new Operation(OperationTag.AddHotKey, info);
		}

		public static Operation ChangeAutoStakeMaturity(ChangeAutoStakeMaturity info)
		{
			return new Operation(OperationTag.ChangeAutoStakeMaturity, info);
		}

		public static Operation StopDissolving(Operation.StopDissolvingInfo info)
		{
			return new Operation(OperationTag.StopDissolving, info);
		}

		public static Operation StartDissolving(Operation.StartDissolvingInfo info)
		{
			return new Operation(OperationTag.StartDissolving, info);
		}

		public static Operation IncreaseDissolveDelay(IncreaseDissolveDelay info)
		{
			return new Operation(OperationTag.IncreaseDissolveDelay, info);
		}

		public static Operation JoinCommunityFund(Operation.JoinCommunityFundInfo info)
		{
			return new Operation(OperationTag.JoinCommunityFund, info);
		}

		public static Operation LeaveCommunityFund(Operation.LeaveCommunityFundInfo info)
		{
			return new Operation(OperationTag.LeaveCommunityFund, info);
		}

		public static Operation SetDissolveTimestamp(SetDissolveTimestamp info)
		{
			return new Operation(OperationTag.SetDissolveTimestamp, info);
		}

		public RemoveHotKey AsRemoveHotKey()
		{
			this.ValidateTag(OperationTag.RemoveHotKey);
			return (RemoveHotKey)this.Value!;
		}

		public AddHotKey AsAddHotKey()
		{
			this.ValidateTag(OperationTag.AddHotKey);
			return (AddHotKey)this.Value!;
		}

		public ChangeAutoStakeMaturity AsChangeAutoStakeMaturity()
		{
			this.ValidateTag(OperationTag.ChangeAutoStakeMaturity);
			return (ChangeAutoStakeMaturity)this.Value!;
		}

		public Operation.StopDissolvingInfo AsStopDissolving()
		{
			this.ValidateTag(OperationTag.StopDissolving);
			return (Operation.StopDissolvingInfo)this.Value!;
		}

		public Operation.StartDissolvingInfo AsStartDissolving()
		{
			this.ValidateTag(OperationTag.StartDissolving);
			return (Operation.StartDissolvingInfo)this.Value!;
		}

		public IncreaseDissolveDelay AsIncreaseDissolveDelay()
		{
			this.ValidateTag(OperationTag.IncreaseDissolveDelay);
			return (IncreaseDissolveDelay)this.Value!;
		}

		public Operation.JoinCommunityFundInfo AsJoinCommunityFund()
		{
			this.ValidateTag(OperationTag.JoinCommunityFund);
			return (Operation.JoinCommunityFundInfo)this.Value!;
		}

		public Operation.LeaveCommunityFundInfo AsLeaveCommunityFund()
		{
			this.ValidateTag(OperationTag.LeaveCommunityFund);
			return (Operation.LeaveCommunityFundInfo)this.Value!;
		}

		public SetDissolveTimestamp AsSetDissolveTimestamp()
		{
			this.ValidateTag(OperationTag.SetDissolveTimestamp);
			return (SetDissolveTimestamp)this.Value!;
		}

		private void ValidateTag(OperationTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}

		public class StopDissolvingInfo
		{
			public StopDissolvingInfo()
			{
			}
		}

		public class StartDissolvingInfo
		{
			public StartDissolvingInfo()
			{
			}
		}

		public class JoinCommunityFundInfo
		{
			public JoinCommunityFundInfo()
			{
			}
		}

		public class LeaveCommunityFundInfo
		{
			public LeaveCommunityFundInfo()
			{
			}
		}
	}

	public enum OperationTag
	{
		RemoveHotKey,
		AddHotKey,
		ChangeAutoStakeMaturity,
		StopDissolving,
		StartDissolving,
		IncreaseDissolveDelay,
		JoinCommunityFund,
		LeaveCommunityFund,
		SetDissolveTimestamp
	}
}