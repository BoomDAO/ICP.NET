using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(Command2Tag))]
	public class Command2
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public Command2Tag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Command2(Command2Tag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Command2()
		{
		}
		
		public static Command2 Spawn(Spawn info)
		{
			return new Command2(Command2Tag.Spawn, info);
		}
		
		public Spawn AsSpawn()
		{
			this.ValidateTag(Command2Tag.Spawn);
			return (Spawn)this.Value!;
		}
		
		public static Command2 Split(Split info)
		{
			return new Command2(Command2Tag.Split, info);
		}
		
		public Split AsSplit()
		{
			this.ValidateTag(Command2Tag.Split);
			return (Split)this.Value!;
		}
		
		public static Command2 Configure(Configure info)
		{
			return new Command2(Command2Tag.Configure, info);
		}
		
		public Configure AsConfigure()
		{
			this.ValidateTag(Command2Tag.Configure);
			return (Configure)this.Value!;
		}
		
		public static Command2 Merge(Merge info)
		{
			return new Command2(Command2Tag.Merge, info);
		}
		
		public Merge AsMerge()
		{
			this.ValidateTag(Command2Tag.Merge);
			return (Merge)this.Value!;
		}
		
		public static Command2 DisburseToNeuron(DisburseToNeuron info)
		{
			return new Command2(Command2Tag.DisburseToNeuron, info);
		}
		
		public DisburseToNeuron AsDisburseToNeuron()
		{
			this.ValidateTag(Command2Tag.DisburseToNeuron);
			return (DisburseToNeuron)this.Value!;
		}
		
		public static Command2 ClaimOrRefreshNeuron(ClaimOrRefresh info)
		{
			return new Command2(Command2Tag.ClaimOrRefreshNeuron, info);
		}
		
		public ClaimOrRefresh AsClaimOrRefreshNeuron()
		{
			this.ValidateTag(Command2Tag.ClaimOrRefreshNeuron);
			return (ClaimOrRefresh)this.Value!;
		}
		
		public static Command2 MergeMaturity(MergeMaturity info)
		{
			return new Command2(Command2Tag.MergeMaturity, info);
		}
		
		public MergeMaturity AsMergeMaturity()
		{
			this.ValidateTag(Command2Tag.MergeMaturity);
			return (MergeMaturity)this.Value!;
		}
		
		public static Command2 Disburse(Disburse info)
		{
			return new Command2(Command2Tag.Disburse, info);
		}
		
		public Disburse AsDisburse()
		{
			this.ValidateTag(Command2Tag.Disburse);
			return (Disburse)this.Value!;
		}
		
		private void ValidateTag(Command2Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum Command2Tag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Spawn")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Spawn))]
		Spawn,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Split")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Split))]
		Split,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Configure")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Configure))]
		Configure,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Merge")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Merge))]
		Merge,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("DisburseToNeuron")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(DisburseToNeuron))]
		DisburseToNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ClaimOrRefreshNeuron")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(ClaimOrRefresh))]
		ClaimOrRefreshNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MergeMaturity")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(MergeMaturity))]
		MergeMaturity,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Disburse")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Disburse))]
		Disburse,
	}
}

