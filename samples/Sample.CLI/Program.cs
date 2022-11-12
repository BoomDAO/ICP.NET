using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance;

Uri url = new Uri($"https://ic0.app");

var identity = new AnonymousIdentity();
IAgent agent = new HttpAgent(identity, url);


Principal canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
var client = new GovernanceApiClient(agent, canisterId);


var info = await client.GetProposalInfoAsync(62143, null);

var paths = new List<List<PathSegment>>
{
	PathSegment.FromMultiString("time"),
	PathSegment.FromMultiString("canister", "az5sd-cqaaa-aaaae-aaarq-cai", "module_hash"),
};
var a = await agent.ReadStateAsync(Principal.FromText("az5sd-cqaaa-aaaae-aaarq-cai"), paths);
