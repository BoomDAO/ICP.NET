namespace Sample.Shared.Governance.Models
{
	public enum Command2Type
	{
		Spawn,
		Split,
		Configure,
		Merge,
		DisburseToNeuron,
		ClaimOrRefreshNeuron,
		MergeMaturity,
		Disburse,
	}
	public class Command2
	{
		public Command2Type Type { get; }
		private readonly object? value;
		
		public Command2(Command2Type type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static Command2 Spawn(Spawn info)
		{
			return new Command2(Command2Type.Spawn, info);
		}
		
		public Spawn AsSpawn()
		{
			this.ValidateType(Command2Type.Spawn);
			return (Spawn)this.value!;
		}
		
		public static Command2 Split(Split info)
		{
			return new Command2(Command2Type.Split, info);
		}
		
		public Split AsSplit()
		{
			this.ValidateType(Command2Type.Split);
			return (Split)this.value!;
		}
		
		public static Command2 Configure(Configure info)
		{
			return new Command2(Command2Type.Configure, info);
		}
		
		public Configure AsConfigure()
		{
			this.ValidateType(Command2Type.Configure);
			return (Configure)this.value!;
		}
		
		public static Command2 Merge(Merge info)
		{
			return new Command2(Command2Type.Merge, info);
		}
		
		public Merge AsMerge()
		{
			this.ValidateType(Command2Type.Merge);
			return (Merge)this.value!;
		}
		
		public static Command2 DisburseToNeuron(DisburseToNeuron info)
		{
			return new Command2(Command2Type.DisburseToNeuron, info);
		}
		
		public DisburseToNeuron AsDisburseToNeuron()
		{
			this.ValidateType(Command2Type.DisburseToNeuron);
			return (DisburseToNeuron)this.value!;
		}
		
		public static Command2 ClaimOrRefreshNeuron(ClaimOrRefresh info)
		{
			return new Command2(Command2Type.ClaimOrRefreshNeuron, info);
		}
		
		public ClaimOrRefresh AsClaimOrRefreshNeuron()
		{
			this.ValidateType(Command2Type.ClaimOrRefreshNeuron);
			return (ClaimOrRefresh)this.value!;
		}
		
		public static Command2 MergeMaturity(MergeMaturity info)
		{
			return new Command2(Command2Type.MergeMaturity, info);
		}
		
		public MergeMaturity AsMergeMaturity()
		{
			this.ValidateType(Command2Type.MergeMaturity);
			return (MergeMaturity)this.value!;
		}
		
		public static Command2 Disburse(Disburse info)
		{
			return new Command2(Command2Type.Disburse, info);
		}
		
		public Disburse AsDisburse()
		{
			this.ValidateType(Command2Type.Disburse);
			return (Disburse)this.value!;
		}
		
		private void ValidateType(Command2Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
