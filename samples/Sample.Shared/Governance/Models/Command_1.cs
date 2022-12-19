namespace Sample.Shared.Governance.Models
{
	public enum Command_1Type
	{
		Error,
		Spawn,
		Split,
		Follow,
		ClaimOrRefresh,
		Configure,
		RegisterVote,
		Merge,
		DisburseToNeuron,
		MakeProposal,
		MergeMaturity,
		Disburse,
	}
	public class Command_1 : EdjCase.ICP.Candid.CandidVariantValueBase<Command_1Type>
	{
		public Command_1(Command_1Type type, object? value)  : base(type, value)
		{
		}
		
		protected Command_1()
		{
		}
		
		public static Command_1 Error(GovernanceError info)
		{
			return new Command_1(Command_1Type.Error, info);
		}
		
		public GovernanceError AsError()
		{
			this.ValidateType(Command_1Type.Error);
			return (GovernanceError)this.value!;
		}
		
		public static Command_1 Spawn(SpawnResponse info)
		{
			return new Command_1(Command_1Type.Spawn, info);
		}
		
		public SpawnResponse AsSpawn()
		{
			this.ValidateType(Command_1Type.Spawn);
			return (SpawnResponse)this.value!;
		}
		
		public static Command_1 Split(SpawnResponse info)
		{
			return new Command_1(Command_1Type.Split, info);
		}
		
		public SpawnResponse AsSplit()
		{
			this.ValidateType(Command_1Type.Split);
			return (SpawnResponse)this.value!;
		}
		
		public static Command_1 Follow(FollowInfo info)
		{
			return new Command_1(Command_1Type.Follow, info);
		}
		
		public FollowInfo AsFollow()
		{
			this.ValidateType(Command_1Type.Follow);
			return (FollowInfo)this.value!;
		}
		
		public static Command_1 ClaimOrRefresh(ClaimOrRefreshResponse info)
		{
			return new Command_1(Command_1Type.ClaimOrRefresh, info);
		}
		
		public ClaimOrRefreshResponse AsClaimOrRefresh()
		{
			this.ValidateType(Command_1Type.ClaimOrRefresh);
			return (ClaimOrRefreshResponse)this.value!;
		}
		
		public static Command_1 Configure(ConfigureInfo info)
		{
			return new Command_1(Command_1Type.Configure, info);
		}
		
		public ConfigureInfo AsConfigure()
		{
			this.ValidateType(Command_1Type.Configure);
			return (ConfigureInfo)this.value!;
		}
		
		public static Command_1 RegisterVote(RegisterVoteInfo info)
		{
			return new Command_1(Command_1Type.RegisterVote, info);
		}
		
		public RegisterVoteInfo AsRegisterVote()
		{
			this.ValidateType(Command_1Type.RegisterVote);
			return (RegisterVoteInfo)this.value!;
		}
		
		public static Command_1 Merge(MergeInfo info)
		{
			return new Command_1(Command_1Type.Merge, info);
		}
		
		public MergeInfo AsMerge()
		{
			this.ValidateType(Command_1Type.Merge);
			return (MergeInfo)this.value!;
		}
		
		public static Command_1 DisburseToNeuron(SpawnResponse info)
		{
			return new Command_1(Command_1Type.DisburseToNeuron, info);
		}
		
		public SpawnResponse AsDisburseToNeuron()
		{
			this.ValidateType(Command_1Type.DisburseToNeuron);
			return (SpawnResponse)this.value!;
		}
		
		public static Command_1 MakeProposal(MakeProposalResponse info)
		{
			return new Command_1(Command_1Type.MakeProposal, info);
		}
		
		public MakeProposalResponse AsMakeProposal()
		{
			this.ValidateType(Command_1Type.MakeProposal);
			return (MakeProposalResponse)this.value!;
		}
		
		public static Command_1 MergeMaturity(MergeMaturityResponse info)
		{
			return new Command_1(Command_1Type.MergeMaturity, info);
		}
		
		public MergeMaturityResponse AsMergeMaturity()
		{
			this.ValidateType(Command_1Type.MergeMaturity);
			return (MergeMaturityResponse)this.value!;
		}
		
		public static Command_1 Disburse(DisburseResponse info)
		{
			return new Command_1(Command_1Type.Disburse, info);
		}
		
		public DisburseResponse AsDisburse()
		{
			this.ValidateType(Command_1Type.Disburse);
			return (DisburseResponse)this.value!;
		}
		
		public class FollowInfo
		{
		}
		public class ConfigureInfo
		{
		}
		public class RegisterVoteInfo
		{
		}
		public class MergeInfo
		{
		}
	}
}
