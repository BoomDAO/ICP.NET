namespace Sample.Shared.Governance.Models
{
	public class NetworkEconomics
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neuron_minimum_stake_e8s")]
		public ulong NeuronMinimumStakeE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("max_proposals_to_keep_per_topic")]
		public uint MaxProposalsToKeepPerTopic { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neuron_management_fee_per_proposal_e8s")]
		public ulong NeuronManagementFeePerProposalE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("reject_cost_e8s")]
		public ulong RejectCostE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("transaction_fee_e8s")]
		public ulong TransactionFeeE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neuron_spawn_dissolve_delay_seconds")]
		public ulong NeuronSpawnDissolveDelaySeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("minimum_icp_xdr_rate")]
		public ulong MinimumIcpXdrRate { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("maximum_node_provider_rewards_e8s")]
		public ulong MaximumNodeProviderRewardsE8s { get; set; }
		
	}
}

