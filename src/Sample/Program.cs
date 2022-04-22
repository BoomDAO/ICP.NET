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
string governanceCanister = "rrkah-fqaaa-aaaaa-aaaaq-cai";

PrincipalId canisterId = PrincipalId.FromText(governanceCanister);

string method = "get_proposal_info";
var candidArg = CandidPrimitive.Nat64(54021);
var def = new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64);
CandidArg encodedArgument = CandidArg.FromCandid((candidArg, def));

QueryResponse response = await agent.QueryAsync(canisterId, method, encodedArgument, identityOverride: null);
int a = 1;