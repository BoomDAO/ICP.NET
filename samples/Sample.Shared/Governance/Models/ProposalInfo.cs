using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ProposalInfo
	{
		[CandidName("id")]
		public OptionalValue<NeuronId> Id { get; set; }

		[CandidName("status")]
		public int Status { get; set; }

		[CandidName("topic")]
		public int Topic { get; set; }

		[CandidName("failure_reason")]
		public OptionalValue<GovernanceError> FailureReason { get; set; }

		[CandidName("ballots")]
		public List<ProposalInfo.BallotsItemRecord> Ballots { get; set; }

		[CandidName("proposal_timestamp_seconds")]
		public ulong ProposalTimestampSeconds { get; set; }

		[CandidName("reward_event_round")]
		public ulong RewardEventRound { get; set; }

		[CandidName("deadline_timestamp_seconds")]
		public OptionalValue<ulong> DeadlineTimestampSeconds { get; set; }

		[CandidName("failed_timestamp_seconds")]
		public ulong FailedTimestampSeconds { get; set; }

		[CandidName("reject_cost_e8s")]
		public ulong RejectCostE8s { get; set; }

		[CandidName("latest_tally")]
		public OptionalValue<Tally> LatestTally { get; set; }

		[CandidName("reward_status")]
		public int RewardStatus { get; set; }

		[CandidName("decided_timestamp_seconds")]
		public ulong DecidedTimestampSeconds { get; set; }

		[CandidName("proposal")]
		public OptionalValue<Proposal> Proposal { get; set; }

		[CandidName("proposer")]
		public OptionalValue<NeuronId> Proposer { get; set; }

		[CandidName("executed_timestamp_seconds")]
		public ulong ExecutedTimestampSeconds { get; set; }

		public ProposalInfo(OptionalValue<NeuronId> id, int status, int topic, OptionalValue<GovernanceError> failureReason, List<ProposalInfo.BallotsItemRecord> ballots, ulong proposalTimestampSeconds, ulong rewardEventRound, OptionalValue<ulong> deadlineTimestampSeconds, ulong failedTimestampSeconds, ulong rejectCostE8s, OptionalValue<Tally> latestTally, int rewardStatus, ulong decidedTimestampSeconds, OptionalValue<Proposal> proposal, OptionalValue<NeuronId> proposer, ulong executedTimestampSeconds)
		{
			this.Id = id;
			this.Status = status;
			this.Topic = topic;
			this.FailureReason = failureReason;
			this.Ballots = ballots;
			this.ProposalTimestampSeconds = proposalTimestampSeconds;
			this.RewardEventRound = rewardEventRound;
			this.DeadlineTimestampSeconds = deadlineTimestampSeconds;
			this.FailedTimestampSeconds = failedTimestampSeconds;
			this.RejectCostE8s = rejectCostE8s;
			this.LatestTally = latestTally;
			this.RewardStatus = rewardStatus;
			this.DecidedTimestampSeconds = decidedTimestampSeconds;
			this.Proposal = proposal;
			this.Proposer = proposer;
			this.ExecutedTimestampSeconds = executedTimestampSeconds;
		}

		public ProposalInfo()
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