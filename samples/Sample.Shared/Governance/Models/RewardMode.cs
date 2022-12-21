using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public enum RewardModeType
	{
		RewardToNeuron,
		RewardToAccount,
	}
	public class RewardMode : EdjCase.ICP.Candid.CandidVariantValueBase<RewardModeType>
	{
		public RewardMode(RewardModeType type, object? value)  : base(type, value)
		{
		}
		
		protected RewardMode()
		{
		}
		
		public static RewardMode RewardToNeuron(RewardToNeuron info)
		{
			return new RewardMode(RewardModeType.RewardToNeuron, info);
		}
		
		public RewardToNeuron AsRewardToNeuron()
		{
			this.ValidateType(RewardModeType.RewardToNeuron);
			return (RewardToNeuron)this.value!;
		}
		
		public static RewardMode RewardToAccount(RewardToAccount info)
		{
			return new RewardMode(RewardModeType.RewardToAccount, info);
		}
		
		public RewardToAccount AsRewardToAccount()
		{
			this.ValidateType(RewardModeType.RewardToAccount);
			return (RewardToAccount)this.value!;
		}
		
	}
}

