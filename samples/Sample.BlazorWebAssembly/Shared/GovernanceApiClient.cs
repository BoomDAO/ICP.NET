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

        public async Task<ProposalInfo> GetProposalInfoAsync(ulong proposalId)
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
            return new ProposalInfo
            {
                Id = arg.Value.AsRecord().Fields["id"],

            };
        }
    }
}
