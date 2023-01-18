namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshNeuronFromAccountResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("result")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Result1> Result { get; set; }
		
	}
}

