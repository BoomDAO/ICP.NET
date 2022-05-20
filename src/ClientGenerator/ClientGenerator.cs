using ICP.Candid;
using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ICP.ClientGenerator
{
    [Generator]
    public class ClientGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            IEnumerable<AdditionalText> didFiles = this.GetDidFiles(context);
            foreach (AdditionalText didFile in didFiles)
            {
                this.GenerateClient(context, didFile);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        private void GenerateClient(GeneratorExecutionContext context, AdditionalText didFile)
        {
            SourceText? sourceText = didFile.GetText();
            if (sourceText == null)
            {
                return;
            }
            string value = sourceText.ToString();
            string name = Path.GetFileNameWithoutExtension(didFile.Path);
            CandidServiceType service;
            try
            {
                //service = CandidTextParser.Parse<CandidServiceType>(value);
                var methods = new Dictionary<string, CandidFuncType>
                {
                    {"one", new CandidFuncType(new List<FuncMode>(), new List<CandidType>(), new List<CandidType>()) }
                };
                service = new CandidServiceType(methods, CandidId.Parse("Governance"));
            }
            catch (Exception ex)
            {
                // TODO handle this better
                DiagnosticDescriptor descriptor = new DiagnosticDescriptor("1", "Failed to parse DID file", ex.ToString(), "ParseError", DiagnosticSeverity.Error, true);
                var location = Location.Create(didFile.Path, TextSpan.FromBounds(0, 0), new LinePositionSpan());
                context.ReportDiagnostic(Diagnostic.Create(descriptor, location));
                return;
            }
            string source = this.GenerateFromService(name, service);
            context.AddSource(name, source);
        }

        private string GenerateFromService(string name, CandidServiceType service)
        {
            var stringBuilder = new IndentedStringBuilder();
            stringBuilder.AppendLine($"public class {name}ApiClient");
            stringBuilder.AppendLine("{");
            foreach ((string methodName, CandidFuncType func) in service.Methods)
            {
                stringBuilder.AppendLine($"public void {methodName}()");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("}");
            }
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString() ?? "";
        }

        private IEnumerable<AdditionalText> GetDidFiles(GeneratorExecutionContext context)
        {
            foreach (AdditionalText file in context.AdditionalFiles)
            {
                if (Path.GetExtension(file.Path).Equals(".did", StringComparison.OrdinalIgnoreCase))
                {
                    yield return file;
                }
            }
        }
    }
}