using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class KnownNeuron
	{
		public NeuronId? id { get; set; }
		
		public KnownNeuronData? known_neuron_data { get; set; }
		
	}
}

