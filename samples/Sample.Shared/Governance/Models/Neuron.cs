namespace Sample.Shared.Governance.Models
{
	public class Neuron
	{
		public NeuronId? id { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal? controller { get; set; }
		
		public List<BallotInfo> recent_ballots { get; set; }
		
		public bool kyc_verified { get; set; }
		
		public bool not_for_profit { get; set; }
		
		public ulong maturity_e8s_equivalent { get; set; }
		
		public ulong cached_neuron_stake_e8s { get; set; }
		
		public ulong created_timestamp_seconds { get; set; }
		
		public ulong aging_since_timestamp_seconds { get; set; }
		
		public List<EdjCase.ICP.Candid.Models.Principal> hot_keys { get; set; }
		
		public List<byte> account { get; set; }
		
		public ulong? joined_community_fund_timestamp_seconds { get; set; }
		
		public DissolveState? dissolve_state { get; set; }
		
		public List<followeesInfo> followees { get; set; }
		
		public ulong neuron_fees_e8s { get; set; }
		
		public NeuronStakeTransfer? transfer { get; set; }
		
		public KnownNeuronData? known_neuron_data { get; set; }
		
		public class followeesInfo
		{
			public int F0 { get; set; }
			
			public Followees F1 { get; set; }
			
		}
	}
}
