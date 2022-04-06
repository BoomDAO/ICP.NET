using ICP.Common.Candid;
using ICP.Agent.Agents;
using ICP.Agent.Auth;
using ICP.Agent.Responses;
using ICP.Common.Models;

var identity = new AnonymousIdentity();
IAgent agent = new HttpAgent(identity, new Uri("http://127.0.0.1:8000"));

string characterDatabaseText = "";
PrincipalId characterDatabase = PrincipalId.FromText(characterDatabaseText);
string method = "create";
var candidArg = CandidRecord.FromDictionary(new Dictionary<string, CandidToken>
{
	{"attack", CandidPrimitive.Nat(UnboundedUInt.FromUInt64(1))}
});
EncodedArgument encodedArgument = EncodedArgument.FromCandid(candidArg);

QueryResponse response = await agent.QueryAsync(characterDatabase, method, encodedArgument, identityOverride: null);