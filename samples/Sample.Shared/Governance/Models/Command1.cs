using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Command1
	{
		[VariantTagProperty()]
		public Command1Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public GovernanceError? Error { get => this.Tag == Command1Tag.Error ? (GovernanceError)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.Error, value); }

		public SpawnResponse? Spawn { get => this.Tag == Command1Tag.Spawn ? (SpawnResponse)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.Spawn, value); }

		public SpawnResponse? Split { get => this.Tag == Command1Tag.Split ? (SpawnResponse)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.Split, value); }

		public Command1.FollowInfo? Follow { get => this.Tag == Command1Tag.Follow ? (Command1.FollowInfo)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.Follow, value); }

		public ClaimOrRefreshResponse? ClaimOrRefresh { get => this.Tag == Command1Tag.ClaimOrRefresh ? (ClaimOrRefreshResponse)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.ClaimOrRefresh, value); }

		public Command1.ConfigureInfo? Configure { get => this.Tag == Command1Tag.Configure ? (Command1.ConfigureInfo)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.Configure, value); }

		public Command1.RegisterVoteInfo? RegisterVote { get => this.Tag == Command1Tag.RegisterVote ? (Command1.RegisterVoteInfo)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.RegisterVote, value); }

		public MergeResponse? Merge { get => this.Tag == Command1Tag.Merge ? (MergeResponse)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.Merge, value); }

		public SpawnResponse? DisburseToNeuron { get => this.Tag == Command1Tag.DisburseToNeuron ? (SpawnResponse)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.DisburseToNeuron, value); }

		public MakeProposalResponse? MakeProposal { get => this.Tag == Command1Tag.MakeProposal ? (MakeProposalResponse)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.MakeProposal, value); }

		public StakeMaturityResponse? StakeMaturity { get => this.Tag == Command1Tag.StakeMaturity ? (StakeMaturityResponse)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.StakeMaturity, value); }

		public MergeMaturityResponse? MergeMaturity { get => this.Tag == Command1Tag.MergeMaturity ? (MergeMaturityResponse)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.MergeMaturity, value); }

		public DisburseResponse? Disburse { get => this.Tag == Command1Tag.Disburse ? (DisburseResponse)this.Value : default; set => (this.Tag, this.Value) = (Command1Tag.Disburse, value); }

		public Command1(Command1Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Command1()
		{
		}

		public class FollowInfo
		{
			public FollowInfo()
			{
			}
		}

		public class ConfigureInfo
		{
			public ConfigureInfo()
			{
			}
		}

		public class RegisterVoteInfo
		{
			public RegisterVoteInfo()
			{
			}
		}
	}

	public enum Command1Tag
	{
		Error,
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