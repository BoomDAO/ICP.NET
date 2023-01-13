namespace Sample.Shared.Governance.Models
{
	public class RewardNodeProviders
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("use_registry_derived_rewards")]
		public EdjCase.ICP.Candid.Models.OptionalValue<bool> UseRegistryDerivedRewards { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("rewards")]
		public System.Collections.Generic.List<RewardNodeProvider> Rewards { get; set; }
		
	}
}

