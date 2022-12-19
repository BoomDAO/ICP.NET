namespace Sample.Shared.Governance.Models
{
	public class RewardNodeProviders
	{
		public bool? use_registry_derived_rewards { get; set; }
		
		public List<RewardNodeProvider> rewards { get; set; }
		
	}
}
