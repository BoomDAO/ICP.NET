using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Command1Tag))]
	public class Command1
	{
		[VariantTagProperty()]
		public Command1Tag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

		public Command1(Command1Tag tag, object? value = null)
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

		public static Command1 Spawn(SpawnResponse info)
		{
			return new Command1(Command1Tag.Spawn, info);
		}

		public static Command1 Split(SpawnResponse info)
		{
			return new Command1(Command1Tag.Split, info);
		}

		public static Command1 Follow(Command1.FollowRecord info)
		{
			return new Command1(Command1Tag.Follow, info);
		}

		public static Command1 ClaimOrRefresh(ClaimOrRefreshResponse info)
		{
			return new Command1(Command1Tag.ClaimOrRefresh, info);
		}

		public static Command1 Configure(Command1.ConfigureRecord info)
		{
			return new Command1(Command1Tag.Configure, info);
		}

		public static Command1 RegisterVote(Command1.RegisterVoteRecord info)
		{
			return new Command1(Command1Tag.RegisterVote, info);
		}

		public static Command1 Merge(Command1.MergeRecord info)
		{
			return new Command1(Command1Tag.Merge, info);
		}

		public static Command1 DisburseToNeuron(SpawnResponse info)
		{
			return new Command1(Command1Tag.DisburseToNeuron, info);
		}

		public static Command1 MakeProposal(MakeProposalResponse info)
		{
			return new Command1(Command1Tag.MakeProposal, info);
		}

		public static Command1 StakeMaturity(StakeMaturityResponse info)
		{
			return new Command1(Command1Tag.StakeMaturity, info);
		}

		public static Command1 MergeMaturity(MergeMaturityResponse info)
		{
			return new Command1(Command1Tag.MergeMaturity, info);
		}

		public static Command1 Disburse(DisburseResponse info)
		{
			return new Command1(Command1Tag.Disburse, info);
		}

		public GovernanceError AsError()
		{
			this.ValidateTag(Command1Tag.Error);
			return (GovernanceError)this.Value!;
		}

		public SpawnResponse AsSpawn()
		{
			this.ValidateTag(Command1Tag.Spawn);
			return (SpawnResponse)this.Value!;
		}

		public SpawnResponse AsSplit()
		{
			this.ValidateTag(Command1Tag.Split);
			return (SpawnResponse)this.Value!;
		}

		public Command1.FollowRecord AsFollow()
		{
			this.ValidateTag(Command1Tag.Follow);
			return (Command1.FollowRecord)this.Value!;
		}

		public ClaimOrRefreshResponse AsClaimOrRefresh()
		{
			this.ValidateTag(Command1Tag.ClaimOrRefresh);
			return (ClaimOrRefreshResponse)this.Value!;
		}

		public Command1.ConfigureRecord AsConfigure()
		{
			this.ValidateTag(Command1Tag.Configure);
			return (Command1.ConfigureRecord)this.Value!;
		}

		public Command1.RegisterVoteRecord AsRegisterVote()
		{
			this.ValidateTag(Command1Tag.RegisterVote);
			return (Command1.RegisterVoteRecord)this.Value!;
		}

		public Command1.MergeRecord AsMerge()
		{
			this.ValidateTag(Command1Tag.Merge);
			return (Command1.MergeRecord)this.Value!;
		}

		public SpawnResponse AsDisburseToNeuron()
		{
			this.ValidateTag(Command1Tag.DisburseToNeuron);
			return (SpawnResponse)this.Value!;
		}

		public MakeProposalResponse AsMakeProposal()
		{
			this.ValidateTag(Command1Tag.MakeProposal);
			return (MakeProposalResponse)this.Value!;
		}

		public StakeMaturityResponse AsStakeMaturity()
		{
			this.ValidateTag(Command1Tag.StakeMaturity);
			return (StakeMaturityResponse)this.Value!;
		}

		public MergeMaturityResponse AsMergeMaturity()
		{
			this.ValidateTag(Command1Tag.MergeMaturity);
			return (MergeMaturityResponse)this.Value!;
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

		public class FollowRecord
		{
			public FollowRecord()
			{
			}
		}

		public class ConfigureRecord
		{
			public ConfigureRecord()
			{
			}
		}

		public class RegisterVoteRecord
		{
			public RegisterVoteRecord()
			{
			}
		}

		public class MergeRecord
		{
			public MergeRecord()
			{
			}
		}
	}

	public enum Command1Tag
	{
		[VariantOptionType(typeof(GovernanceError))]
		Error,
		[VariantOptionType(typeof(SpawnResponse))]
		Spawn,
		[VariantOptionType(typeof(SpawnResponse))]
		Split,
		[VariantOptionType(typeof(Command1.FollowRecord))]
		Follow,
		[VariantOptionType(typeof(ClaimOrRefreshResponse))]
		ClaimOrRefresh,
		[VariantOptionType(typeof(Command1.ConfigureRecord))]
		Configure,
		[VariantOptionType(typeof(Command1.RegisterVoteRecord))]
		RegisterVote,
		[VariantOptionType(typeof(Command1.MergeRecord))]
		Merge,
		[VariantOptionType(typeof(SpawnResponse))]
		DisburseToNeuron,
		[VariantOptionType(typeof(MakeProposalResponse))]
		MakeProposal,
		[VariantOptionType(typeof(StakeMaturityResponse))]
		StakeMaturity,
		[VariantOptionType(typeof(MergeMaturityResponse))]
		MergeMaturity,
		[VariantOptionType(typeof(DisburseResponse))]
		Disburse
	}
}