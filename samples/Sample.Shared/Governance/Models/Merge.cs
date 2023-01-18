namespace Sample.Shared.Governance.Models
{
	public class Merge
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("source_neuron_id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> SourceNeuronId { get; set; }
		
	}
}

