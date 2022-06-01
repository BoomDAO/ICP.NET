namespace Sample.Shared.Governance.Models
{
	public class Proposal
	{
		public string Url { get; set; }
		
		public string? Title { get; set; }
		
		public Action? Action { get; set; }
		
		public string Summary { get; set; }
		
	}
}
