using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Action : EdjCase.ICP.Candid.Models.CandidVariantValueBase<ActionType>
	{
		public Action(ActionType type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Action()
		{
		}
		
		public static Action RegisterKnownNeuron(KnownNeuron info)
		{
			return new Action(ActionType.RegisterKnownNeuron, info);
		}
		
		public KnownNeuron AsRegisterKnownNeuron()
		{
			this.ValidateType(ActionType.RegisterKnownNeuron);
			return (KnownNeuron)this.value!;
		}
		
		public static Action ManageNeuron(ManageNeuron info)
		{
			return new Action(ActionType.ManageNeuron, info);
		}
		
		public ManageNeuron AsManageNeuron()
		{
			this.ValidateType(ActionType.ManageNeuron);
			return (ManageNeuron)this.value!;
		}
		
		public static Action ExecuteNnsFunction(ExecuteNnsFunction info)
		{
			return new Action(ActionType.ExecuteNnsFunction, info);
		}
		
		public ExecuteNnsFunction AsExecuteNnsFunction()
		{
			this.ValidateType(ActionType.ExecuteNnsFunction);
			return (ExecuteNnsFunction)this.value!;
		}
		
		public static Action RewardNodeProvider(RewardNodeProvider info)
		{
			return new Action(ActionType.RewardNodeProvider, info);
		}
		
		public RewardNodeProvider AsRewardNodeProvider()
		{
			this.ValidateType(ActionType.RewardNodeProvider);
			return (RewardNodeProvider)this.value!;
		}
		
		public static Action SetDefaultFollowees(SetDefaultFollowees info)
		{
			return new Action(ActionType.SetDefaultFollowees, info);
		}
		
		public SetDefaultFollowees AsSetDefaultFollowees()
		{
			this.ValidateType(ActionType.SetDefaultFollowees);
			return (SetDefaultFollowees)this.value!;
		}
		
		public static Action RewardNodeProviders(RewardNodeProviders info)
		{
			return new Action(ActionType.RewardNodeProviders, info);
		}
		
		public RewardNodeProviders AsRewardNodeProviders()
		{
			this.ValidateType(ActionType.RewardNodeProviders);
			return (RewardNodeProviders)this.value!;
		}
		
		public static Action ManageNetworkEconomics(NetworkEconomics info)
		{
			return new Action(ActionType.ManageNetworkEconomics, info);
		}
		
		public NetworkEconomics AsManageNetworkEconomics()
		{
			this.ValidateType(ActionType.ManageNetworkEconomics);
			return (NetworkEconomics)this.value!;
		}
		
		public static Action ApproveGenesisKyc(ApproveGenesisKyc info)
		{
			return new Action(ActionType.ApproveGenesisKyc, info);
		}
		
		public ApproveGenesisKyc AsApproveGenesisKyc()
		{
			this.ValidateType(ActionType.ApproveGenesisKyc);
			return (ApproveGenesisKyc)this.value!;
		}
		
		public static Action AddOrRemoveNodeProvider(AddOrRemoveNodeProvider info)
		{
			return new Action(ActionType.AddOrRemoveNodeProvider, info);
		}
		
		public AddOrRemoveNodeProvider AsAddOrRemoveNodeProvider()
		{
			this.ValidateType(ActionType.AddOrRemoveNodeProvider);
			return (AddOrRemoveNodeProvider)this.value!;
		}
		
		public static Action Motion(Motion info)
		{
			return new Action(ActionType.Motion, info);
		}
		
		public Motion AsMotion()
		{
			this.ValidateType(ActionType.Motion);
			return (Motion)this.value!;
		}
		
	}
	public enum ActionType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RegisterKnownNeuron")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(KnownNeuron))]
		RegisterKnownNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ManageNeuron")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ManageNeuron))]
		ManageNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ExecuteNnsFunction")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ExecuteNnsFunction))]
		ExecuteNnsFunction,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RewardNodeProvider")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(RewardNodeProvider))]
		RewardNodeProvider,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("SetDefaultFollowees")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(SetDefaultFollowees))]
		SetDefaultFollowees,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RewardNodeProviders")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(RewardNodeProviders))]
		RewardNodeProviders,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ManageNetworkEconomics")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(NetworkEconomics))]
		ManageNetworkEconomics,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ApproveGenesisKyc")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ApproveGenesisKyc))]
		ApproveGenesisKyc,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("AddOrRemoveNodeProvider")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(AddOrRemoveNodeProvider))]
		AddOrRemoveNodeProvider,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Motion")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Motion))]
		Motion,
	}
}

