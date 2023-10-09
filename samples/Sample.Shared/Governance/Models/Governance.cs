using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class Governance
	{
		[CandidName("default_followees")]
		public Dictionary<int, Followees> DefaultFollowees { get; set; }

		[CandidName("making_sns_proposal")]
		public OptionalValue<MakingSnsProposal> MakingSnsProposal { get; set; }

		[CandidName("most_recent_monthly_node_provider_rewards")]
		public OptionalValue<MostRecentMonthlyNodeProviderRewards> MostRecentMonthlyNodeProviderRewards { get; set; }

		[CandidName("maturity_modulation_last_updated_at_timestamp_seconds")]
		public OptionalValue<ulong> MaturityModulationLastUpdatedAtTimestampSeconds { get; set; }

		[CandidName("wait_for_quiet_threshold_seconds")]
		public ulong WaitForQuietThresholdSeconds { get; set; }

		[CandidName("metrics")]
		public OptionalValue<GovernanceCachedMetrics> Metrics { get; set; }

		[CandidName("neuron_management_voting_period_seconds")]
		public OptionalValue<ulong> NeuronManagementVotingPeriodSeconds { get; set; }

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

		[CandidName("migrations")]
		public OptionalValue<Migrations> Migrations { get; set; }

		[CandidName("proposals")]
		public Dictionary<ulong, ProposalData> Proposals { get; set; }

		[CandidName("in_flight_commands")]
		public Dictionary<ulong, NeuronInFlightCommand> InFlightCommands { get; set; }

		[CandidName("neurons")]
		public Dictionary<ulong, Neuron> Neurons { get; set; }

		[CandidName("genesis_timestamp_seconds")]
		public ulong GenesisTimestampSeconds { get; set; }

		public Governance(Dictionary<int, Followees> defaultFollowees, OptionalValue<MakingSnsProposal> makingSnsProposal, OptionalValue<MostRecentMonthlyNodeProviderRewards> mostRecentMonthlyNodeProviderRewards, OptionalValue<ulong> maturityModulationLastUpdatedAtTimestampSeconds, ulong waitForQuietThresholdSeconds, OptionalValue<GovernanceCachedMetrics> metrics, OptionalValue<ulong> neuronManagementVotingPeriodSeconds, List<NodeProvider> nodeProviders, OptionalValue<int> cachedDailyMaturityModulationBasisPoints, OptionalValue<NetworkEconomics> economics, OptionalValue<bool> spawningNeurons, OptionalValue<RewardEvent> latestRewardEvent, List<NeuronStakeTransfer> toClaimTransfers, ulong shortVotingPeriodSeconds, OptionalValue<Migrations> migrations, Dictionary<ulong, ProposalData> proposals, Dictionary<ulong, NeuronInFlightCommand> inFlightCommands, Dictionary<ulong, Neuron> neurons, ulong genesisTimestampSeconds)
		{
			this.DefaultFollowees = defaultFollowees;
			this.MakingSnsProposal = makingSnsProposal;
			this.MostRecentMonthlyNodeProviderRewards = mostRecentMonthlyNodeProviderRewards;
			this.MaturityModulationLastUpdatedAtTimestampSeconds = maturityModulationLastUpdatedAtTimestampSeconds;
			this.WaitForQuietThresholdSeconds = waitForQuietThresholdSeconds;
			this.Metrics = metrics;
			this.NeuronManagementVotingPeriodSeconds = neuronManagementVotingPeriodSeconds;
			this.NodeProviders = nodeProviders;
			this.CachedDailyMaturityModulationBasisPoints = cachedDailyMaturityModulationBasisPoints;
			this.Economics = economics;
			this.SpawningNeurons = spawningNeurons;
			this.LatestRewardEvent = latestRewardEvent;
			this.ToClaimTransfers = toClaimTransfers;
			this.ShortVotingPeriodSeconds = shortVotingPeriodSeconds;
			this.Migrations = migrations;
			this.Proposals = proposals;
			this.InFlightCommands = inFlightCommands;
			this.Neurons = neurons;
			this.GenesisTimestampSeconds = genesisTimestampSeconds;
		}

		public Governance()
		{
		}
	}
}