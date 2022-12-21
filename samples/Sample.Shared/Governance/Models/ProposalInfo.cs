using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ProposalInfo
	{
		public NeuronId? id { get; set; }
		
		public int status { get; set; }
		
		public int topic { get; set; }
		
		public GovernanceError? failure_reason { get; set; }
		
		public List<ballotsInfo> ballots { get; set; }
		
		public ulong proposal_timestamp_seconds { get; set; }
		
		public ulong reward_event_round { get; set; }
		
		public ulong? deadline_timestamp_seconds { get; set; }
		
		public ulong failed_timestamp_seconds { get; set; }
		
		public ulong reject_cost_e8s { get; set; }
		
		public Tally? latest_tally { get; set; }
		
		public int reward_status { get; set; }
		
		public ulong decided_timestamp_seconds { get; set; }
		
		public Proposal? proposal { get; set; }
		
		public NeuronId? proposer { get; set; }
		
		public ulong executed_timestamp_seconds { get; set; }
		
		public class ballotsInfo
		{
			public ulong F0 { get; set; }
			
			public Ballot F1 { get; set; }
			
		}
	}
}

