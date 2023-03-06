using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class NetworkEconomics
	{
		[CandidName("neuron_minimum_stake_e8s")]
		public ulong NeuronMinimumStakeE8s { get; set; }

		[CandidName("max_proposals_to_keep_per_topic")]
		public uint MaxProposalsToKeepPerTopic { get; set; }

		[CandidName("neuron_management_fee_per_proposal_e8s")]
		public ulong NeuronManagementFeePerProposalE8s { get; set; }

		[CandidName("reject_cost_e8s")]
		public ulong RejectCostE8s { get; set; }

		[CandidName("transaction_fee_e8s")]
		public ulong TransactionFeeE8s { get; set; }

		[CandidName("neuron_spawn_dissolve_delay_seconds")]
		public ulong NeuronSpawnDissolveDelaySeconds { get; set; }

		[CandidName("minimum_icp_xdr_rate")]
		public ulong MinimumIcpXdrRate { get; set; }

		[CandidName("maximum_node_provider_rewards_e8s")]
		public ulong MaximumNodeProviderRewardsE8s { get; set; }

		public NetworkEconomics(ulong neuronMinimumStakeE8s, uint maxProposalsToKeepPerTopic, ulong neuronManagementFeePerProposalE8s, ulong rejectCostE8s, ulong transactionFeeE8s, ulong neuronSpawnDissolveDelaySeconds, ulong minimumIcpXdrRate, ulong maximumNodeProviderRewardsE8s)
		{
			this.NeuronMinimumStakeE8s = neuronMinimumStakeE8s;
			this.MaxProposalsToKeepPerTopic = maxProposalsToKeepPerTopic;
			this.NeuronManagementFeePerProposalE8s = neuronManagementFeePerProposalE8s;
			this.RejectCostE8s = rejectCostE8s;
			this.TransactionFeeE8s = transactionFeeE8s;
			this.NeuronSpawnDissolveDelaySeconds = neuronSpawnDissolveDelaySeconds;
			this.MinimumIcpXdrRate = minimumIcpXdrRate;
			this.MaximumNodeProviderRewardsE8s = maximumNodeProviderRewardsE8s;
		}

		public NetworkEconomics()
		{
		}
	}
}