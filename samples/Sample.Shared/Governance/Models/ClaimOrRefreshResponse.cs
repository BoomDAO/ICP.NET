namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("refreshed_neuron_id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> RefreshedNeuronId { get; set; }
		
	}
}

