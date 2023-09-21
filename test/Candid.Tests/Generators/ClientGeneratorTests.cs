using Xunit;
using EdjCase.ICP.ClientGenerator;
using EdjCase.ICP.Candid.Models;
using System.Reflection;
using System.IO;
using Snapshooter.Xunit;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis;
using System;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models.Values;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EdjCase.ICP.Candid.Tests.Generators
{
	public class ClientGeneratorTests
	{
		[Theory]
		[InlineData("Governance")]
		[InlineData("Dex")]
		[InlineData("AnonymousTuples")]
		[InlineData("DuplicatePropertyNames")]
		public void GenerateClients(string serviceName)
		{
			string fileText = GetFileText(serviceName + ".did");
			string baseNamespace = "Test";
			CandidServiceDescription serviceFile = CandidServiceDescription.Parse(fileText);
			this.GenerateClientInternal(serviceFile, baseNamespace, serviceName, true, true, true);
			this.GenerateClientInternal(serviceFile, baseNamespace, serviceName, true, true, false);
			this.GenerateClientInternal(serviceFile, baseNamespace, serviceName, true, false, false);
			this.GenerateClientInternal(serviceFile, baseNamespace, serviceName, true, false, true);
			this.GenerateClientInternal(serviceFile, baseNamespace, serviceName, false, true, true);
			this.GenerateClientInternal(serviceFile, baseNamespace, serviceName, false, true, false);
			this.GenerateClientInternal(serviceFile, baseNamespace, serviceName, false, false, true);
			this.GenerateClientInternal(serviceFile, baseNamespace, serviceName, false, false, false);
		}

		private void GenerateClientInternal(
			CandidServiceDescription serviceFile,
			string baseNamespace,
			string serviceName,
			bool noFolders,
			bool featureNullable,
			bool keepCandidCase
		)
		{
			ClientGenerationOptions options = new(
				serviceName,
				baseNamespace,
				false,
				"",
				"./",
				noFolders,
				featureNullable, 
				keepCandidCase,
				boundryNodeUrl: null,
				types: null
			);

			ClientSyntax syntax = ClientCodeGenerator.GenerateClient(serviceFile, options);

			AdhocWorkspace workspace = new();
			OptionSet optionsSet = workspace.Options
				.WithChangedOption(FormattingOptions.UseTabs, LanguageNames.CSharp, value: true)
				.WithChangedOption(FormattingOptions.NewLine, LanguageNames.CSharp, value: Environment.NewLine);

			string clientCode = Formatter.Format(syntax.ClientFile, workspace, optionsSet).ToFullString();

			//List<SyntaxTree> trees = new()
			//{
			//	SyntaxFactory.SyntaxTree(syntax.ClientFile)
			//};
			foreach ((string typeName, CompilationUnitSyntax typeSyntax) in syntax.TypeFiles)
			{
				clientCode += $"\n\nType File: '{typeName}'\n\n";
				clientCode += Formatter.Format(typeSyntax, workspace, optionsSet).ToFullString();
				//trees.Add(SyntaxFactory.SyntaxTree(f.Syntax));
			}

			Snapshot.Match(clientCode, $"{serviceName}_NoFolders_{noFolders}_Nullable_{featureNullable}_KeepCandidCase_{keepCandidCase}");

			//TODO
			//CSharpCompilation compilation = CSharpCompilation
			//	.Create(null)
			//	.AddSyntaxTrees(trees)
			//	.WithReferences(

			//	);
			//ImmutableArray<Diagnostic> diagnostics = compilation.GetDiagnostics();
			//Assert.Empty(diagnostics);
		}

		private static string GetFileText(string fileName)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string resourceName = "ICP.Candid.Tests.Generators.Files." + fileName;

			using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
			{
				if (stream == null)
				{
					throw new FileNotFoundException(resourceName);
				}
				using (StreamReader reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}



		// TODO analyzer
		//[Fact]
		//public async Task Should_return_all_known_items()
		//{
		//    // Create references for assemblies we require
		//    // We could add multiple references if required
		//    IEnumerable<PortableExecutableReference> references = new[]
		//    {
		//        MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
		//        MetadataReference.CreateFromFile(typeof(Agent.Agents.HttpAgent).Assembly.Location)
		//    };

		//    CSharpCompilation compilation = CSharpCompilation.Create(
		//        assemblyName: "MyApp",
		//        syntaxTrees: null,
		//        references: references); // 👈 pass the references to the compilation

		//    var generator = new ClientGenerator.ClientSourceGenerator();
		//    var additionalTexts = new List<AdditionalText>
		//    {
		//        new GovernanceDidFile(),
		//        new DefiDappDidFile()
		//    };
		//    GeneratorDriver driver = CSharpGeneratorDriver.Create(new[] { generator }, additionalTexts);

		//    driver = driver.RunGeneratorsAndUpdateCompilation(compilation,
		//        out Compilation outputCompilation,
		//        out ImmutableArray<Diagnostic> diagnostics);

		//    IEnumerable<string> fileOutputs;
		//    if (diagnostics.Any(d => d.Severity >= DiagnosticSeverity.Error))
		//    {
		//        fileOutputs = diagnostics
		//            .Where(d => d.Severity >= DiagnosticSeverity.Error)
		//            .Select(d => d.ToString());
		//    }
		//    else
		//    {
		//        fileOutputs = outputCompilation.SyntaxTrees
		//            .Select(t => t.GetText().ToString());
		//    }
		//    await Verifier
		//        .Verify(fileOutputs)
		//        .UseDirectory("Snapshots");
		//}


	}
}
