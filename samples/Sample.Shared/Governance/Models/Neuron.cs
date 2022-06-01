namespace Sample.Shared.Governance.Models
{
	public class Neuron
	{
		public NeuronId? Id { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal? Controller { get; set; }
		
		public List<BallotInfo> RecentBallots { get; set; }
		
		public bool KycVerified { get; set; }
		
		public bool NotForProfit { get; set; }
		
		public ulong MaturityE8sEquivalent { get; set; }
		
		public ulong CachedNeuronStakeE8s { get; set; }
		
		public ulong CreatedTimestampSeconds { get; set; }
		
		public ulong AgingSinceTimestampSeconds { get; set; }
		
		public List<EdjCase.ICP.Candid.Models.Principal> HotKeys { get; set; }
		
		public List<byte> Account { get; set; }
		
		public ulong? JoinedCommunityFundTimestampSeconds { get; set; }
		
		public DissolveState? DissolveState { get; set; }
		
		public List<FolloweesInfo> Followees { get; set; }
		
		public ulong NeuronFeesE8s { get; set; }
		
		public NeuronStakeTransfer? Transfer { get; set; }
		
		public KnownNeuronData? KnownNeuronData { get; set; }
		
		public class FolloweesInfo
		{
			public int F0 { get; set; }
			
			public Followees F1 { get; set; }
			
		}
	}
}
