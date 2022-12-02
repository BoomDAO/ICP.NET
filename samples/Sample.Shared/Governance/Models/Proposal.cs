namespace Sample.Shared.Governance.Models
{
	public class Proposal
	{
		public string url { get; set; }
		
		public string? title { get; set; }
		
		public Action? action { get; set; }
		
		public string summary { get; set; }
		
	}
}
