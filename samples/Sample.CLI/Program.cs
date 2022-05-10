using ICP.Agent.Agents;
using ICP.Agent.Auth;
using ICP.Agent.Responses;
using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using ICP.Candid.Models.Values;

Uri url = new Uri($"https://ic0.app");

var identity = new AnonymousIdentity();
IAgent agent = new HttpAgent(identity, url);


await CallGetProposalInfoAsync(54021);

async Task CallGetProposalInfoAsync(ulong proposalId)
{
    string method = "get_proposal_info";
    var candidArg = CandidValueWithType.FromValueAndType(
        CandidPrimitive.Nat64(proposalId),
        new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
    );

    CandidArg encodedArgument = CandidArg.FromCandid(candidArg);

    string governanceCanister = "rrkah-fqaaa-aaaaa-aaaaq-cai";
    Principal canisterId = Principal.FromText(governanceCanister);
    QueryResponse response = await agent.QueryAsync(canisterId, method, encodedArgument, identityOverride: null);
    QueryReply reply = response.ThrowOrGetReply();

}