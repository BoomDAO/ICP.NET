using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class Command
	{
		[VariantTagProperty]
		public CommandTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Command(CommandTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Command()
		{
		}

		public static Command Spawn(Spawn info)
		{
			return new Command(CommandTag.Spawn, info);
		}

		public static Command Split(Split info)
		{
			return new Command(CommandTag.Split, info);
		}

		public static Command Follow(Follow info)
		{
			return new Command(CommandTag.Follow, info);
		}

		public static Command ClaimOrRefresh(ClaimOrRefresh info)
		{
			return new Command(CommandTag.ClaimOrRefresh, info);
		}

		public static Command Configure(Configure info)
		{
			return new Command(CommandTag.Configure, info);
		}

		public static Command RegisterVote(RegisterVote info)
		{
			return new Command(CommandTag.RegisterVote, info);
		}

		public static Command Merge(Merge info)
		{
			return new Command(CommandTag.Merge, info);
		}

		public static Command DisburseToNeuron(DisburseToNeuron info)
		{
			return new Command(CommandTag.DisburseToNeuron, info);
		}

		public static Command MakeProposal(Proposal info)
		{
			return new Command(CommandTag.MakeProposal, info);
		}

		public static Command StakeMaturity(StakeMaturity info)
		{
			return new Command(CommandTag.StakeMaturity, info);
		}

		public static Command MergeMaturity(MergeMaturity info)
		{
			return new Command(CommandTag.MergeMaturity, info);
		}

		public static Command Disburse(Disburse info)
		{
			return new Command(CommandTag.Disburse, info);
		}

		public Spawn AsSpawn()
		{
			this.ValidateTag(CommandTag.Spawn);
			return (Spawn)this.Value!;
		}

		public Split AsSplit()
		{
			this.ValidateTag(CommandTag.Split);
			return (Split)this.Value!;
		}

		public Follow AsFollow()
		{
			this.ValidateTag(CommandTag.Follow);
			return (Follow)this.Value!;
		}

		public ClaimOrRefresh AsClaimOrRefresh()
		{
			this.ValidateTag(CommandTag.ClaimOrRefresh);
			return (ClaimOrRefresh)this.Value!;
		}

		public Configure AsConfigure()
		{
			this.ValidateTag(CommandTag.Configure);
			return (Configure)this.Value!;
		}

		public RegisterVote AsRegisterVote()
		{
			this.ValidateTag(CommandTag.RegisterVote);
			return (RegisterVote)this.Value!;
		}

		public Merge AsMerge()
		{
			this.ValidateTag(CommandTag.Merge);
			return (Merge)this.Value!;
		}

		public DisburseToNeuron AsDisburseToNeuron()
		{
			this.ValidateTag(CommandTag.DisburseToNeuron);
			return (DisburseToNeuron)this.Value!;
		}

		public Proposal AsMakeProposal()
		{
			this.ValidateTag(CommandTag.MakeProposal);
			return (Proposal)this.Value!;
		}

		public StakeMaturity AsStakeMaturity()
		{
			this.ValidateTag(CommandTag.StakeMaturity);
			return (StakeMaturity)this.Value!;
		}

		public MergeMaturity AsMergeMaturity()
		{
			this.ValidateTag(CommandTag.MergeMaturity);
			return (MergeMaturity)this.Value!;
		}

		public Disburse AsDisburse()
		{
			this.ValidateTag(CommandTag.Disburse);
			return (Disburse)this.Value!;
		}

		private void ValidateTag(CommandTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
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