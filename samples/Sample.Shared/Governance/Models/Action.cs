using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Action
	{
		[VariantTagProperty()]
		public ActionTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public KnownNeuron? RegisterNeuron { get => this.Tag == ActionTag.RegisterNeuron ? (KnownNeuron)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.RegisterNeuron, value); }

		public ManageNeuron? ManageNeuron { get => this.Tag == ActionTag.ManageNeuron ? (ManageNeuron)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.ManageNeuron, value); }

		public CreateServiceNervousSystem? CreateServiceNervousSystem { get => this.Tag == ActionTag.CreateServiceNervousSystem ? (CreateServiceNervousSystem)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.CreateServiceNervousSystem, value); }

		public ExecuteNnsFunction? ExecuteNnsFunction { get => this.Tag == ActionTag.ExecuteNnsFunction ? (ExecuteNnsFunction)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.ExecuteNnsFunction, value); }

		public RewardNodeProvider? RewardNodeProvider { get => this.Tag == ActionTag.RewardNodeProvider ? (RewardNodeProvider)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.RewardNodeProvider, value); }

		public OpenSnsTokenSwap? OpenSnsTokenSwap { get => this.Tag == ActionTag.OpenSnsTokenSwap ? (OpenSnsTokenSwap)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.OpenSnsTokenSwap, value); }

		public SetSnsTokenSwapOpenTimeWindow? SetSnsTokenSwapOpenTimeWindow { get => this.Tag == ActionTag.SetSnsTokenSwapOpenTimeWindow ? (SetSnsTokenSwapOpenTimeWindow)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.SetSnsTokenSwapOpenTimeWindow, value); }

		public SetDefaultFollowees? SetDefaultFollowees { get => this.Tag == ActionTag.SetDefaultFollowees ? (SetDefaultFollowees)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.SetDefaultFollowees, value); }

		public RewardNodeProviders? RewardNodeProviders { get => this.Tag == ActionTag.RewardNodeProviders ? (RewardNodeProviders)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.RewardNodeProviders, value); }

		public NetworkEconomics? ManageNetworkEconomics { get => this.Tag == ActionTag.ManageNetworkEconomics ? (NetworkEconomics)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.ManageNetworkEconomics, value); }

		public ApproveGenesisKyc? ApproveGenesisKyc { get => this.Tag == ActionTag.ApproveGenesisKyc ? (ApproveGenesisKyc)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.ApproveGenesisKyc, value); }

		public AddOrRemoveNodeProvider? AddOrRemoveNodeProvider { get => this.Tag == ActionTag.AddOrRemoveNodeProvider ? (AddOrRemoveNodeProvider)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.AddOrRemoveNodeProvider, value); }

		public Motion? Motion { get => this.Tag == ActionTag.Motion ? (Motion)this.Value : default; set => (this.Tag, this.Value) = (ActionTag.Motion, value); }

		public Action(ActionTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Action()
		{
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