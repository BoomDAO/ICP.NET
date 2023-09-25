using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class Neuron
	{
		[CandidName("id")]
		public OptionalValue<NeuronId> Id { get; set; }

		[CandidName("staked_maturity_e8s_equivalent")]
		public OptionalValue<ulong> StakedMaturityE8sEquivalent { get; set; }

		[CandidName("controller")]
		public OptionalValue<Principal> Controller { get; set; }

		[CandidName("recent_ballots")]
		public List<BallotInfo> RecentBallots { get; set; }

		[CandidName("kyc_verified")]
		public bool KycVerified { get; set; }

		[CandidName("not_for_profit")]
		public bool NotForProfit { get; set; }

		[CandidName("maturity_e8s_equivalent")]
		public ulong MaturityE8sEquivalent { get; set; }

		[CandidName("cached_neuron_stake_e8s")]
		public ulong CachedNeuronStakeE8s { get; set; }

		[CandidName("created_timestamp_seconds")]
		public ulong CreatedTimestampSeconds { get; set; }

		[CandidName("auto_stake_maturity")]
		public OptionalValue<bool> AutoStakeMaturity { get; set; }

		[CandidName("aging_since_timestamp_seconds")]
		public ulong AgingSinceTimestampSeconds { get; set; }

		[CandidName("hot_keys")]
		public List<Principal> HotKeys { get; set; }

		[CandidName("account")]
		public List<byte> Account { get; set; }

		[CandidName("joined_community_fund_timestamp_seconds")]
		public OptionalValue<ulong> JoinedCommunityFundTimestampSeconds { get; set; }

		[CandidName("dissolve_state")]
		public OptionalValue<DissolveState> DissolveState { get; set; }

		[CandidName("followees")]
		public Dictionary<int, Followees> Followees { get; set; }

		[CandidName("neuron_fees_e8s")]
		public ulong NeuronFeesE8s { get; set; }

		[CandidName("transfer")]
		public OptionalValue<NeuronStakeTransfer> Transfer { get; set; }

		[CandidName("known_neuron_data")]
		public OptionalValue<KnownNeuronData> KnownNeuronData { get; set; }

		[CandidName("spawn_at_timestamp_seconds")]
		public OptionalValue<ulong> SpawnAtTimestampSeconds { get; set; }

		public Neuron(OptionalValue<NeuronId> id, OptionalValue<ulong> stakedMaturityE8sEquivalent, OptionalValue<Principal> controller, List<BallotInfo> recentBallots, bool kycVerified, bool notForProfit, ulong maturityE8sEquivalent, ulong cachedNeuronStakeE8s, ulong createdTimestampSeconds, OptionalValue<bool> autoStakeMaturity, ulong agingSinceTimestampSeconds, List<Principal> hotKeys, List<byte> account, OptionalValue<ulong> joinedCommunityFundTimestampSeconds, OptionalValue<DissolveState> dissolveState, Dictionary<int, Followees> followees, ulong neuronFeesE8s, OptionalValue<NeuronStakeTransfer> transfer, OptionalValue<KnownNeuronData> knownNeuronData, OptionalValue<ulong> spawnAtTimestampSeconds)
		{
			this.Id = id;
			this.StakedMaturityE8sEquivalent = stakedMaturityE8sEquivalent;
			this.Controller = controller;
			this.RecentBallots = recentBallots;
			this.KycVerified = kycVerified;
			this.NotForProfit = notForProfit;
			this.MaturityE8sEquivalent = maturityE8sEquivalent;
			this.CachedNeuronStakeE8s = cachedNeuronStakeE8s;
			this.CreatedTimestampSeconds = createdTimestampSeconds;
			this.AutoStakeMaturity = autoStakeMaturity;
			this.AgingSinceTimestampSeconds = agingSinceTimestampSeconds;
			this.HotKeys = hotKeys;
			this.Account = account;
			this.JoinedCommunityFundTimestampSeconds = joinedCommunityFundTimestampSeconds;
			this.DissolveState = dissolveState;
			this.Followees = followees;
			this.NeuronFeesE8s = neuronFeesE8s;
			this.Transfer = transfer;
			this.KnownNeuronData = knownNeuronData;
			this.SpawnAtTimestampSeconds = spawnAtTimestampSeconds;
		}

		public Neuron()
		{
		}
	}
}