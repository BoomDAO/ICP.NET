namespace Sample.Shared.Governance.Models
{
	public class ListKnownNeuronsResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("known_neurons")]
		public System.Collections.Generic.List<KnownNeuron> KnownNeurons { get; set; }
		
	}
}

