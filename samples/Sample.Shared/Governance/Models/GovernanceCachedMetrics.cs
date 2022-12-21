using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class GovernanceCachedMetrics
	{
		public List<not_dissolving_neurons_e8s_bucketsInfo> not_dissolving_neurons_e8s_buckets { get; set; }
		
		public ulong garbage_collectable_neurons_count { get; set; }
		
		public ulong neurons_with_invalid_stake_count { get; set; }
		
		public List<not_dissolving_neurons_count_bucketsInfo> not_dissolving_neurons_count_buckets { get; set; }
		
		public ulong total_supply_icp { get; set; }
		
		public ulong neurons_with_less_than_6_months_dissolve_delay_count { get; set; }
		
		public ulong dissolved_neurons_count { get; set; }
		
		public ulong total_staked_e8s { get; set; }
		
		public ulong not_dissolving_neurons_count { get; set; }
		
		public ulong dissolved_neurons_e8s { get; set; }
		
		public ulong neurons_with_less_than_6_months_dissolve_delay_e8s { get; set; }
		
		public List<dissolving_neurons_count_bucketsInfo> dissolving_neurons_count_buckets { get; set; }
		
		public ulong dissolving_neurons_count { get; set; }
		
		public List<dissolving_neurons_e8s_bucketsInfo> dissolving_neurons_e8s_buckets { get; set; }
		
		public ulong community_fund_total_staked_e8s { get; set; }
		
		public ulong timestamp_seconds { get; set; }
		
		public class not_dissolving_neurons_e8s_bucketsInfo
		{
			public ulong F0 { get; set; }
			
			public double F1 { get; set; }
			
		}
		public class not_dissolving_neurons_count_bucketsInfo
		{
			public ulong F0 { get; set; }
			
			public ulong F1 { get; set; }
			
		}
		public class dissolving_neurons_count_bucketsInfo
		{
			public ulong F0 { get; set; }
			
			public ulong F1 { get; set; }
			
		}
		public class dissolving_neurons_e8s_bucketsInfo
		{
			public ulong F0 { get; set; }
			
			public double F1 { get; set; }
			
		}
	}
}

