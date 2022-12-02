namespace Sample.Shared.Governance.Models
{
	public class Follow
	{
		public int topic { get; set; }
		
		public List<NeuronId> followees { get; set; }
		
	}
}
