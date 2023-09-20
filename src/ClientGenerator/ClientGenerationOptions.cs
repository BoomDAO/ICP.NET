using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	/// <summary>
	/// Options for generating a client
	/// </summary>
	public class ClientGenerationOptions
	{
		/// <summary>
		/// The name of the client class and file to use
		/// </summary>
		public string Name { get; }
		public bool GetDefinitionFromCansiter { get; }
		public string FilePathOrCanisterId { get; }
		public bool PurgeOutputDirectory { get; }
		/// <summary>
		/// The base namespace to use in the generated files
		/// </summary>
		public string Namespace { get; }
		/// <summary>
		/// If true, there will be no folders, all files will be in the same directory
		/// </summary>
		public string OutputDirectory { get; }
		public bool NoFolders { get; }
		/// <summary>
		/// If true, the nullable C# feature will be used
		/// </summary>
		public bool FeatureNullable { get; }
		/// <summary>
		/// If true, the names of properties and methods will keep the raw candid name.
		/// Otherwise they will be converted to something prettier
		/// </summary>
		public bool KeepCandidCase { get; }
		public Uri? BoundryNodeUrl { get; }
		/// <summary>
		/// Optional. Specifies options for each candid type in the definition.
		/// Only supports named types, no anonymous types
		/// </summary>
		public Dictionary<string, ITypeOptions> Types { get; }



		/// <param name="name">The name of the client class and file to use</param>
		/// <param name="namespace">The base namespace to use in the generated files</param>
		/// <param name="noFolders">If true, there will be no folders, all files will be in the same directory</param>
		/// <param name="featureNullable">If true, the nullable C# feature will be used</param>
		/// <param name="keepCandidCase">If true, the names of properties and methods will keep the raw candid name. Otherwise they will be converted to something prettier</param>
		/// <param name="types">Optional. Specifies options for each candid type in the definition</param>
		public ClientGenerationOptions(
			string name,
			string @namespace,
			bool getDefinitionFromCanister,
			string filePathOrCandidId,
			string outputDirectory,
			bool purgeOutputDirectory = true,
			bool noFolders = false,
			bool featureNullable = false,
			bool keepCandidCase = false,
			Uri? boundryNodeUrl = null,
			Dictionary<string, ITypeOptions>? types = null
		)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException(nameof(name));
			}
			// Trim whitespace and replace spaces with underscores
			this.Name = StringUtil.ToPascalCase(name.Trim().Replace(' ', '_'));
			this.Namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
			this.GetDefinitionFromCansiter = getDefinitionFromCanister;
			this.FilePathOrCanisterId = filePathOrCandidId;
			this.OutputDirectory = outputDirectory;
			this.PurgeOutputDirectory = purgeOutputDirectory;
			this.NoFolders = noFolders;
			this.FeatureNullable = featureNullable;
			this.KeepCandidCase = keepCandidCase;
			this.BoundryNodeUrl = boundryNodeUrl;
			this.Types = types ?? new Dictionary<string, ITypeOptions>();
		}
	}

	public interface ITypeOptions
	{
		//public  string? NameOverride { get; init; } // TODO

		public CandidTypeCode Type { get; }

	}

	public class RecordTypeOptions : ITypeOptions
	{
		public CandidTypeCode Type { get; } = CandidTypeCode.Record;
		//public string? NameOverride { get; init; } // TODO
		public RecordRepresentation? Representation { get; init; }
		public Dictionary<string, ITypeOptions> Fields { get; init; }

	}

	public enum RecordRepresentation
	{
		CustomType,
		Tuple
	}

	public class VectorTypeOptions : ITypeOptions
	{
		public CandidTypeCode Type { get; } = CandidTypeCode.Vector;
		//public string? NameOverride { get; init; } // TODO
		public VectorRepresentation? Representation { get; init; }
		public ITypeOptions? InnerType { get; init; }
	}

	public enum VectorRepresentation
	{
		List,
		Array,
		Dictionary
	}

	public class VariantTypeOptions : ITypeOptions
	{
		public CandidTypeCode Type { get; } = CandidTypeCode.Vector;
		//public string? NameOverride { get; init; } // TODO
		public Dictionary<string, ITypeOptions> Options { get; init; }
	}
}
