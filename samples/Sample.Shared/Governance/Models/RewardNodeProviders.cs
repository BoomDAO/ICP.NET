namespace Sample.Shared.Governance.Models
{
	public class RewardNodeProviders
	{
		public bool? UseRegistryDerivedRewards { get; set; }
		
		public List<RewardNodeProvider> Rewards { get; set; }
		
	}
}
