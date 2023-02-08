using EdjCase.ICP.Candid.Mapping.Mappers;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using ICP.ClientGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
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

			var typeResolver = new RoslynTypeResolver();

			// Mapping of A => Type
			// where candid is: type A = Type;
			Dictionary<ValueName, SourceCodeType> declaredTypes = service.DeclaredTypes
				.Where(t => !(t.Value is CandidServiceType)) // avoid duplication service of type
				.ToDictionary(t => ValueName.Default(t.Key.ToString()), t => ResolveSourceCodeType(t.Value));

			Dictionary<ValueName, TypeName> aliases = new();
			var typeFiles = new List<(string FileName, string Source)>();
			foreach ((ValueName id, SourceCodeType typeInfo) in declaredTypes)
			{
				ResolvedType type = typeResolver.ResolveTypeDeclaration(id, typeInfo);
				if (name == null)
				{
					// Skip null, empty and reserved types
					continue;
				}

				if (customType)
				{
					if (typeBuilder == null)
					{
						throw new NotImplementedException("No type builder made for custom type");
					}
					typeFiles.Add(
						RoslynSourceGenerator.GenerateTypeSourceCode(id, baseNamespace, typeResolver)
					);
				}
				else
				{
					aliases.Add(id, name);
					if (typeBuilder != null)
					{
						typeFiles.Add(
							RoslynSourceGenerator.GenerateTypeSourceCode(id, baseNamespace, typeResolver)
						);
					}
				}

			}


			TypeName clientName = new TypeName(StringUtil.ToPascalCase(serviceName) + "ApiClient", baseNamespace);
			ServiceSourceCodeType serviceSourceType = ResolveService(service.Service);
			string clientSource = RoslynSourceGenerator.GenerateClientSourceCode(clientName, baseNamespace, serviceSourceType, typeResolver);

			string? aliasFile = null; // TODO? global using only supported in C# 10+
			return new ClientCodeResult(clientName, clientSource, typeFiles, aliasFile);
		}

		private static SourceCodeType ResolveSourceCodeType(CandidType type)
		{

			switch (type)
			{
				case CandidFuncType f:
					{
						return new CsharpBuiltInTypeSourceCodeType(typeof(CandidFunc));
					}
				case CandidPrimitiveType p:
					{
						return p.PrimitiveType switch
						{
							PrimitiveType.Text => new CsharpBuiltInTypeSourceCodeType(typeof(string)),
							PrimitiveType.Nat => new CsharpBuiltInTypeSourceCodeType(typeof(UnboundedUInt)),
							PrimitiveType.Nat8 => new CsharpBuiltInTypeSourceCodeType(typeof(byte)),
							PrimitiveType.Nat16 => new CsharpBuiltInTypeSourceCodeType(typeof(ushort)),
							PrimitiveType.Nat32 => new CsharpBuiltInTypeSourceCodeType(typeof(uint)),
							PrimitiveType.Nat64 => new CsharpBuiltInTypeSourceCodeType(typeof(ulong)),
							PrimitiveType.Int => new CsharpBuiltInTypeSourceCodeType(typeof(UnboundedInt)),
							PrimitiveType.Int8 => new CsharpBuiltInTypeSourceCodeType(typeof(sbyte)),
							PrimitiveType.Int16 => new CsharpBuiltInTypeSourceCodeType(typeof(short)),
							PrimitiveType.Int32 => new CsharpBuiltInTypeSourceCodeType(typeof(int)),
							PrimitiveType.Int64 => new CsharpBuiltInTypeSourceCodeType(typeof(long)),
							PrimitiveType.Float32 => new CsharpBuiltInTypeSourceCodeType(typeof(float)),
							PrimitiveType.Float64 => new CsharpBuiltInTypeSourceCodeType(typeof(double)),
							PrimitiveType.Bool => new CsharpBuiltInTypeSourceCodeType(typeof(bool)),
							PrimitiveType.Principal => new CsharpBuiltInTypeSourceCodeType(typeof(Principal)),
							PrimitiveType.Reserved => new NullEmptyOrReservedSourceCodeType(),
							PrimitiveType.Empty => new NullEmptyOrReservedSourceCodeType(),
							PrimitiveType.Null => new NullEmptyOrReservedSourceCodeType(),
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
						return new CsharpBuiltInTypeSourceCodeType(typeof(List<>), innerType);
					}
				case CandidOptionalType o:
					{
						SourceCodeType innerType = ResolveSourceCodeType(o.Value);
						return new CsharpBuiltInTypeSourceCodeType(typeof(OptionalValue<>), innerType);
					}
				case CandidRecordType o:
					{
						List<(ValueName Key, SourceCodeType Type)> fields = o.Fields
							.Select(f => (ValueName.Default(f.Key), ResolveSourceCodeType(f.Value)))
							.ToList();
						return new RecordSourceCodeType(fields);
					}
				case CandidVariantType va:
					{
						List<(ValueName Key, SourceCodeType Type)> fields = va.Options
							.Select(f => (ValueName.Default(f.Key), ResolveSourceCodeType(f.Value)))
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