using ICP.Candid;
using ICP.Candid.Models.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
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
            if(sourceText == null)
            {
                return;
            }
            string value = sourceText.ToString();
            string name = didFile.Path;
            //CandidServiceType service = CandidTextParser.Parse<CandidServiceType>(value);
            string source = this.GenerateFromService(name, null);

            context.AddSource(name, source);
        }

        private string GenerateFromService(string name, CandidServiceType service)
        {
            var stringBuilder = new IndentedStringBuilder();
            stringBuilder.AppendLine($"public class {name}ApiClient");
            stringBuilder.AppendLine("{");
            //foreach ((string methodName, CandidFuncType func) in service.Methods)
            //{
            //    stringBuilder.
            //}
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