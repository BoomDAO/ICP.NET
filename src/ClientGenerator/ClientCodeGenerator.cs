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

			var typeSourceGenerator = new TypeSourceGenerator();

			// Mapping of A => Type
			// where candid is: type A = Type;
			Dictionary<ValueName, SourceCodeType> declaredTypes = service.DeclaredTypes
				.Where(t => !(t.Value is CandidServiceType)) // avoid duplication service of type
				.ToDictionary(t => ValueName.Default(t.Key.ToString()), t => ResolveSourceCodeType(t.Value));

			Dictionary<ValueName, TypeName> aliases = new();
			var typeFileGenerators = new List<Func<List<string>, (string Name, string SourceCode)>>();
			foreach ((ValueName id, SourceCodeType typeInfo) in declaredTypes)
			{
				(TypeName? name, bool customType) = typeSourceGenerator.ResolveTypeDeclaration(id, typeInfo, out Action<IndentedStringBuilder>? typeBuilder);
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
					typeFileGenerators.Add((importedNamespaces) =>
					{
						return TypeSourceGenerator.GenerateTypeSourceCode(id, baseNamespace, typeBuilder, importedNamespaces);
					});
				}
				else
				{
					aliases.Add(id, name);
					if (typeBuilder != null)
					{
						typeFileGenerators.Add((importedNamespaces) =>
						{
							return TypeSourceGenerator.GenerateTypeSourceCode(id, baseNamespace, typeBuilder, importedNamespaces);
						});
					}
				}

			}

			List<string> importedNamespaces = new List<string>()
			{
				"System",
				"System.Threading.Tasks",
				"System.Collections.Generic",
				"EdjCase.ICP.Candid.Mapping",
				"EdjCase.ICP.Candid"
			}
			.Concat(aliases
				.Select(a => $"{a.Key.PascalCaseValue} = {a.Value.GetNamespacedName()}")
			)
			.ToList();

			List<(string Name, string SourceCode)> typeFiles = typeFileGenerators
				.Select(g => g(importedNamespaces))
				.Select(g => (g.Name, g.SourceCode))
				.ToList();
			TypeName clientName = new TypeName(StringUtil.ToPascalCase(serviceName) + "ApiClient", baseNamespace);
			ServiceSourceCodeType serviceSourceType = ResolveService(service.Service);
			string clientSource = RoslynSourceGenerator.GenerateClientSourceCode(clientName, baseNamespace, serviceSourceType, importedNamespaces);

			string? aliasFile = null; // TODO? global using only supported in C# 10+
			return new ClientCodeResult(clientName, clientSource, typeFiles, aliasFile);
		}

		private static SourceCodeType ResolveSourceCodeType(CandidType type)
		{

			switch (type)
			{
				case CandidFuncType f:
					{
						return new CsharpTypeSourceCodeType(typeof(CandidFunc));
					}
				case CandidPrimitiveType p:
					{
						return p.PrimitiveType switch
						{
							PrimitiveType.Text => new CsharpTypeSourceCodeType(typeof(string)),
							PrimitiveType.Nat => new CsharpTypeSourceCodeType(typeof(UnboundedUInt)),
							PrimitiveType.Nat8 => new CsharpTypeSourceCodeType(typeof(byte)),
							PrimitiveType.Nat16 => new CsharpTypeSourceCodeType(typeof(ushort)),
							PrimitiveType.Nat32 => new CsharpTypeSourceCodeType(typeof(uint)),
							PrimitiveType.Nat64 => new CsharpTypeSourceCodeType(typeof(ulong)),
							PrimitiveType.Int => new CsharpTypeSourceCodeType(typeof(UnboundedInt)),
							PrimitiveType.Int8 => new CsharpTypeSourceCodeType(typeof(sbyte)),
							PrimitiveType.Int16 => new CsharpTypeSourceCodeType(typeof(short)),
							PrimitiveType.Int32 => new CsharpTypeSourceCodeType(typeof(int)),
							PrimitiveType.Int64 => new CsharpTypeSourceCodeType(typeof(long)),
							PrimitiveType.Float32 => new CsharpTypeSourceCodeType(typeof(float)),
							PrimitiveType.Float64 => new CsharpTypeSourceCodeType(typeof(double)),
							PrimitiveType.Bool => new CsharpTypeSourceCodeType(typeof(bool)),
							PrimitiveType.Principal => new CsharpTypeSourceCodeType(typeof(Principal)),
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
						return new CsharpTypeSourceCodeType(typeof(List<>), innerType);
					}
				case CandidOptionalType o:
					{
						SourceCodeType innerType = ResolveSourceCodeType(o.Value);
						return new CsharpTypeSourceCodeType(typeof(OptionalValue<>), innerType);
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