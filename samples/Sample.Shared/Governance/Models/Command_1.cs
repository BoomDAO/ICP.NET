using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public enum Command_1Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Error")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Error,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Spawn")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(SpawnResponse))]
		Spawn,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Split")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(SpawnResponse))]
		Split,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Follow")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Command_1))]
		Follow,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ClaimOrRefresh")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ClaimOrRefreshResponse))]
		ClaimOrRefresh,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Configure")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Command_1))]
		Configure,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RegisterVote")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Command_1))]
		RegisterVote,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Merge")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Command_1))]
		Merge,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("DisburseToNeuron")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(SpawnResponse))]
		DisburseToNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MakeProposal")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(MakeProposalResponse))]
		MakeProposal,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MergeMaturity")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(MergeMaturityResponse))]
		MergeMaturity,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Disburse")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(DisburseResponse))]
		Disburse,
	}
	public class Command_1 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Command_1Type>
	{
		public Command_1(Command_1Type type, System.Object? value)  : base(type, value)
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
		
		public static Command_1 Follow(Command_1 info)
		{
			return new Command_1(Command_1Type.Follow, info);
		}
		
		public Command_1 AsFollow()
		{
			this.ValidateType(Command_1Type.Follow);
			return (Command_1)this.value!;
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
		
		public static Command_1 Configure(Command_1 info)
		{
			return new Command_1(Command_1Type.Configure, info);
		}
		
		public Command_1 AsConfigure()
		{
			this.ValidateType(Command_1Type.Configure);
			return (Command_1)this.value!;
		}
		
		public static Command_1 RegisterVote(Command_1 info)
		{
			return new Command_1(Command_1Type.RegisterVote, info);
		}
		
		public Command_1 AsRegisterVote()
		{
			this.ValidateType(Command_1Type.RegisterVote);
			return (Command_1)this.value!;
		}
		
		public static Command_1 Merge(Command_1 info)
		{
			return new Command_1(Command_1Type.Merge, info);
		}
		
		public Command_1 AsMerge()
		{
			this.ValidateType(Command_1Type.Merge);
			return (Command_1)this.value!;
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
		
	}
}

