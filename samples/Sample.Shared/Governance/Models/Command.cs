using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Command
	{
		[VariantTagProperty()]
		public CommandTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public Spawn? Spawn { get => this.Tag == CommandTag.Spawn ? (Spawn)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.Spawn, value); }

		public Split? Split { get => this.Tag == CommandTag.Split ? (Split)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.Split, value); }

		public Follow? Follow { get => this.Tag == CommandTag.Follow ? (Follow)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.Follow, value); }

		public ClaimOrRefresh? ClaimOrRefresh { get => this.Tag == CommandTag.ClaimOrRefresh ? (ClaimOrRefresh)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.ClaimOrRefresh, value); }

		public Configure? Configure { get => this.Tag == CommandTag.Configure ? (Configure)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.Configure, value); }

		public RegisterVote? RegisterVote { get => this.Tag == CommandTag.RegisterVote ? (RegisterVote)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.RegisterVote, value); }

		public Merge? Merge { get => this.Tag == CommandTag.Merge ? (Merge)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.Merge, value); }

		public DisburseToNeuron? DisburseToNeuron { get => this.Tag == CommandTag.DisburseToNeuron ? (DisburseToNeuron)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.DisburseToNeuron, value); }

		public Proposal? MakeProposal { get => this.Tag == CommandTag.MakeProposal ? (Proposal)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.MakeProposal, value); }

		public StakeMaturity? StakeMaturity { get => this.Tag == CommandTag.StakeMaturity ? (StakeMaturity)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.StakeMaturity, value); }

		public MergeMaturity? MergeMaturity { get => this.Tag == CommandTag.MergeMaturity ? (MergeMaturity)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.MergeMaturity, value); }

		public Disburse? Disburse { get => this.Tag == CommandTag.Disburse ? (Disburse)this.Value : default; set => (this.Tag, this.Value) = (CommandTag.Disburse, value); }

		public Command(CommandTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Command()
		{
		}
	}

	public enum CommandTag
	{
		Spawn,
		Split,
		Follow,
		ClaimOrRefresh,
		Configure,
		RegisterVote,
		Merge,
		DisburseToNeuron,
		MakeProposal,
		StakeMaturity,
		MergeMaturity,
		Disburse
	}
}