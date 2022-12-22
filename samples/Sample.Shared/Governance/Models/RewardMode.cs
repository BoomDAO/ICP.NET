using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public enum RewardModeType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RewardToNeuron")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(RewardToNeuron))]
		RewardToNeuron,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("RewardToAccount")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(RewardToAccount))]
		RewardToAccount,
	}
	public class RewardMode : EdjCase.ICP.Candid.Models.CandidVariantValueBase<RewardModeType>
	{
		public RewardMode(RewardModeType type, System.Object? value)  : base(type, value)
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

