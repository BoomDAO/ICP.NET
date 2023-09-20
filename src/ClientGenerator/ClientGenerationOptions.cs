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
		/// <summary>
		/// If true, will treat `FilePathOrCanisterId` as a canister id and get the definition from the canister. Otherwise will treat it as a file path and get the definition from the file
		/// </summary>
		public bool GetDefinitionFromCansiter { get; }
		/// <summary>
		/// The file path to a local *.did file to get definition from or the canister id, depending on `GetDefinitionFromCansiter` value
		/// </summary>
		public string FilePathOrCanisterId { get; }
		/// <summary>
		/// If true, removes all files in the output directory before regeneration, otherwise does nothing. Defaults to true
		/// </summary>
		public bool PurgeOutputDirectory { get; }
		/// <summary>
		/// The base namespace to use in the generated files
		/// </summary>
		public string Namespace { get; }
		/// <summary>
		/// If true, there will be no folders, all files will be in the same directory
		/// </summary>
		public string OutputDirectory { get; }
		/// <summary>
		/// If true, there will be no folders, all files will be in the same directory
		/// </summary>
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
		/// <summary>
		/// Optional. The url of the boundry node for the internet computer. Defaults to ic0.app
		/// </summary>
		public Uri? BoundryNodeUrl { get; }
		/// <summary>
		/// Optional. Specifies options for each candid type in the definition.
		/// Only supports named types, no anonymous types
		/// </summary>
		public Dictionary<string, NamedTypeOptions> Types { get; }



		/// <param name="name">The name of the client class and file to use</param>
		/// <param name="namespace">The base namespace to use in the generated files</param>
		/// <param name="getDefinitionFromCanister">If true, will treat <paramref name="filePathOrCandidId"/> as a canister id and get the definition from the canister. Otherwise will treat it as a file path and get the definition from the file</param>
		/// <param name="filePathOrCandidId">The file path to a local *.did file to get definition from or the canister id, depending on <paramref name="getDefinitionFromCanister"/> value</param>
		/// <param name="outputDirectory">The output directory to generate the client files</param>
		/// <param name="purgeOutputDirectory">If true, removes all files in the output directory before regeneration. Defaults to true</param>
		/// <param name="noFolders">If true, there will be no folders, all files will be in the same directory</param>
		/// <param name="featureNullable">If true, the nullable C# feature will be used</param>
		/// <param name="keepCandidCase">If true, the names of properties and methods will keep the raw candid name. Otherwise they will be converted to something prettier</param>
		/// <param name="boundryNodeUrl">Optional. The url of the boundry node for the internet computer. Defaults to ic0.app</param>
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
			Dictionary<string, NamedTypeOptions>? types = null
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
			this.Types = types ?? new Dictionary<string, NamedTypeOptions>();
		}
	}

	/// <summary>
	/// Interface to specify generation options for specific types in the candid
	/// </summary>
	public interface ITypeOptions
	{
		//public  string? NameOverride { get; init; } // TODO

		/// <summary>
		/// The candid type of the implementation class
		/// </summary>
		public CandidTypeCode Type { get; }

	}

	/// <summary>
	/// Type options for a record field or variant option
	/// </summary>
	public class NamedTypeOptions
	{
		/// <summary>
		/// Optional. The C# type name to use instead of the default
		/// </summary>
		public string? NameOverride { get; }
		/// <summary>
		/// Optional. The field or option type information
		/// </summary>
		public ITypeOptions? TypeOptions { get; }

		/// <param name="nameOverride">Optional. The C# type name to use instead of the default</param>
		/// <param name="typeOptions">Optional. The field or option type information</param>
		public NamedTypeOptions(
			string? nameOverride = null,
			ITypeOptions? typeOptions = null
		)
		{
			this.NameOverride = nameOverride;
			this.TypeOptions = typeOptions;
		}
	}

	/// <summary>
	/// Type generation options for record candid types
	/// </summary>
	public class RecordTypeOptions : ITypeOptions
	{

		/// <inheritdoc />
		public CandidTypeCode Type { get; } = CandidTypeCode.Record;
		/// <summary>
		/// Optional. The type options for each of the records fields
		/// </summary>
		public Dictionary<string, NamedTypeOptions> Fields { get; }

		/// <param name="fields">Optional. The type options for each of the records fields</param>
		public RecordTypeOptions(
			Dictionary<string, NamedTypeOptions>? fields = null
		)
		{
			this.Fields = fields ?? new Dictionary<string, NamedTypeOptions>();
		}

	}

	/// <summary>
	/// Type generation options for vec candid types
	/// </summary>
	public class VectorTypeOptions : ITypeOptions
	{
		/// <inheritdoc />
		public CandidTypeCode Type { get; } = CandidTypeCode.Vector;
		/// <summary>
		/// Optional. The representation the vec should take in the generated code
		/// </summary>
		public VectorRepresentation? Representation { get; }
		/// <summary>
		/// Optional. The type options for the element type in the vec
		/// </summary>
		public ITypeOptions? ElementType { get; }

		/// <param name="representation">Optional. The representation the vec should take in the generated code</param>
		/// <param name="elementType">Optional. The type options for the element type in the vec</param>
		public VectorTypeOptions(
			VectorRepresentation? representation = null,
			ITypeOptions? elementType = null
		)
		{
			this.Representation = representation;
			this.ElementType = elementType;
		}
	}

	/// <summary>
	/// C# type representations for the vec type
	/// </summary>
	public enum VectorRepresentation
	{
		/// <summary>
		/// Vector will be a List
		/// </summary>
		List,
		/// <summary>
		/// Vector will be an array
		/// </summary>
		Array,
		/// <summary>
		/// Vector will be a dictionary, only if the element type is a record with 2 fields
		/// </summary>
		Dictionary
	}

	/// <summary>
	/// Type generation options for variant candid types
	/// </summary>
	public class VariantTypeOptions : ITypeOptions
	{
		/// <inheritdoc />
		public CandidTypeCode Type { get; } = CandidTypeCode.Vector;
		/// <summary>
		/// Optional. The type options for each of the variant options
		/// </summary>
		public Dictionary<string, NamedTypeOptions> Options { get; }

		/// <param name="options">Optional. The type options for each of the variant options</param>
		public VariantTypeOptions(
			Dictionary<string, NamedTypeOptions>? options = null
		)
		{
			this.Options = options ?? new Dictionary<string, NamedTypeOptions>();
		}
	}
}
