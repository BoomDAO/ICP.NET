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
		public TypeName Name { get; }
        public string ClientFile { get; }
        public List<(string Name, string SourceCode)> DataModelFiles { get; }
        public string? AliasFile { get; }

        public ClientCodeResult(TypeName name, string clientFile, List<(string Name, string SourceCode)> typeFiles, string? aliasFile)
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
			ServiceSourceInfo service = TypeSourceConverter.ConvertService(serviceName, baseNamespace, serviceFile);

			return FromServiceInfo(baseNamespace, service);
        }

		internal static ClientCodeResult FromServiceInfo(string baseNamespace, ServiceSourceInfo service)
		{
			int csharpVersion = 9; // TODO configurable

			List<string>? importedNamespaces = null;
			if (csharpVersion < 10)
			{
				// If global usings feature doesnt exist, import per file
				importedNamespaces = service.Aliases
					.Select(a => $"{a.Alias} = {a.Type.GetNamespacedName()}")
					.Concat(new List<string>
					{
						"System",
						"System.Threading.Tasks",
						"System.Collections.Generic",
						"EdjCase.ICP.Candid.Mappers",
						"EdjCase.ICP.Candid"
					})
					.ToList();
			}

			TypeName clientName = new TypeName(service.Name + "ApiClient", baseNamespace);
			string clientSource = TypeSourceGenerator.GenerateClientSourceCode(clientName, baseNamespace, service.Service, importedNamespaces);


			var typeFiles = new List<(string Name, string SourceCode)>();
			foreach ((TypeName name, TypeSourceDescriptor desc) in service.Types)
			{
				(string fileName, string source) = TypeSourceGenerator.GenerateTypeSourceCode(name, baseNamespace, desc, importedNamespaces);

				typeFiles.Add((fileName, source));
			}
			string? aliasFile = null;
			if (service.Aliases.Any())
			{
				bool useGlobal = csharpVersion >= 10;
				aliasFile = TypeSourceGenerator.GenerateAliasSourceCode(baseNamespace, service.Aliases, useGlobal);
			}
			return new ClientCodeResult(clientName, clientSource, typeFiles, aliasFile);
		}

    }

}