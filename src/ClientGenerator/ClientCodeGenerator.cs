using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	/// <summary>
	/// Generator to create client source code based of candid definitions from `.did` files
	/// or from a canister id
	/// </summary>
	public static class ClientCodeGenerator
	{
		/// <summary>
		/// Creates client source code for a canister based on its id. This only works if 
		/// the canister has the `candid:service` meta data available in its public state
		/// </summary>
		/// <param name="canisterId">The canister to get the definition from</param>
		/// <param name="options">The options for client generation</param>
		public static async Task<ClientSyntax> GenerateClientFromCanisterAsync(
			Principal canisterId,
			ClientGenerationOptions options
		)
		{
			var agent = new HttpAgent(identity: null, httpBoundryNodeUrl: options.BoundryNodeUrl);
			var candidServicePath = StatePath.FromSegments("canister", canisterId.Raw, "metadata", "candid:service");
			var paths = new List<StatePath>
			{
				candidServicePath
			};
			var response = await agent.ReadStateAsync(canisterId, paths);
			string? fileText = response.Certificate.Tree.GetValueOrDefault(candidServicePath)?.AsLeaf().AsUtf8();

			if (string.IsNullOrWhiteSpace(fileText))
			{
				throw new Exception("Canister does not have 'candid:service' exposed");
			}

			return GenerateClientFromFile(fileText, options);
		}

		/// <summary>
		/// Generates client source code for a canister based on a `.did` file definition
		/// </summary>
		/// <param name="fileText">The text content of the `.did` definition file</param>
		/// <param name="options">The options for client generation</param>
		public static ClientSyntax GenerateClientFromFile(
			string fileText,
			ClientGenerationOptions options
		)
		{
			CandidServiceDescription serviceFile = CandidServiceDescription.Parse(fileText);

			return GenerateClient(serviceFile, options);
		}

		/// <summary>
		/// Generates client source code for a canister based on a `.did` file definition
		/// </summary>
		/// <param name="service">The service definition to generate the client from</param>
		/// <param name="options">The options for client generation</param>
		public static ClientSyntax GenerateClient(
			CandidServiceDescription service,
			ClientGenerationOptions options
		)
		{
			var nameHelper = new NameHelper(options.KeepCandidCase);
			// Mapping of A => Type
			// where candid is: type A = Type;
			Dictionary<string, (string Name, SourceCodeType Type)> declaredTypes = service.DeclaredTypes
				.Where(t => t.Value is not CandidServiceType) // avoid duplication service of type
				.Select(t =>
				{
					NamedTypeOptions? typeOptions = options.Types.GetValueOrDefault(t.Key.ToString());
					ResolvedName typeName = nameHelper.ResolveName(t.Key.ToString(), typeOptions?.NameOverride);
					SourceCodeType sourceCodeType = ResolveSourceCodeType(
						t.Value,
						nameHelper,
						typeOptions?.TypeOptions
					);
					return (Id: t.Key.ToString(), Name: typeName.Name, Type: sourceCodeType);
				})
				.ToDictionary(t => t.Id, t => (t.Name, t.Type));

			string modelNamespace = options.NoFolders
				? options.Namespace
				: options.Namespace + ".Models";
			HashSet<string> aliases = declaredTypes
				.Where(t => t.Value.Type.IsPredefinedType)
				.Select(t => t.Key)
				.ToHashSet();

			var typeResolver = new RoslynTypeResolver(
				modelNamespace,
				aliases,
				options.FeatureNullable,
				options.VariantsUseProperties,
				nameHelper,
				declaredTypes
			);
			Dictionary<string, ResolvedType> resolvedTypes = declaredTypes
				.ToDictionary(
					t => t.Key,
					t => typeResolver.ResolveTypeDeclaration(t.Value.Name, t.Value.Type)
				);

			var typeFiles = new List<(string FileName, CompilationUnitSyntax Source)>();


			Dictionary<string, string> aliasTypes = aliases
				.ToDictionary(
					t => declaredTypes[t].Name,
					t => resolvedTypes[t].Name.BuildName(options.FeatureNullable, includeNamespace: true, resolveAliases: true)
				);
			foreach ((string id, ResolvedType typeInfo) in resolvedTypes)
			{
				CompilationUnitSyntax? sourceCode = RoslynSourceGenerator.GenerateTypeSourceCode(
					typeInfo,
					typeResolver.ModelNamespace,
					aliasTypes
				);
				if (sourceCode != null)
				{
					typeFiles.Add((typeInfo.Name.BuildName(options.FeatureNullable, false), sourceCode));
				}
			}

			string clientName = options.Name + "ApiClient";
			TypeName clientTypeName = new SimpleTypeName(clientName, options.Namespace, isDefaultNullable: true);
			ServiceSourceCodeType serviceSourceType = ResolveService(service.Service, nameHelper);
			CompilationUnitSyntax clientSource = RoslynSourceGenerator.GenerateClientSourceCode(
				clientTypeName,
				options.Namespace,
				serviceSourceType,
				typeResolver,
				aliasTypes
			);

			// TODO? global using only supported in C# 10+
			return new ClientSyntax(clientName, clientSource, typeFiles);
		}

		private static SourceCodeType ResolveSourceCodeType(
			CandidType type,
			NameHelper nameHelper,
			TypeOptions? typeOptions)
		{

			switch (type)
			{
				case CandidFuncType f:
					{
						return new NonGenericSourceCodeType(typeof(CandidFunc));
					}
				case CandidPrimitiveType p:
					{
						return p.PrimitiveType switch
						{
							PrimitiveType.Text => new NonGenericSourceCodeType(typeof(string)),
							PrimitiveType.Nat => new NonGenericSourceCodeType(typeof(UnboundedUInt)),
							PrimitiveType.Nat8 => new NonGenericSourceCodeType(typeof(byte)),
							PrimitiveType.Nat16 => new NonGenericSourceCodeType(typeof(ushort)),
							PrimitiveType.Nat32 => new NonGenericSourceCodeType(typeof(uint)),
							PrimitiveType.Nat64 => new NonGenericSourceCodeType(typeof(ulong)),
							PrimitiveType.Int => new NonGenericSourceCodeType(typeof(UnboundedInt)),
							PrimitiveType.Int8 => new NonGenericSourceCodeType(typeof(sbyte)),
							PrimitiveType.Int16 => new NonGenericSourceCodeType(typeof(short)),
							PrimitiveType.Int32 => new NonGenericSourceCodeType(typeof(int)),
							PrimitiveType.Int64 => new NonGenericSourceCodeType(typeof(long)),
							PrimitiveType.Float32 => new NonGenericSourceCodeType(typeof(float)),
							PrimitiveType.Float64 => new NonGenericSourceCodeType(typeof(double)),
							PrimitiveType.Bool => new NonGenericSourceCodeType(typeof(bool)),
							PrimitiveType.Principal => new NonGenericSourceCodeType(typeof(Principal)),
							PrimitiveType.Reserved => new NonGenericSourceCodeType(typeof(ReservedValue)),
							PrimitiveType.Empty => new NonGenericSourceCodeType(typeof(EmptyValue)),
							PrimitiveType.Null => new NonGenericSourceCodeType(typeof(NullValue)),
							_ => throw new NotImplementedException(),
						};
					}
				case CandidReferenceType r:
					{
						return new ReferenceSourceCodeType(r.Id);
					}
				case CandidVectorType v:
					{
						TypeOptions? innerTypeOptions = typeOptions?.InnerType;
						SourceCodeType innerType = ResolveSourceCodeType(
							v.InnerType,
							nameHelper,
							innerTypeOptions
						);
						bool isDictionaryCompatible = innerType is TupleSourceCodeType t && t.Fields.Count == 2;
						string defaultRepresentation = isDictionaryCompatible
							? "dictionary"
							: "list";
						string rep = typeOptions?.Representation ?? defaultRepresentation;
						switch (rep.ToLower())
						{
							case "array":
								return new ArraySourceCodeType(innerType);
							case "list":
								return new ListSourceCodeType(innerType);
							case "dictionary":
								if (!isDictionaryCompatible)
								{
									throw new Exception("List to dictionary conversion is only compatible with `vec record { a; b }` candid types");
								}
								TupleSourceCodeType tuple = (TupleSourceCodeType)innerType;
								SourceCodeType keyType = tuple.Fields[0];
								SourceCodeType valueType = tuple.Fields[1];
								return new DictionarySourceCodeType(keyType, valueType);
							default:
								throw new Exception($"Vec types do not support representation: '{rep}'");
						}
					}
				case CandidOptionalType o:
					{
						SourceCodeType innerType = ResolveSourceCodeType(
							o.Value,
							nameHelper,
							typeOptions: null
						);

						return new OptionalValueSourceCodeType(innerType);
					}
				case CandidRecordType o:
					{
						// Check if tuple (tag ids are 0...N)

						bool isTuple = o.Fields.Any()
							&& o.Fields
								.Select(f => f.Key.Id)
								.OrderBy(f => f)
								.SequenceEqual(Enumerable.Range(0, o.Fields.Count).Select(i => (uint)i))
							// Only be a tuple if it doesnt reference itself
							&& !o.Fields.Any(f => f.Value is CandidReferenceType r && r.Id == o.RecursiveId);

						if(isTuple)
						{
							List<SourceCodeType> tupleFields = o.Fields
									.Select(f =>
									{
										NamedTypeOptions? fieldTypeOptions = typeOptions?.Fields.GetValueOrDefault(f.Key.ToString());
										return ResolveSourceCodeType(
											f.Value,
											nameHelper,
											fieldTypeOptions?.TypeOptions
										);
									})
									.Where(f => f != null)
									.ToList()!;
							return new TupleSourceCodeType(tupleFields);
						}
						List<(ResolvedName Key, SourceCodeType Type)> fields = o.Fields
								.Select(f =>
								{
									CandidType fCandidType = f.Value;
									NamedTypeOptions? fieldTypeOptions = f.Key.Name == null ? null : typeOptions?.Fields.GetValueOrDefault(f.Key.Name);
									SourceCodeType fType = ResolveSourceCodeType(fCandidType, nameHelper, fieldTypeOptions?.TypeOptions);
									ResolvedName fieldName = nameHelper.ResolveName(f.Key, fieldTypeOptions?.NameOverride);
									return (fieldName, fType);
								})
								.Where(f => f.Item2 != null)
								.ToList()!;
						return new RecordSourceCodeType(fields);
					}
				case CandidVariantType va:
					{
						List<(ResolvedName Key, SourceCodeType? Type)> fields = va.Options
							.Select(f =>
							{
								NamedTypeOptions? innerTypeOptions = f.Key.Name == null ? null : typeOptions?.Fields.GetValueOrDefault(f.Key.Name);
								// If type is null, then just be a typeless variant
								SourceCodeType? sourceCodeType = f.Value == CandidType.Null()
									? null
									: ResolveSourceCodeType(f.Value, nameHelper, innerTypeOptions?.TypeOptions);
								ResolvedName optionName = nameHelper.ResolveName(f.Key, innerTypeOptions?.NameOverride);
								return (optionName, sourceCodeType);
							})
							.ToList();
						return new VariantSourceCodeType(fields);
					}
				default:
					throw new NotImplementedException();
			}
		}

		internal static ServiceSourceCodeType ResolveService(CandidServiceType s, NameHelper nameHelper)
		{
			List<(string CsharpName, string CandidName, ServiceSourceCodeType.Func FuncInfo)> methods = s.Methods
				.Select(m =>
				{
					ResolvedName valueName = nameHelper.ResolveName(m.Key.ToString());
					return (valueName.Name, valueName.CandidTag.Name!, ResolveFunc(m.Value, nameHelper));
				})
				.ToList();
			return new ServiceSourceCodeType(methods);
		}

		private static ServiceSourceCodeType.Func ResolveFunc(CandidFuncType value, NameHelper nameHelper)
		{
			List<(ResolvedName Name, SourceCodeType Type)> argTypes = value.ArgTypes
				.Select(ResolveXType)
				.ToList();
			List<(ResolvedName Name, SourceCodeType Type)> returnTypes = value.ReturnTypes
				.Select(ResolveXType)
				.ToList();
			bool isOneway = value.Modes.Contains(FuncMode.Oneway);
			bool isQuery = value.Modes.Contains(FuncMode.Query) || value.Modes.Contains(FuncMode.CompositeQuery);
			return new ServiceSourceCodeType.Func(argTypes, returnTypes, isOneway, isQuery);


			(ResolvedName Name, SourceCodeType Type) ResolveXType((CandidId? Name, CandidType Type) a, int i)
			{
				ResolvedName name = nameHelper.ResolveName(a.Name?.ToString() ?? "arg" + i);
				SourceCodeType type = ResolveSourceCodeType(a.Type, nameHelper, null);
				return (name, type);
			}
		}


	}
}
