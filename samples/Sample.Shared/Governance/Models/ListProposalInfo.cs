namespace Sample.Shared.Governance.Models
{
	public class ListProposalInfo
	{
		public List<int> IncludeRewardStatus { get; set; }
		
		public NeuronId? BeforeProposal { get; set; }
		
		public uint Limit { get; set; }
		
		public List<int> ExcludeTopic { get; set; }
		
		public List<int> IncludeStatus { get; set; }
		
	}
}
