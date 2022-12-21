using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class RegisterVote
	{
		public int vote { get; set; }
		
		public NeuronId? proposal { get; set; }
		
	}
}

