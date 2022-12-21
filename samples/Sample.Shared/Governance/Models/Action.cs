using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public enum ActionType
	{
		RegisterKnownNeuron,
		ManageNeuron,
		ExecuteNnsFunction,
		RewardNodeProvider,
		SetDefaultFollowees,
		RewardNodeProviders,
		ManageNetworkEconomics,
		ApproveGenesisKyc,
		AddOrRemoveNodeProvider,
		Motion,
	}
	public class Action : EdjCase.ICP.Candid.CandidVariantValueBase<ActionType>
	{
		public Action(ActionType type, object? value)  : base(type, value)
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
}

