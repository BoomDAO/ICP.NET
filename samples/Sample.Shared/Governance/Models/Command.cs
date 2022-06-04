namespace Sample.Shared.Governance.Models
{
	public enum CommandType
	{
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
	public class Command : EdjCase.ICP.Candid.CandidVariantValueBase<CommandType>
	{
		public Command(CommandType type, object? value)  : base(type, value)
		{
		}
		
		protected Command()
		{
		}
		
		public static Command Spawn(Spawn info)
		{
			return new Command(CommandType.Spawn, info);
		}
		
		public Spawn AsSpawn()
		{
			this.ValidateType(CommandType.Spawn);
			return (Spawn)this.value!;
		}
		
		public static Command Split(Split info)
		{
			return new Command(CommandType.Split, info);
		}
		
		public Split AsSplit()
		{
			this.ValidateType(CommandType.Split);
			return (Split)this.value!;
		}
		
		public static Command Follow(Follow info)
		{
			return new Command(CommandType.Follow, info);
		}
		
		public Follow AsFollow()
		{
			this.ValidateType(CommandType.Follow);
			return (Follow)this.value!;
		}
		
		public static Command ClaimOrRefresh(ClaimOrRefresh info)
		{
			return new Command(CommandType.ClaimOrRefresh, info);
		}
		
		public ClaimOrRefresh AsClaimOrRefresh()
		{
			this.ValidateType(CommandType.ClaimOrRefresh);
			return (ClaimOrRefresh)this.value!;
		}
		
		public static Command Configure(Configure info)
		{
			return new Command(CommandType.Configure, info);
		}
		
		public Configure AsConfigure()
		{
			this.ValidateType(CommandType.Configure);
			return (Configure)this.value!;
		}
		
		public static Command RegisterVote(RegisterVote info)
		{
			return new Command(CommandType.RegisterVote, info);
		}
		
		public RegisterVote AsRegisterVote()
		{
			this.ValidateType(CommandType.RegisterVote);
			return (RegisterVote)this.value!;
		}
		
		public static Command Merge(Merge info)
		{
			return new Command(CommandType.Merge, info);
		}
		
		public Merge AsMerge()
		{
			this.ValidateType(CommandType.Merge);
			return (Merge)this.value!;
		}
		
		public static Command DisburseToNeuron(DisburseToNeuron info)
		{
			return new Command(CommandType.DisburseToNeuron, info);
		}
		
		public DisburseToNeuron AsDisburseToNeuron()
		{
			this.ValidateType(CommandType.DisburseToNeuron);
			return (DisburseToNeuron)this.value!;
		}
		
		public static Command MakeProposal(Proposal info)
		{
			return new Command(CommandType.MakeProposal, info);
		}
		
		public Proposal AsMakeProposal()
		{
			this.ValidateType(CommandType.MakeProposal);
			return (Proposal)this.value!;
		}
		
		public static Command MergeMaturity(MergeMaturity info)
		{
			return new Command(CommandType.MergeMaturity, info);
		}
		
		public MergeMaturity AsMergeMaturity()
		{
			this.ValidateType(CommandType.MergeMaturity);
			return (MergeMaturity)this.value!;
		}
		
		public static Command Disburse(Disburse info)
		{
			return new Command(CommandType.Disburse, info);
		}
		
		public Disburse AsDisburse()
		{
			this.ValidateType(CommandType.Disburse);
			return (Disburse)this.value!;
		}
		
	}
}
