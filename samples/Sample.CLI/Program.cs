using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using Sample.Shared.Governance;
using Sample.Shared.Governance.Models;
using System;
using System.Collections.Generic;
using Path = EdjCase.ICP.Candid.Models.Path;

Uri url = new Uri($"https://ic0.app");

var identity = new AnonymousIdentity();
IAgent agent = new HttpAgent(identity, url);


Principal canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
var client = new GovernanceApiClient(agent, canisterId);


var info = await client.ListProposals(, null);

Console.WriteLine(info.GetValueOrDefault()?.Proposal.GetValueOrDefault()?.Title);

// Create identity
var identity = new AnonymousIdentity();

// Create http agent
IAgent agent = new HttpAgent(identity);

// Create Candid arg to send in request
ulong proposalId = 1234;
CandidArg arg = CandidArg.FromCandid(
	new CandidTypedValue(
		value: CandidPrimitive.Nat64(proposalId),
		type: new CandidPrimitiveType(PrimitiveType.Nat64)
	)
);


CandidConverter.Default.FromObject(proposalId)
// Make request to IC
string method = "get_proposal_info";
Principal governanceCanisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
QueryResponse response = await agent.QueryAsync(governanceCanisterId, method, arg);

QueryReply reply = response.ThrowOrGetReply();
// Convert to custom class
OptionalValue<ProposalInfo> info = reply.Arg.Values[0].ToOptionalObject<ProposalInfo>()

