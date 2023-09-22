using CommandLine;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tomlyn.Model;

namespace EdjCase.ICP.ClientGenerator
{
	internal static class TomlConfigParser
	{
		public static List<ClientGenerationOptions> Parse(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new InvalidOperationException($"Configuration file '{filePath}' not found. Run `candid-client-generator init`.");
			}
			string configToml = File.ReadAllText(filePath);
			TomlTable config = Tomlyn.Toml.ToModel(configToml);

			CommonOptions globalOptions = ParseCommonOptions(config);
			if (string.IsNullOrWhiteSpace(globalOptions.Namespace))
			{
				throw new InvalidOperationException($"Failed to parse the `candid-client.toml` config. Missing required global field: 'namespace'");
			}


			TomlTableArray clients = config["clients"].Cast<TomlTableArray>();

			return clients
				.Select(client =>
				{
					string name = GetRequired<string>(client, "name");
					string type = GetRequired<string>(client, "type");
					bool getDefinitionFromCanister;
					string filePathOrCandidId;
					switch (type)
					{
						case "file":
							getDefinitionFromCanister = false;
							filePathOrCandidId = GetRequired<string>(client, "file-path");
							break;
						case "canister":
							getDefinitionFromCanister = true;
							filePathOrCandidId = GetRequired<string>(client, "canister-id");
							break;
						default:
							throw new NotImplementedException();
					}

					CommonOptions clientOptions = ParseCommonOptions(client, (globalOptions, name));
					return new ClientGenerationOptions(
						name,
						@namespace: clientOptions.Namespace!,
						getDefinitionFromCanister: getDefinitionFromCanister,
						filePathOrCandidId: filePathOrCandidId,
						outputDirectory: clientOptions.OutputDirectory ?? "./",
						purgeOutputDirectory: clientOptions.PurgeDirectory ?? true,
						noFolders: clientOptions.NoFolders  ?? false,
						featureNullable: clientOptions.FeatureNullable ?? true,
						keepCandidCase: clientOptions.KeepCandidCase ?? false,
						boundryNodeUrl: clientOptions.BaseUrl,
						types: clientOptions.Types
					);
				})
				.ToList();
		}

		private static CommonOptions ParseCommonOptions(TomlTable config, (CommonOptions Options, string ClientName)? parent = null)
		{
			
			string? @namespace = GetOptional<string?>(config, "namespace") ?? (parent == null ? null : parent.Value.Options.Namespace + "." + parent.Value.ClientName);
			string? baseUrl = GetOptional<string?>(config, "url");
			Uri? boundryNodeUrl = baseUrl == null ? parent?.Options.BaseUrl : new Uri(baseUrl);
			bool? purgeDirectory = GetOptional<bool?>(config, "purge-directory") ?? parent?.Options.PurgeDirectory;
			bool? noFolders = GetOptional<bool?>(config, "no-folders") ?? parent?.Options.NoFolders;
			string? outputDirectory = GetOptional<string?>(config, "output-directory") ?? (parent == null ? null : Path.Combine(parent.Value.Options.OutputDirectory ?? "./", parent.Value.ClientName));
			bool? featureNullable = GetOptional<bool?>(config, "feature-nullable") ?? parent?.Options.FeatureNullable;
			bool? keepCandidCase = GetOptional<bool?>(config, "keep-candid-case") ?? parent?.Options.KeepCandidCase;
			TomlTable? typeTable = GetOptional<TomlTable?>(config, "types");
			Dictionary<string, NamedTypeOptions> types = BuildTypes(typeTable);
			if (parent?.Options.Types != null)
			{
				foreach ((string key, NamedTypeOptions value) in parent.Value.Options.Types)
				{
					if (!parent.Value.Options.Types.ContainsKey(key))
					{
						parent.Value.Options.Types.Add(key, value);
					}
					else
					{
						parent.Value.Options.Types[key] = MergeOptions(value, parent.Value.Options.Types[key]);
					}
				}
			}

			return new CommonOptions(
				@namespace,
				boundryNodeUrl,
				purgeDirectory,
				noFolders,
				outputDirectory,
				featureNullable,
				keepCandidCase,
				types
			);
		}

		private static NamedTypeOptions MergeOptions(NamedTypeOptions? primary, NamedTypeOptions? secondary)
		{
			return new NamedTypeOptions(
				nameOverride: primary?.NameOverride ?? secondary?.NameOverride,
				typeOptions: primary?.TypeOptions ?? secondary?.TypeOptions // TODO merge vs override?
			);
		}

		private static Dictionary<string, NamedTypeOptions> BuildTypes(TomlTable? typeTable)
		{
			Dictionary<string, NamedTypeOptions> types = new();
			if (typeTable != null)
			{
				foreach ((string key, object value) in typeTable)
				{
					if (value is not TomlTable t)
					{
						throw new Exception($"Options for type '{key}' cannot be parsed");
					}
					string? name = GetOptional<string>(t, "name");
					ITypeOptions? typeOptions = BuildTypeOptions(t);
					types.Add(key, new NamedTypeOptions(name, typeOptions));
				}
			}
			return types;
		}

		private static ITypeOptions? BuildTypeOptions(TomlTable t)
		{
			string? typeType = GetOptional<string>(t, "type");
			if (string.IsNullOrWhiteSpace(typeType))
			{
				return null;
			}
			switch (typeType.ToLower())
			{
				case "record":
					{
						TomlTable? fieldTypeTable = GetOptional<TomlTable>(t, "fields");
						Dictionary<string, NamedTypeOptions> fields = BuildTypes(fieldTypeTable);
						return new RecordTypeOptions(
							fields: fields
						);
					}
				case "vec":
					{
						ITypeOptions? elementType = GetOptional<ITypeOptions>(t, "elementType");
						VectorRepresentation? representation = GetEnumOptional<VectorRepresentation>(t, "representation");
						return new VectorTypeOptions(
							representation: representation,
							elementType: elementType
						);
					}
				case "variant":
					{
						TomlTable? optionTypeTable = GetOptional<TomlTable>(t, "options");
						Dictionary<string, NamedTypeOptions> options = BuildTypes(optionTypeTable);
						return new VariantTypeOptions(
							options: options
						);
					}
				default:
					throw new Exception($"Type '{typeType.ToLower()}' is invalid");
			}
		}

		private static T GetRequired<T>(TomlTable table, string key, string? prefix = null)
		{
			if (table.ContainsKey(key))
			{
				return table[key].Cast<T>();
			}
			if (prefix != null)
			{
				key = prefix + "." + key;
			}
			throw new InvalidOperationException($"Failed to parse the `candid-client.toml` config. Missing required field: '{key}'");
		}

		private static T? GetOptional<T>(TomlTable table, string key)
		{
			if (table.ContainsKey(key))
			{
				return table[key].Cast<T>();
			}
			return default;
		}

		private static TEnum GetEnumRequired<TEnum>(TomlTable table, string key)
			where TEnum : struct
		{
			string stringValue = GetRequired<string>(table, key);
			return (TEnum)Enum.Parse(typeof(TEnum), stringValue);
		}

		private static TEnum? GetEnumOptional<TEnum>(TomlTable table, string key)
			where TEnum : struct
		{
			string? stringValue = GetOptional<string>(table, key);
			if (stringValue == null)
			{
				return null;
			}
			return (TEnum)Enum.Parse(typeof(TEnum), stringValue);
		}

	}

	internal class CommonOptions
	{
		public string? Namespace { get; }
		public Uri? BaseUrl { get; }
		public bool? PurgeDirectory { get; }
		public bool? NoFolders { get; }
		public string? OutputDirectory { get; }
		public bool? FeatureNullable { get; }
		public bool? KeepCandidCase { get; }
		public Dictionary<string, NamedTypeOptions> Types { get; }

		public CommonOptions(
			string? @namespace,
			Uri? baseUrl,
			bool? purgeDirectory,
			bool? noFolders,
			string? outputDirectory,
			bool? featureNullable,
			bool? keepCandidCase,
			Dictionary<string, NamedTypeOptions>? types
		)
		{
			this.Namespace = @namespace;
			this.BaseUrl = baseUrl;
			this.PurgeDirectory = purgeDirectory;
			this.NoFolders = noFolders;
			this.OutputDirectory = outputDirectory;
			this.FeatureNullable = featureNullable;
			this.KeepCandidCase = keepCandidCase;
			this.Types = types ?? new Dictionary<string, NamedTypeOptions>();
		}
	}
}
