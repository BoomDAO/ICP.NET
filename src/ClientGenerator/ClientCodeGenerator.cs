using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

			// Mapping of A => Type
			// where candid is: type A = Type;
			Dictionary<string, SourceCodeType> declaredTypes = service.DeclaredTypes
				.Where(t => t.Value is not CandidServiceType) // avoid duplication service of type
				.Select(t =>
				{
					ITypeOptions? typeOptions = options.Types.GetValueOrDefault(t.Key.ToString());
					//string typeName = typeOptions?.NameOverride ?? t.Key.ToString(); // TODO
					string typeName = t.Key.ToString();
					SourceCodeType sourceCodeType = ResolveSourceCodeType(
						t.Value,
						options.KeepCandidCase,
						typeOptions
					);
					return (Name: typeName, Type: sourceCodeType);
				})
				.ToDictionary(t => t.Name, t => t.Type);

			string modelNamespace = options.NoFolders
				? options.Namespace
				: options.Namespace + ".Models";
			HashSet<string> aliases = declaredTypes
				.Where(t => t.Value.IsPredefinedType)
				.Select(t => t.Key)
				.ToHashSet();

			var typeResolver = new RoslynTypeResolver(modelNamespace, aliases, options.FeatureNullable, options.KeepCandidCase);
			Dictionary<string, ResolvedType> resolvedTypes = declaredTypes
				.ToDictionary(
					t => t.Key,
					t => typeResolver.ResolveTypeDeclaration(t.Key, t.Value)
				);

			var typeFiles = new List<(string FileName, CompilationUnitSyntax Source)>();
			Dictionary<string, TypeName> aliasTypes = aliases
				.ToDictionary(t => t, t => resolvedTypes[t].Name);
			foreach ((string id, ResolvedType typeInfo) in resolvedTypes)
			{
				CompilationUnitSyntax? sourceCode = RoslynSourceGenerator.GenerateTypeSourceCode(typeInfo, typeResolver.ModelNamespace, aliasTypes);
				if (sourceCode != null)
				{
					typeFiles.Add((id, sourceCode));
				}
			}

			string clientName = options.Name + "ApiClient";
			TypeName clientTypeName = new SimpleTypeName(clientName, options.Namespace, prefix: null);
			ServiceSourceCodeType serviceSourceType = ResolveService(service.Service, options.KeepCandidCase);
			CompilationUnitSyntax clientSource = RoslynSourceGenerator.GenerateClientSourceCode(clientTypeName, options.Namespace, serviceSourceType, typeResolver, aliasTypes);

			// TODO? global using only supported in C# 10+
			return new ClientSyntax(clientName, clientSource, typeFiles);
		}

		private static SourceCodeType ResolveSourceCodeType(
			CandidType type,
			bool keepCandidCase,
			ITypeOptions? typeOptions)
		{
			T? GetTypeOptions<T>()
				where T : class, ITypeOptions
			{
				if (typeOptions != null
					&& typeOptions is not T)
				{
					throw new Exception($"Type is '{typeOptions.GetType()}' but type options assume '{typeof(T)}'. Fix config");
				}
				return typeOptions as T;
			}

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
						VectorTypeOptions? vTypeOptions = GetTypeOptions<VectorTypeOptions>();
						ITypeOptions? innerTypeOptions = vTypeOptions?.InnerType;
						SourceCodeType innerType = ResolveSourceCodeType(
							v.InnerType,
							keepCandidCase,
							innerTypeOptions
						);
						switch (vTypeOptions?.Representation ?? VectorRepresentation.List)
						{
							case VectorRepresentation.Array:
								return new ArraySourceCodeType(innerType);
							case VectorRepresentation.List:
								return new ListSourceCodeType(innerType);
							case VectorRepresentation.Dictionary:
								if (innerType is not TupleSourceCodeType t || t.Fields.Count != 2)
								{
									throw new Exception("List to dictionary conversion is only compatible with `vec record { a; b }` candid types");
								}
								SourceCodeType keyType = t.Fields[0];
								SourceCodeType valueType = t.Fields[1];
								return new DictionarySourceCodeType(keyType, valueType);
							default:
								throw new NotImplementedException();
						}
					}
				case CandidOptionalType o:
					{
						SourceCodeType innerType = ResolveSourceCodeType(
							o.Value,
							keepCandidCase,
							typeOptions: null
						);

						return new OptionalValueSourceCodeType(innerType);
					}
				case CandidRecordType o:
					{
						RecordTypeOptions? rTypeOptions = GetTypeOptions<RecordTypeOptions>();
						// Check if tuple (tag ids are 0...N)

						RecordRepresentation representation;
						if ((rTypeOptions?.Representation) != null)
						{
							representation = rTypeOptions!.Representation.Value;
						}
						else
						{
							bool isTuple = o.Fields.Any()
								&& o.Fields
									.Select(f => f.Key.Id)
									.OrderBy(f => f)
									.SequenceEqual(Enumerable.Range(0, o.Fields.Count).Select(i => (uint)i))
								// Only be a tuple if it doesnt reference any other type
								// This is due to complications with C# aliasing
								&& !o.Fields.Any(f => f.Value is CandidReferenceType);

							representation = isTuple ? RecordRepresentation.Tuple : RecordRepresentation.CustomType;
						}

						switch (representation)
						{
							case RecordRepresentation.CustomType:
								List<(ValueName Key, SourceCodeType Type)> fields = o.Fields
									.Select(f =>
									{
										CandidType fCandidType = f.Value;
										ITypeOptions? fieldTypeOptions = f.Key.Name == null ? null : rTypeOptions?.Fields.GetValueOrDefault(f.Key.Name);
										SourceCodeType fType = ResolveSourceCodeType(fCandidType, keepCandidCase, fieldTypeOptions);
										return (ValueName.Default(f.Key, keepCandidCase), fType);
									})
									.Where(f => f.Item2 != null)
									.ToList()!;
								return new RecordSourceCodeType(fields);
							case RecordRepresentation.Tuple:
								List<SourceCodeType> tupleFields = o.Fields
									.Select(f =>
									{
										ITypeOptions? fieldTypeOptions = rTypeOptions?.Fields.GetValueOrDefault(f.Key.ToString());
										return ResolveSourceCodeType(
											f.Value,
											keepCandidCase,
											fieldTypeOptions
										);
									})
									.Where(f => f != null)
									.ToList()!;
								return new TupleSourceCodeType(tupleFields);

							default:
								throw new NotImplementedException();
						}
					}
				case CandidVariantType va:
					{
						VariantTypeOptions? variantTypeOptions = GetTypeOptions<VariantTypeOptions>();
						List<(ValueName Key, SourceCodeType? Type)> fields = va.Options
							.Select(f =>
							{
								ITypeOptions? innerTypeOptions = f.Key.Name == null ? null : variantTypeOptions?.Options.GetValueOrDefault(f.Key.Name);
								// If type is null, then just be a typeless variant
								SourceCodeType? sourceCodeType = f.Value == CandidType.Null()
									? null
									: ResolveSourceCodeType(f.Value, keepCandidCase, innerTypeOptions);
								return (ValueName.Default(f.Key, keepCandidCase), sourceCodeType);
							})
							.ToList();
						return new VariantSourceCodeType(fields);
					}
				default:
					throw new NotImplementedException();
			}
		}

		internal static ServiceSourceCodeType ResolveService(CandidServiceType s, bool keepCandidCase)
		{
			List<(string CsharpName, string CandidName, ServiceSourceCodeType.Func FuncInfo)> methods = s.Methods
				.Select(m =>
				{
					ValueName valueName = ValueName.Default(m.Key.ToString(), keepCandidCase);
					return (valueName.PropertyName, valueName.CandidTag.Name!, ResolveFunc(m.Value, keepCandidCase));
				})
				.ToList();
			return new ServiceSourceCodeType(methods);
		}

		private static ServiceSourceCodeType.Func ResolveFunc(CandidFuncType value, bool keepCandidCase)
		{
			List<(ValueName Name, SourceCodeType Type)> argTypes = value.ArgTypes
				.Select(ResolveXType)
				.ToList();
			List<(ValueName Name, SourceCodeType Type)> returnTypes = value.ReturnTypes
				.Select(ResolveXType)
				.ToList();
			bool isOneway = value.Modes.Contains(FuncMode.Oneway);
			bool isQuery = value.Modes.Contains(FuncMode.Query);
			return new ServiceSourceCodeType.Func(argTypes, returnTypes, isOneway, isQuery);


			(ValueName Name, SourceCodeType Type) ResolveXType((CandidId? Name, CandidType Type) a, int i)
			{
				ValueName name = ValueName.Default(a.Name?.ToString() ?? "arg" + i, keepCandidCase);
				SourceCodeType type = ResolveSourceCodeType(a.Type, keepCandidCase, null);
				return (name, type);
			}
		}


	}
}
