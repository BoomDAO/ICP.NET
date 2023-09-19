using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Command_2Tag))]
	public class Command_2
	{
		[VariantTagProperty()]
		public Command_2Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public Command_2(Command_2Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Command_2()
		{
		}

		public static Command_2 Spawn(NeuronId info)
		{
			return new Command_2(Command_2Tag.Spawn, info);
		}

		public static Command_2 Split(Split info)
		{
			return new Command_2(Command_2Tag.Split, info);
		}

		public static Command_2 Configure(Configure info)
		{
			return new Command_2(Command_2Tag.Configure, info);
		}

		public static Command_2 Merge(Merge info)
		{
			return new Command_2(Command_2Tag.Merge, info);
		}

		public static Command_2 DisburseToNeuron(DisburseToNeuron info)
		{
			return new Command_2(Command_2Tag.DisburseToNeuron, info);
		}

		public static Command_2 SyncCommand(Command_2.SyncCommandInfo info)
		{
			return new Command_2(Command_2Tag.SyncCommand, info);
		}

		public static Command_2 ClaimOrRefreshNeuron(ClaimOrRefresh info)
		{
			return new Command_2(Command_2Tag.ClaimOrRefreshNeuron, info);
		}

		public static Command_2 MergeMaturity(MergeMaturity info)
		{
			return new Command_2(Command_2Tag.MergeMaturity, info);
		}

		public static Command_2 Disburse(Disburse info)
		{
			return new Command_2(Command_2Tag.Disburse, info);
		}

		public NeuronId AsSpawn()
		{
			this.ValidateTag(Command_2Tag.Spawn);
			return (NeuronId)this.Value!;
		}

		public Split AsSplit()
		{
			this.ValidateTag(Command_2Tag.Split);
			return (Split)this.Value!;
		}

		public Configure AsConfigure()
		{
			this.ValidateTag(Command_2Tag.Configure);
			return (Configure)this.Value!;
		}

		public Merge AsMerge()
		{
			this.ValidateTag(Command_2Tag.Merge);
			return (Merge)this.Value!;
		}

		public DisburseToNeuron AsDisburseToNeuron()
		{
			this.ValidateTag(Command_2Tag.DisburseToNeuron);
			return (DisburseToNeuron)this.Value!;
		}

		public Command_2.SyncCommandInfo AsSyncCommand()
		{
			this.ValidateTag(Command_2Tag.SyncCommand);
			return (Command_2.SyncCommandInfo)this.Value!;
		}

		public ClaimOrRefresh AsClaimOrRefreshNeuron()
		{
			this.ValidateTag(Command_2Tag.ClaimOrRefreshNeuron);
			return (ClaimOrRefresh)this.Value!;
		}

		public MergeMaturity AsMergeMaturity()
		{
			this.ValidateTag(Command_2Tag.MergeMaturity);
			return (MergeMaturity)this.Value!;
		}

		public Disburse AsDisburse()
		{
			this.ValidateTag(Command_2Tag.Disburse);
			return (Disburse)this.Value!;
		}

		private void ValidateTag(Command_2Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}

		public class SyncCommandInfo
		{
			public SyncCommandInfo()
			{
			}
		}
	}

	public enum Command_2Tag
	{
		[VariantOptionType(typeof(NeuronId))]
		Spawn,
		[VariantOptionType(typeof(Split))]
		Split,
		[VariantOptionType(typeof(Configure))]
		Configure,
		[VariantOptionType(typeof(Merge))]
		Merge,
		[VariantOptionType(typeof(DisburseToNeuron))]
		DisburseToNeuron,
		[VariantOptionType(typeof(Command_2.SyncCommandInfo))]
		SyncCommand,
		[VariantOptionType(typeof(ClaimOrRefresh))]
		ClaimOrRefreshNeuron,
		[VariantOptionType(typeof(MergeMaturity))]
		MergeMaturity,
		[VariantOptionType(typeof(Disburse))]
		Disburse
	}
}