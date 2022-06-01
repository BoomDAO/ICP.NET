namespace Sample.Shared.Governance.Models
{
	public class RegisterVote
	{
		public int Vote { get; set; }
		
		public NeuronId? Proposal { get; set; }
		
	}
}
