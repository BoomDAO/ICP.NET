using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Governance
	{
		public class R0V0
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("0")]
			public int F0 { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("1")]
			public Followees F1 { get; set; }
			
		}
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("default_followees")]
		public System.Collections.Generic.List<R0V0> DefaultFollowees { get; set; }
		
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
		
		public class R8V0
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("0")]
			public ulong F0 { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("1")]
			public ProposalData F1 { get; set; }
			
		}
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("proposals")]
		public System.Collections.Generic.List<R8V0> Proposals { get; set; }
		
		public class R9V0
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("0")]
			public ulong F0 { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("1")]
			public NeuronInFlightCommand F1 { get; set; }
			
		}
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("in_flight_commands")]
		public System.Collections.Generic.List<R9V0> InFlightCommands { get; set; }
		
		public class R10V0
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("0")]
			public ulong F0 { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("1")]
			public Neuron F1 { get; set; }
			
		}
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neurons")]
		public System.Collections.Generic.List<R10V0> Neurons { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("genesis_timestamp_seconds")]
		public ulong GenesisTimestampSeconds { get; set; }
		
	}
}

