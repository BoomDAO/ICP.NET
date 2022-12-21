using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public enum Command_2Type
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
	public class Command_2 : EdjCase.ICP.Candid.CandidVariantValueBase<Command_2Type>
	{
		public Command_2(Command_2Type type, object? value)  : base(type, value)
		{
		}
		
		protected Command_2()
		{
		}
		
		public static Command_2 Spawn(Spawn info)
		{
			return new Command_2(Command_2Type.Spawn, info);
		}
		
		public Spawn AsSpawn()
		{
			this.ValidateType(Command_2Type.Spawn);
			return (Spawn)this.value!;
		}
		
		public static Command_2 Split(Split info)
		{
			return new Command_2(Command_2Type.Split, info);
		}
		
		public Split AsSplit()
		{
			this.ValidateType(Command_2Type.Split);
			return (Split)this.value!;
		}
		
		public static Command_2 Configure(Configure info)
		{
			return new Command_2(Command_2Type.Configure, info);
		}
		
		public Configure AsConfigure()
		{
			this.ValidateType(Command_2Type.Configure);
			return (Configure)this.value!;
		}
		
		public static Command_2 Merge(Merge info)
		{
			return new Command_2(Command_2Type.Merge, info);
		}
		
		public Merge AsMerge()
		{
			this.ValidateType(Command_2Type.Merge);
			return (Merge)this.value!;
		}
		
		public static Command_2 DisburseToNeuron(DisburseToNeuron info)
		{
			return new Command_2(Command_2Type.DisburseToNeuron, info);
		}
		
		public DisburseToNeuron AsDisburseToNeuron()
		{
			this.ValidateType(Command_2Type.DisburseToNeuron);
			return (DisburseToNeuron)this.value!;
		}
		
		public static Command_2 ClaimOrRefreshNeuron(ClaimOrRefresh info)
		{
			return new Command_2(Command_2Type.ClaimOrRefreshNeuron, info);
		}
		
		public ClaimOrRefresh AsClaimOrRefreshNeuron()
		{
			this.ValidateType(Command_2Type.ClaimOrRefreshNeuron);
			return (ClaimOrRefresh)this.value!;
		}
		
		public static Command_2 MergeMaturity(MergeMaturity info)
		{
			return new Command_2(Command_2Type.MergeMaturity, info);
		}
		
		public MergeMaturity AsMergeMaturity()
		{
			this.ValidateType(Command_2Type.MergeMaturity);
			return (MergeMaturity)this.value!;
		}
		
		public static Command_2 Disburse(Disburse info)
		{
			return new Command_2(Command_2Type.Disburse, info);
		}
		
		public Disburse AsDisburse()
		{
			this.ValidateType(Command_2Type.Disburse);
			return (Disburse)this.value!;
		}
		
	}
}

