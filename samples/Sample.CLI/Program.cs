using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using System;
using System.Threading.Tasks;
using EdjCase.ICP.InternetIdentity;
using EdjCase.ICP.Agent.Identities;
using System.IO;
using CommandLine;
using Sample.Shared.Governance;

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
		if(args.Length == 0)
		{
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
		Authenticator connection = Authenticator.WithHttpAgent();
		DelegationIdentity identity = await connection.LoginAsync(anchor, hostname);

		var agent = new HttpAgent(identity);
		Principal canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
		var client = new GovernanceApiClient(agent, canisterId);
		var a = await client.GetProposalInfo(1999);
	}
}


