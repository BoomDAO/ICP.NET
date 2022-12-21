using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class NeuronInfo
	{
		public ulong dissolve_delay_seconds { get; set; }
		
		public List<BallotInfo> recent_ballots { get; set; }
		
		public ulong created_timestamp_seconds { get; set; }
		
		public int state { get; set; }
		
		public ulong stake_e8s { get; set; }
		
		public ulong? joined_community_fund_timestamp_seconds { get; set; }
		
		public ulong retrieved_at_timestamp_seconds { get; set; }
		
		public KnownNeuronData? known_neuron_data { get; set; }
		
		public ulong voting_power { get; set; }
		
		public ulong age_seconds { get; set; }
		
	}
}

