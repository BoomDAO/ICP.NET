using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class SwapParameters
	{
		[CandidName("minimum_participants")]
		public OptionalValue<ulong> MinimumParticipants { get; set; }

		[CandidName("duration")]
		public OptionalValue<Duration> Duration { get; set; }

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

		[CandidName("start_time")]
		public OptionalValue<GlobalTimeOfDay> StartTime { get; set; }

		[CandidName("maximum_icp")]
		public OptionalValue<Tokens> MaximumIcp { get; set; }

		[CandidName("neurons_fund_investment_icp")]
		public OptionalValue<Tokens> NeuronsFundInvestmentIcp { get; set; }

		[CandidName("restricted_countries")]
		public OptionalValue<Countries> RestrictedCountries { get; set; }

		public SwapParameters(OptionalValue<ulong> minimumParticipants, OptionalValue<Duration> duration, OptionalValue<NeuronBasketConstructionParameters> neuronBasketConstructionParameters, OptionalValue<string> confirmationText, OptionalValue<Tokens> maximumParticipantIcp, OptionalValue<Tokens> minimumIcp, OptionalValue<Tokens> minimumParticipantIcp, OptionalValue<GlobalTimeOfDay> startTime, OptionalValue<Tokens> maximumIcp, OptionalValue<Tokens> neuronsFundInvestmentIcp, OptionalValue<Countries> restrictedCountries)
		{
			this.MinimumParticipants = minimumParticipants;
			this.Duration = duration;
			this.NeuronBasketConstructionParameters = neuronBasketConstructionParameters;
			this.ConfirmationText = confirmationText;
			this.MaximumParticipantIcp = maximumParticipantIcp;
			this.MinimumIcp = minimumIcp;
			this.MinimumParticipantIcp = minimumParticipantIcp;
			this.StartTime = startTime;
			this.MaximumIcp = maximumIcp;
			this.NeuronsFundInvestmentIcp = neuronsFundInvestmentIcp;
			this.RestrictedCountries = restrictedCountries;
		}

		public SwapParameters()
		{
		}
	}
}