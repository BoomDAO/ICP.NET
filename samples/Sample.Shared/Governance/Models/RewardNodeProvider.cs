namespace Sample.Shared.Governance.Models
{
	public class RewardNodeProvider
	{
		public NodeProvider? NodeProvider { get; set; }
		
		public RewardMode? RewardMode { get; set; }
		
		public ulong AmountE8s { get; set; }
		
	}
}
