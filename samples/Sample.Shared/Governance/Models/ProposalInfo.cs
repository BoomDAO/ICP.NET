namespace Sample.Shared.Governance.Models
{
	public class ProposalInfo
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> Id { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("status")]
		public int Status { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("topic")]
		public int Topic { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("failure_reason")]
		public EdjCase.ICP.Candid.Models.OptionalValue<GovernanceError> FailureReason { get; set; }
		
		public class R4V0
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("0")]
			public ulong F0 { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("1")]
			public Ballot F1 { get; set; }
			
		}
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("ballots")]
		public System.Collections.Generic.List<R4V0> Ballots { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("proposal_timestamp_seconds")]
		public ulong ProposalTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("reward_event_round")]
		public ulong RewardEventRound { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("deadline_timestamp_seconds")]
		public EdjCase.ICP.Candid.Models.OptionalValue<ulong> DeadlineTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("failed_timestamp_seconds")]
		public ulong FailedTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("reject_cost_e8s")]
		public ulong RejectCostE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("latest_tally")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Tally> LatestTally { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("reward_status")]
		public int RewardStatus { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("decided_timestamp_seconds")]
		public ulong DecidedTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("proposal")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Proposal> Proposal { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("proposer")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> Proposer { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("executed_timestamp_seconds")]
		public ulong ExecutedTimestampSeconds { get; set; }
		
	}
}

