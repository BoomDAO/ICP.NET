using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using ICP.ClientGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.ClientGenerator
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
            string didFileName = Path.GetFileNameWithoutExtension(didFile.Path);
            CandidServiceFile serviceFile;
            try
            {
                serviceFile = CandidServiceFile.Parse(value);
            }
            catch (Exception ex)
            {
                // TODO handle this better
                DiagnosticDescriptor descriptor = new DiagnosticDescriptor("1", "Failed to parse DID file", ex.ToString(), "ParseError", DiagnosticSeverity.Error, true);
                var location = Location.Create(didFile.Path, TextSpan.FromBounds(0, 0), new LinePositionSpan());
                context.ReportDiagnostic(Diagnostic.Create(descriptor, location));
                return;
            }

            (List<DeclaredTypeSourceDescriptor> descriptors, Dictionary<string, string> aliases) = TypeSourceConverter.Build(serviceFile, didFileName);

            foreach (DeclaredTypeSourceDescriptor type in descriptors)
            {
                (string fileName, string source) = TypeSourceGenerator.GenerateSourceCode(type);

                AddSourceFile(fileName, source);
            }
            string aliasSource = TypeSourceGenerator.GenerateAliasSourceCode(aliases);
            AddSourceFile("Aliases", aliasSource);

            void AddSourceFile(string fileName, string source)
            {
                context.AddSource($"{didFileName}_{fileName}", SourceText.From(source, Encoding.UTF8));
            }
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