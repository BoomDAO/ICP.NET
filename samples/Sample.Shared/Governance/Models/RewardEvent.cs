namespace Sample.Shared.Governance.Models
{
	public class RewardEvent
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("day_after_genesis")]
		public ulong DayAfterGenesis { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("actual_timestamp_seconds")]
		public ulong ActualTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("distributed_e8s_equivalent")]
		public ulong DistributedE8sEquivalent { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("settled_proposals")]
		public System.Collections.Generic.List<NeuronId> SettledProposals { get; set; }
		
	}
}

