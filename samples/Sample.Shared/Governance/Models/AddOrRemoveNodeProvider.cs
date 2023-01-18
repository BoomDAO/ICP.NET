namespace Sample.Shared.Governance.Models
{
	public class AddOrRemoveNodeProvider
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("change")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Change> Change { get; set; }
		
	}
}

