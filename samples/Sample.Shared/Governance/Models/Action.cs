using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class Action
	{
		[VariantTagProperty]
		public ActionTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Action(ActionTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Action()
		{
		}

		public static Action RegisterNeuron(KnownNeuron info)
		{
			return new Action(ActionTag.RegisterNeuron, info);
		}

		public static Action ManageNeuron(ManageNeuron info)
		{
			return new Action(ActionTag.ManageNeuron, info);
		}

		public static Action CreateServiceNervousSystem(CreateServiceNervousSystem info)
		{
			return new Action(ActionTag.CreateServiceNervousSystem, info);
		}

		public static Action ExecuteNnsFunction(ExecuteNnsFunction info)
		{
			return new Action(ActionTag.ExecuteNnsFunction, info);
		}

		public static Action RewardNodeProvider(RewardNodeProvider info)
		{
			return new Action(ActionTag.RewardNodeProvider, info);
		}

		public static Action OpenSnsTokenSwap(OpenSnsTokenSwap info)
		{
			return new Action(ActionTag.OpenSnsTokenSwap, info);
		}

		public static Action SetSnsTokenSwapOpenTimeWindow(SetSnsTokenSwapOpenTimeWindow info)
		{
			return new Action(ActionTag.SetSnsTokenSwapOpenTimeWindow, info);
		}

		public static Action SetDefaultFollowees(SetDefaultFollowees info)
		{
			return new Action(ActionTag.SetDefaultFollowees, info);
		}

		public static Action RewardNodeProviders(RewardNodeProviders info)
		{
			return new Action(ActionTag.RewardNodeProviders, info);
		}

		public static Action ManageNetworkEconomics(NetworkEconomics info)
		{
			return new Action(ActionTag.ManageNetworkEconomics, info);
		}

		public static Action ApproveGenesisKyc(ApproveGenesisKyc info)
		{
			return new Action(ActionTag.ApproveGenesisKyc, info);
		}

		public static Action AddOrRemoveNodeProvider(AddOrRemoveNodeProvider info)
		{
			return new Action(ActionTag.AddOrRemoveNodeProvider, info);
		}

		public static Action Motion(Motion info)
		{
			return new Action(ActionTag.Motion, info);
		}

		public KnownNeuron AsRegisterNeuron()
		{
			this.ValidateTag(ActionTag.RegisterNeuron);
			return (KnownNeuron)this.Value!;
		}

		public ManageNeuron AsManageNeuron()
		{
			this.ValidateTag(ActionTag.ManageNeuron);
			return (ManageNeuron)this.Value!;
		}

		public CreateServiceNervousSystem AsCreateServiceNervousSystem()
		{
			this.ValidateTag(ActionTag.CreateServiceNervousSystem);
			return (CreateServiceNervousSystem)this.Value!;
		}

		public ExecuteNnsFunction AsExecuteNnsFunction()
		{
			this.ValidateTag(ActionTag.ExecuteNnsFunction);
			return (ExecuteNnsFunction)this.Value!;
		}

		public RewardNodeProvider AsRewardNodeProvider()
		{
			this.ValidateTag(ActionTag.RewardNodeProvider);
			return (RewardNodeProvider)this.Value!;
		}

		public OpenSnsTokenSwap AsOpenSnsTokenSwap()
		{
			this.ValidateTag(ActionTag.OpenSnsTokenSwap);
			return (OpenSnsTokenSwap)this.Value!;
		}

		public SetSnsTokenSwapOpenTimeWindow AsSetSnsTokenSwapOpenTimeWindow()
		{
			this.ValidateTag(ActionTag.SetSnsTokenSwapOpenTimeWindow);
			return (SetSnsTokenSwapOpenTimeWindow)this.Value!;
		}

		public SetDefaultFollowees AsSetDefaultFollowees()
		{
			this.ValidateTag(ActionTag.SetDefaultFollowees);
			return (SetDefaultFollowees)this.Value!;
		}

		public RewardNodeProviders AsRewardNodeProviders()
		{
			this.ValidateTag(ActionTag.RewardNodeProviders);
			return (RewardNodeProviders)this.Value!;
		}

		public NetworkEconomics AsManageNetworkEconomics()
		{
			this.ValidateTag(ActionTag.ManageNetworkEconomics);
			return (NetworkEconomics)this.Value!;
		}

		public ApproveGenesisKyc AsApproveGenesisKyc()
		{
			this.ValidateTag(ActionTag.ApproveGenesisKyc);
			return (ApproveGenesisKyc)this.Value!;
		}

		public AddOrRemoveNodeProvider AsAddOrRemoveNodeProvider()
		{
			this.ValidateTag(ActionTag.AddOrRemoveNodeProvider);
			return (AddOrRemoveNodeProvider)this.Value!;
		}

		public Motion AsMotion()
		{
			this.ValidateTag(ActionTag.Motion);
			return (Motion)this.Value!;
		}

		private void ValidateTag(ActionTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum ActionTag
	{
		[CandidName("RegisterKnownNeuron")]
		RegisterNeuron,
		ManageNeuron,
		CreateServiceNervousSystem,
		ExecuteNnsFunction,
		RewardNodeProvider,
		OpenSnsTokenSwap,
		SetSnsTokenSwapOpenTimeWindow,
		SetDefaultFollowees,
		RewardNodeProviders,
		ManageNetworkEconomics,
		ApproveGenesisKyc,
		AddOrRemoveNodeProvider,
		Motion
	}
}