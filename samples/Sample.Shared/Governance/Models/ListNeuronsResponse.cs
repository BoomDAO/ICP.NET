namespace Sample.Shared.Governance.Models
{
	public class ListNeuronsResponse
	{
		public class R0V0
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("0")]
			public ulong F0 { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("1")]
			public NeuronInfo F1 { get; set; }
			
		}
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neuron_infos")]
		public System.Collections.Generic.List<R0V0> NeuronInfos { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("full_neurons")]
		public System.Collections.Generic.List<Neuron> FullNeurons { get; set; }
		
	}
}

