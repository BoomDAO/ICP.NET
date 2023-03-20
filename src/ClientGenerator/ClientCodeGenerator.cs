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
		/// <param name="httpBoundryNodeUrl">Optional. The http boundry node url to use, otherwise uses the default</param>
		public static async Task<ClientSyntax> GenerateClientFromCanisterAsync(
			Principal canisterId,
			ClientGenerationOptions options,
			Uri? httpBoundryNodeUrl = null
		)
		{
			var agent = new HttpAgent(identity: null, httpBoundryNodeUrl: httpBoundryNodeUrl);
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
			Dictionary<ValueName, SourceCodeType> declaredTypes = service.DeclaredTypes
				.Where(t => t.Value is not CandidServiceType) // avoid duplication service of type
				.ToDictionary(
					t => ValueName.Default(t.Key.ToString(), false),
					t => ResolveSourceCodeType(t.Value, options.KeepCandidCase)
				);

			string modelNamespace = options.NoFolders
				? options.Namespace 
				: options.Namespace + ".Models";
			HashSet<ValueName> aliases = declaredTypes
				.Where(t => t.Value is CompiledTypeSourceCodeType || t.Value is ReferenceSourceCodeType)
				.Select(t => t.Key)
				.ToHashSet();

			var typeResolver = new RoslynTypeResolver(modelNamespace, aliases, options.FeatureNullable, options.KeepCandidCase);
			Dictionary<ValueName, ResolvedType> resolvedTypes = declaredTypes
				.ToDictionary(
					t => t.Key,
					t => typeResolver.ResolveTypeDeclaration(t.Key, t.Value)
				);

			var typeFiles = new List<(string FileName, CompilationUnitSyntax Source)>();
			Dictionary<ValueName, TypeName> aliasTypes = aliases
				.ToDictionary(t => t, t => resolvedTypes[t].Name);
			foreach ((ValueName id, ResolvedType typeInfo) in resolvedTypes)
			{
				CompilationUnitSyntax? sourceCode = RoslynSourceGenerator.GenerateTypeSourceCode(typeInfo, typeResolver.ModelNamespace, aliasTypes);
				if (sourceCode != null)
				{
					typeFiles.Add((id.PropertyName, sourceCode));
				}
			}

			string clientName = options.Name + "ApiClient";
			TypeName clientTypeName = new(clientName, options.Namespace, prefix: null);
			ServiceSourceCodeType serviceSourceType = ResolveService(service.Service, options.KeepCandidCase);
			CompilationUnitSyntax clientSource = RoslynSourceGenerator.GenerateClientSourceCode(clientTypeName, options.Namespace, serviceSourceType, typeResolver, aliasTypes);

			// TODO? global using only supported in C# 10+
			return new ClientSyntax(clientName, clientSource, typeFiles);
		}

		private static SourceCodeType ResolveSourceCodeType(CandidType type, bool keepCandidCase)
		{

			switch (type)
			{
				case CandidFuncType f:
					{
						return new CompiledTypeSourceCodeType(typeof(CandidFunc));
					}
				case CandidPrimitiveType p:
					{
						return p.PrimitiveType switch
						{
							PrimitiveType.Text => new CompiledTypeSourceCodeType(typeof(string)),
							PrimitiveType.Nat => new CompiledTypeSourceCodeType(typeof(UnboundedUInt)),
							PrimitiveType.Nat8 => new CompiledTypeSourceCodeType(typeof(byte)),
							PrimitiveType.Nat16 => new CompiledTypeSourceCodeType(typeof(ushort)),
							PrimitiveType.Nat32 => new CompiledTypeSourceCodeType(typeof(uint)),
							PrimitiveType.Nat64 => new CompiledTypeSourceCodeType(typeof(ulong)),
							PrimitiveType.Int => new CompiledTypeSourceCodeType(typeof(UnboundedInt)),
							PrimitiveType.Int8 => new CompiledTypeSourceCodeType(typeof(sbyte)),
							PrimitiveType.Int16 => new CompiledTypeSourceCodeType(typeof(short)),
							PrimitiveType.Int32 => new CompiledTypeSourceCodeType(typeof(int)),
							PrimitiveType.Int64 => new CompiledTypeSourceCodeType(typeof(long)),
							PrimitiveType.Float32 => new CompiledTypeSourceCodeType(typeof(float)),
							PrimitiveType.Float64 => new CompiledTypeSourceCodeType(typeof(double)),
							PrimitiveType.Bool => new CompiledTypeSourceCodeType(typeof(bool)),
							PrimitiveType.Principal => new CompiledTypeSourceCodeType(typeof(Principal)),
							PrimitiveType.Reserved => new CompiledTypeSourceCodeType(typeof(ReservedValue)),
							PrimitiveType.Empty => new CompiledTypeSourceCodeType(typeof(EmptyValue)),
							PrimitiveType.Null => new CompiledTypeSourceCodeType(typeof(NullValue)),
							_ => throw new NotImplementedException(),
						};
					}
				case CandidReferenceType r:
					{
						return new ReferenceSourceCodeType(r.Id);
					}
				case CandidVectorType v:
					{
						SourceCodeType innerType = ResolveSourceCodeType(v.InnerType, keepCandidCase);
						return new CompiledTypeSourceCodeType(typeof(List<>), innerType);
					}
				case CandidOptionalType o:
					{
						SourceCodeType innerType = ResolveSourceCodeType(o.Value, keepCandidCase);

						return new CompiledTypeSourceCodeType(typeof(OptionalValue<>), innerType);
					}
				case CandidRecordType o:
					{
						List<(ValueName Key, SourceCodeType Type)> fields = o.Fields
							.Select(f =>
							{
								CandidType fCandidType = f.Value;
								SourceCodeType fType = ResolveSourceCodeType(fCandidType, keepCandidCase);
								return (ValueName.Default(f.Key, keepCandidCase), fType);
								})
							.Where(f => f.Item2 != null)
							.ToList()!;
						return new RecordSourceCodeType(fields);
					}
				case CandidVariantType va:
					{
						List<(ValueName Key, SourceCodeType? Type)> fields = va.Options
							.Select(f =>
							{
								// If type is null, then just be a typeless variant
								SourceCodeType? sourceCodeType = f.Value == CandidType.Null()
									? null
									: ResolveSourceCodeType(f.Value, keepCandidCase);
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
			List<(ValueName Name, ServiceSourceCodeType.Func FuncInfo)> methods = s.Methods
				.Select(m => (ValueName.Default(m.Key.ToString(), keepCandidCase), ResolveFunc(m.Value, keepCandidCase)))
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
				SourceCodeType type = ResolveSourceCodeType(a.Type, keepCandidCase);
				return (name, type);
			}
		}


	}
}
