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

		[CandidName("confirmation_text")]
		public OptionalValue<string> ConfirmationText { get; set; }

		[CandidName("maximum_participant_icp")]
		public OptionalValue<Tokens> MaximumParticipantIcp { get; set; }

		[CandidName("minimum_icp")]
		public OptionalValue<Tokens> MinimumIcp { get; set; }

		[CandidName("minimum_participant_icp")]
		public OptionalValue<Tokens> MinimumParticipantIcp { get; set; }

		[CandidName("maximum_icp")]
		public OptionalValue<Tokens> MaximumIcp { get; set; }

		[CandidName("restricted_countries")]
		public OptionalValue<Countries> RestrictedCountries { get; set; }

		public SwapParameters(OptionalValue<ulong> minimumParticipants, OptionalValue<NeuronBasketConstructionParameters> neuronBasketConstructionParameters, OptionalValue<string> confirmationText, OptionalValue<Tokens> maximumParticipantIcp, OptionalValue<Tokens> minimumIcp, OptionalValue<Tokens> minimumParticipantIcp, OptionalValue<Tokens> maximumIcp, OptionalValue<Countries> restrictedCountries)
		{
			this.MinimumParticipants = minimumParticipants;
			this.NeuronBasketConstructionParameters = neuronBasketConstructionParameters;
			this.ConfirmationText = confirmationText;
			this.MaximumParticipantIcp = maximumParticipantIcp;
			this.MinimumIcp = minimumIcp;
			this.MinimumParticipantIcp = minimumParticipantIcp;
			this.MaximumIcp = maximumIcp;
			this.RestrictedCountries = restrictedCountries;
		}

		public SwapParameters()
		{
		}
	}
}