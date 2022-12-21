using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class Governance
	{
		public List<default_followeesInfo> default_followees { get; set; }
		
		public ulong wait_for_quiet_threshold_seconds { get; set; }
		
		public GovernanceCachedMetrics? metrics { get; set; }
		
		public List<NodeProvider> node_providers { get; set; }
		
		public NetworkEconomics? economics { get; set; }
		
		public RewardEvent? latest_reward_event { get; set; }
		
		public List<NeuronStakeTransfer> to_claim_transfers { get; set; }
		
		public ulong short_voting_period_seconds { get; set; }
		
		public List<proposalsInfo> proposals { get; set; }
		
		public List<in_flight_commandsInfo> in_flight_commands { get; set; }
		
		public List<neuronsInfo> neurons { get; set; }
		
		public ulong genesis_timestamp_seconds { get; set; }
		
		public class default_followeesInfo
		{
			public int F0 { get; set; }
			
			public Followees F1 { get; set; }
			
		}
		public class proposalsInfo
		{
			public ulong F0 { get; set; }
			
			public ProposalData F1 { get; set; }
			
		}
		public class in_flight_commandsInfo
		{
			public ulong F0 { get; set; }
			
			public NeuronInFlightCommand F1 { get; set; }
			
		}
		public class neuronsInfo
		{
			public ulong F0 { get; set; }
			
			public Neuron F1 { get; set; }
			
		}
	}
}

