using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ListNeurons
	{
		[CandidName("neuron_ids")]
		public List<ulong> NeuronIds { get; set; }

		[CandidName("include_neurons_readable_by_caller")]
		public bool IncludeNeuronsReadableByCaller { get; set; }

		public ListNeurons(List<ulong> neuronIds, bool includeNeuronsReadableByCaller)
		{
			this.NeuronIds = neuronIds;
			this.IncludeNeuronsReadableByCaller = includeNeuronsReadableByCaller;
		}

		public ListNeurons()
		{
		}
	}
}