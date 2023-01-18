namespace Sample.Shared.Governance.Models
{
	public class Proposal
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("url")]
		public string Url { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("title")]
		public EdjCase.ICP.Candid.Models.OptionalValue<string> Title { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("action")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Action> Action { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("summary")]
		public string Summary { get; set; }
		
	}
}

