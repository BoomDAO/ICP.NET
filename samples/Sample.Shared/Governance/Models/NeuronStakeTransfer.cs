using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class NeuronStakeTransfer
	{
		public List<byte> to_subaccount { get; set; }
		
		public ulong neuron_stake_e8s { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal? from { get; set; }
		
		public ulong memo { get; set; }
		
		public List<byte> from_subaccount { get; set; }
		
		public ulong transfer_timestamp { get; set; }
		
		public ulong block_height { get; set; }
		
	}
}

