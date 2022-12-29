using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Command1 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Command1Type>
	{
		public Command1(Command1Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Command1()
		{
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
		
		public class O3
		{
		}
		public static Command1 Follow(Command1.O3 info)
		{
			return new Command1(Command1Type.Follow, info);
		}
		
		public Command1.O3 AsFollow()
		{
			this.ValidateType(Command1Type.Follow);
			return (Command1.O3)this.value!;
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
		
		public class O5
		{
		}
		public static Command1 Configure(Command1.O5 info)
		{
			return new Command1(Command1Type.Configure, info);
		}
		
		public Command1.O5 AsConfigure()
		{
			this.ValidateType(Command1Type.Configure);
			return (Command1.O5)this.value!;
		}
		
		public class O6
		{
		}
		public static Command1 RegisterVote(Command1.O6 info)
		{
			return new Command1(Command1Type.RegisterVote, info);
		}
		
		public Command1.O6 AsRegisterVote()
		{
			this.ValidateType(Command1Type.RegisterVote);
			return (Command1.O6)this.value!;
		}
		
		public class O7
		{
		}
		public static Command1 Merge(Command1.O7 info)
		{
			return new Command1(Command1Type.Merge, info);
		}
		
		public Command1.O7 AsMerge()
		{
			this.ValidateType(Command1Type.Merge);
			return (Command1.O7)this.value!;
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
		
	}
	public enum Command1Type
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
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Command1.O3))]
		Follow,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ClaimOrRefresh")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ClaimOrRefreshResponse))]
		ClaimOrRefresh,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Configure")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Command1.O5))]
		Configure,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RegisterVote")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Command1.O6))]
		RegisterVote,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Merge")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Command1.O7))]
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
}

