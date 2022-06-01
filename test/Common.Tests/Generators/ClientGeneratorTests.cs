using System.Threading.Tasks;
using Xunit;
using EdjCase.ICP.ClientGenerator;
using EdjCase.ICP.Candid.Models;
using System.Reflection;
using System.IO;
using Snapshooter.Xunit;

namespace EdjCase.ICP.Candid.Tests.Generators
{
	public class ClientGeneratorTests
    {
        [Theory]
		[InlineData("Governance")]
		[InlineData("Dex")]
		public void GenerateClients(string serviceName)
		{
			string fileText = GetFileText(serviceName + ".did");
			CandidServiceFile serviceFile = CandidServiceFile.Parse(fileText);
			ClientCodeResult source = ClientGenerator.ClientCodeGenerator.FromServiceFile(serviceName, serviceFile);
			Snapshot.Match(source, serviceName);
		}

		private static string GetFileText(string fileName)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string resourceName = "ICP.Candid.Tests.Generators.Files." + fileName;

			using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
			{
				if(stream == null)
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
