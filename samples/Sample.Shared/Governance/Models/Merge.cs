using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class Merge
	{
		[CandidName("source_neuron_id")]
		public OptionalValue<NeuronId> SourceNeuronId { get; set; }

		public Merge(OptionalValue<NeuronId> sourceNeuronId)
		{
			this.SourceNeuronId = sourceNeuronId;
		}

		public Merge()
		{
		}
	}
}