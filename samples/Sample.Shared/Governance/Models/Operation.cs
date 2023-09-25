using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Operation
	{
		[VariantTagProperty()]
		public OperationTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public RemoveHotKey? RemoveHotKey { get => this.Tag == OperationTag.RemoveHotKey ? (RemoveHotKey)this.Value : default; set => (this.Tag, this.Value) = (OperationTag.RemoveHotKey, value); }

		public AddHotKey? AddHotKey { get => this.Tag == OperationTag.AddHotKey ? (AddHotKey)this.Value : default; set => (this.Tag, this.Value) = (OperationTag.AddHotKey, value); }

		public ChangeAutoStakeMaturity? ChangeAutoStakeMaturity { get => this.Tag == OperationTag.ChangeAutoStakeMaturity ? (ChangeAutoStakeMaturity)this.Value : default; set => (this.Tag, this.Value) = (OperationTag.ChangeAutoStakeMaturity, value); }

		public Operation.StopDissolvingInfo? StopDissolving { get => this.Tag == OperationTag.StopDissolving ? (Operation.StopDissolvingInfo)this.Value : default; set => (this.Tag, this.Value) = (OperationTag.StopDissolving, value); }

		public Operation.StartDissolvingInfo? StartDissolving { get => this.Tag == OperationTag.StartDissolving ? (Operation.StartDissolvingInfo)this.Value : default; set => (this.Tag, this.Value) = (OperationTag.StartDissolving, value); }

		public IncreaseDissolveDelay? IncreaseDissolveDelay { get => this.Tag == OperationTag.IncreaseDissolveDelay ? (IncreaseDissolveDelay)this.Value : default; set => (this.Tag, this.Value) = (OperationTag.IncreaseDissolveDelay, value); }

		public Operation.JoinCommunityFundInfo? JoinCommunityFund { get => this.Tag == OperationTag.JoinCommunityFund ? (Operation.JoinCommunityFundInfo)this.Value : default; set => (this.Tag, this.Value) = (OperationTag.JoinCommunityFund, value); }

		public Operation.LeaveCommunityFundInfo? LeaveCommunityFund { get => this.Tag == OperationTag.LeaveCommunityFund ? (Operation.LeaveCommunityFundInfo)this.Value : default; set => (this.Tag, this.Value) = (OperationTag.LeaveCommunityFund, value); }

		public SetDissolveTimestamp? SetDissolveTimestamp { get => this.Tag == OperationTag.SetDissolveTimestamp ? (SetDissolveTimestamp)this.Value : default; set => (this.Tag, this.Value) = (OperationTag.SetDissolveTimestamp, value); }

		public Operation(OperationTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Operation()
		{
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