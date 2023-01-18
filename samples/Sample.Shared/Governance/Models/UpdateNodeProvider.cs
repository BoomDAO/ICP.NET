namespace Sample.Shared.Governance.Models
{
	public class UpdateNodeProvider
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("reward_account")]
		public EdjCase.ICP.Candid.Models.OptionalValue<AccountIdentifier> RewardAccount { get; set; }
		
	}
}

