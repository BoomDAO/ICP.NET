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
using EdjCase.ICP.Agent;
using System.IO;

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
		Console.WriteLine("Press ENTER to exit");
		Console.ReadLine();
	}
}