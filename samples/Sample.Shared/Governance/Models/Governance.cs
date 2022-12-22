using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Governance
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("default_followees")]
		public System.Collections.Generic.List<Governance> DefaultFollowees { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("wait_for_quiet_threshold_seconds")]
		public ulong WaitForQuietThresholdSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("metrics")]
		public EdjCase.ICP.Candid.Models.OptionalValue<GovernanceCachedMetrics> Metrics { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("node_providers")]
		public System.Collections.Generic.List<NodeProvider> NodeProviders { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("economics")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NetworkEconomics> Economics { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("latest_reward_event")]
		public EdjCase.ICP.Candid.Models.OptionalValue<RewardEvent> LatestRewardEvent { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("to_claim_transfers")]
		public System.Collections.Generic.List<NeuronStakeTransfer> ToClaimTransfers { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("short_voting_period_seconds")]
		public ulong ShortVotingPeriodSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("proposals")]
		public System.Collections.Generic.List<Governance> Proposals { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("in_flight_commands")]
		public System.Collections.Generic.List<Governance> InFlightCommands { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neurons")]
		public System.Collections.Generic.List<Governance> Neurons { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("genesis_timestamp_seconds")]
		public ulong GenesisTimestampSeconds { get; set; }
		
	}
}

