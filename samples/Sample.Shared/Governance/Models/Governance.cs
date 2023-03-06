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

		[CandidName("wait_for_quiet_threshold_seconds")]
		public ulong WaitForQuietThresholdSeconds { get; set; }

		[CandidName("metrics")]
		public OptionalValue<GovernanceCachedMetrics> Metrics { get; set; }

		[CandidName("node_providers")]
		public List<NodeProvider> NodeProviders { get; set; }

		[CandidName("economics")]
		public OptionalValue<NetworkEconomics> Economics { get; set; }

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

		public Governance(List<Governance.DefaultFolloweesItemRecord> defaultFollowees, ulong waitForQuietThresholdSeconds, OptionalValue<GovernanceCachedMetrics> metrics, List<NodeProvider> nodeProviders, OptionalValue<NetworkEconomics> economics, OptionalValue<RewardEvent> latestRewardEvent, List<NeuronStakeTransfer> toClaimTransfers, ulong shortVotingPeriodSeconds, List<Governance.ProposalsItemRecord> proposals, List<Governance.InFlightCommandsItemRecord> inFlightCommands, List<Governance.NeuronsItemRecord> neurons, ulong genesisTimestampSeconds)
		{
			this.DefaultFollowees = defaultFollowees;
			this.WaitForQuietThresholdSeconds = waitForQuietThresholdSeconds;
			this.Metrics = metrics;
			this.NodeProviders = nodeProviders;
			this.Economics = economics;
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
			[CandidName("0")]
			public int F0 { get; set; }

			[CandidName("1")]
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
			[CandidName("0")]
			public ulong F0 { get; set; }

			[CandidName("1")]
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
			[CandidName("0")]
			public ulong F0 { get; set; }

			[CandidName("1")]
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
			[CandidName("0")]
			public ulong F0 { get; set; }

			[CandidName("1")]
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