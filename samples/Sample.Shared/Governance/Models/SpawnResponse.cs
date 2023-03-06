using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class SpawnResponse
	{
		[CandidName("created_neuron_id")]
		public OptionalValue<NeuronId> CreatedNeuronId { get; set; }

		public SpawnResponse(OptionalValue<NeuronId> createdNeuronId)
		{
			this.CreatedNeuronId = createdNeuronId;
		}

		public SpawnResponse()
		{
		}
	}
}