using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class NeuronInfo
	{
		[CandidName("dissolve_delay_seconds")]
		public ulong DissolveDelaySeconds { get; set; }

		[CandidName("recent_ballots")]
		public List<BallotInfo> RecentBallots { get; set; }

		[CandidName("created_timestamp_seconds")]
		public ulong CreatedTimestampSeconds { get; set; }

		[CandidName("state")]
		public int State { get; set; }

		[CandidName("stake_e8s")]
		public ulong StakeE8s { get; set; }

		[CandidName("joined_community_fund_timestamp_seconds")]
		public OptionalValue<ulong> JoinedCommunityFundTimestampSeconds { get; set; }

		[CandidName("retrieved_at_timestamp_seconds")]
		public ulong RetrievedAtTimestampSeconds { get; set; }

		[CandidName("known_neuron_data")]
		public OptionalValue<KnownNeuronData> KnownNeuronData { get; set; }

		[CandidName("voting_power")]
		public ulong VotingPower { get; set; }

		[CandidName("age_seconds")]
		public ulong AgeSeconds { get; set; }

		public NeuronInfo(ulong dissolveDelaySeconds, List<BallotInfo> recentBallots, ulong createdTimestampSeconds, int state, ulong stakeE8s, OptionalValue<ulong> joinedCommunityFundTimestampSeconds, ulong retrievedAtTimestampSeconds, OptionalValue<KnownNeuronData> knownNeuronData, ulong votingPower, ulong ageSeconds)
		{
			this.DissolveDelaySeconds = dissolveDelaySeconds;
			this.RecentBallots = recentBallots;
			this.CreatedTimestampSeconds = createdTimestampSeconds;
			this.State = state;
			this.StakeE8s = stakeE8s;
			this.JoinedCommunityFundTimestampSeconds = joinedCommunityFundTimestampSeconds;
			this.RetrievedAtTimestampSeconds = retrievedAtTimestampSeconds;
			this.KnownNeuronData = knownNeuronData;
			this.VotingPower = votingPower;
			this.AgeSeconds = ageSeconds;
		}

		public NeuronInfo()
		{
		}
	}
}