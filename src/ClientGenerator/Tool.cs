using CommandLine;
using EdjCase.ICP.Candid.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Threading.Tasks;
using Tomlyn.Model;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Xml.Linq;

namespace EdjCase.ICP.ClientGenerator
{
	internal class Tool
	{
		[Verb("init")]
		public class InitOptions
		{
			[Value(0, Required = false)]
			public string? Directory { get; set; }
		}


		[Verb("gen", isDefault: true)]
		public class GenerateOptions
		{
			[Value(0, Required = false)]
			public string? Directory { get; set; }
		}
		const string CONFIG_FILE_NAME = "candid-client.toml";

		public static async Task<int> Main(string[] args)
		{
			return await Parser.Default.ParseArguments<InitOptions, GenerateOptions>(args)
				.MapResult<InitOptions, GenerateOptions, Task<int>>(
					Initialize,
					Generate,
					errs => Task.FromResult(1)
				);
		}

		private static Task<int> Initialize(InitOptions options)
		{
			string filePath = Path.Combine(options.Directory ?? "", CONFIG_FILE_NAME);
			if (File.Exists(filePath))
			{
				Console.WriteLine("Configuration has already been intialized. Skipping...");
				return Task.FromResult(1);
			}


			const string contents = @"# Base namespace for all generated files
namespace = ""My.Namespace""

# Will create a subfolder within this directory with all the files
output-directory = ""./Clients""

# Will override default boundry node url (like for local development)
# Only useful for generating clients from a canister id
#url = ""https://localhost:8000""

# Will make generated files in a flat structure with no folders, for all clients
#no-folders = true

[[clients]]
# Defines name folder and api client class name
name = ""MyClient""

# Get definition from a *.did file
type = ""file""

# Path to the *.did file to use
file-path = ""../MyService.did""

# Or use the following to get definition from a canister
# and remove type and file-path from above

# Get the definition from a live canister on the IC
#type = ""canister""

# Canister to pull definition from
#canister-id = ""rrkah-fqaaa-aaaaa-aaaaq-cai""

# Override base output directory, but this specifies the subfolder
#output-directory = ""./Clients/MyS""

# Will make generated files in a flat structure with no folders, for this client
#no-folders = true
				
# Can specify multiple clients by creating another [[clients]]
#[[clients]]
#name = ""MyClient2"" 
#type = ""file""
#file-path = ""../MyService2.did""
				";
			Console.WriteLine("Generating config file...");
			if (options.Directory != null && !Directory.Exists(options.Directory))
			{
				Directory.CreateDirectory(options.Directory);
			}
			File.WriteAllText(filePath, contents);

			return Task.FromResult(0);
		}

		private async static Task<int> Generate(GenerateOptions options)
		{
			try
			{
				string filePath = Path.Combine(options.Directory ?? "", CONFIG_FILE_NAME);
				if (!File.Exists(filePath))
				{
					throw new InvalidOperationException($"Configuration file '{CONFIG_FILE_NAME}' not found in the directory. Run `candid-client-generator init`.");
				}
				string configToml = File.ReadAllText(filePath);
				TomlTable config = Tomlyn.Toml.ToModel(configToml);

				string baseNamespace = GetRequired<string>(config, "namespace");
				string? baseUrl = GetOptional<string?>(config, "url");
				bool? noFolders = GetOptional<bool?>(config, "no-folders");
				Uri? boundryNodeUrl = baseUrl == null ? null : new Uri(baseUrl);
				string outputDirectory = Path.GetRelativePath("./", GetOptional<string?>(config, "output-directory") ?? "./");
				bool featureNullable = GetOptional<bool?>(config, "feature-nullable") ?? true;

				TomlTableArray clients = config["clients"].Cast<TomlTableArray>();

				foreach (TomlTable client in clients)
				{
					string name = GetRequired<string>(client, "name");
					string type = GetRequired<string>(client, "type");
					string @namespace = baseNamespace + "." + name;
					string? clientOutputDirectory = GetOptional<string?>(client, "output-directory");
					string? clientNamespace = GetOptional<string?>(client, "namespace");
					bool clientNoFolders = GetOptional<bool?>(client, "no-folders") ?? noFolders ?? false;
					ClientGenerationOptions clientOptions = new(name, clientNamespace ?? @namespace, clientNoFolders, featureNullable);
					ClientSyntax source;
					switch (type)
					{
						case "file":
							string candidFilePath = GetRequired<string>(client, "file-path");
							string dir = new FileInfo(filePath).Directory!.FullName;
							candidFilePath = Path.GetRelativePath("./", Path.Combine(dir, candidFilePath));
							Console.WriteLine($"Reading text from {candidFilePath}");
							string fileText = File.ReadAllText(candidFilePath);
							// Use file name for client name
							source = ClientCodeGenerator.GenerateClientFromFile(fileText, clientOptions);
							break;
						case "canister":
							string canisterIdString = GetRequired<string>(client, "canister-id");
							Principal canisterId = Principal.FromText(canisterIdString);
							Console.WriteLine($"Fetching definition from canister {canisterId}");
							source = await ClientCodeGenerator.GenerateClientFromCanisterAsync(canisterId, clientOptions, boundryNodeUrl);
							break;
						default:
							throw new InvalidOperationException($"Invalid client type '{type}'");
					}
					WriteClient(source, clientOutputDirectory ?? Path.Combine(outputDirectory, name), clientNoFolders);
				}
				return 0;
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
				return 1;
			}
		}

		private static T GetRequired<T>(TomlTable table, string key, string? prefix = null)
		{
			if (table.ContainsKey(key))
			{
				return table[key].Cast<T>();
			}
			if (prefix != null)
			{
				key = prefix + "." + key;
			}
			throw new InvalidOperationException($"Missing key '{key}'");
		}

		private static T? GetOptional<T>(TomlTable table, string key)
		{
			if (table.ContainsKey(key))
			{
				return table[key].Cast<T>();
			}
			return default;
		}

		private static void WriteClient(ClientSyntax result, string outputDirectory, bool noFolders)
		{
			Console.WriteLine($"Writing client file to: {outputDirectory}\\{result.Name}.cs");
			WriteFile(null, result.Name, result.ClientFile);


			Console.WriteLine($"Writing data model files to directory: {outputDirectory}\\Models\\");
			foreach ((string name, CompilationUnitSyntax sourceCode) in result.TypeFiles)
			{
				WriteFile(noFolders ? null : "Models", name, sourceCode);
			}
			Console.WriteLine("Client successfully generated!");
			Console.WriteLine();

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
