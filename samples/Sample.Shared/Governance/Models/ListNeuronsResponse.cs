using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class ListNeuronsResponse
	{
		[CandidName("neuron_infos")]
		public List<(ulong, NeuronInfo)> NeuronInfos { get; set; }

		[CandidName("full_neurons")]
		public List<Neuron> FullNeurons { get; set; }

		public ListNeuronsResponse(List<(ulong, NeuronInfo)> neuronInfos, List<Neuron> fullNeurons)
		{
			this.NeuronInfos = neuronInfos;
			this.FullNeurons = fullNeurons;
		}

		public ListNeuronsResponse()
		{
		}
	}
}