namespace Sample.Shared.Governance.Models
{
	public class ListNeurons
	{
		public List<ulong> NeuronIds { get; set; }
		
		public bool IncludeNeuronsReadableByCaller { get; set; }
		
	}
}
