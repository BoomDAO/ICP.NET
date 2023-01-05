using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(CommandTag))]
	public class Command
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public CommandTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Command(CommandTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Command()
		{
		}
		
		public static Command Spawn(Spawn info)
		{
			return new Command(CommandTag.Spawn, info);
		}
		
		public Spawn AsSpawn()
		{
			this.ValidateTag(CommandTag.Spawn);
			return (Spawn)this.Value!;
		}
		
		public static Command Split(Split info)
		{
			return new Command(CommandTag.Split, info);
		}
		
		public Split AsSplit()
		{
			this.ValidateTag(CommandTag.Split);
			return (Split)this.Value!;
		}
		
		public static Command Follow(Follow info)
		{
			return new Command(CommandTag.Follow, info);
		}
		
		public Follow AsFollow()
		{
			this.ValidateTag(CommandTag.Follow);
			return (Follow)this.Value!;
		}
		
		public static Command ClaimOrRefresh(ClaimOrRefresh info)
		{
			return new Command(CommandTag.ClaimOrRefresh, info);
		}
		
		public ClaimOrRefresh AsClaimOrRefresh()
		{
			this.ValidateTag(CommandTag.ClaimOrRefresh);
			return (ClaimOrRefresh)this.Value!;
		}
		
		public static Command Configure(Configure info)
		{
			return new Command(CommandTag.Configure, info);
		}
		
		public Configure AsConfigure()
		{
			this.ValidateTag(CommandTag.Configure);
			return (Configure)this.Value!;
		}
		
		public static Command RegisterVote(RegisterVote info)
		{
			return new Command(CommandTag.RegisterVote, info);
		}
		
		public RegisterVote AsRegisterVote()
		{
			this.ValidateTag(CommandTag.RegisterVote);
			return (RegisterVote)this.Value!;
		}
		
		public static Command Merge(Merge info)
		{
			return new Command(CommandTag.Merge, info);
		}
		
		public Merge AsMerge()
		{
			this.ValidateTag(CommandTag.Merge);
			return (Merge)this.Value!;
		}
		
		public static Command DisburseToNeuron(DisburseToNeuron info)
		{
			return new Command(CommandTag.DisburseToNeuron, info);
		}
		
		public DisburseToNeuron AsDisburseToNeuron()
		{
			this.ValidateTag(CommandTag.DisburseToNeuron);
			return (DisburseToNeuron)this.Value!;
		}
		
		public static Command MakeProposal(Proposal info)
		{
			return new Command(CommandTag.MakeProposal, info);
		}
		
		public Proposal AsMakeProposal()
		{
			this.ValidateTag(CommandTag.MakeProposal);
			return (Proposal)this.Value!;
		}
		
		public static Command MergeMaturity(MergeMaturity info)
		{
			return new Command(CommandTag.MergeMaturity, info);
		}
		
		public MergeMaturity AsMergeMaturity()
		{
			this.ValidateTag(CommandTag.MergeMaturity);
			return (MergeMaturity)this.Value!;
		}
		
		public static Command Disburse(Disburse info)
		{
			return new Command(CommandTag.Disburse, info);
		}
		
		public Disburse AsDisburse()
		{
			this.ValidateTag(CommandTag.Disburse);
			return (Disburse)this.Value!;
		}
		
		private void ValidateTag(CommandTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum CommandTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Spawn")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Spawn))]
		Spawn,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Split")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Split))]
		Split,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Follow")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Follow))]
		Follow,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ClaimOrRefresh")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(ClaimOrRefresh))]
		ClaimOrRefresh,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Configure")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Configure))]
		Configure,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RegisterVote")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(RegisterVote))]
		RegisterVote,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Merge")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Merge))]
		Merge,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("DisburseToNeuron")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(DisburseToNeuron))]
		DisburseToNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MakeProposal")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Proposal))]
		MakeProposal,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MergeMaturity")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(MergeMaturity))]
		MergeMaturity,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Disburse")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Disburse))]
		Disburse,
	}
}

