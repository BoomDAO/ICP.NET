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
            CandidValueWithType candidArg = CandidValueWithType.FromObject(proposalId);

            CandidArg encodedArgument = CandidArg.FromCandid(candidArg);

            string governanceCanister = "rrkah-fqaaa-aaaaa-aaaaq-cai";
            Principal canisterId = Principal.FromText(governanceCanister);
            QueryResponse response = await this.agent.QueryAsync(canisterId, method, encodedArgument, identityOverride: null);
            QueryReply reply = response.ThrowOrGetReply();

            CandidValueWithType arg = reply.Arg.Values[0];
            return arg.ToObject<ProposalInfo?>();
        }

    }
}
