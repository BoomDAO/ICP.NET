using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ListNeuronsResponse
	{
		public List<neuron_infosInfo> neuron_infos { get; set; }
		
		public List<Neuron> full_neurons { get; set; }
		
		public class neuron_infosInfo
		{
			public ulong F0 { get; set; }
			
			public NeuronInfo F1 { get; set; }
			
		}
	}
}

