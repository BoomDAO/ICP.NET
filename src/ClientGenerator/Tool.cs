using CommandLine;
using EdjCase.ICP.Candid.Models;
using System;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	internal class Tool
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public class Options
		{
			[Option('f', "candid-file-path", Required = false, HelpText = "The file path to the .did file to generate from")]
			public string? CandidFilePath { get; set; }

			[Option('i', "canister-id", Required = false, HelpText = "The canister to generate the client for")]
			public string? CanisterId { get; set; }

			[Option('o', "output-directory", Required = true, HelpText = "Set the directory to generate the client files")]
			public string OutputDirectory { get; set; }

			[Option('n', "namespace", Required = true, HelpText = "Set the base namespace for the generated code")]
			public string Namespace { get; set; }

			[Option('c', "client-name", Required = false, HelpText = "Set the name of the client to generate. Otherwise will automatically be generated")]
			public string? ClientName { get; set; }

			[Option('u', "base-url", Required = false, HelpText = "Set the base url of http agent. Defaults to mainnet")]
			public string? BaseUrl { get; set; }
		}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


		public static async Task Main(string[] args)
		{
#if DEBUG
			while (true)
			{
#endif
				ParserResult<Options> result = await Parser.Default.ParseArguments<Options>(args)
					.WithParsedAsync<Options>(async o =>
					{
						if (string.IsNullOrWhiteSpace(o.CanisterId))
						{
							ClientFileGenerator.GenerateClientFromFile(o.CandidFilePath!, o.OutputDirectory, o.Namespace, o.ClientName);
						}
						else
						{
							Principal canisterId = Principal.FromText(o.CanisterId);
							Uri? baseUrl = string.IsNullOrWhiteSpace(o.BaseUrl) ? null : new Uri(o.BaseUrl);
							await ClientFileGenerator.GenerateClientFromCanisterAsync(canisterId, o.OutputDirectory, o.Namespace, o.ClientName, baseUrl);
						}
					});

#if DEBUG
				if (result.Tag == ParserResultType.NotParsed)
				{
					string? argString = Console.ReadLine();
					args = argString?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
				}
				else
				{
					// Reset on success
					Console.WriteLine("Generation Complete. Press Enter to run again");
					Console.ReadLine();
					args = new string[0];
				}
			}
#endif
		}
	}
}
