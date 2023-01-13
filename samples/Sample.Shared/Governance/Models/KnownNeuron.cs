namespace Sample.Shared.Governance.Models
{
	public class KnownNeuron
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> Id { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("known_neuron_data")]
		public EdjCase.ICP.Candid.Models.OptionalValue<KnownNeuronData> KnownNeuronData { get; set; }
		
	}
}

