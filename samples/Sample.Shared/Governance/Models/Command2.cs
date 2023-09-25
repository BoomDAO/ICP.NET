using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Command2
	{
		[VariantTagProperty()]
		public Command2Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public NeuronId? Spawn { get => this.Tag == Command2Tag.Spawn ? (NeuronId)this.Value : default; set => (this.Tag, this.Value) = (Command2Tag.Spawn, value); }

		public Split? Split { get => this.Tag == Command2Tag.Split ? (Split)this.Value : default; set => (this.Tag, this.Value) = (Command2Tag.Split, value); }

		public Configure? Configure { get => this.Tag == Command2Tag.Configure ? (Configure)this.Value : default; set => (this.Tag, this.Value) = (Command2Tag.Configure, value); }

		public Merge? Merge { get => this.Tag == Command2Tag.Merge ? (Merge)this.Value : default; set => (this.Tag, this.Value) = (Command2Tag.Merge, value); }

		public DisburseToNeuron? DisburseToNeuron { get => this.Tag == Command2Tag.DisburseToNeuron ? (DisburseToNeuron)this.Value : default; set => (this.Tag, this.Value) = (Command2Tag.DisburseToNeuron, value); }

		public Command2.SyncCommandInfo? SyncCommand { get => this.Tag == Command2Tag.SyncCommand ? (Command2.SyncCommandInfo)this.Value : default; set => (this.Tag, this.Value) = (Command2Tag.SyncCommand, value); }

		public ClaimOrRefresh? ClaimOrRefreshNeuron { get => this.Tag == Command2Tag.ClaimOrRefreshNeuron ? (ClaimOrRefresh)this.Value : default; set => (this.Tag, this.Value) = (Command2Tag.ClaimOrRefreshNeuron, value); }

		public MergeMaturity? MergeMaturity { get => this.Tag == Command2Tag.MergeMaturity ? (MergeMaturity)this.Value : default; set => (this.Tag, this.Value) = (Command2Tag.MergeMaturity, value); }

		public Disburse? Disburse { get => this.Tag == Command2Tag.Disburse ? (Disburse)this.Value : default; set => (this.Tag, this.Value) = (Command2Tag.Disburse, value); }

		public Command2(Command2Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Command2()
		{
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