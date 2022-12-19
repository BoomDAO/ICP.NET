namespace Sample.Shared.Governance.Models
{
	public class RewardNodeProvider
	{
		public NodeProvider? node_provider { get; set; }
		
		public RewardMode? reward_mode { get; set; }
		
		public ulong amount_e8s { get; set; }
		
	}
}
