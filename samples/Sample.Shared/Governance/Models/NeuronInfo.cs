namespace Sample.Shared.Governance.Models
{
	public class NeuronInfo
	{
		public ulong DissolveDelaySeconds { get; set; }
		
		public List<BallotInfo> RecentBallots { get; set; }
		
		public ulong CreatedTimestampSeconds { get; set; }
		
		public int State { get; set; }
		
		public ulong StakeE8s { get; set; }
		
		public ulong? JoinedCommunityFundTimestampSeconds { get; set; }
		
		public ulong RetrievedAtTimestampSeconds { get; set; }
		
		public KnownNeuronData? KnownNeuronData { get; set; }
		
		public ulong VotingPower { get; set; }
		
		public ulong AgeSeconds { get; set; }
		
	}
}
