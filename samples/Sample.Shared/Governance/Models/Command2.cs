using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Command2 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Command2Type>
	{
		public Command2(Command2Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Command2()
		{
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
		
	}
	public enum Command2Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Spawn")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Spawn))]
		Spawn,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Split")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Split))]
		Split,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Configure")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Configure))]
		Configure,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Merge")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Merge))]
		Merge,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("DisburseToNeuron")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(DisburseToNeuron))]
		DisburseToNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ClaimOrRefreshNeuron")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ClaimOrRefresh))]
		ClaimOrRefreshNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MergeMaturity")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(MergeMaturity))]
		MergeMaturity,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Disburse")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Disburse))]
		Disburse,
	}
}

