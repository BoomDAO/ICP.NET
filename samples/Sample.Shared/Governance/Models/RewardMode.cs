using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(RewardModeTag))]
	public class RewardMode
	{
		[VariantTagProperty()]
		public RewardModeTag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

		public RewardMode(RewardModeTag tag, object? value = null)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected RewardMode()
		{
		}

		public static RewardMode RewardToNeuron(RewardToNeuron info)
		{
			return new RewardMode(RewardModeTag.RewardToNeuron, info);
		}

		public static RewardMode RewardToAccount(RewardToAccount info)
		{
			return new RewardMode(RewardModeTag.RewardToAccount, info);
		}

		public RewardToNeuron AsRewardToNeuron()
		{
			this.ValidateTag(RewardModeTag.RewardToNeuron);
			return (RewardToNeuron)this.Value!;
		}

		public RewardToAccount AsRewardToAccount()
		{
			this.ValidateTag(RewardModeTag.RewardToAccount);
			return (RewardToAccount)this.Value!;
		}

		private void ValidateTag(RewardModeTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum RewardModeTag
	{
		[VariantOptionType(typeof(RewardToNeuron))]
		RewardToNeuron,
		[VariantOptionType(typeof(RewardToAccount))]
		RewardToAccount
	}
}