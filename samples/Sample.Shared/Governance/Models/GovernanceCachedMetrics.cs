using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class GovernanceCachedMetrics
	{
		[CandidName("not_dissolving_neurons_e8s_buckets")]
		public List<GovernanceCachedMetrics.NotDissolvingNeuronsE8sBucketsItemRecord> NotDissolvingNeuronsE8sBuckets { get; set; }

		[CandidName("garbage_collectable_neurons_count")]
		public ulong GarbageCollectableNeuronsCount { get; set; }

		[CandidName("neurons_with_invalid_stake_count")]
		public ulong NeuronsWithInvalidStakeCount { get; set; }

		[CandidName("not_dissolving_neurons_count_buckets")]
		public List<GovernanceCachedMetrics.NotDissolvingNeuronsCountBucketsItemRecord> NotDissolvingNeuronsCountBuckets { get; set; }

		[CandidName("total_supply_icp")]
		public ulong TotalSupplyIcp { get; set; }

		[CandidName("neurons_with_less_than_6_months_dissolve_delay_count")]
		public ulong NeuronsWithLessThan6MonthsDissolveDelayCount { get; set; }

		[CandidName("dissolved_neurons_count")]
		public ulong DissolvedNeuronsCount { get; set; }

		[CandidName("community_fund_total_maturity_e8s_equivalent")]
		public ulong CommunityFundTotalMaturityE8sEquivalent { get; set; }

		[CandidName("total_staked_e8s")]
		public ulong TotalStakedE8s { get; set; }

		[CandidName("not_dissolving_neurons_count")]
		public ulong NotDissolvingNeuronsCount { get; set; }

		[CandidName("total_locked_e8s")]
		public ulong TotalLockedE8s { get; set; }

		[CandidName("dissolved_neurons_e8s")]
		public ulong DissolvedNeuronsE8s { get; set; }

		[CandidName("neurons_with_less_than_6_months_dissolve_delay_e8s")]
		public ulong NeuronsWithLessThan6MonthsDissolveDelayE8s { get; set; }

		[CandidName("dissolving_neurons_count_buckets")]
		public List<GovernanceCachedMetrics.DissolvingNeuronsCountBucketsItemRecord> DissolvingNeuronsCountBuckets { get; set; }

		[CandidName("dissolving_neurons_count")]
		public ulong DissolvingNeuronsCount { get; set; }

		[CandidName("dissolving_neurons_e8s_buckets")]
		public List<GovernanceCachedMetrics.DissolvingNeuronsE8sBucketsItemRecord> DissolvingNeuronsE8sBuckets { get; set; }

		[CandidName("community_fund_total_staked_e8s")]
		public ulong CommunityFundTotalStakedE8s { get; set; }

		[CandidName("timestamp_seconds")]
		public ulong TimestampSeconds { get; set; }

		public GovernanceCachedMetrics(List<GovernanceCachedMetrics.NotDissolvingNeuronsE8sBucketsItemRecord> notDissolvingNeuronsE8sBuckets, ulong garbageCollectableNeuronsCount, ulong neuronsWithInvalidStakeCount, List<GovernanceCachedMetrics.NotDissolvingNeuronsCountBucketsItemRecord> notDissolvingNeuronsCountBuckets, ulong totalSupplyIcp, ulong neuronsWithLessThan6MonthsDissolveDelayCount, ulong dissolvedNeuronsCount, ulong communityFundTotalMaturityE8sEquivalent, ulong totalStakedE8s, ulong notDissolvingNeuronsCount, ulong totalLockedE8s, ulong dissolvedNeuronsE8s, ulong neuronsWithLessThan6MonthsDissolveDelayE8s, List<GovernanceCachedMetrics.DissolvingNeuronsCountBucketsItemRecord> dissolvingNeuronsCountBuckets, ulong dissolvingNeuronsCount, List<GovernanceCachedMetrics.DissolvingNeuronsE8sBucketsItemRecord> dissolvingNeuronsE8sBuckets, ulong communityFundTotalStakedE8s, ulong timestampSeconds)
		{
			this.NotDissolvingNeuronsE8sBuckets = notDissolvingNeuronsE8sBuckets;
			this.GarbageCollectableNeuronsCount = garbageCollectableNeuronsCount;
			this.NeuronsWithInvalidStakeCount = neuronsWithInvalidStakeCount;
			this.NotDissolvingNeuronsCountBuckets = notDissolvingNeuronsCountBuckets;
			this.TotalSupplyIcp = totalSupplyIcp;
			this.NeuronsWithLessThan6MonthsDissolveDelayCount = neuronsWithLessThan6MonthsDissolveDelayCount;
			this.DissolvedNeuronsCount = dissolvedNeuronsCount;
			this.CommunityFundTotalMaturityE8sEquivalent = communityFundTotalMaturityE8sEquivalent;
			this.TotalStakedE8s = totalStakedE8s;
			this.NotDissolvingNeuronsCount = notDissolvingNeuronsCount;
			this.TotalLockedE8s = totalLockedE8s;
			this.DissolvedNeuronsE8s = dissolvedNeuronsE8s;
			this.NeuronsWithLessThan6MonthsDissolveDelayE8s = neuronsWithLessThan6MonthsDissolveDelayE8s;
			this.DissolvingNeuronsCountBuckets = dissolvingNeuronsCountBuckets;
			this.DissolvingNeuronsCount = dissolvingNeuronsCount;
			this.DissolvingNeuronsE8sBuckets = dissolvingNeuronsE8sBuckets;
			this.CommunityFundTotalStakedE8s = communityFundTotalStakedE8s;
			this.TimestampSeconds = timestampSeconds;
		}

		public GovernanceCachedMetrics()
		{
		}

		public class NotDissolvingNeuronsE8sBucketsItemRecord
		{
			[CandidTag(0U)]
			public ulong F0 { get; set; }

			[CandidTag(1U)]
			public double F1 { get; set; }

			public NotDissolvingNeuronsE8sBucketsItemRecord(ulong f0, double f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public NotDissolvingNeuronsE8sBucketsItemRecord()
			{
			}
		}

		public class NotDissolvingNeuronsCountBucketsItemRecord
		{
			[CandidTag(0U)]
			public ulong F0 { get; set; }

			[CandidTag(1U)]
			public ulong F1 { get; set; }

			public NotDissolvingNeuronsCountBucketsItemRecord(ulong f0, ulong f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public NotDissolvingNeuronsCountBucketsItemRecord()
			{
			}
		}

		public class DissolvingNeuronsCountBucketsItemRecord
		{
			[CandidTag(0U)]
			public ulong F0 { get; set; }

			[CandidTag(1U)]
			public ulong F1 { get; set; }

			public DissolvingNeuronsCountBucketsItemRecord(ulong f0, ulong f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public DissolvingNeuronsCountBucketsItemRecord()
			{
			}
		}

		public class DissolvingNeuronsE8sBucketsItemRecord
		{
			[CandidTag(0U)]
			public ulong F0 { get; set; }

			[CandidTag(1U)]
			public double F1 { get; set; }

			public DissolvingNeuronsE8sBucketsItemRecord(ulong f0, double f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public DissolvingNeuronsE8sBucketsItemRecord()
			{
			}
		}
	}
}