using ICP.Agent.Agents;
using ICP.Agent.Responses;
using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using ICP.Candid.Models.Values;
using Sample.BlazorWebAssembly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.BlazorWebAssembly.Shared
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
            return arg.Value.AsOptionalValueOrDefault(v =>
            {
                CandidRecord root = v.AsRecord();
                ProposalInfo.Tally? latestTally = root.Fields["latest_tally"].AsOptionalValueOrDefault(t =>
                {
                    CandidRecord tally = t.AsRecord(); // TODO
                    return new ProposalInfo.Tally
                    {
                        No = tally.Fields["no"].AsPrimitive().AsNat64(),
                        Yes = tally.Fields["yes"].AsPrimitive().AsNat64(),
                        Total = tally.Fields["total"].AsPrimitive().AsNat64(),
                        TimestampSeconds = tally.Fields["timestamp_seconds"].AsPrimitive().AsNat64()
                    };
                });
                ProposalInfo.ProposalDetails? proposal = root.Fields["proposal"].AsOptionalValueOrDefault(p =>
                {
                    CandidRecord prop = p.AsRecord();
                    return new ProposalInfo.ProposalDetails
                    {
                        Url = prop.Fields["url"].AsPrimitive().AsText(),
                        Title = prop.Fields["title"].AsOptionalValueOrDefault(v => v.AsPrimitive().AsText()),
                        Action = null // TODO
                    };
                });
                return new ProposalInfo
                {
                    Id = root.Fields["id"].AsOptionalValueOrDefault(i =>
                    {
                        return new ProposalInfo.IdInfo
                        {
                            Id = i.AsRecord().Fields["id"].AsPrimitive().AsNat64()
                        };
                    }),
                    LatestTally = latestTally,
                    Proposal = proposal
                };
            });
        }
    }
}
