using CommandLine;
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
			[Option('f', "candid-file-path", Required = true, HelpText = "The file path to the .did file to generate from")]
			public string CandidFilePath { get; set; }

			// TODO
			//[Option('c', "canister-id", Required = false, HelpText = "The canister to generate the client for")]
			//public string? CannisterId { get; set; }

			[Option('o', "output-directory", Required = true, HelpText = "Set the directory to generate the client files")]
			public string OutputDirectory { get; set; }

			[Option('n', "namespace", Required = true, HelpText = "Set the base namespace for the generated code")]
			public string Namespace { get; set; }

			[Option('c', "client-name", Required = false, HelpText = "Set the name of the client to generate. Otherwise will automatically be generated")]
			public string? ClientName { get; set; }
		}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


		public static void Main(string[] args)
		{
#if DEBUG
			Console.WriteLine("File Path: ");
			string filePath = Console.ReadLine()!;
			string fileName = Path.GetFileName(filePath);
			if (fileName.EndsWith(".did"))
			{
				fileName = fileName.Substring(0, fileName.Length - 4);
			}
			Console.WriteLine("Output Directory: ");
			string outputDirectory = Console.ReadLine()!;
			args = new string[]
			{
				"-o",
				Path.Combine(outputDirectory, fileName),
				"-f",
				filePath,
				"-n",
				$"Sample.Shared.{fileName}"
			};
#endif
			Parser.Default.ParseArguments<Options>(args)
				   .WithParsed<Options>(o =>
				   {
					   ClientFileGenerator.WriteClientFiles(o.CandidFilePath, o.OutputDirectory, o.Namespace, o.ClientName);
				   });
		}
	}
}
