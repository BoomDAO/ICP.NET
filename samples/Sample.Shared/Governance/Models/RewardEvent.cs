namespace Sample.Shared.Governance.Models
{
	public class RewardEvent
	{
		public ulong DayAfterGenesis { get; set; }
		
		public ulong ActualTimestampSeconds { get; set; }
		
		public ulong DistributedE8sEquivalent { get; set; }
		
		public List<NeuronId> SettledProposals { get; set; }
		
	}
}
