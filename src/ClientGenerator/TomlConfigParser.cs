using CommandLine;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			string baseNamespace = GetRequired<string>(config, "namespace");
			string? baseUrl = GetOptional<string?>(config, "url");
			bool? purgeDirectory = GetOptional<bool?>(config, "purge-directory");
			bool? noFolders = GetOptional<bool?>(config, "no-folders");
			Uri? boundryNodeUrl = baseUrl == null ? null : new Uri(baseUrl);
			string outputDirectory = Path.GetRelativePath("./", GetOptional<string?>(config, "output-directory") ?? "./");
			bool? featureNullable = GetOptional<bool?>(config, "feature-nullable");
			bool? keepCandidCase = GetOptional<bool?>(config, "keep-candid-case");

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
					string @namespace = baseNamespace + "." + name;
					string clientOutputDirectory = GetOptional<string?>(client, "output-directory") ?? Path.Combine(outputDirectory, name);
					string clientNamespace = GetOptional<string?>(client, "namespace") ?? @namespace;
					bool clientPurgeOutputDirectory = GetOptional<bool?>(client, "purge-directory") ?? purgeDirectory ?? true;
					bool clientNoFolders = GetOptional<bool?>(client, "no-folders") ?? noFolders ?? false;
					bool clientFeatureNullable = GetOptional<bool?>(client, "feature-nullable") ?? featureNullable ?? true;
					bool clientKeepCandidCase = GetOptional<bool?>(client, "keep-candid-case") ?? keepCandidCase ?? false;
					TomlTable? typeTable = GetOptional<TomlTable?>(client, "types");
					Dictionary<string, NamedTypeOptions> types = BuildTypes(typeTable);

					return new ClientGenerationOptions(
						name,
						clientNamespace,
						getDefinitionFromCanister,
						filePathOrCandidId,
						clientOutputDirectory,
						clientPurgeOutputDirectory,
						clientNoFolders,
						clientFeatureNullable,
						clientKeepCandidCase,
						boundryNodeUrl,
						types
					);
				})
				.ToList();
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
						RecordRepresentation? representation = GetEnumOptional<RecordRepresentation>(t, "representation");
						return new RecordTypeOptions(
							representation: representation,
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
}
