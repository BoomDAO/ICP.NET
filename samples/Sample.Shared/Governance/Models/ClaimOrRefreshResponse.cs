using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshResponse
	{
		[CandidName("refreshed_neuron_id")]
		public OptionalValue<NeuronId> RefreshedNeuronId { get; set; }

		public ClaimOrRefreshResponse(OptionalValue<NeuronId> refreshedNeuronId)
		{
			this.RefreshedNeuronId = refreshedNeuronId;
		}

		public ClaimOrRefreshResponse()
		{
		}
	}
}