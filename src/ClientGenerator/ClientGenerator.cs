using ICP.Candid.Models.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;

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
                this.GenerateClient(didFile);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }

        private void GenerateClient(AdditionalText didFile)
        {
            SourceText? sourceText = didFile.GetText();
            if(sourceText == null)
            {
                return;
            }
            string value = sourceText.ToString();

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