using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class KnownNeuron
	{
		[CandidName("id")]
		public OptionalValue<NeuronId> Id { get; set; }

		[CandidName("known_neuron_data")]
		public OptionalValue<KnownNeuronData> KnownNeuronData { get; set; }

		public KnownNeuron(OptionalValue<NeuronId> id, OptionalValue<KnownNeuronData> knownNeuronData)
		{
			this.Id = id;
			this.KnownNeuronData = knownNeuronData;
		}

		public KnownNeuron()
		{
		}
	}
}