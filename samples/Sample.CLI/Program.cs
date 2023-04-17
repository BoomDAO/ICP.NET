using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using System;
using System.Threading.Tasks;
using EdjCase.ICP.Agent.Identities;
using CommandLine;
using Sample.Shared.Governance;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Agent.Responses;
using System.Collections.Generic;
using EdjCase.ICP.ClientGenerator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

public class Program
{

	public static async Task Main()
	{
		IIdentity? identity = null;
		var agent = new HttpAgent(identity);
		Principal canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
		var client = new GovernanceApiClient(agent, canisterId);
		ulong proposalId = 110174;
		Console.WriteLine($"Getting info for proposal {proposalId}...");
		OptionalValue<Sample.Shared.Governance.Models.ProposalInfo> proposalInfo = await client.GetProposalInfo(proposalId);

		CandidTypedValue rawCandid = CandidTypedValue.FromObject(proposalInfo);
		Console.WriteLine("ProposalInfo:\n" + rawCandid.Value.ToString());

		Console.WriteLine();
		Console.WriteLine($"Getting the state for the governance canister ({canisterId})...");
		var paths = new List<StatePath>
		{
		};
		ReadStateResponse readStateResponse = await agent.ReadStateAsync(canisterId, paths);

		Console.WriteLine("State:\n" + readStateResponse.Certificate.Tree);

		Console.WriteLine("Press ENTER to exit");
		Console.ReadLine();

	}
}

internal class MyCustomCSharpSyntaxRewriter : CSharpSyntaxRewriter
{
}