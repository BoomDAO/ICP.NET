using EdjCase.ICP.Candid.Mapping.Mappers;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EdjCase.ICP.ClientGenerator
{
	internal class ClientCodeResult
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
	internal static class ClientCodeGenerator
	{
		public static ClientCodeResult FromService(string serviceName, string baseNamespace, CandidServiceDescription service)
		{


			// Mapping of A => Type
			// where candid is: type A = Type;
			Dictionary<ValueName, SourceCodeType> declaredTypes = service.DeclaredTypes
				.Where(t => t.Value is not CandidServiceType) // avoid duplication service of type
				.ToDictionary(
					t => ValueName.Default(t.Key.ToString()),
					t => ResolveSourceCodeType(t.Value)
				);

			string modelNamespace = baseNamespace + ".Models";
			HashSet<ValueName> aliases = declaredTypes
				.Where(t => t.Value is CompiledTypeSourceCodeType || t.Value is ReferenceSourceCodeType)
				.Select(t => t.Key)
				.ToHashSet();

			var typeResolver = new RoslynTypeResolver(modelNamespace, aliases);
			Dictionary<ValueName, ResolvedType> resolvedTypes = declaredTypes
				.ToDictionary(
					t => t.Key,
					t => typeResolver.ResolveTypeDeclaration(t.Key, t.Value)
				);

			var typeFiles = new List<(string FileName, string Source)>();
			Dictionary<ValueName, TypeName> aliasTypes = aliases
				.ToDictionary(t => t, t => resolvedTypes[t].Name);
			foreach ((ValueName id, ResolvedType typeInfo) in resolvedTypes)
			{
				string? sourceCode = RoslynSourceGenerator.GenerateTypeSourceCode(typeInfo, typeResolver.ModelNamespace, aliasTypes);
				if (sourceCode != null)
				{
					typeFiles.Add((typeInfo.Name.GetName(), sourceCode));
				}
			}


			TypeName clientName = new(StringUtil.ToPascalCase(serviceName) + "ApiClient", baseNamespace, prefix: null);
			ServiceSourceCodeType serviceSourceType = ResolveService(service.Service);
			string clientSource = RoslynSourceGenerator.GenerateClientSourceCode(clientName, baseNamespace, serviceSourceType, typeResolver, aliasTypes);

			string? aliasFile = null; // TODO? global using only supported in C# 10+
			return new ClientCodeResult(clientName, clientSource, typeFiles, aliasFile);
		}

		private static SourceCodeType ResolveSourceCodeType(CandidType type)
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
						SourceCodeType innerType = ResolveSourceCodeType(v.InnerType);
						return new CompiledTypeSourceCodeType(typeof(List<>), innerType);
					}
				case CandidOptionalType o:
					{
						SourceCodeType innerType = ResolveSourceCodeType(o.Value);
						return new CompiledTypeSourceCodeType(typeof(OptionalValue<>), innerType);
					}
				case CandidRecordType o:
					{
						List<(ValueName Key, SourceCodeType Type)> fields = o.Fields
							.Select(f => (ValueName.Default(f.Key), ResolveSourceCodeType(f.Value)))
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
									: ResolveSourceCodeType(f.Value);
								return (ValueName.Default(f.Key), sourceCodeType);
							})
							.ToList();
						return new VariantSourceCodeType(fields);
					}
				default:
					throw new NotImplementedException();
			}
		}

		internal static ServiceSourceCodeType ResolveService(CandidServiceType s)
		{
			List<(ValueName Name, ServiceSourceCodeType.Func FuncInfo)> methods = s.Methods
				.Select(m => (ValueName.Default(m.Key.ToString()), ResolveFunc(m.Value)))
				.ToList();
			return new ServiceSourceCodeType(methods);
		}

		private static ServiceSourceCodeType.Func ResolveFunc(CandidFuncType value)
		{
			List<(ValueName Name, SourceCodeType Type)> argTypes = value.ArgTypes
				.Select(ResolveXType)
				.ToList();
			List<(ValueName Name, SourceCodeType Type)> returnTypes = value.ReturnTypes
				.Select(ResolveXType)
				.ToList();
			bool isFireAndForget = value.Modes.Contains(FuncMode.Oneway);
			bool isQuery = value.Modes.Contains(FuncMode.Query);
			return new ServiceSourceCodeType.Func(argTypes, returnTypes, isFireAndForget, isQuery);


			static (ValueName Name, SourceCodeType Type) ResolveXType((CandidId? Name, CandidType Type) a, int i)
			{
				ValueName name = ValueName.Default(a.Name?.ToString() ?? "arg" + i);
				SourceCodeType type = ResolveSourceCodeType(a.Type);
				return (name, type);
			}
		}

	}

}