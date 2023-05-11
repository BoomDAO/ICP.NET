using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class SwapParameters
	{
		[CandidName("minimum_participants")]
		public OptionalValue<ulong> MinimumParticipants { get; set; }

		[CandidName("neuron_basket_construction_parameters")]
		public OptionalValue<NeuronBasketConstructionParameters> NeuronBasketConstructionParameters { get; set; }

		[CandidName("maximum_participant_icp")]
		public OptionalValue<ulong> MaximumParticipantIcp { get; set; }

		[CandidName("minimum_icp")]
		public OptionalValue<ulong> MinimumIcp { get; set; }

		[CandidName("minimum_participant_icp")]
		public OptionalValue<ulong> MinimumParticipantIcp { get; set; }

		[CandidName("maximum_icp")]
		public OptionalValue<ulong> MaximumIcp { get; set; }

		public SwapParameters(OptionalValue<ulong> minimumParticipants, OptionalValue<NeuronBasketConstructionParameters> neuronBasketConstructionParameters, OptionalValue<ulong> maximumParticipantIcp, OptionalValue<ulong> minimumIcp, OptionalValue<ulong> minimumParticipantIcp, OptionalValue<ulong> maximumIcp)
		{
			this.MinimumParticipants = minimumParticipants;
			this.NeuronBasketConstructionParameters = neuronBasketConstructionParameters;
			this.MaximumParticipantIcp = maximumParticipantIcp;
			this.MinimumIcp = minimumIcp;
			this.MinimumParticipantIcp = minimumParticipantIcp;
			this.MaximumIcp = maximumIcp;
		}

		public SwapParameters()
		{
		}
	}
}