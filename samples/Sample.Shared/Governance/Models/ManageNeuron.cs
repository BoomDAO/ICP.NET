namespace Sample.Shared.Governance.Models
{
	public class ManageNeuron
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> Id { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("command")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Command> Command { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neuron_id_or_subaccount")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronIdOrSubaccount> NeuronIdOrSubaccount { get; set; }
		
	}
}

