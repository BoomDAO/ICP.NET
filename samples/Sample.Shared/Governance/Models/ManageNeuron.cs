using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ManageNeuron
	{
		public NeuronId? id { get; set; }
		
		public Command? command { get; set; }
		
		public NeuronIdOrSubaccount? neuron_id_or_subaccount { get; set; }
		
	}
}

