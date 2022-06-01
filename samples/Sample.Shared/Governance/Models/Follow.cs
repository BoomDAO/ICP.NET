namespace Sample.Shared.Governance.Models
{
	public class Follow
	{
		public int Topic { get; set; }
		
		public List<NeuronId> Followees { get; set; }
		
	}
}
