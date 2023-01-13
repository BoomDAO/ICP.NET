namespace Sample.Shared.Governance.Models
{
	public class NeuronInfo
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolve_delay_seconds")]
		public ulong DissolveDelaySeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("recent_ballots")]
		public System.Collections.Generic.List<BallotInfo> RecentBallots { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("created_timestamp_seconds")]
		public ulong CreatedTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("state")]
		public int State { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("stake_e8s")]
		public ulong StakeE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("joined_community_fund_timestamp_seconds")]
		public EdjCase.ICP.Candid.Models.OptionalValue<ulong> JoinedCommunityFundTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("retrieved_at_timestamp_seconds")]
		public ulong RetrievedAtTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("known_neuron_data")]
		public EdjCase.ICP.Candid.Models.OptionalValue<KnownNeuronData> KnownNeuronData { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("voting_power")]
		public ulong VotingPower { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("age_seconds")]
		public ulong AgeSeconds { get; set; }
		
	}
}

