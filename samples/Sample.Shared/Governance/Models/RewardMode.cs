using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class RewardMode
	{
		[VariantTagProperty()]
		public RewardModeTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public RewardToNeuron? RewardToNeuron { get => this.Tag == RewardModeTag.RewardToNeuron ? (RewardToNeuron)this.Value : default; set => (this.Tag, this.Value) = (RewardModeTag.RewardToNeuron, value); }

		public RewardToAccount? RewardToAccount { get => this.Tag == RewardModeTag.RewardToAccount ? (RewardToAccount)this.Value : default; set => (this.Tag, this.Value) = (RewardModeTag.RewardToAccount, value); }

		public RewardMode(RewardModeTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected RewardMode()
		{
		}
	}

	public enum RewardModeTag
	{
		RewardToNeuron,
		RewardToAccount
	}
}