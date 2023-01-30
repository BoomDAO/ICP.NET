using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	/// <summary>
	/// Generator to create client files based of candid definitions from `.did` files
	/// or from a canister id
	/// </summary>
	public static class ClientFileGenerator
	{
		/// <summary>
		/// Creates client files for a canister based on its id. This only works if 
		/// the canister has the `candid:service` meta data available in its public state
		/// </summary>
		/// <param name="canisterId">The canister to get the definition from</param>
		/// <param name="outputDirectory">The directory to output to</param>
		/// <param name="baseNamespace">The base namespace to use in the generated files</param>
		/// <param name="clientName">Optional. The name of the client class and file to use. Defaults to 'Service'</param>
		/// <param name="httpBoundryNodeUrl">Optional. The http boundry node url to use, otherwise uses the default</param>
		public static async Task GenerateClientFromCanisterAsync(
			Principal canisterId,
			string outputDirectory,
			string baseNamespace,
			string? clientName = null,
			Uri? httpBoundryNodeUrl = null
		)
		{
			var agent = new HttpAgent(identity: null, httpBoundryNodeUrl: httpBoundryNodeUrl);
			var candidServicePath = StatePath.FromSegments("canister", canisterId.Raw, "metadata", "candid:service");
			var paths = new List<StatePath>
			{
				candidServicePath
			};
			var response = await agent.ReadStateAsync(canisterId, paths);
			string? fileText = response.Certificate.Tree.GetValueOrDefault(candidServicePath)?.AsLeaf().AsUtf8();

			if (string.IsNullOrWhiteSpace(fileText))
			{
				throw new Exception("Canister does not have 'candid:service' exposed");
			}
			WriteClientFiles(fileText, outputDirectory, baseNamespace, clientName ?? "Service");
		}

		/// <summary>
		/// Generates client files for a canister based on a `.did` file definition
		/// </summary>
		/// <param name="outputDirectory">The directory to output to</param>
		/// <param name="baseNamespace">The base namespace to use in the generated files</param>
		/// <param name="clientName">Optional. The name of the client class and file to use. Defaults to 'Service'</param>
		/// <param name="candidFilePath">The path where the `.did` definition file is located</param>
		public static void GenerateClientFromFile(
			string candidFilePath,
			string outputDirectory,
			string baseNamespace,
			string? clientName = null
		)
		{
			Console.WriteLine($"Reading text from {candidFilePath}...");
			string fileText = File.ReadAllText(candidFilePath);
			// Use file name for client name
			clientName ??= Path.GetFileNameWithoutExtension(candidFilePath);

			WriteClientFiles(fileText, outputDirectory, baseNamespace, clientName);
		}

		private static void WriteClientFiles(
			string fileText,
			string outputDirectory,
			string baseNamespace,
			string clientName
		)
		{
			fileText = string.Join("\n",
				fileText.Split(new[] { '\r', '\n' })
						.Where(l => !l.TrimStart().StartsWith("//"))
			);

			Console.WriteLine($"Parsing file contents...");
			CandidServiceDescription serviceFile = CandidServiceDescription.Parse(fileText);

			Console.WriteLine($"Generating client '{clientName}' from parse candid file...");
			ClientCodeResult result = ClientCodeGenerator.FromService(clientName, baseNamespace, serviceFile);


			Console.WriteLine($"Writing client file ./{result.Name.GetName()}.cs...");
			WriteFile(null, result.Name.GetName(), result.ClientFile);


			foreach ((string name, string sourceCode) in result.DataModelFiles)
			{
				Console.WriteLine($"Writing data model file ./Models/{name}.cs...");
				WriteFile("Models", name, sourceCode);
			}

			if (result.AliasFile != null)
			{
				Console.WriteLine($"Writing aliases file ./Aliases.cs...");
				WriteFile(null, "Aliases", result.AliasFile);
			}
			else
			{
				Console.WriteLine($"No aliases found. Skipping aliases file generation...");
			}

			void WriteFile(string? directory, string fileName, string text)
			{
				directory = directory == null
					? outputDirectory
					: Path.Combine(outputDirectory, directory);
				Directory.CreateDirectory(directory);
				string filePath = Path.Combine(directory, fileName + ".cs");
				File.WriteAllText(filePath, text);
			}
		}
	}
}
