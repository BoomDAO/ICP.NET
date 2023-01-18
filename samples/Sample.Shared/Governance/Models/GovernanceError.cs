namespace Sample.Shared.Governance.Models
{
	public class GovernanceError
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("error_message")]
		public string ErrorMessage { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("error_type")]
		public int ErrorType { get; set; }
		
	}
}

