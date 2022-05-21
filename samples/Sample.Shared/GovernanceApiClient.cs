using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using Sample.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Shared
{
    public class GovernanceApiClient
    {
        private readonly IAgent agent;
        public GovernanceApiClient(IAgent agent)
        {
            this.agent = agent;
        }

        public async Task<ProposalInfo?> GetProposalInfoAsync(ulong proposalId)
        {
            string method = "get_proposal_info";
            var candidArg = CandidValueWithType.FromValueAndType(
                CandidPrimitive.Nat64(proposalId),
                new CandidPrimitiveType(PrimitiveType.Nat64)
            );

            CandidArg encodedArgument = CandidArg.FromCandid(candidArg);

            string governanceCanister = "rrkah-fqaaa-aaaaa-aaaaq-cai";
            Principal canisterId = Principal.FromText(governanceCanister);
            QueryResponse response = await this.agent.QueryAsync(canisterId, method, encodedArgument, identityOverride: null);
            QueryReply reply = response.ThrowOrGetReply();

            CandidValueWithType arg = reply.Arg.Values[0];
            return arg.Value.AsOptional(MapProposalInfo);
        }

        private static ProposalInfo MapProposalInfo(CandidValue value)
        {
            return value.AsRecord(MapProposalInfo);
        }

        private static ProposalInfo MapProposalInfo(CandidRecord r)
        {
            return new ProposalInfo
            {
                Id = r["id"].AsOptional(i =>
                {
                    return i.AsRecord(MapIdInfo);
                }),
                LatestTally = r["latest_tally"].AsOptional(t =>
                {
                    return t.AsRecord(tally =>
                    {
                        return new ProposalInfo.Tally
                        {
                            No = tally["no"].AsNat64(),
                            Yes = tally["yes"].AsNat64(),
                            Total = tally["total"].AsNat64(),
                            TimestampSeconds = tally["timestamp_seconds"].AsNat64()
                        };
                    });
                }),
                Proposal = r["proposal"].AsOptional(p =>
                {
                    return p.AsRecord(r2 =>
                    {
                        return new ProposalInfo.ProposalDetails
                        {
                            Url = r2["url"].AsText(),
                            Title = r2["title"].AsOptional(v => v.AsText()),
                            Action = r2["action"].AsOptional(o => o.AsVariant(v =>
                            {
                                if (v.Tag == CandidTag.FromName("RegisterKnownNeuron"))
                                {
                                    var info = v.Value.AsRecord(r =>
                                    {
                                        return new ProposalInfo.ActionVariant.RegisterKnownNeuronInfo
                                        {
                                            Id = r["id"].AsOptional(o => o.AsRecord(MapIdInfo)),
                                            KnownNeuronData = r["knownNeuronData"].AsRecord(r =>
                                            {
                                                return new ProposalInfo.ActionVariant.RegisterKnownNeuronInfo.Data
                                                {
                                                    Name = r["name"].AsText(),
                                                    Description = r["description"].AsOptional(o => o.AsText()),
                                                };
                                            })
                                        };
                                    });
                                    return ProposalInfo.ActionVariant.RegisterKnownNeuron(info);
                                }
                                if (v.Tag == CandidTag.FromName("ManageNeuron"))
                                {
                                    var info = v.Value.AsRecord(v =>
                                    {
                                        return new ProposalInfo.ActionVariant.ManageNeuronInfo
                                        {
                                            Id = v["id"].AsOptional(o => o.AsRecord(MapIdInfo)),
                                            Command = v["command"].AsOptional(o => o.AsVariant<ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant>(v =>
                                            {
                                                if (v.Tag == CandidTag.FromName("Spawn"))
                                                {
                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.SpawnInfo
                                                    {
                                                        NewController = r["new_controller"].AsOptional(c => c.AsPrincipal()),
                                                        Nonce = r["nonce"].AsOptional(c => c.AsNat64()),
                                                        PercentageToSpawn = r["percentage_to_spawn"].AsOptional(c => c.AsNat32())
                                                    });
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.Spawn(info);
                                                }
                                                if (v.Tag == CandidTag.FromName("Split"))
                                                {
                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.SplitInfo
                                                    {
                                                        AmountE8s = r["amount_e8s"].AsNat64()
                                                    });
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.Split(info);
                                                }
                                                if (v.Tag == CandidTag.FromName("Follow"))
                                                {
                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.FollowInfo
                                                    {
                                                        Topic = r["topic"].AsInt32(),
                                                        Followees = r["followees"].AsVector(v => v.AsRecord(MapIdInfo))
                                                    });
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.Follow(info);
                                                }
                                                if (v.Tag == CandidTag.FromName("ClaimOrRefresh"))
                                                {
                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ClaimOrRefreshInfo
                                                    {
                                                        By = r["by"].AsVariant(b =>
                                                        {
                                                            if (v.Tag == CandidTag.FromName("NeuronIdOrSubaccount"))
                                                            {
                                                                return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ClaimOrRefreshInfo.ByVariant.NeuronIdOrSubaccount();
                                                            }
                                                            if (v.Tag == CandidTag.FromName("MemoAndController"))
                                                            {
                                                                var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ClaimOrRefreshInfo.ByVariant.MemoAndControllerInfo
                                                                {
                                                                    Memo = r["memo"].AsNat32(),
                                                                    Controller = r["controller"].AsOptional(o => o.AsPrincipal())
                                                                });
                                                                return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ClaimOrRefreshInfo.ByVariant.MemoAndController(info);
                                                            }
                                                            if (v.Tag == CandidTag.FromName("Memo"))
                                                            {
                                                                return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ClaimOrRefreshInfo.ByVariant.Memo();
                                                            }
                                                            throw new NotImplementedException(v.Tag.ToString());
                                                        })
                                                    });
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ClaimOrRefresh(info);
                                                }
                                                if (v.Tag == CandidTag.FromName("Configure"))
                                                {
                                                    var info = v.Value.AsRecord(r =>
                                                    {
                                                        return new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo
                                                        {
                                                            Operation = r["operation"].AsVariant(v =>
                                                            {
                                                                if (v.Tag == CandidTag.FromName("RemoveHotKey"))
                                                                {
                                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.RemoveHotKeyInfo
                                                                    {
                                                                        HotKeyToRemove = r["hot_key_to_remove"].AsOptional(o => o.AsPrincipal())
                                                                    });
                                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.RemoveHotKey(info);
                                                                }
                                                                if (v.Tag == CandidTag.FromName("AddHotKey"))
                                                                {
                                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.AddHotKeyInfo
                                                                    {
                                                                        NewHotKey = r["new_hot_key"].AsOptional(o => o.AsPrincipal())
                                                                    });
                                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.AddHotKey(info);
                                                                }
                                                                if (v.Tag == CandidTag.FromName("StopDissolving"))
                                                                {
                                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.StopDissolving();
                                                                }
                                                                if (v.Tag == CandidTag.FromName("StartDissolving"))
                                                                {
                                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.StartDissolving();
                                                                }
                                                                if (v.Tag == CandidTag.FromName("IncreaseDissolveDelay"))
                                                                {
                                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.IncreaseDissolveDelayInfo
                                                                    {
                                                                        AdditionalDissolveDeplaySeconds = r["additional_dissolve_deplay_seconds"].AsNat32()
                                                                    });
                                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.IncreaseDissolveDelay(info);
                                                                }
                                                                if (v.Tag == CandidTag.FromName("JoinCommunityFund"))
                                                                {
                                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.JoinCommunityFund();
                                                                }
                                                                if (v.Tag == CandidTag.FromName("SetDissolveTimestamp"))
                                                                {
                                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.SetDissolveTimestampInfo
                                                                    {
                                                                        DissolveTimestampSeconds = r["dissolve_timestamp_seconds"].AsNat64()
                                                                    });
                                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.ConfigureInfo.OperationVariant.SetDissolveTimestamp(info);
                                                                }
                                                                throw new NotImplementedException(v.Tag.ToString());
                                                            })
                                                        };
                                                    });
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.Configure(info);
                                                }
                                                if (v.Tag == CandidTag.FromName("RegisterVote"))
                                                {
                                                    var info = v.Value.AsRecord(r =>
                                                    {
                                                        return new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.RegisterVoteInfo
                                                        {
                                                            Vote = r["vote"].AsInt32(),
                                                            Proposal = r["proposal"].AsRecord(MapIdInfo)
                                                        };
                                                    });
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.RegisterVote(info);
                                                }
                                                if (v.Tag == CandidTag.FromName("Merge"))
                                                {
                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.MergeInfo
                                                    {
                                                        SourceNeuronId = r["source_neuron_id"].AsRecord(MapIdInfo)
                                                    });
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.Merge(info);
                                                }
                                                if (v.Tag == CandidTag.FromName("DisburseToNeuron"))
                                                {
                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.DisburseToNeuronInfo
                                                    {
                                                        DissolveDelaySeconds = r["dissolve_delay_seconds"].AsNat64(),
                                                        KycVerified = r["kyc_verified"].AsBool(),
                                                        AmountE8s = r["amount_e8s"].AsNat64(),
                                                        NewController = r["new_controller"].AsOptional(o => o.AsPrincipal()),
                                                        Nonce = r["nonce"].AsNat64()
                                                    });
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.DisburseToNeuron(info);
                                                }
                                                if (v.Tag == CandidTag.FromName("MakeProposal"))
                                                {
                                                    var info = v.Value.AsRecord(MapProposalInfo);
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.MakeProposal(info);
                                                }
                                                if (v.Tag == CandidTag.FromName("MergeMaturity"))
                                                {
                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.MergeMaturityInfo
                                                    {
                                                        PercentageToMerge = r["percentag_to_merge"].AsNat32()
                                                    });
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.MergeMaturity(info);
                                                }
                                                if (v.Tag == CandidTag.FromName("Disburse"))
                                                {
                                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.DisburseInfo
                                                    {
                                                        ToAccount = r["to_account"].AsOptional(o => o.AsRecord(MapAccountInfo)),
                                                        Amount = r["amount"].AsOptional(o => o.AsRecord(r => new ProposalInfo.AmountInfo
                                                        {
                                                            E8s = r["e8s"].AsNat64()
                                                        })),
                                                    });
                                                    return ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant.Disburse(info);
                                                }
                                                throw new NotImplementedException(v.Tag.ToString());
                                            }))
                                        };
                                    });
                                    return ProposalInfo.ActionVariant.ManageNeuron(info);
                                }
                                if (v.Tag == CandidTag.FromName("ExecuteNnsFunction"))
                                {
                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ExecuteNnsFunctionInfo
                                    {
                                        NnsFunction = r["nns_function"].AsInt32(),
                                        Payload = r["payload"].AsVector(v => v.AsNat8())
                                    });
                                    return ProposalInfo.ActionVariant.ExecuteNnsFunction(info);
                                }
                                if (v.Tag == CandidTag.FromName("RewardNodeProvider"))
                                {
                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.RewardNodeProviderInfo
                                    {
                                        NodeProvider = r["node_provider"].AsOptional(o => o.AsRecord(MapNodeProvider)),
                                        RewardMode = r["reward_mode"].AsOptional(o => o.AsVariant(MapRewardMode))
                                    });
                                    return ProposalInfo.ActionVariant.RewardNodeProvider(info);
                                }
                                if (v.Tag == CandidTag.FromName("SetDefaultFollowees"))
                                {
                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.SetDefaultFolloweesInfo
                                    {
                                        DefaultFollowees = r["default_followees"].AsRecord(r =>
                                        {
                                            return (r[0].AsInt32(), r[1].AsRecord(r => new ProposalInfo.ActionVariant.SetDefaultFolloweesInfo.FolloweesInfo
                                            {
                                                Followees = r["followees"].AsVector(v => v.AsRecord(MapIdInfo))
                                            }));
                                        })
                                    });
                                    return ProposalInfo.ActionVariant.SetDefaultFollowees(info);
                                }
                                if (v.Tag == CandidTag.FromName("RewardNodeProviders"))
                                {
                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.RewardNodeProvidersInfo
                                    {
                                        UseRegistryDerivedRewards = r["use_registry_derived_rewards"].AsOptional(o => o.AsBool()),
                                        Rewards = r["rewards"].AsVector(v => new ProposalInfo.ActionVariant.RewardNodeProvidersInfo.RewardInfo
                                        {
                                            NodeProvider = v.AsOptional(o => o.AsRecord(MapNodeProvider)),
                                            RewardMode = v.AsOptional(o => o.AsVariant(MapRewardMode))
                                        })
                                    });
                                    return ProposalInfo.ActionVariant.RewardNodeProviders(info);
                                }
                                if (v.Tag == CandidTag.FromName("ManageNetworkEconomics"))
                                {
                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ManageNetworkEconomicsInfo
                                    {
                                        MaximumNodeProviderRewardsE8s = r["maximum_node_provider_rewards_e8s"].AsNat64(),
                                        MaxProposalsToKeepPerTopic = r["max_proposals_to_keep_per_topic"].AsNat32(),
                                        MinimumIcpXdrRate = r["minimum_icp_xdr_rate"].AsNat64(),
                                        NeuronManagementFeePerProposalE8s = r["neuron_management_fee_per_proposal_e8s"].AsNat64(),
                                        NeuronMinimumStakeE8s = r["neuron_minimum_stake_e8s"].AsNat64(),
                                        NeuronSpawnDissolveDelaySeconds = r["neuron_spawn_dissolve_delay_seconds"].AsNat64(),
                                        RejectCostE8s = r["reject_cost_e8s"].AsNat64(),
                                        TransactionFeeE8s = r["transaction_fee_e8s"].AsNat64()
                                    });
                                    return ProposalInfo.ActionVariant.ManageNetworkEconomics(info);
                                }
                                if (v.Tag == CandidTag.FromName("ApproveGenesisKyc"))
                                {
                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.ApproveGenesisKycInfo
                                    {
                                        Principals = r["principals"].AsVector(v => v.AsPrincipal()!)
                                    });
                                    return ProposalInfo.ActionVariant.ApproveGenesisKyc(info);
                                }
                                if (v.Tag == CandidTag.FromName("AddOrRemoveNodeProvider"))
                                {
                                    var info = v.Value.AsRecord(r => new ProposalInfo.ActionVariant.AddOrRemoveNodeProviderInfo
                                    {
                                        Change = r["change"].AsVariant(v =>
                                        {
                                            if (v.Tag == CandidTag.FromName("ToAdd"))
                                            {
                                                var info = v.Value.AsRecord(MapNodeProvider);
                                                return ProposalInfo.ActionVariant.AddOrRemoveNodeProviderInfo.ChangeVariant.ToAdd(info);
                                            }
                                            if (v.Tag == CandidTag.FromName("ToRemove"))
                                            {
                                                var info = v.Value.AsRecord(MapNodeProvider);
                                                return ProposalInfo.ActionVariant.AddOrRemoveNodeProviderInfo.ChangeVariant.ToRemove(info);
                                            }
                                            throw new NotImplementedException(v.Tag.ToString());
                                        })
                                    });
                                    return ProposalInfo.ActionVariant.AddOrRemoveNodeProvider(info);
                                }
                                if (v.Tag == CandidTag.FromName("Motion"))
                                {
                                    var info = v.Value.AsRecord(r =>
                                    {
                                        return new ProposalInfo.ActionVariant.MotionInfo
                                        {
                                            MotionText = r["motion_text"].AsText()
                                        };
                                    });
                                    return ProposalInfo.ActionVariant.Motion(info);
                                }
                                throw new NotImplementedException(v.Tag.ToString());
                            })),
                            Summary = r2["summary"].AsText()
                        };
                    });
                }),
                Ballots = r["ballots"].AsVector(v =>
                {
                    return v.AsRecord(r =>
                    {
                        ulong c = r[CandidTag.FromId(0)].AsNat64();
                        var prop = r[CandidTag.FromId(1)].AsRecord(r =>
                        {
                            return new ProposalInfo.VoteInfo
                            {
                                Vote = r["vote"].AsInt32(),
                                VotingPower = r["voting_power"].AsNat64()
                            };
                        });
                        return (c, prop);
                    });
                }),
                Topic = r["topic"].AsInt32(),
                Status = r["status"].AsInt32()
            };
        }

        private static ProposalInfo.RewardModeVariant MapRewardMode(CandidVariant v)
        {
            if (v.Tag == CandidTag.FromName("RewardToNeuron"))
            {
                var info = v.Value.AsRecord(r => new ProposalInfo.RewardModeVariant.RewardToNeuronInfo
                {
                    DissolveDelaySeconds = r["dissolve_delay_seconds"].AsNat64()
                });
                return ProposalInfo.RewardModeVariant.RewardToNeuron(info);
            }
            if (v.Tag == CandidTag.FromName("RewardToAccount"))
            {
                var info = v.Value.AsRecord(r => new ProposalInfo.RewardModeVariant.RewardToAccountInfo
                {
                    ToAccount = r["to_account"].AsRecord(MapAccountInfo)
                });
                return ProposalInfo.RewardModeVariant.RewardToAccount(info);
            }

            throw new NotImplementedException(v.Tag.ToString());
        }

        private static ProposalInfo.NodeProviderInfo MapNodeProvider(CandidRecord r)
        {
            return new ProposalInfo.NodeProviderInfo
            {
                Id = r["id"].AsOptional(o => o.AsPrincipal()),
                AmountE8s = r["amount_e8s"].AsNat64(),
                RewardAccount = r["reward_account"].AsOptional(o => o.AsRecord(MapAccountInfo)),
            };
        }
        private static ProposalInfo.AccountInfo MapAccountInfo(CandidRecord r)
        {
            return new ProposalInfo.AccountInfo
            {
                Hash = r["hash"].AsVector(v => v.AsNat8())
            };
        }

        private static ProposalInfo.IdInfo MapIdInfo(CandidRecord r)
        {
            return new ProposalInfo.IdInfo
            {
                Id = r["id"].AsNat64()
            };
        }
    }
}
