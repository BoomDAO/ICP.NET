using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Neuron
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> Id { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("controller")]
		public EdjCase.ICP.Candid.Models.OptionalValue<EdjCase.ICP.Candid.Models.Principal> Controller { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("recent_ballots")]
		public System.Collections.Generic.List<BallotInfo> RecentBallots { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("kyc_verified")]
		public bool KycVerified { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("not_for_profit")]
		public bool NotForProfit { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("maturity_e8s_equivalent")]
		public ulong MaturityE8sEquivalent { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("cached_neuron_stake_e8s")]
		public ulong CachedNeuronStakeE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("created_timestamp_seconds")]
		public ulong CreatedTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("aging_since_timestamp_seconds")]
		public ulong AgingSinceTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("hot_keys")]
		public System.Collections.Generic.List<EdjCase.ICP.Candid.Models.Principal> HotKeys { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("account")]
		public System.Collections.Generic.List<byte> Account { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("joined_community_fund_timestamp_seconds")]
		public EdjCase.ICP.Candid.Models.OptionalValue<ulong> JoinedCommunityFundTimestampSeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolve_state")]
		public EdjCase.ICP.Candid.Models.OptionalValue<DissolveState> DissolveState { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("followees")]
		public System.Collections.Generic.List<Neuron> Followees { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neuron_fees_e8s")]
		public ulong NeuronFeesE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("transfer")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronStakeTransfer> Transfer { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("known_neuron_data")]
		public EdjCase.ICP.Candid.Models.OptionalValue<KnownNeuronData> KnownNeuronData { get; set; }
		
	}
}

