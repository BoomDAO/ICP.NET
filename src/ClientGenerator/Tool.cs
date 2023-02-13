using CommandLine;
using EdjCase.ICP.Candid.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis;
using System;
using System.IO;
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
						ClientSyntax source;
						if (string.IsNullOrWhiteSpace(o.CanisterId))
						{
							string candidFilePath = o.CandidFilePath!;
							Console.WriteLine($"Reading text from {candidFilePath}...");
							string fileText = File.ReadAllText(candidFilePath);
							// Use file name for client name
							string clientName = o.ClientName ?? Path.GetFileNameWithoutExtension(candidFilePath);
							source = ClientCodeGenerator.GenerateClientFromFile(fileText, o.Namespace, clientName);
						}
						else
						{
							Principal canisterId = Principal.FromText(o.CanisterId);
							Uri? baseUrl = string.IsNullOrWhiteSpace(o.BaseUrl) ? null : new Uri(o.BaseUrl);
							source = await ClientCodeGenerator.GenerateClientFromCanisterAsync(canisterId, o.Namespace, o.ClientName, baseUrl);
						}
						WriteClient(source, o.OutputDirectory);
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

		private static void WriteClient(ClientSyntax result, string outputDirectory)
		{
			Console.WriteLine($"Writing client file ./{result.Name}.cs...");
			WriteFile(null, result.Name, result.ClientFile);


			foreach ((string name, CompilationUnitSyntax sourceCode) in result.TypeFiles)
			{
				Console.WriteLine($"Writing data model file ./Models/{name}.cs...");
				WriteFile("Models", name, sourceCode);
			}

			void WriteFile(
				string? subDirectory,
				string fileName,
				CompilationUnitSyntax syntax
			)
			{
				// Fix any bad chars
				char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
				string[] split = fileName
					.Split(invalidFileNameChars, StringSplitOptions.RemoveEmptyEntries);
				fileName = string.Join("_", split).TrimEnd('.');
				string directory = subDirectory == null
					? outputDirectory
					: Path.Combine(outputDirectory, subDirectory);
				Directory.CreateDirectory(directory);
				string filePath = Path.Combine(directory, fileName + ".cs");
				// TODO?
				//CSharpCompilation compilation = CSharpCompilation
				//	.Create(null)
				//	.AddSyntaxTrees(tree);
				//ImmutableArray<Diagnostic> diagnostics = compilation.GetDiagnostics();
				//foreach(Diagnostic d in diagnostics)
				//{
				//	Console.WriteLine(d.ToString());
				//}
				// Setup formatting options
				AdhocWorkspace workspace = new();
				OptionSet options = workspace.Options
					.WithChangedOption(FormattingOptions.UseTabs, LanguageNames.CSharp, value: true)
					.WithChangedOption(FormattingOptions.NewLine, LanguageNames.CSharp, value: Environment.NewLine);

				string text = Formatter.Format(syntax, workspace, options).ToFullString();

				File.WriteAllText(filePath, text);
			}
		}
	}
}
