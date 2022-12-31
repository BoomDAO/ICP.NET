using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(ActionTag))]
	public class Action
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public ActionTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Action(ActionTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Action()
		{
		}
		
		public static Action RegisterKnownNeuron(KnownNeuron info)
		{
			return new Action(ActionTag.RegisterKnownNeuron, info);
		}
		
		public KnownNeuron AsRegisterKnownNeuron()
		{
			this.ValidateType(ActionTag.RegisterKnownNeuron);
			return (KnownNeuron)this.Value!;
		}
		
		public static Action ManageNeuron(ManageNeuron info)
		{
			return new Action(ActionTag.ManageNeuron, info);
		}
		
		public ManageNeuron AsManageNeuron()
		{
			this.ValidateType(ActionTag.ManageNeuron);
			return (ManageNeuron)this.Value!;
		}
		
		public static Action ExecuteNnsFunction(ExecuteNnsFunction info)
		{
			return new Action(ActionTag.ExecuteNnsFunction, info);
		}
		
		public ExecuteNnsFunction AsExecuteNnsFunction()
		{
			this.ValidateType(ActionTag.ExecuteNnsFunction);
			return (ExecuteNnsFunction)this.Value!;
		}
		
		public static Action RewardNodeProvider(RewardNodeProvider info)
		{
			return new Action(ActionTag.RewardNodeProvider, info);
		}
		
		public RewardNodeProvider AsRewardNodeProvider()
		{
			this.ValidateType(ActionTag.RewardNodeProvider);
			return (RewardNodeProvider)this.Value!;
		}
		
		public static Action SetDefaultFollowees(SetDefaultFollowees info)
		{
			return new Action(ActionTag.SetDefaultFollowees, info);
		}
		
		public SetDefaultFollowees AsSetDefaultFollowees()
		{
			this.ValidateType(ActionTag.SetDefaultFollowees);
			return (SetDefaultFollowees)this.Value!;
		}
		
		public static Action RewardNodeProviders(RewardNodeProviders info)
		{
			return new Action(ActionTag.RewardNodeProviders, info);
		}
		
		public RewardNodeProviders AsRewardNodeProviders()
		{
			this.ValidateType(ActionTag.RewardNodeProviders);
			return (RewardNodeProviders)this.Value!;
		}
		
		public static Action ManageNetworkEconomics(NetworkEconomics info)
		{
			return new Action(ActionTag.ManageNetworkEconomics, info);
		}
		
		public NetworkEconomics AsManageNetworkEconomics()
		{
			this.ValidateType(ActionTag.ManageNetworkEconomics);
			return (NetworkEconomics)this.Value!;
		}
		
		public static Action ApproveGenesisKyc(ApproveGenesisKyc info)
		{
			return new Action(ActionTag.ApproveGenesisKyc, info);
		}
		
		public ApproveGenesisKyc AsApproveGenesisKyc()
		{
			this.ValidateType(ActionTag.ApproveGenesisKyc);
			return (ApproveGenesisKyc)this.Value!;
		}
		
		public static Action AddOrRemoveNodeProvider(AddOrRemoveNodeProvider info)
		{
			return new Action(ActionTag.AddOrRemoveNodeProvider, info);
		}
		
		public AddOrRemoveNodeProvider AsAddOrRemoveNodeProvider()
		{
			this.ValidateType(ActionTag.AddOrRemoveNodeProvider);
			return (AddOrRemoveNodeProvider)this.Value!;
		}
		
		public static Action Motion(Motion info)
		{
			return new Action(ActionTag.Motion, info);
		}
		
		public Motion AsMotion()
		{
			this.ValidateType(ActionTag.Motion);
			return (Motion)this.Value!;
		}
		
		private void ValidateType(ActionTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum ActionTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RegisterKnownNeuron")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(KnownNeuron))]
		RegisterKnownNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ManageNeuron")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(ManageNeuron))]
		ManageNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ExecuteNnsFunction")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(ExecuteNnsFunction))]
		ExecuteNnsFunction,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RewardNodeProvider")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(RewardNodeProvider))]
		RewardNodeProvider,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("SetDefaultFollowees")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(SetDefaultFollowees))]
		SetDefaultFollowees,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RewardNodeProviders")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(RewardNodeProviders))]
		RewardNodeProviders,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ManageNetworkEconomics")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(NetworkEconomics))]
		ManageNetworkEconomics,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ApproveGenesisKyc")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(ApproveGenesisKyc))]
		ApproveGenesisKyc,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("AddOrRemoveNodeProvider")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(AddOrRemoveNodeProvider))]
		AddOrRemoveNodeProvider,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Motion")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Motion))]
		Motion,
	}
}

