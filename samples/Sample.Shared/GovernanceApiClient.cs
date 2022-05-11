using ICP.Agent.Agents;
using ICP.Agent.Responses;
using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using ICP.Candid.Models.Values;
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
                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
            );

            CandidArg encodedArgument = CandidArg.FromCandid(candidArg);

            string governanceCanister = "rrkah-fqaaa-aaaaa-aaaaq-cai";
            Principal canisterId = Principal.FromText(governanceCanister);
            QueryResponse response = await this.agent.QueryAsync(canisterId, method, encodedArgument, identityOverride: null);
            QueryReply reply = response.ThrowOrGetReply();

            CandidValueWithType arg = reply.Arg.Values[0];
            return MapCandid(arg);
        }

        private static ProposalInfo? MapCandid(CandidValueWithType arg)
        {
            return arg.Value.AsOptional(v =>
            {
                return v.AsRecord(r =>
                {
                    return new ProposalInfo
                    {
                        Id = r["id"].AsOptional(i =>
                        {
                            return i.AsRecord(ConvertIdInfo);
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
                                        if (v.Tag == CandidLabel.FromName("RegisterKnownNeuron"))
                                        {
                                            var info = v.Value.AsRecord(r =>
                                            {
                                                return new ProposalInfo.ActionVariant.RegisterKnownNeuronInfo
                                                {
                                                    Id = r["id"].AsOptional(o => o.AsRecord(ConvertIdInfo)),
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
                                        if (v.Tag == CandidLabel.FromName("ManageNeuron"))
                                        {
                                            var info = v.Value.AsRecord(v =>
                                            {
                                                return new ProposalInfo.ActionVariant.ManageNeuronInfo
                                                {
                                                    Id = v["id"].AsOptional(o => o.AsRecord(ConvertIdInfo)),
                                                    Command = v["command"].AsOptional(o => o.AsVariant<ProposalInfo.ActionVariant.ManageNeuronInfo.CommandVariant>(v =>
                                                    {
                                                        // TODO finish
                                                        throw new NotImplementedException();
                                                    }))
                                                };
                                            });
                                            return ProposalInfo.ActionVariant.ManageNeuron(info);
                                        }
                                        if (v.Tag == CandidLabel.FromName("ExecuteNnsFunction"))
                                        {
                                            // TODO finish
                                            throw new NotImplementedException();
                                        }
                                        if (v.Tag == CandidLabel.FromName("RewardNodeProvider"))
                                        {
                                            // TODO finish
                                            throw new NotImplementedException();
                                        }
                                        if (v.Tag == CandidLabel.FromName("SetDefaultFollowees"))
                                        {
                                            // TODO finish
                                            throw new NotImplementedException();
                                        }
                                        if (v.Tag == CandidLabel.FromName("RewardNodeProviders"))
                                        {
                                            // TODO finish
                                            throw new NotImplementedException();
                                        }
                                        if (v.Tag == CandidLabel.FromName("ManageNetworkEconomics"))
                                        {
                                            // TODO finish
                                            throw new NotImplementedException();
                                        }
                                        if (v.Tag == CandidLabel.FromName("ApproveGenesisKyc"))
                                        {
                                            // TODO finish
                                            throw new NotImplementedException();
                                        }
                                        if (v.Tag == CandidLabel.FromName("AddOrRemoveNodeProvider"))
                                        {
                                            // TODO finish
                                            throw new NotImplementedException();
                                        }
                                        if (v.Tag == CandidLabel.FromName("Motion"))
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
                                ulong c = r[CandidLabel.FromId(0)].AsNat64();
                                var prop = r[CandidLabel.FromId(1)].AsRecord(r =>
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
                });
            });
        }

        private static ProposalInfo.IdInfo ConvertIdInfo(CandidRecord r)
        {
            return new ProposalInfo.IdInfo
            {
                Id = r["id"].AsNat64()
            };
        }
    }
}
