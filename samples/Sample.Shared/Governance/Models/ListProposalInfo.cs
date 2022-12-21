using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ListProposalInfo
	{
		public List<int> include_reward_status { get; set; }
		
		public NeuronId? before_proposal { get; set; }
		
		public uint limit { get; set; }
		
		public List<int> exclude_topic { get; set; }
		
		public List<int> include_status { get; set; }
		
	}
}

