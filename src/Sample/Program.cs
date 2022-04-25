using ICP.Common.Candid;
using ICP.Agent.Agents;
using ICP.Agent.Auth;
using ICP.Agent.Responses;
using ICP.Common.Models;
using Common.Models;

//Uri url = new Uri("http://127.0.0.1:8000");
Uri url = new Uri($"https://ic0.app");

var identity = new AnonymousIdentity();
IAgent agent = new HttpAgent(identity, url);

//string icManagementCanister = "aaaaa-aa";


//QueryResponse response1 = await agent.QueryAsync(canisterId, "get_build_metadata", CandidArg.FromCandid(), identityOverride: null);
//QueryResponse response2 = await agent.QueryAsync(canisterId, "get_neuron_ids", CandidArg.FromCandid(), identityOverride: null);

await CallGetProposalInfoAsync(54021);
await CallPageVisitsAsync();


async Task CallPageVisitsAsync()
{
    // Page Visits - kyle-peacock.com
    PrincipalId canisterId = PrincipalId.FromText("q4j37-xqaaa-aaaab-qadrq-cai");
    string method = "getSummary";
    var route = CandidPrimitive.Text("route1");
    var routeType = new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text);
    CandidArg arg = CandidArg.FromCandid((route, routeType));
    QueryResponse response = await agent.QueryAsync(canisterId, method, arg, identityOverride: null);
}
async Task CallGetProposalInfoAsync(ulong proposalId)
{
    string method = "get_proposal_info";
    var candidArg = CandidPrimitive.Nat64(proposalId);
    var def = new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64);
    CandidArg encodedArgument = CandidArg.FromCandid((candidArg, def));

    string governanceCanister = "rrkah-fqaaa-aaaaa-aaaaq-cai";
    PrincipalId canisterId = PrincipalId.FromText(governanceCanister);
    QueryResponse response = await agent.QueryAsync(canisterId, method, encodedArgument, identityOverride: null);
}