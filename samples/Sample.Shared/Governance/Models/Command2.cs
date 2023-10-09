using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class Command2
	{
		[VariantTagProperty]
		public Command2Tag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Command2(Command2Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Command2()
		{
		}

		public static Command2 Spawn(NeuronId info)
		{
			return new Command2(Command2Tag.Spawn, info);
		}

		public static Command2 Split(Split info)
		{
			return new Command2(Command2Tag.Split, info);
		}

		public static Command2 Configure(Configure info)
		{
			return new Command2(Command2Tag.Configure, info);
		}

		public static Command2 Merge(Merge info)
		{
			return new Command2(Command2Tag.Merge, info);
		}

		public static Command2 DisburseToNeuron(DisburseToNeuron info)
		{
			return new Command2(Command2Tag.DisburseToNeuron, info);
		}

		public static Command2 SyncCommand(Command2.SyncCommandInfo info)
		{
			return new Command2(Command2Tag.SyncCommand, info);
		}

		public static Command2 ClaimOrRefreshNeuron(ClaimOrRefresh info)
		{
			return new Command2(Command2Tag.ClaimOrRefreshNeuron, info);
		}

		public static Command2 MergeMaturity(MergeMaturity info)
		{
			return new Command2(Command2Tag.MergeMaturity, info);
		}

		public static Command2 Disburse(Disburse info)
		{
			return new Command2(Command2Tag.Disburse, info);
		}

		public NeuronId AsSpawn()
		{
			this.ValidateTag(Command2Tag.Spawn);
			return (NeuronId)this.Value!;
		}

		public Split AsSplit()
		{
			this.ValidateTag(Command2Tag.Split);
			return (Split)this.Value!;
		}

		public Configure AsConfigure()
		{
			this.ValidateTag(Command2Tag.Configure);
			return (Configure)this.Value!;
		}

		public Merge AsMerge()
		{
			this.ValidateTag(Command2Tag.Merge);
			return (Merge)this.Value!;
		}

		public DisburseToNeuron AsDisburseToNeuron()
		{
			this.ValidateTag(Command2Tag.DisburseToNeuron);
			return (DisburseToNeuron)this.Value!;
		}

		public Command2.SyncCommandInfo AsSyncCommand()
		{
			this.ValidateTag(Command2Tag.SyncCommand);
			return (Command2.SyncCommandInfo)this.Value!;
		}

		public ClaimOrRefresh AsClaimOrRefreshNeuron()
		{
			this.ValidateTag(Command2Tag.ClaimOrRefreshNeuron);
			return (ClaimOrRefresh)this.Value!;
		}

		public MergeMaturity AsMergeMaturity()
		{
			this.ValidateTag(Command2Tag.MergeMaturity);
			return (MergeMaturity)this.Value!;
		}

		public Disburse AsDisburse()
		{
			this.ValidateTag(Command2Tag.Disburse);
			return (Disburse)this.Value!;
		}

		private void ValidateTag(Command2Tag tag)
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

	public enum Command2Tag
	{
		Spawn,
		Split,
		Configure,
		Merge,
		DisburseToNeuron,
		SyncCommand,
		ClaimOrRefreshNeuron,
		MergeMaturity,
		Disburse
	}
}