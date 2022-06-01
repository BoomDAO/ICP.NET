using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using Sample.Shared;
using Sample.Shared.Models;

Uri url = new Uri($"https://ic0.app");

var identity = new AnonymousIdentity();
IAgent agent = new HttpAgent(identity, url);

var client = new GovernanceApiClient(agent);


ProposalInfo? info = await client.GetProposalInfoAsync(62143);
int a = 1;
string sourceDir = @"C:\Git\ICP.NET\samples\Sample.CLI\Client";
ClientFileGenerator.WriteClientFiles("Dex.did", sourceDir);