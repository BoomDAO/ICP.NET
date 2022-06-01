namespace Sample.Shared.Governance.Models
{
	public class GovernanceCachedMetrics
	{
		public List<NotDissolvingNeuronsE8sBucketsInfo> NotDissolvingNeuronsE8sBuckets { get; set; }
		
		public ulong GarbageCollectableNeuronsCount { get; set; }
		
		public ulong NeuronsWithInvalidStakeCount { get; set; }
		
		public List<NotDissolvingNeuronsCountBucketsInfo> NotDissolvingNeuronsCountBuckets { get; set; }
		
		public ulong TotalSupplyIcp { get; set; }
		
		public ulong NeuronsWithLessThan6MonthsDissolveDelayCount { get; set; }
		
		public ulong DissolvedNeuronsCount { get; set; }
		
		public ulong TotalStakedE8s { get; set; }
		
		public ulong NotDissolvingNeuronsCount { get; set; }
		
		public ulong DissolvedNeuronsE8s { get; set; }
		
		public ulong NeuronsWithLessThan6MonthsDissolveDelayE8s { get; set; }
		
		public List<DissolvingNeuronsCountBucketsInfo> DissolvingNeuronsCountBuckets { get; set; }
		
		public ulong DissolvingNeuronsCount { get; set; }
		
		public List<DissolvingNeuronsE8sBucketsInfo> DissolvingNeuronsE8sBuckets { get; set; }
		
		public ulong CommunityFundTotalStakedE8s { get; set; }
		
		public ulong TimestampSeconds { get; set; }
		
		public class NotDissolvingNeuronsE8sBucketsInfo
		{
			public ulong F0 { get; set; }
			
			public double F1 { get; set; }
			
		}
		public class NotDissolvingNeuronsCountBucketsInfo
		{
			public ulong F0 { get; set; }
			
			public ulong F1 { get; set; }
			
		}
		public class DissolvingNeuronsCountBucketsInfo
		{
			public ulong F0 { get; set; }
			
			public ulong F1 { get; set; }
			
		}
		public class DissolvingNeuronsE8sBucketsInfo
		{
			public ulong F0 { get; set; }
			
			public double F1 { get; set; }
			
		}
	}
}
