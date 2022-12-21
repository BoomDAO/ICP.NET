using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance;
using System;
using System.Collections.Generic;
using Path = EdjCase.ICP.Candid.Models.Path;

Uri url = new Uri($"https://ic0.app");

var identity = new AnonymousIdentity();
IAgent agent = new HttpAgent(identity, url);


Principal canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
var client = new GovernanceApiClient(agent, canisterId);


var info = await client.get_proposal_info(94182, null);

var paths = new List<Path>
{
	Path.FromSegments("time"),
	Path.FromSegments("canister", "az5sd-cqaaa-aaaae-aaarq-cai", "module_hash"),
};
var a = await agent.ReadStateAsync(Principal.FromText("az5sd-cqaaa-aaaae-aaarq-cai"), paths);


