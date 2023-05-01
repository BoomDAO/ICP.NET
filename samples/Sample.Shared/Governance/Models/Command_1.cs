using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Command_1Tag))]
	public class Command_1
	{
		[VariantTagProperty()]
		public Command_1Tag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

		public Command_1(Command_1Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Command_1()
		{
		}

		public static Command_1 Error(GovernanceError info)
		{
			return new Command_1(Command_1Tag.Error, info);
		}

		public static Command_1 Spawn(SpawnResponse info)
		{
			return new Command_1(Command_1Tag.Spawn, info);
		}

		public static Command_1 Split(SpawnResponse info)
		{
			return new Command_1(Command_1Tag.Split, info);
		}

		public static Command_1 Follow(Command_1.FollowRecord info)
		{
			return new Command_1(Command_1Tag.Follow, info);
		}

		public static Command_1 ClaimOrRefresh(ClaimOrRefreshResponse info)
		{
			return new Command_1(Command_1Tag.ClaimOrRefresh, info);
		}

		public static Command_1 Configure(Command_1.ConfigureRecord info)
		{
			return new Command_1(Command_1Tag.Configure, info);
		}

		public static Command_1 RegisterVote(Command_1.RegisterVoteRecord info)
		{
			return new Command_1(Command_1Tag.RegisterVote, info);
		}

		public static Command_1 Merge(Command_1.MergeRecord info)
		{
			return new Command_1(Command_1Tag.Merge, info);
		}

		public static Command_1 DisburseToNeuron(SpawnResponse info)
		{
			return new Command_1(Command_1Tag.DisburseToNeuron, info);
		}

		public static Command_1 MakeProposal(MakeProposalResponse info)
		{
			return new Command_1(Command_1Tag.MakeProposal, info);
		}

		public static Command_1 StakeMaturity(StakeMaturityResponse info)
		{
			return new Command_1(Command_1Tag.StakeMaturity, info);
		}

		public static Command_1 MergeMaturity(MergeMaturityResponse info)
		{
			return new Command_1(Command_1Tag.MergeMaturity, info);
		}

		public static Command_1 Disburse(DisburseResponse info)
		{
			return new Command_1(Command_1Tag.Disburse, info);
		}

		public GovernanceError AsError()
		{
			this.ValidateTag(Command_1Tag.Error);
			return (GovernanceError)this.Value!;
		}

		public SpawnResponse AsSpawn()
		{
			this.ValidateTag(Command_1Tag.Spawn);
			return (SpawnResponse)this.Value!;
		}

		public SpawnResponse AsSplit()
		{
			this.ValidateTag(Command_1Tag.Split);
			return (SpawnResponse)this.Value!;
		}

		public Command_1.FollowRecord AsFollow()
		{
			this.ValidateTag(Command_1Tag.Follow);
			return (Command_1.FollowRecord)this.Value!;
		}

		public ClaimOrRefreshResponse AsClaimOrRefresh()
		{
			this.ValidateTag(Command_1Tag.ClaimOrRefresh);
			return (ClaimOrRefreshResponse)this.Value!;
		}

		public Command_1.ConfigureRecord AsConfigure()
		{
			this.ValidateTag(Command_1Tag.Configure);
			return (Command_1.ConfigureRecord)this.Value!;
		}

		public Command_1.RegisterVoteRecord AsRegisterVote()
		{
			this.ValidateTag(Command_1Tag.RegisterVote);
			return (Command_1.RegisterVoteRecord)this.Value!;
		}

		public Command_1.MergeRecord AsMerge()
		{
			this.ValidateTag(Command_1Tag.Merge);
			return (Command_1.MergeRecord)this.Value!;
		}

		public SpawnResponse AsDisburseToNeuron()
		{
			this.ValidateTag(Command_1Tag.DisburseToNeuron);
			return (SpawnResponse)this.Value!;
		}

		public MakeProposalResponse AsMakeProposal()
		{
			this.ValidateTag(Command_1Tag.MakeProposal);
			return (MakeProposalResponse)this.Value!;
		}

		public StakeMaturityResponse AsStakeMaturity()
		{
			this.ValidateTag(Command_1Tag.StakeMaturity);
			return (StakeMaturityResponse)this.Value!;
		}

		public MergeMaturityResponse AsMergeMaturity()
		{
			this.ValidateTag(Command_1Tag.MergeMaturity);
			return (MergeMaturityResponse)this.Value!;
		}

		public DisburseResponse AsDisburse()
		{
			this.ValidateTag(Command_1Tag.Disburse);
			return (DisburseResponse)this.Value!;
		}

		private void ValidateTag(Command_1Tag tag)
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

	public enum Command_1Tag
	{
		[VariantOptionType(typeof(GovernanceError))]
		Error,
		[VariantOptionType(typeof(SpawnResponse))]
		Spawn,
		[VariantOptionType(typeof(SpawnResponse))]
		Split,
		[VariantOptionType(typeof(Command_1.FollowRecord))]
		Follow,
		[VariantOptionType(typeof(ClaimOrRefreshResponse))]
		ClaimOrRefresh,
		[VariantOptionType(typeof(Command_1.ConfigureRecord))]
		Configure,
		[VariantOptionType(typeof(Command_1.RegisterVoteRecord))]
		RegisterVote,
		[VariantOptionType(typeof(Command_1.MergeRecord))]
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