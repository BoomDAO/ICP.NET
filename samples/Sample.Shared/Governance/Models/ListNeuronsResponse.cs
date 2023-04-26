using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ListNeuronsResponse
	{
		[CandidName("neuron_infos")]
		public List<ValueTuple<ulong, NeuronInfo>> NeuronInfos { get; set; }

		[CandidName("full_neurons")]
		public List<Neuron> FullNeurons { get; set; }

		public ListNeuronsResponse(List<ValueTuple<ulong, NeuronInfo>> neuronInfos, List<Neuron> fullNeurons)
		{
			this.NeuronInfos = neuronInfos;
			this.FullNeurons = fullNeurons;
		}

		public ListNeuronsResponse()
		{
		}
	}
}