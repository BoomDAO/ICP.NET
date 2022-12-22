using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class GovernanceCachedMetrics
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("not_dissolving_neurons_e8s_buckets")]
		public System.Collections.Generic.List<GovernanceCachedMetrics> NotDissolvingNeuronsE8sBuckets { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("garbage_collectable_neurons_count")]
		public ulong GarbageCollectableNeuronsCount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neurons_with_invalid_stake_count")]
		public ulong NeuronsWithInvalidStakeCount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("not_dissolving_neurons_count_buckets")]
		public System.Collections.Generic.List<GovernanceCachedMetrics> NotDissolvingNeuronsCountBuckets { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("total_supply_icp")]
		public ulong TotalSupplyIcp { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neurons_with_less_than_6_months_dissolve_delay_count")]
		public ulong NeuronsWithLessThan6MonthsDissolveDelayCount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolved_neurons_count")]
		public ulong DissolvedNeuronsCount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("total_staked_e8s")]
		public ulong TotalStakedE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("not_dissolving_neurons_count")]
		public ulong NotDissolvingNeuronsCount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolved_neurons_e8s")]
		public ulong DissolvedNeuronsE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neurons_with_less_than_6_months_dissolve_delay_e8s")]
		public ulong NeuronsWithLessThan6MonthsDissolveDelayE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolving_neurons_count_buckets")]
		public System.Collections.Generic.List<GovernanceCachedMetrics> DissolvingNeuronsCountBuckets { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolving_neurons_count")]
		public ulong DissolvingNeuronsCount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolving_neurons_e8s_buckets")]
		public System.Collections.Generic.List<GovernanceCachedMetrics> DissolvingNeuronsE8sBuckets { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("community_fund_total_staked_e8s")]
		public ulong CommunityFundTotalStakedE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("timestamp_seconds")]
		public ulong TimestampSeconds { get; set; }
		
	}
}

