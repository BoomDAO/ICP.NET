using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class NetworkEconomics
	{
		public ulong neuron_minimum_stake_e8s { get; set; }
		
		public uint max_proposals_to_keep_per_topic { get; set; }
		
		public ulong neuron_management_fee_per_proposal_e8s { get; set; }
		
		public ulong reject_cost_e8s { get; set; }
		
		public ulong transaction_fee_e8s { get; set; }
		
		public ulong neuron_spawn_dissolve_delay_seconds { get; set; }
		
		public ulong minimum_icp_xdr_rate { get; set; }
		
		public ulong maximum_node_provider_rewards_e8s { get; set; }
		
	}
}

