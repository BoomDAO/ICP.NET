namespace Sample.Shared.Governance.Models
{
	public class ListNeuronsResponse
	{
		public List<NeuronInfosInfo> NeuronInfos { get; set; }
		
		public List<Neuron> FullNeurons { get; set; }
		
		public class NeuronInfosInfo
		{
			public ulong F0 { get; set; }
			
			public NeuronInfo F1 { get; set; }
			
		}
	}
}
