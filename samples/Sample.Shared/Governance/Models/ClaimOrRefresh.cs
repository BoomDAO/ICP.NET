namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefresh
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("by")]
		public EdjCase.ICP.Candid.Models.OptionalValue<By> By { get; set; }
		
	}
}

