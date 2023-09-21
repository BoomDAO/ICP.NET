using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class VotingRewardParameters
	{
		[CandidName("reward_rate_transition_duration")]
		public OptionalValue<Duration> RewardRateTransitionDuration { get; set; }

		[CandidName("initial_reward_rate")]
		public OptionalValue<Percentage> InitialRewardRate { get; set; }

		[CandidName("final_reward_rate")]
		public OptionalValue<Percentage> FinalRewardRate { get; set; }

		public VotingRewardParameters(OptionalValue<Duration> rewardRateTransitionDuration, OptionalValue<Percentage> initialRewardRate, OptionalValue<Percentage> finalRewardRate)
		{
			this.RewardRateTransitionDuration = rewardRateTransitionDuration;
			this.InitialRewardRate = initialRewardRate;
			this.FinalRewardRate = finalRewardRate;
		}

		public VotingRewardParameters()
		{
		}
	}
}