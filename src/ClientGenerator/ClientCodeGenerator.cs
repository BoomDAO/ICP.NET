using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using ICP.ClientGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Path = EdjCase.ICP.Candid.Models.Path;

namespace EdjCase.ICP.ClientGenerator
{
    public class ClientCodeResult
    {
		public string Name { get; }
        public string ClientFile { get; }
        public List<(string Name, string SourceCode)> DataModelFiles { get; }
        public string? AliasFile { get; }

        public ClientCodeResult(string name, string clientFile, List<(string Name, string SourceCode)> typeFiles, string? aliasFile)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.ClientFile = clientFile ?? throw new ArgumentNullException(nameof(clientFile));
            this.DataModelFiles = typeFiles ?? throw new ArgumentNullException(nameof(typeFiles));
            this.AliasFile = aliasFile;
        }
    }
    public static class ClientCodeGenerator
    {
        public static ClientCodeResult FromService(string serviceName, string baseNamespace, CandidServiceDescription serviceFile)
        {
			ServiceSourceInfo service = TypeSourceConverter.ConvertService(serviceName, serviceFile);

			return FromServiceInfo(serviceName, baseNamespace, service);
        }

		internal static ClientCodeResult FromServiceInfo(string serviceName, string baseNamespace, ServiceSourceInfo service)
		{
			string clientSource = TypeSourceGenerator.GenerateClientSourceCode(baseNamespace, service.Service);

			var typeFiles = new List<(string Name, string SourceCode)>();
			foreach (TypeSourceDescriptor type in service.Types)
			{
				(string fileName, string source) = TypeSourceGenerator.GenerateTypeSourceCode(baseNamespace, type);

				typeFiles.Add((fileName, source));
			}
			string? aliasFile = null;
			if (service.Aliases.Any())
			{
				aliasFile = TypeSourceGenerator.GenerateAliasSourceCode(service.Aliases);
			}
			return new ClientCodeResult(service.Name + "ApiClient", clientSource, typeFiles, aliasFile);
		}

    }

}