using ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Shared.Models
{
    public class ProposalInfo
    {
        public IdInfo? Id { get; set; }
        public int Status { get; set; }
        public int Topic { get; set; }
        public Failure? FailureReason { get; set; }
        public List<(ulong Value, VoteInfo Info)> Ballots { get; set; }
        public ulong ProposalTimestampSeconds { get; set; }
        public ulong RewardEventRound { get; set; }
        public ulong? DeadlineTimestampSeconds { get; set; }
        public ulong FailedTimestampSeconds { get; set; }
        public ulong RejectCostE8s { get; set; }
        public Tally? LatestTally { get; set; }
        public int RewardStatus { get; set; }
        public ulong DecidedTimestampSeconds { get; set; }
        public ProposalDetails? Proposal { get; set; }
        public IdInfo? Proposer { get; set; }
        public ulong ExecutedTimestampSeconds { get; set; }

        public class Failure
        {
            public string ErrorMessage { get; set; }
            public string ErrorType { get; set; }
            public Failure(string errorMessage, string errorType)
            {
                this.ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
                this.ErrorType = errorType ?? throw new ArgumentNullException(nameof(errorType));
            }
        }

        public class VoteInfo
        {
            public int Vote { get; set; }
            public ulong VotingPower { get; set; }
        }

        public class Tally
        {
            public ulong No { get; set; }
            public ulong Yes { get; set; }
            public ulong Total { get; set; }
            public ulong TimestampSeconds { get; set; }
        }

        public class ProposalDetails
        {
            public string Url { get; set; }
            public string? Title { get; set; }
            public ActionVariant? Action { get; set; }
            public string Summary { get; set; }
        }
        public class ActionVariant
        {
            public ActionVariantType Type { get; }
            private readonly object value;

            private ActionVariant(ActionVariantType type, object value)
            {
                this.Type = type;
                this.value = value;
            }

            public static ActionVariant RegisterKnownNeuron(RegisterKnownNeuronInfo info)
            {
                return new ActionVariant(ActionVariantType.RegisterKnownNeuron, info);
            }
            public static ActionVariant ManageNeuron(ManageNeuronInfo info)
            {
                return new ActionVariant(ActionVariantType.ManageNeuron, info);
            }
            public static ActionVariant ExecuteNnsFunction(ExecuteNnsFunctionInfo info)
            {
                return new ActionVariant(ActionVariantType.ExecuteNnsFunction, info);
            }
            public static ActionVariant RewardNodeProvider(RewardNodeProviderInfo info)
            {
                return new ActionVariant(ActionVariantType.RewardNodeProvider, info);
            }
            public static ActionVariant SetDefaultFollowees(SetDefaultFolloweesInfo info)
            {
                return new ActionVariant(ActionVariantType.SetDefaultFollowees, info);
            }
            public static ActionVariant RewardNodeProviders(RewardNodeProvidersInfo info)
            {
                return new ActionVariant(ActionVariantType.RewardNodeProviders, info);
            }
            public static ActionVariant ManageNetworkEconomics(ManageNetworkEconomicsInfo info)
            {
                return new ActionVariant(ActionVariantType.ManageNetworkEconomics, info);
            }
            public static ActionVariant ApproveGenesisKyc(ApproveGenesisKycInfo info)
            {
                return new ActionVariant(ActionVariantType.ApproveGenesisKyc, info);
            }
            public static ActionVariant AddOrRemoveNodeProvider(AddOrRemoveNodeProviderInfo info)
            {
                return new ActionVariant(ActionVariantType.AddOrRemoveNodeProvider, info);
            }
            public static ActionVariant Motion(MotionInfo info)
            {
                return new ActionVariant(ActionVariantType.Motion, info);
            }

            public class RegisterKnownNeuronInfo
            {
                public IdInfo? Id { get; set; }
                public Data? KnownNeuronData { get; set; }

                public class Data
                {
                    public string Name { get; set; }
                    public string? Description { get; set; }
                }
            }

            public class ManageNeuronInfo
            {
                public IdInfo? Id { get; set; }
                public CommandVariant? Command { get; set; }
                public NeuronIdOrSubaccountVariant? NeuronIdOrSubaccount { get; set; }


                public class CommandVariant
                {
                    public CommandVariantType Type { get; }
                    private readonly object value;

                    private CommandVariant(CommandVariantType type, object value)
                    {
                        this.Type = type;
                        this.value = value;
                    }

                    public static CommandVariant Spawn(SpawnInfo info)
                    {
                        return new CommandVariant(CommandVariantType.Spawn, info);
                    }

                    public static CommandVariant Split(SplitInfo info)
                    {
                        return new CommandVariant(CommandVariantType.Split, info);
                    }

                    public static CommandVariant Follow(FollowInfo info)
                    {
                        return new CommandVariant(CommandVariantType.Follow, info);
                    }

                    public static CommandVariant ClaimOrRefresh(ClaimOrRefreshInfo info)
                    {
                        return new CommandVariant(CommandVariantType.ClaimOrRefresh, info);
                    }

                    public static CommandVariant Configure(ConfigureInfo info)
                    {
                        return new CommandVariant(CommandVariantType.Configure, info);
                    }

                    public static CommandVariant RegisterVote(RegisterVoteInfo info)
                    {
                        return new CommandVariant(CommandVariantType.RegisterVote, info);
                    }

                    public static CommandVariant Merge(MergeInfo info)
                    {
                        return new CommandVariant(CommandVariantType.Merge, info);
                    }

                    public static CommandVariant DisburseToNeuron(DisburseToNeuronInfo info)
                    {
                        return new CommandVariant(CommandVariantType.DisburseToNeuron, info);
                    }

                    public static CommandVariant MakeProposal(ProposalInfo info)
                    {
                        return new CommandVariant(CommandVariantType.MakeProposal, info);
                    }

                    public static CommandVariant MergeMaturity(MergeMaturityInfo info)
                    {
                        return new CommandVariant(CommandVariantType.MergeMaturity, info);
                    }

                    public static CommandVariant Disburse(DisburseInfo info)
                    {
                        return new CommandVariant(CommandVariantType.Disburse, info);
                    }

                    public class SpawnInfo
                    {
                        public uint? PercentageToSpawn { get; set; }
                        public Principal? NewController { get; set; }
                        public ulong? Nonce { get; set; }
                    }

                    public class SplitInfo
                    {
                        public ulong AmountE8s { get; set; }
                    }

                    public class FollowInfo
                    {
                        public int Topic { get; set; }
                        public List<IdInfo> Followees { get; set; }
                    }

                    public class ClaimOrRefreshInfo
                    {
                        public ByVariant? By { get; set; }

                        public class ByVariant
                        {
                            public ByVariantType Type { get; }
                            private readonly object? value;

                            private ByVariant(ByVariantType type, object? value)
                            {
                                this.Type = type;
                                this.value = value;
                            }

                            public static ByVariant NeuronIdOrSubaccount()
                            {
                                return new ByVariant(ByVariantType.NeuronIdOrSubaccount, null);
                            }

                            public static ByVariant MemoAndController(MemoAndControllerInfo info)
                            {
                                return new ByVariant(ByVariantType.MemoAndController, null);
                            }

                            public static ByVariant Memo()
                            {
                                return new ByVariant(ByVariantType.Memo, null);
                            }

                            public class MemoAndControllerInfo
                            {
                                public Principal? Controller { get; set; }
                                public ulong Memo { get; set; }
                            }
                        }

                        public enum ByVariantType
                        {
                            NeuronIdOrSubaccount,
                            MemoAndController,
                            Memo
                        }
                    }

                    public class ConfigureInfo
                    {
                        public OperationVariant? Operation { get; set; }

                        public class OperationVariant
                        {
                            public OperationVariantType Type { get; }
                            private readonly object? value;

                            private OperationVariant(OperationVariantType type, object? value)
                            {
                                this.Type = type;
                                this.value = value;
                            }

                            public static OperationVariant RemoveHotKey(RemoveHotKeyInfo info)
                            {
                                return new OperationVariant(OperationVariantType.RemoveHotKey, info);
                            }

                            public static OperationVariant AddHotKey(AddHotKeyInfo info)
                            {
                                return new OperationVariant(OperationVariantType.AddHotKey, null);
                            }

                            public static OperationVariant StopDissolving()
                            {
                                return new OperationVariant(OperationVariantType.StopDissolving, null);
                            }

                            public static OperationVariant StartDissolving()
                            {
                                return new OperationVariant(OperationVariantType.StartDissolving, null);
                            }

                            public static OperationVariant IncreaseDissolveDelay(IncreaseDissolveDelayInfo info)
                            {
                                return new OperationVariant(OperationVariantType.IncreaseDissolveDelay, info);
                            }

                            public static OperationVariant JoinCommunityFund()
                            {
                                return new OperationVariant(OperationVariantType.JoinCommunityFund, null);
                            }

                            public static OperationVariant SetDissolveTimestamp(SetDissolveTimestampInfo info)
                            {
                                return new OperationVariant(OperationVariantType.SetDissolveTimestamp, info);
                            }

                            public class RemoveHotKeyInfo
                            {
                                public Principal? HotKeyToRemove { get; set; }
                            }

                            public class AddHotKeyInfo
                            {
                                public Principal? NewHotKey { get; set; }
                            }

                            public class IncreaseDissolveDelayInfo
                            {
                                public uint AdditionalDissolveDeplaySeconds { get; set; }
                            }

                            public class SetDissolveTimestampInfo
                            {
                                public ulong DissolveTimestampSeconds { get; set; }
                            }
                        }

                        public enum OperationVariantType
                        {
                            RemoveHotKey,
                            AddHotKey,
                            StopDissolving,
                            StartDissolving,
                            IncreaseDissolveDelay,
                            JoinCommunityFund,
                            SetDissolveTimestamp
                        }
                    }

                    public class RegisterVoteInfo
                    {
                        public int Vote { get; set; }
                        public IdInfo? Proposal { get; set; }
                    }

                    public class MergeInfo
                    {
                        public IdInfo? SourceNeuronId { get; set; }
                    }

                    public class DisburseToNeuronInfo
                    {
                        public ulong DissolveDelaySeconds { get; set; }
                        public bool KycVerified { get; set; }
                        public ulong AmountE8s { get; set; }
                        public Principal? NewControllewr { get; set; }
                        public ulong Nonce { get; set; }
                    }

                    public class MergeMaturityInfo
                    {
                        public uint PercentageToMerge { get; set; }
                    }

                    public class DisburseInfo
                    {
                        public AccountInfo? ToAccount { get; set; }
                        public AmountInfo? Amount { get; set; }
                    }
                }

                public enum CommandVariantType
                {
                    Spawn,
                    Split,
                    Follow,
                    ClaimOrRefresh,
                    Configure,
                    RegisterVote,
                    Merge,
                    DisburseToNeuron,
                    MakeProposal,
                    MergeMaturity,
                    Disburse
                }

                public class NeuronIdOrSubaccountVariant
                {
                    public NeuronIdOrSubaccountVariantType Type { get; }
                    private readonly object? value;

                    private NeuronIdOrSubaccountVariant(NeuronIdOrSubaccountVariantType type, object? value)
                    {
                        this.Type = type;
                        this.value = value;
                    }

                    public static NeuronIdOrSubaccountVariant Subaccount(byte[] info)
                    {
                        return new NeuronIdOrSubaccountVariant(NeuronIdOrSubaccountVariantType.Subaccount, info);
                    }

                    public static NeuronIdOrSubaccountVariant NeuronId(IdInfo info)
                    {
                        return new NeuronIdOrSubaccountVariant(NeuronIdOrSubaccountVariantType.NeuronId, info);
                    }
                }

                public enum NeuronIdOrSubaccountVariantType
                {
                    Subaccount,
                    NeuronId
                }
            }

            public class ExecuteNnsFunctionInfo
            {
                public int NnsFunction { get; set; }
                public byte[] Payload { get; set; }
            }

            public class RewardNodeProviderInfo
            {
                public NodeProviderInfo? NodeProvider { get; set; }
                public RewardModeVariant? RewardMode { get; set; }
            }

            public class SetDefaultFolloweesInfo
            {
                public (int, FolloweesInfo) DefaultFollowees { get; set; }

                public class FolloweesInfo
                {
                    public List<IdInfo> Followees { get; set; }
                }
            }

            public class RewardNodeProvidersInfo
            {
                public bool? UseRegistryDerivedRewards { get; set; }
                public List<RewardInfo> Rewards { get; set; }

                public class RewardInfo
                {
                    public NodeProviderInfo? NodeProvider { get; set; }
                    public RewardModeVariant? RewardMode { get; set; }
                }
            }

            public class ManageNetworkEconomicsInfo
            {
                public ulong NeuronMinimumStakeE8s { get; set; }
                public uint MaxProposalsToKeepPerTopic { get; set; }
                public ulong NeuronManagementFeePerProposalE8s {get; set;}
                public ulong RejectCostE8s { get; set; }
                public ulong TransactionFeeE8s { get; set; }
                public ulong NeuronSpawnDissolveDelaySeconds { get; set; }
                public ulong MinimumIcpXdrRate { get; set; }
                public ulong MaximumNodeProviderRewardsE8s { get; set; }
            }

            public class ApproveGenesisKycInfo
            {
                public List<Principal> Principals { get; set; }
            }

            public class AddOrRemoveNodeProviderInfo
            {
                public ChangeVariant? Change { get; set; }

                public class ChangeVariant
                {
                    public ChangeVariantType Type { get; }
                    private readonly object? value;

                    private ChangeVariant(ChangeVariantType type, object? value)
                    {
                        this.Type = type;
                        this.value = value;
                    }

                    public static ChangeVariant ToRemove(NodeProviderInfo info)
                    {
                        return new ChangeVariant(ChangeVariantType.ToRemove, info);
                    }

                    public static ChangeVariant ToAdd(NodeProviderInfo info)
                    {
                        return new ChangeVariant(ChangeVariantType.ToAdd, info);
                    }
                }

                public enum ChangeVariantType
                {
                    ToRemove,
                    ToAdd
                }
            }

            public class MotionInfo
            {
                public string MotionText { get; set; }
            }
        }

        public enum ActionVariantType
        {
            RegisterKnownNeuron,
            ManageNeuron,
            ExecuteNnsFunction,
            RewardNodeProvider,
            SetDefaultFollowees,
            RewardNodeProviders,
            ManageNetworkEconomics,
            ApproveGenesisKyc,
            AddOrRemoveNodeProvider,
            Motion
        }

        public class IdInfo
        {
            public ulong Id { get; set; }
        }

        public class AccountInfo
        {
            public byte[] Hash { get; set; }
        }

        public class AmountInfo
        {
            public ulong E8s { get; set; }
        }


        public class RewardModeVariant
        {
            public RewardModeVariantType Type { get; }
            private readonly object? value;

            private RewardModeVariant(RewardModeVariantType type, object? value)
            {
                this.Type = type;
                this.value = value;
            }

            public static RewardModeVariant RewardToNeuron(RewardToNeuronInfo info)
            {
                return new RewardModeVariant(RewardModeVariantType.RewardToNeuron, info);
            }

            public static RewardModeVariant RewardToAccount(RewardToAccountInfo info)
            {
                return new RewardModeVariant(RewardModeVariantType.RewardToAccount, info);
            }

            public class RewardToNeuronInfo
            {
                public ulong DissolveDelaySeconds { get; set; }
            }
            public class RewardToAccountInfo
            {
                public AccountInfo? ToAccount { get; set; }
            }
        }

        public enum RewardModeVariantType
        {
            RewardToNeuron,
            RewardToAccount
        }


        public class NodeProviderInfo
        {
            public Principal? Id { get; set; }
            public AccountInfo? RewardAccount { get; set; }
            public ulong AmountE8s { get; set; }
        }
    }
}
