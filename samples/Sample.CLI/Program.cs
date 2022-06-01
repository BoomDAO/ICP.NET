using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Agent.Identity;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using Sample.Shared.Governance;

Uri url = new Uri($"https://ic0.app");

var identity = new AnonymousIdentity();
IAgent agent = new HttpAgent(identity, url);


var client = new GovernanceApiClient(agent, Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai"));
var ident = new ED25519Identity(null);
var info = await client.GetProposalInfoAsync(62143, ident);


int a = 1;
