using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ProposalData
	{
		[CandidName("id")]
		public OptionalValue<NeuronId> Id { get; set; }

		[CandidName("failure_reason")]
		public OptionalValue<GovernanceError> FailureReason { get; set; }

		[CandidName("ballots")]
		public List<ProposalData.BallotsItemRecord> Ballots { get; set; }

		[CandidName("proposal_timestamp_seconds")]
		public ulong ProposalTimestampSeconds { get; set; }

		[CandidName("reward_event_round")]
		public ulong RewardEventRound { get; set; }

		[CandidName("failed_timestamp_seconds")]
		public ulong FailedTimestampSeconds { get; set; }

		[CandidName("reject_cost_e8s")]
		public ulong RejectCostE8s { get; set; }

		[CandidName("latest_tally")]
		public OptionalValue<Tally> LatestTally { get; set; }

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

		public ProposalData(OptionalValue<NeuronId> id, OptionalValue<GovernanceError> failureReason, List<ProposalData.BallotsItemRecord> ballots, ulong proposalTimestampSeconds, ulong rewardEventRound, ulong failedTimestampSeconds, ulong rejectCostE8s, OptionalValue<Tally> latestTally, ulong decidedTimestampSeconds, OptionalValue<Proposal> proposal, OptionalValue<NeuronId> proposer, OptionalValue<WaitForQuietState> waitForQuietState, ulong executedTimestampSeconds)
		{
			this.Id = id;
			this.FailureReason = failureReason;
			this.Ballots = ballots;
			this.ProposalTimestampSeconds = proposalTimestampSeconds;
			this.RewardEventRound = rewardEventRound;
			this.FailedTimestampSeconds = failedTimestampSeconds;
			this.RejectCostE8s = rejectCostE8s;
			this.LatestTally = latestTally;
			this.DecidedTimestampSeconds = decidedTimestampSeconds;
			this.Proposal = proposal;
			this.Proposer = proposer;
			this.WaitForQuietState = waitForQuietState;
			this.ExecutedTimestampSeconds = executedTimestampSeconds;
		}

		public ProposalData()
		{
		}

		public class BallotsItemRecord
		{
			[CandidName("0")]
			public ulong F0 { get; set; }

			[CandidName("1")]
			public Ballot F1 { get; set; }

			public BallotsItemRecord(ulong f0, Ballot f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public BallotsItemRecord()
			{
			}
		}
	}
}