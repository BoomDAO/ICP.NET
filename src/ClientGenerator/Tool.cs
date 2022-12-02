using CommandLine;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
			Console.WriteLine("CanisterId (leave blank if using file): ");
			string? canisterId = Console.ReadLine()?.Trim();
			string fileName;
			string? filePath = null;
			string? baseUrl = null;
			bool useFile = string.IsNullOrWhiteSpace(canisterId);
			if (useFile)
			{
				Console.WriteLine("File Path: ");
				filePath = Console.ReadLine()!;
				fileName = System.IO.Path.GetFileName(filePath);
				if (fileName.EndsWith(".did"))
				{
					fileName = fileName.Substring(0, fileName.Length - 4);
				}
			}
			else
			{
				Console.WriteLine("Base Url (leave blank from mainnet): ");
				baseUrl = Console.ReadLine()!;
				Console.WriteLine("Service Name: ");
				fileName = Console.ReadLine()!;
			}
			Console.WriteLine("Output Directory: ");
			string outputDirectory = Console.ReadLine()!;
			if (useFile)
			{
				args = new string[]
				{
					"-o",
					System.IO.Path.Combine(outputDirectory, fileName),
					"-f",
					filePath!,
					"-n",
					$"Sample.Shared.{fileName}"
				};
			}
			else
			{
				args = new string[]
				{
					"-o",
					System.IO.Path.Combine(outputDirectory, fileName),
					"-i",
					canisterId!,
					"-n",
					$"Sample.Shared.{fileName}",
					"-u",
					baseUrl ?? "https://ic0.app"
				};
			}
#endif
			await Parser.Default.ParseArguments<Options>(args)
				.WithParsedAsync<Options>(async o =>
				{
					if (string.IsNullOrWhiteSpace(o.CanisterId))
					{
						ClientFileGenerator.GenerateClientFromFile(o.CandidFilePath!, o.OutputDirectory, o.Namespace, o.ClientName);
					}
					else
					{
						Principal canisterId = Principal.FromText(o.CanisterId);
						Uri? baseUrl = o.BaseUrl == null ? null : new Uri(o.BaseUrl);
						await ClientFileGenerator.GenerateClientFromCanisterAsync(canisterId, o.OutputDirectory, o.Namespace, o.ClientName, baseUrl);
					}
				});
		}
	}
}
