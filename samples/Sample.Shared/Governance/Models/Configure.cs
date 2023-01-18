namespace Sample.Shared.Governance.Models
{
	public class Configure
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("operation")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Operation> Operation { get; set; }
		
	}
}

