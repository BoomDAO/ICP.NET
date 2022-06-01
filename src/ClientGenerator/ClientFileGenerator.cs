using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.ClientGenerator;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EdjCase.ICP.ClientGenerator
{
	public static class ClientFileGenerator
	{
		public static void WriteClientFiles(string candidFilePath, string outputDirectory, string baseNamespace, string? clientName = null)
		{
			Console.WriteLine($"Reading text from {candidFilePath}...");
			string fileText = File.ReadAllText(candidFilePath);
			if (clientName == null)
			{
				// Use file name for client name
				clientName = Path.GetFileNameWithoutExtension(candidFilePath);
			}
			Console.WriteLine($"Parsing file contents...");
			CandidServiceFile serviceFile = CandidServiceFile.Parse(fileText);

			Console.WriteLine($"Generating client '{clientName}' from parse candid file...");
			ClientCodeResult result = ClientCodeGenerator.FromServiceFile(clientName, baseNamespace, serviceFile);

			Console.WriteLine($"Writing client file ./{result.Name}.cs...");
			WriteFile(null, result.Name, result.ClientFile);


			foreach ((string name, string sourceCode) in result.DataModelFiles)
			{
				Console.WriteLine($"Writing data model file ./Models/{name}.cs...");
				WriteFile("Models", name, sourceCode);
			}

			if (result.AliasFile != null)
			{
				Console.WriteLine($"Writing aliases file ./{result.Name}.cs...");
				WriteFile(null, "Aliases", result.AliasFile);
			}
			else
			{
				Console.WriteLine($"No aliases found. Skipping aliases file generation...");
			}

			void WriteFile(string? directory, string fileName, string text)
			{
				directory = directory == null ? outputDirectory : Path.Combine(outputDirectory, directory);
				Directory.CreateDirectory(directory);
				string filePath = Path.Combine(directory, fileName + ".cs");
				File.WriteAllText(filePath, text);
			}
		}
	}
}
