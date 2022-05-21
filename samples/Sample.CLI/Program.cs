using ICP.Agent.Agents;
using ICP.Agent.Auth;
using ICP.Agent.Responses;
using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using ICP.Candid.Models.Values;
using Sample.Shared;
using Sample.Shared.Models;

Uri url = new Uri($"https://ic0.app");

var identity = new AnonymousIdentity();
IAgent agent = new HttpAgent(identity, url);

var client = new GovernanceApiClient(agent);

ProposalInfo? info = await client.GetProposalInfoAsync(54021);
