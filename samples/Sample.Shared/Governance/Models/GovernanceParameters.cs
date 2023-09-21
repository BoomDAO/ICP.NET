using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class GovernanceParameters
	{
		[CandidName("neuron_maximum_dissolve_delay_bonus")]
		public OptionalValue<Percentage> NeuronMaximumDissolveDelayBonus { get; set; }

		[CandidName("neuron_maximum_age_for_age_bonus")]
		public OptionalValue<Duration> NeuronMaximumAgeForAgeBonus { get; set; }

		[CandidName("neuron_maximum_dissolve_delay")]
		public OptionalValue<Duration> NeuronMaximumDissolveDelay { get; set; }

		[CandidName("neuron_minimum_dissolve_delay_to_vote")]
		public OptionalValue<Duration> NeuronMinimumDissolveDelayToVote { get; set; }

		[CandidName("neuron_maximum_age_bonus")]
		public OptionalValue<Percentage> NeuronMaximumAgeBonus { get; set; }

		[CandidName("neuron_minimum_stake")]
		public OptionalValue<Tokens> NeuronMinimumStake { get; set; }

		[CandidName("proposal_wait_for_quiet_deadline_increase")]
		public OptionalValue<Duration> ProposalWaitForQuietDeadlineIncrease { get; set; }

		[CandidName("proposal_initial_voting_period")]
		public OptionalValue<Duration> ProposalInitialVotingPeriod { get; set; }

		[CandidName("proposal_rejection_fee")]
		public OptionalValue<Tokens> ProposalRejectionFee { get; set; }

		[CandidName("voting_reward_parameters")]
		public OptionalValue<VotingRewardParameters> VotingRewardParameters { get; set; }

		public GovernanceParameters(OptionalValue<Percentage> neuronMaximumDissolveDelayBonus, OptionalValue<Duration> neuronMaximumAgeForAgeBonus, OptionalValue<Duration> neuronMaximumDissolveDelay, OptionalValue<Duration> neuronMinimumDissolveDelayToVote, OptionalValue<Percentage> neuronMaximumAgeBonus, OptionalValue<Tokens> neuronMinimumStake, OptionalValue<Duration> proposalWaitForQuietDeadlineIncrease, OptionalValue<Duration> proposalInitialVotingPeriod, OptionalValue<Tokens> proposalRejectionFee, OptionalValue<VotingRewardParameters> votingRewardParameters)
		{
			this.NeuronMaximumDissolveDelayBonus = neuronMaximumDissolveDelayBonus;
			this.NeuronMaximumAgeForAgeBonus = neuronMaximumAgeForAgeBonus;
			this.NeuronMaximumDissolveDelay = neuronMaximumDissolveDelay;
			this.NeuronMinimumDissolveDelayToVote = neuronMinimumDissolveDelayToVote;
			this.NeuronMaximumAgeBonus = neuronMaximumAgeBonus;
			this.NeuronMinimumStake = neuronMinimumStake;
			this.ProposalWaitForQuietDeadlineIncrease = proposalWaitForQuietDeadlineIncrease;
			this.ProposalInitialVotingPeriod = proposalInitialVotingPeriod;
			this.ProposalRejectionFee = proposalRejectionFee;
			this.VotingRewardParameters = votingRewardParameters;
		}

		public GovernanceParameters()
		{
		}
	}
}