using System;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(Command1Tag))]
	public class Command1
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public Command1Tag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Command1(Command1Tag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Command1()
		{
		}
		
		public static Command1 Error(GovernanceError info)
		{
			return new Command1(Command1Tag.Error, info);
		}
		
		public GovernanceError AsError()
		{
			this.ValidateTag(Command1Tag.Error);
			return (GovernanceError)this.Value!;
		}
		
		public static Command1 Spawn(SpawnResponse info)
		{
			return new Command1(Command1Tag.Spawn, info);
		}
		
		public SpawnResponse AsSpawn()
		{
			this.ValidateTag(Command1Tag.Spawn);
			return (SpawnResponse)this.Value!;
		}
		
		public static Command1 Split(SpawnResponse info)
		{
			return new Command1(Command1Tag.Split, info);
		}
		
		public SpawnResponse AsSplit()
		{
			this.ValidateTag(Command1Tag.Split);
			return (SpawnResponse)this.Value!;
		}
		
		public class O3
		{
		}
		public static Command1 Follow(Command1.O3 info)
		{
			return new Command1(Command1Tag.Follow, info);
		}
		
		public Command1.O3 AsFollow()
		{
			this.ValidateTag(Command1Tag.Follow);
			return (Command1.O3)this.Value!;
		}
		
		public static Command1 ClaimOrRefresh(ClaimOrRefreshResponse info)
		{
			return new Command1(Command1Tag.ClaimOrRefresh, info);
		}
		
		public ClaimOrRefreshResponse AsClaimOrRefresh()
		{
			this.ValidateTag(Command1Tag.ClaimOrRefresh);
			return (ClaimOrRefreshResponse)this.Value!;
		}
		
		public class O5
		{
		}
		public static Command1 Configure(Command1.O5 info)
		{
			return new Command1(Command1Tag.Configure, info);
		}
		
		public Command1.O5 AsConfigure()
		{
			this.ValidateTag(Command1Tag.Configure);
			return (Command1.O5)this.Value!;
		}
		
		public class O6
		{
		}
		public static Command1 RegisterVote(Command1.O6 info)
		{
			return new Command1(Command1Tag.RegisterVote, info);
		}
		
		public Command1.O6 AsRegisterVote()
		{
			this.ValidateTag(Command1Tag.RegisterVote);
			return (Command1.O6)this.Value!;
		}
		
		public class O7
		{
		}
		public static Command1 Merge(Command1.O7 info)
		{
			return new Command1(Command1Tag.Merge, info);
		}
		
		public Command1.O7 AsMerge()
		{
			this.ValidateTag(Command1Tag.Merge);
			return (Command1.O7)this.Value!;
		}
		
		public static Command1 DisburseToNeuron(SpawnResponse info)
		{
			return new Command1(Command1Tag.DisburseToNeuron, info);
		}
		
		public SpawnResponse AsDisburseToNeuron()
		{
			this.ValidateTag(Command1Tag.DisburseToNeuron);
			return (SpawnResponse)this.Value!;
		}
		
		public static Command1 MakeProposal(MakeProposalResponse info)
		{
			return new Command1(Command1Tag.MakeProposal, info);
		}
		
		public MakeProposalResponse AsMakeProposal()
		{
			this.ValidateTag(Command1Tag.MakeProposal);
			return (MakeProposalResponse)this.Value!;
		}
		
		public static Command1 MergeMaturity(MergeMaturityResponse info)
		{
			return new Command1(Command1Tag.MergeMaturity, info);
		}
		
		public MergeMaturityResponse AsMergeMaturity()
		{
			this.ValidateTag(Command1Tag.MergeMaturity);
			return (MergeMaturityResponse)this.Value!;
		}
		
		public static Command1 Disburse(DisburseResponse info)
		{
			return new Command1(Command1Tag.Disburse, info);
		}
		
		public DisburseResponse AsDisburse()
		{
			this.ValidateTag(Command1Tag.Disburse);
			return (DisburseResponse)this.Value!;
		}
		
		private void ValidateTag(Command1Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum Command1Tag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Error")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Error,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Spawn")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(SpawnResponse))]
		Spawn,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Split")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(SpawnResponse))]
		Split,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Follow")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Command1.O3))]
		Follow,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ClaimOrRefresh")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(ClaimOrRefreshResponse))]
		ClaimOrRefresh,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Configure")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Command1.O5))]
		Configure,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RegisterVote")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Command1.O6))]
		RegisterVote,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Merge")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Command1.O7))]
		Merge,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("DisburseToNeuron")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(SpawnResponse))]
		DisburseToNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MakeProposal")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(MakeProposalResponse))]
		MakeProposal,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MergeMaturity")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(MergeMaturityResponse))]
		MergeMaturity,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Disburse")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(DisburseResponse))]
		Disburse,
	}
}

