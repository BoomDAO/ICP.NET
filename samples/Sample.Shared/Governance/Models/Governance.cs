namespace Sample.Shared.Governance.Models
{
	public class Governance
	{
		public List<DefaultFolloweesInfo> DefaultFollowees { get; set; }
		
		public ulong WaitForQuietThresholdSeconds { get; set; }
		
		public GovernanceCachedMetrics? Metrics { get; set; }
		
		public List<NodeProvider> NodeProviders { get; set; }
		
		public NetworkEconomics? Economics { get; set; }
		
		public RewardEvent? LatestRewardEvent { get; set; }
		
		public List<NeuronStakeTransfer> ToClaimTransfers { get; set; }
		
		public ulong ShortVotingPeriodSeconds { get; set; }
		
		public List<ProposalsInfo> Proposals { get; set; }
		
		public List<InFlightCommandsInfo> InFlightCommands { get; set; }
		
		public List<NeuronsInfo> Neurons { get; set; }
		
		public ulong GenesisTimestampSeconds { get; set; }
		
		public class DefaultFolloweesInfo
		{
			public int F0 { get; set; }
			
			public Followees F1 { get; set; }
			
		}
		public class ProposalsInfo
		{
			public ulong F0 { get; set; }
			
			public ProposalData F1 { get; set; }
			
		}
		public class InFlightCommandsInfo
		{
			public ulong F0 { get; set; }
			
			public NeuronInFlightCommand F1 { get; set; }
			
		}
		public class NeuronsInfo
		{
			public ulong F0 { get; set; }
			
			public Neuron F1 { get; set; }
			
		}
	}
}
