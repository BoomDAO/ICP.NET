using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class ListNeuronsResponse
	{
		[CandidName("neuron_infos")]
		public List<ListNeuronsResponse.NeuronInfosItem> NeuronInfos { get; set; }

		[CandidName("full_neurons")]
		public List<Neuron> FullNeurons { get; set; }

		public ListNeuronsResponse(List<ListNeuronsResponse.NeuronInfosItem> neuronInfos, List<Neuron> fullNeurons)
		{
			this.NeuronInfos = neuronInfos;
			this.FullNeurons = fullNeurons;
		}

		public ListNeuronsResponse()
		{
		}

		public class NeuronInfosItem
		{
			[CandidTag(0U)]
			public ulong F0 { get; set; }

			[CandidTag(1U)]
			public NeuronInfo F1 { get; set; }

			public NeuronInfosItem(ulong f0, NeuronInfo f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public NeuronInfosItem()
			{
			}
		}
	}
}