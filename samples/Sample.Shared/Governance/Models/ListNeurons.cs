using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ListNeurons
	{
		public List<ulong> neuron_ids { get; set; }
		
		public bool include_neurons_readable_by_caller { get; set; }
		
	}
}

