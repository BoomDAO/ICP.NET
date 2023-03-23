using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class Governance
	{
		[CandidName("default_followees")]
		public List<Governance.DefaultFolloweesItemRecord> DefaultFollowees { get; set; }

		[CandidName("most_recent_monthly_node_provider_rewards")]
		public OptionalValue<MostRecentMonthlyNodeProviderRewards> MostRecentMonthlyNodeProviderRewards { get; set; }

		[CandidName("maturity_modulation_last_updated_at_timestamp_seconds")]
		public OptionalValue<ulong> MaturityModulationLastUpdatedAtTimestampSeconds { get; set; }

		[CandidName("wait_for_quiet_threshold_seconds")]
		public ulong WaitForQuietThresholdSeconds { get; set; }

		[CandidName("metrics")]
		public OptionalValue<GovernanceCachedMetrics> Metrics { get; set; }

		[CandidName("node_providers")]
		public List<NodeProvider> NodeProviders { get; set; }

		[CandidName("cached_daily_maturity_modulation_basis_points")]
		public OptionalValue<int> CachedDailyMaturityModulationBasisPoints { get; set; }

		[CandidName("economics")]
		public OptionalValue<NetworkEconomics> Economics { get; set; }

		[CandidName("spawning_neurons")]
		public OptionalValue<bool> SpawningNeurons { get; set; }

		[CandidName("latest_reward_event")]
		public OptionalValue<RewardEvent> LatestRewardEvent { get; set; }

		[CandidName("to_claim_transfers")]
		public List<NeuronStakeTransfer> ToClaimTransfers { get; set; }

		[CandidName("short_voting_period_seconds")]
		public ulong ShortVotingPeriodSeconds { get; set; }

		[CandidName("proposals")]
		public List<Governance.ProposalsItemRecord> Proposals { get; set; }

		[CandidName("in_flight_commands")]
		public List<Governance.InFlightCommandsItemRecord> InFlightCommands { get; set; }

		[CandidName("neurons")]
		public List<Governance.NeuronsItemRecord> Neurons { get; set; }

		[CandidName("genesis_timestamp_seconds")]
		public ulong GenesisTimestampSeconds { get; set; }

		public Governance(List<Governance.DefaultFolloweesItemRecord> defaultFollowees, OptionalValue<MostRecentMonthlyNodeProviderRewards> mostRecentMonthlyNodeProviderRewards, OptionalValue<ulong> maturityModulationLastUpdatedAtTimestampSeconds, ulong waitForQuietThresholdSeconds, OptionalValue<GovernanceCachedMetrics> metrics, List<NodeProvider> nodeProviders, OptionalValue<int> cachedDailyMaturityModulationBasisPoints, OptionalValue<NetworkEconomics> economics, OptionalValue<bool> spawningNeurons, OptionalValue<RewardEvent> latestRewardEvent, List<NeuronStakeTransfer> toClaimTransfers, ulong shortVotingPeriodSeconds, List<Governance.ProposalsItemRecord> proposals, List<Governance.InFlightCommandsItemRecord> inFlightCommands, List<Governance.NeuronsItemRecord> neurons, ulong genesisTimestampSeconds)
		{
			this.DefaultFollowees = defaultFollowees;
			this.MostRecentMonthlyNodeProviderRewards = mostRecentMonthlyNodeProviderRewards;
			this.MaturityModulationLastUpdatedAtTimestampSeconds = maturityModulationLastUpdatedAtTimestampSeconds;
			this.WaitForQuietThresholdSeconds = waitForQuietThresholdSeconds;
			this.Metrics = metrics;
			this.NodeProviders = nodeProviders;
			this.CachedDailyMaturityModulationBasisPoints = cachedDailyMaturityModulationBasisPoints;
			this.Economics = economics;
			this.SpawningNeurons = spawningNeurons;
			this.LatestRewardEvent = latestRewardEvent;
			this.ToClaimTransfers = toClaimTransfers;
			this.ShortVotingPeriodSeconds = shortVotingPeriodSeconds;
			this.Proposals = proposals;
			this.InFlightCommands = inFlightCommands;
			this.Neurons = neurons;
			this.GenesisTimestampSeconds = genesisTimestampSeconds;
		}

		public Governance()
		{
		}

		public class DefaultFolloweesItemRecord
		{
			[CandidTag(0U)]
			public int F0 { get; set; }

			[CandidTag(1U)]
			public Followees F1 { get; set; }

			public DefaultFolloweesItemRecord(int f0, Followees f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public DefaultFolloweesItemRecord()
			{
			}
		}

		public class ProposalsItemRecord
		{
			[CandidTag(0U)]
			public ulong F0 { get; set; }

			[CandidTag(1U)]
			public ProposalData F1 { get; set; }

			public ProposalsItemRecord(ulong f0, ProposalData f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public ProposalsItemRecord()
			{
			}
		}

		public class InFlightCommandsItemRecord
		{
			[CandidTag(0U)]
			public ulong F0 { get; set; }

			[CandidTag(1U)]
			public NeuronInFlightCommand F1 { get; set; }

			public InFlightCommandsItemRecord(ulong f0, NeuronInFlightCommand f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public InFlightCommandsItemRecord()
			{
			}
		}

		public class NeuronsItemRecord
		{
			[CandidTag(0U)]
			public ulong F0 { get; set; }

			[CandidTag(1U)]
			public Neuron F1 { get; set; }

			public NeuronsItemRecord(ulong f0, Neuron f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public NeuronsItemRecord()
			{
			}
		}
	}
}