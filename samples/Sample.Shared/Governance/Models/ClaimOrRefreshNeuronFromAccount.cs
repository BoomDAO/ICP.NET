namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshNeuronFromAccount
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("controller")]
		public EdjCase.ICP.Candid.Models.OptionalValue<EdjCase.ICP.Candid.Models.Principal> Controller { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("memo")]
		public ulong Memo { get; set; }
		
	}
}

