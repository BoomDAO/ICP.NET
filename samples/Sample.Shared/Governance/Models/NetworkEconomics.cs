namespace Sample.Shared.Governance.Models
{
	public class NetworkEconomics
	{
		public ulong NeuronMinimumStakeE8s { get; set; }
		
		public uint MaxProposalsToKeepPerTopic { get; set; }
		
		public ulong NeuronManagementFeePerProposalE8s { get; set; }
		
		public ulong RejectCostE8s { get; set; }
		
		public ulong TransactionFeeE8s { get; set; }
		
		public ulong NeuronSpawnDissolveDelaySeconds { get; set; }
		
		public ulong MinimumIcpXdrRate { get; set; }
		
		public ulong MaximumNodeProviderRewardsE8s { get; set; }
		
	}
}
