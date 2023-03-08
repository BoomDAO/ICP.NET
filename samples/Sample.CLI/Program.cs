using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using System;
using System.Threading.Tasks;
using EdjCase.ICP.InternetIdentity;
using EdjCase.ICP.Agent.Identities;
using System.IO;
using CommandLine;
using Sample.Shared.Governance;
using System.Security.Principal;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using System.Reflection;

public class Program
{
	public class Options
	{

		[Option('a', "anchor", Required = true, HelpText = "Anchor identifier to authenticate with")]
		public ulong Anchor { get; set; }

		[Option('n', "hostname", Required = true, HelpText = "Client hostname to give access to")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Hostname { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	}

	public static async Task Main(string[] args)
	{
#if DEBUG
		if (args.Length == 0)
		{
			await ParseAndRunAsync(new string[] { "--help" }); // Prints the help if there are no args
			args = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		}
#endif
		await ParseAndRunAsync(args);
	}

	private static async Task<bool> ParseAndRunAsync(string[] args)
	{
		ParserResult<Options> result = await Parser.Default.ParseArguments<Options>(args)
			.WithParsedAsync<Options>(async o =>
			{
				await Run(o.Anchor, o.Hostname);
			});
		return result.Tag == ParserResultType.Parsed;
	}

	public static async Task Run(ulong anchor, string hostname)
	{
		LoginResult result = await Authenticator
			.WithHttpAgent()
			.LoginAsync(anchor, hostname);
		DelegationIdentity identity = result.GetIdentityOrThrow();
		Console.WriteLine("Login success!");
		var agent = new HttpAgent(identity);
		Principal canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
		CandidConverter candidConverter = new CandidConverter(options =>
		{
		});
		var client = new GovernanceApiClient(agent, canisterId, candidConverter);
		var a = await client.GetProposalInfo(110174);

	}
}