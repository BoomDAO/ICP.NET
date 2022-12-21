using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class RewardEvent
	{
		public ulong day_after_genesis { get; set; }
		
		public ulong actual_timestamp_seconds { get; set; }
		
		public ulong distributed_e8s_equivalent { get; set; }
		
		public List<NeuronId> settled_proposals { get; set; }
		
	}
}

