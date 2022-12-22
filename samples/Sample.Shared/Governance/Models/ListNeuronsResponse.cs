using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class ListNeuronsResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neuron_infos")]
		public System.Collections.Generic.List<ListNeuronsResponse> NeuronInfos { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("full_neurons")]
		public System.Collections.Generic.List<Neuron> FullNeurons { get; set; }
		
	}
}

