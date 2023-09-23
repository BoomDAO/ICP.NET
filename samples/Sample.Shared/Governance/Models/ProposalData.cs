using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ProposalData
	{
		[CandidName("id")]
		public OptionalValue<NeuronId> Id { get; set; }

		[CandidName("failure_reason")]
		public OptionalValue<GovernanceError> FailureReason { get; set; }

		[CandidName("cf_participants")]
		public List<CfParticipant> CfParticipants { get; set; }

		[CandidName("ballots")]
		public Dictionary<ulong, Ballot> Ballots { get; set; }

		[CandidName("proposal_timestamp_seconds")]
		public ulong ProposalTimestampSeconds { get; set; }

		[CandidName("reward_event_round")]
		public ulong RewardEventRound { get; set; }

		[CandidName("failed_timestamp_seconds")]
		public ulong FailedTimestampSeconds { get; set; }

		[CandidName("reject_cost_e8s")]
		public ulong RejectCostE8s { get; set; }

		[CandidName("derived_proposal_information")]
		public OptionalValue<DerivedProposalInformation> DerivedProposalInformation { get; set; }

		[CandidName("latest_tally")]
		public OptionalValue<Tally> LatestTally { get; set; }

		[CandidName("sns_token_swap_lifecycle")]
		public OptionalValue<int> SnsTokenSwapLifecycle { get; set; }

		[CandidName("decided_timestamp_seconds")]
		public ulong DecidedTimestampSeconds { get; set; }

		[CandidName("proposal")]
		public OptionalValue<Proposal> Proposal { get; set; }

		[CandidName("proposer")]
		public OptionalValue<NeuronId> Proposer { get; set; }

		[CandidName("wait_for_quiet_state")]
		public OptionalValue<WaitForQuietState> WaitForQuietState { get; set; }

		[CandidName("executed_timestamp_seconds")]
		public ulong ExecutedTimestampSeconds { get; set; }

		[CandidName("original_total_community_fund_maturity_e8s_equivalent")]
		public OptionalValue<ulong> OriginalTotalCommunityFundMaturityE8sEquivalent { get; set; }

		public ProposalData(OptionalValue<NeuronId> id, OptionalValue<GovernanceError> failureReason, List<CfParticipant> cfParticipants, Dictionary<ulong, Ballot> ballots, ulong proposalTimestampSeconds, ulong rewardEventRound, ulong failedTimestampSeconds, ulong rejectCostE8s, OptionalValue<DerivedProposalInformation> derivedProposalInformation, OptionalValue<Tally> latestTally, OptionalValue<int> snsTokenSwapLifecycle, ulong decidedTimestampSeconds, OptionalValue<Proposal> proposal, OptionalValue<NeuronId> proposer, OptionalValue<WaitForQuietState> waitForQuietState, ulong executedTimestampSeconds, OptionalValue<ulong> originalTotalCommunityFundMaturityE8sEquivalent)
		{
			this.Id = id;
			this.FailureReason = failureReason;
			this.CfParticipants = cfParticipants;
			this.Ballots = ballots;
			this.ProposalTimestampSeconds = proposalTimestampSeconds;
			this.RewardEventRound = rewardEventRound;
			this.FailedTimestampSeconds = failedTimestampSeconds;
			this.RejectCostE8s = rejectCostE8s;
			this.DerivedProposalInformation = derivedProposalInformation;
			this.LatestTally = latestTally;
			this.SnsTokenSwapLifecycle = snsTokenSwapLifecycle;
			this.DecidedTimestampSeconds = decidedTimestampSeconds;
			this.Proposal = proposal;
			this.Proposer = proposer;
			this.WaitForQuietState = waitForQuietState;
			this.ExecutedTimestampSeconds = executedTimestampSeconds;
			this.OriginalTotalCommunityFundMaturityE8sEquivalent = originalTotalCommunityFundMaturityE8sEquivalent;
		}

		public ProposalData()
		{
		}
	}
}