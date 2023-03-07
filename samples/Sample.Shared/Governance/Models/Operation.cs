using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(OperationTag))]
	public class Operation
	{
		[VariantTagProperty()]
		public OperationTag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

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

		public static Operation StopDissolving(Operation.StopDissolvingRecord info)
		{
			return new Operation(OperationTag.StopDissolving, info);
		}

		public static Operation StartDissolving(Operation.StartDissolvingRecord info)
		{
			return new Operation(OperationTag.StartDissolving, info);
		}

		public static Operation IncreaseDissolveDelay(IncreaseDissolveDelay info)
		{
			return new Operation(OperationTag.IncreaseDissolveDelay, info);
		}

		public static Operation JoinCommunityFund(Operation.JoinCommunityFundRecord info)
		{
			return new Operation(OperationTag.JoinCommunityFund, info);
		}

		public static Operation LeaveCommunityFund(Operation.LeaveCommunityFundRecord info)
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

		public Operation.StopDissolvingRecord AsStopDissolving()
		{
			this.ValidateTag(OperationTag.StopDissolving);
			return (Operation.StopDissolvingRecord)this.Value!;
		}

		public Operation.StartDissolvingRecord AsStartDissolving()
		{
			this.ValidateTag(OperationTag.StartDissolving);
			return (Operation.StartDissolvingRecord)this.Value!;
		}

		public IncreaseDissolveDelay AsIncreaseDissolveDelay()
		{
			this.ValidateTag(OperationTag.IncreaseDissolveDelay);
			return (IncreaseDissolveDelay)this.Value!;
		}

		public Operation.JoinCommunityFundRecord AsJoinCommunityFund()
		{
			this.ValidateTag(OperationTag.JoinCommunityFund);
			return (Operation.JoinCommunityFundRecord)this.Value!;
		}

		public Operation.LeaveCommunityFundRecord AsLeaveCommunityFund()
		{
			this.ValidateTag(OperationTag.LeaveCommunityFund);
			return (Operation.LeaveCommunityFundRecord)this.Value!;
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

		public class StopDissolvingRecord
		{
			public StopDissolvingRecord()
			{
			}
		}

		public class StartDissolvingRecord
		{
			public StartDissolvingRecord()
			{
			}
		}

		public class JoinCommunityFundRecord
		{
			public JoinCommunityFundRecord()
			{
			}
		}

		public class LeaveCommunityFundRecord
		{
			public LeaveCommunityFundRecord()
			{
			}
		}
	}

	public enum OperationTag
	{
		[VariantOptionType(typeof(RemoveHotKey))]
		RemoveHotKey,
		[VariantOptionType(typeof(AddHotKey))]
		AddHotKey,
		[VariantOptionType(typeof(ChangeAutoStakeMaturity))]
		ChangeAutoStakeMaturity,
		[VariantOptionType(typeof(Operation.StopDissolvingRecord))]
		StopDissolving,
		[VariantOptionType(typeof(Operation.StartDissolvingRecord))]
		StartDissolving,
		[VariantOptionType(typeof(IncreaseDissolveDelay))]
		IncreaseDissolveDelay,
		[VariantOptionType(typeof(Operation.JoinCommunityFundRecord))]
		JoinCommunityFund,
		[VariantOptionType(typeof(Operation.LeaveCommunityFundRecord))]
		LeaveCommunityFund,
		[VariantOptionType(typeof(SetDissolveTimestamp))]
		SetDissolveTimestamp
	}
}