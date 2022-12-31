using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(RewardModeTag))]
	public class RewardMode
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public RewardModeTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private RewardMode(RewardModeTag tag, System.Object? value)
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
		
		public RewardToNeuron AsRewardToNeuron()
		{
			this.ValidateType(RewardModeTag.RewardToNeuron);
			return (RewardToNeuron)this.Value!;
		}
		
		public static RewardMode RewardToAccount(RewardToAccount info)
		{
			return new RewardMode(RewardModeTag.RewardToAccount, info);
		}
		
		public RewardToAccount AsRewardToAccount()
		{
			this.ValidateType(RewardModeTag.RewardToAccount);
			return (RewardToAccount)this.Value!;
		}
		
		private void ValidateType(RewardModeTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum RewardModeTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RewardToNeuron")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(RewardToNeuron))]
		RewardToNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RewardToAccount")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(RewardToAccount))]
		RewardToAccount,
	}
}

