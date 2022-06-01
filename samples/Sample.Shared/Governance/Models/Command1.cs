namespace Sample.Shared.Governance.Models
{
	public enum Command1Type
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
	public class Command1
	{
		public Command1Type Type { get; }
		private readonly object? value;
		
		public Command1(Command1Type type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static Command1 Error(GovernanceError info)
		{
			return new Command1(Command1Type.Error, info);
		}
		
		public GovernanceError AsError()
		{
			this.ValidateType(Command1Type.Error);
			return (GovernanceError)this.value!;
		}
		
		public static Command1 Spawn(SpawnResponse info)
		{
			return new Command1(Command1Type.Spawn, info);
		}
		
		public SpawnResponse AsSpawn()
		{
			this.ValidateType(Command1Type.Spawn);
			return (SpawnResponse)this.value!;
		}
		
		public static Command1 Split(SpawnResponse info)
		{
			return new Command1(Command1Type.Split, info);
		}
		
		public SpawnResponse AsSplit()
		{
			this.ValidateType(Command1Type.Split);
			return (SpawnResponse)this.value!;
		}
		
		public static Command1 Follow(FollowInfo info)
		{
			return new Command1(Command1Type.Follow, info);
		}
		
		public FollowInfo AsFollow()
		{
			this.ValidateType(Command1Type.Follow);
			return (FollowInfo)this.value!;
		}
		
		public static Command1 ClaimOrRefresh(ClaimOrRefreshResponse info)
		{
			return new Command1(Command1Type.ClaimOrRefresh, info);
		}
		
		public ClaimOrRefreshResponse AsClaimOrRefresh()
		{
			this.ValidateType(Command1Type.ClaimOrRefresh);
			return (ClaimOrRefreshResponse)this.value!;
		}
		
		public static Command1 Configure(ConfigureInfo info)
		{
			return new Command1(Command1Type.Configure, info);
		}
		
		public ConfigureInfo AsConfigure()
		{
			this.ValidateType(Command1Type.Configure);
			return (ConfigureInfo)this.value!;
		}
		
		public static Command1 RegisterVote(RegisterVoteInfo info)
		{
			return new Command1(Command1Type.RegisterVote, info);
		}
		
		public RegisterVoteInfo AsRegisterVote()
		{
			this.ValidateType(Command1Type.RegisterVote);
			return (RegisterVoteInfo)this.value!;
		}
		
		public static Command1 Merge(MergeInfo info)
		{
			return new Command1(Command1Type.Merge, info);
		}
		
		public MergeInfo AsMerge()
		{
			this.ValidateType(Command1Type.Merge);
			return (MergeInfo)this.value!;
		}
		
		public static Command1 DisburseToNeuron(SpawnResponse info)
		{
			return new Command1(Command1Type.DisburseToNeuron, info);
		}
		
		public SpawnResponse AsDisburseToNeuron()
		{
			this.ValidateType(Command1Type.DisburseToNeuron);
			return (SpawnResponse)this.value!;
		}
		
		public static Command1 MakeProposal(MakeProposalResponse info)
		{
			return new Command1(Command1Type.MakeProposal, info);
		}
		
		public MakeProposalResponse AsMakeProposal()
		{
			this.ValidateType(Command1Type.MakeProposal);
			return (MakeProposalResponse)this.value!;
		}
		
		public static Command1 MergeMaturity(MergeMaturityResponse info)
		{
			return new Command1(Command1Type.MergeMaturity, info);
		}
		
		public MergeMaturityResponse AsMergeMaturity()
		{
			this.ValidateType(Command1Type.MergeMaturity);
			return (MergeMaturityResponse)this.value!;
		}
		
		public static Command1 Disburse(DisburseResponse info)
		{
			return new Command1(Command1Type.Disburse, info);
		}
		
		public DisburseResponse AsDisburse()
		{
			this.ValidateType(Command1Type.Disburse);
			return (DisburseResponse)this.value!;
		}
		
		private void ValidateType(Command1Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
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
