namespace Sample.Shared.Governance.Models
{
	public class RewardToAccount
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("to_account")]
		public EdjCase.ICP.Candid.Models.OptionalValue<AccountIdentifier> ToAccount { get; set; }
		
	}
}

