using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.ClientGenerator;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Path = EdjCase.ICP.Candid.Models.Path;

namespace EdjCase.ICP.ClientGenerator
{
	public static class ClientFileGenerator
	{
		public static async Task GenerateClientFromCanisterAsync(Principal canisterId, string outputDirectory, string baseNamespace, string? clientName = null, Uri? baseUrl = null)
		{

			var agent = new HttpAgent(new AnonymousIdentity(), baseUrl);
			var candidServicePath = Path.FromSegments("canister", canisterId.Raw, "metadata", "candid:service");
			var paths = new List<Path>
			{
				candidServicePath
			};
			var response = await agent.ReadStateAsync(canisterId, paths);
			string? fileText = response.Certificate.Tree.GetValue(candidServicePath)?.AsLeaf().AsUtf8();

			if (string.IsNullOrWhiteSpace(fileText))
			{
				throw new Exception("Canister does not have 'candid:service' exposed");
			}
			WriteClientFiles(fileText, outputDirectory, baseNamespace, clientName ?? "Service");
		}

		public static void GenerateClientFromFile(string candidFilePath, string outputDirectory, string baseNamespace, string? clientName = null)
		{
			Console.WriteLine($"Reading text from {candidFilePath}...");
			string fileText = File.ReadAllText(candidFilePath);
			if (clientName == null)
			{
				// Use file name for client name
				clientName = System.IO.Path.GetFileNameWithoutExtension(candidFilePath);
			}
			WriteClientFiles(fileText, outputDirectory, baseNamespace, clientName);
		}

		private static void WriteClientFiles(string fileText, string outputDirectory, string baseNamespace, string clientName)
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
				directory = directory == null ? outputDirectory : System.IO.Path.Combine(outputDirectory, directory);
				Directory.CreateDirectory(directory);
				string filePath = System.IO.Path.Combine(directory, fileName + ".cs");
				File.WriteAllText(filePath, text);
			}
		}
	}
}
