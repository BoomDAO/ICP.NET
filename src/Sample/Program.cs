using ICP.Agent.Agents;
using ICP.Agent.Auth;
using ICP.Agent.Responses;
using ICP.Common.Models;

var identity = new AnonymousIdentity();
IAgent agent = new HttpAgent(identity, new Uri("http://127.0.0.1:8000"));
PrincipalId universeCanister = PrincipalId.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
string method = "delete_player";
EncodedArgument encodedArgument = new EncodedArgument(new byte[0]);

QueryResponse response = await agent.QueryAsync(universeCanister, method, encodedArgument, identityOverride: null);