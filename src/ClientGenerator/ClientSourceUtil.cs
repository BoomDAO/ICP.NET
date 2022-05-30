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
    public class ClientSource
    {
		public string Name { get; }
        public string ClientFile { get; }
        public List<(string Name, string SourceCode)> TypeFiles { get; }
        public string? AliasFile { get; }

        public ClientSource(string name, string clientFile, List<(string Name, string SourceCode)> typeFiles, string? aliasFile)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.ClientFile = clientFile ?? throw new ArgumentNullException(nameof(clientFile));
            this.TypeFiles = typeFiles ?? throw new ArgumentNullException(nameof(typeFiles));
            this.AliasFile = aliasFile;
        }
    }
    public static class ClientSourceUtil
    {
        public static ClientSource FromServiceFile(string serviceName, CandidServiceFile serviceFile)
        {
			ServiceSourceInfo service = TypeSourceConverter.ConvertService(serviceName, serviceFile);

            string clientSource = TypeSourceGenerator.GenerateClientSourceCode(service.Service);

            var typeFiles = new List<(string Name, string SourceCode)>();
            foreach (DeclaredTypeSourceDescriptor type in service.Types)
            {
                (string fileName, string source) = TypeSourceGenerator.GenerateSourceCode(type);

                typeFiles.Add((fileName, source));
            }
            string? aliasFile = null;
            if (service.Aliases.Any())
            {
                aliasFile = TypeSourceGenerator.GenerateAliasSourceCode(service.Aliases);
            }
            return new ClientSource(service.Name, clientSource, typeFiles, aliasFile);
        }

    }

}