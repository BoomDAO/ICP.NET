using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using ICP.ClientGenerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.ClientGenerator
{
	internal class ServiceSourceInfo
	{
		public string Name { get; }
		public ServiceSourceDescriptor Service { get; }
		public List<TypeSourceDescriptor> Types { get; }
		public Dictionary<string, string> Aliases { get; }
		public ServiceSourceInfo(string name, ServiceSourceDescriptor service, List<TypeSourceDescriptor> types, Dictionary<string, string> aliases)
		{
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
			this.Service = service ?? throw new ArgumentNullException(nameof(service));
			this.Types = types ?? throw new ArgumentNullException(nameof(types));
			this.Aliases = aliases ?? throw new ArgumentNullException(nameof(aliases));
		}
	}
	internal class TypeSourceConverter
	{
		public static ServiceSourceInfo ConvertService(string serviceName, CandidServiceDescription service)
		{
			Dictionary<CandidId, CandidType> declaredTypes = service.DeclaredTypes.ToDictionary(t => t.Key, t => t.Value);
			
			if (service.ServiceReferenceId != null)
			{
				// If there is a reference to the service type. Remove it to not process it as a type
				declaredTypes.Remove(service.ServiceReferenceId); 
			}


			Dictionary<CandidId, string> declaredTypeNames = declaredTypes
				.ToDictionary(t => t.Key, t => t.Key.ToString());

			List<TypeSourceDescriptor> resolvedTypes = new();
			Dictionary<string, string> aliases = new();

			foreach ((CandidId id, CandidType type) in declaredTypes)
			{
				string typeName = declaredTypeNames[id];
				switch (type)
				{
					case CandidPrimitiveType p:
						{
							// Primitive alias declaration
							// ex: type A = nat8
							AddAlias("Primitive", typeName, p);
							break;
						}
					case CandidReferenceType r:
						{
							// Reference alias declaration
							// ex: type A = B
							AddAlias("Reference", typeName, r);
							break;
						}
					case CandidVectorType v:
						{
							// Vector alias declaration
							// ex: type A = vec nat8
							AddAlias("Item", typeName, v);
							break;
						}
					case CandidOptionalType o:
						{
							// Opt alias declaration
							// ex: type A = opt nat8
							AddAlias("Value", typeName, o);
							break;
						}
					case CandidFuncType f:
						{
							// Func alias declaration
							// ex: type A = func
							aliases.Add(typeName, typeof(CandidFunc).FullName!);
							break;
						}
					default:
						{
							TypeSourceDescriptor? resolvedType = type switch
							{
								CandidRecordType r => ResolveRecord(typeName, r, declaredTypeNames),
								CandidVariantType v => ResolveVariant(typeName, v, declaredTypeNames),
								CandidServiceType s => ResolveService(typeName, s, declaredTypeNames),
								_ => throw new NotImplementedException()
							};
							if (resolvedType != null)
							{
								resolvedTypes.Add(resolvedType);
							}
							break;
						}
				}
			}

			ServiceSourceDescriptor serviceSourceDesc = ResolveService(serviceName.ToString(), service.Service, declaredTypeNames);


			return new ServiceSourceInfo(serviceName, serviceSourceDesc, resolvedTypes, aliases);

			void AddAlias(string variableName, string typeName, CandidType type)
			{
				string? innerTypeName = ResolveInnerTypeName(variableName, type, declaredTypeNames, out TypeSourceDescriptor? subTypeToCreate);
				if (innerTypeName == null)
				{
					// TOOD
					throw new Exception();
				}
				if (subTypeToCreate != null)
				{
					// TOOD
					throw new Exception();
				}
				aliases.Add(typeName, innerTypeName);
			}
		}




		private static RecordSourceDescriptor ResolveRecord(string name, CandidRecordType type, Dictionary<CandidId, string> declaredFullTypeNames)
		{
			var subTypesToCreate = new List<TypeSourceDescriptor>();
			List<(string Name, string TypeName)> resolvedFields = type.Fields
				.Select(f =>
				{
					string fieldName = f.Key.Name ?? "F" + f.Key.Id;
					if (fieldName == name)
					{
						// TODO how to handle property and class name collision?
						fieldName += "_";
					}
					string? typeName = ResolveInnerTypeName(fieldName + "Info", f.Value, declaredFullTypeNames, out TypeSourceDescriptor? subTypeToCreate);
					if (subTypeToCreate != null)
					{
						subTypesToCreate.Add(subTypeToCreate);
					}
					return (fieldName, typeName);
				})
				.Where(f => f.typeName != null)
				.Select(f => (f.fieldName, f.typeName!))
				.ToList();
			return new RecordSourceDescriptor(name, resolvedFields, subTypesToCreate);
		}


		private static VariantSourceDescriptor ResolveVariant(string name, CandidVariantType type, Dictionary<CandidId, string> declaredFullTypeNames)
		{
			var subTypesToCreate = new List<TypeSourceDescriptor>();
			List<(string Name, string? InfoTypeName)> resolvedOptions = type.Fields
				.Select(f =>
				{
					string fieldName = f.Key.Name ?? "O" + f.Key.Id;
					string? typeName = ResolveInnerTypeName(fieldName + "Info", f.Value, declaredFullTypeNames, out TypeSourceDescriptor? subTypeToCreate);
					if (subTypeToCreate != null)
					{
						subTypesToCreate.Add(subTypeToCreate);
					}
					return (fieldName, typeName);
				})
				.ToList();
			return new VariantSourceDescriptor(name, resolvedOptions, subTypesToCreate);
		}

		private static ServiceSourceDescriptor ResolveService(string name, CandidServiceType type, Dictionary<CandidId, string> declaredFullTypeNames)
		{
			var subTypesToCreate = new List<TypeSourceDescriptor>();
			Dictionary<string, FuncSourceDescriptor> resolvedMethods = type.Methods
				.ToDictionary(f => f.Key.ToString(), f =>
				{
					FuncSourceDescriptor func = ResolveFunc(f.Key.ToString(), f.Value, declaredFullTypeNames);
					subTypesToCreate.AddRange(func.SubTypesToCreate);
					return func;
				});
			return new ServiceSourceDescriptor(name, resolvedMethods, subTypesToCreate);
		}
		private static FuncSourceDescriptor ResolveFunc(string name, CandidFuncType type, Dictionary<CandidId, string> declaredFullTypeNames)
		{
			var subTypesToCreate = new List<TypeSourceDescriptor>();
			List<FuncSourceDescriptor.ParameterInfo> resolvedParameters = type.ArgTypes
				.Select((f, i) =>
				{
					string argName = f.Name?.Value ?? $"arg{i}"; // TODO better naming
					string? typeName = ResolveInnerTypeName(argName, f.Type, declaredFullTypeNames, out TypeSourceDescriptor? subTypeToCreate);
					if (subTypeToCreate != null)
					{
						subTypesToCreate.Add(subTypeToCreate);
					}
					return new FuncSourceDescriptor.ParameterInfo(argName, typeName);
				})
				.ToList();
			List<FuncSourceDescriptor.ParameterInfo> resolvedReturnParameters = type.ReturnTypes
				.Select((f, i) =>
				{
					string argName = f.Name?.Value ?? $"R{i}"; // TODO better naming
					string? typeName = ResolveInnerTypeName(argName, f.Type, declaredFullTypeNames, out TypeSourceDescriptor? subTypeToCreate);
					if (subTypeToCreate != null)
					{
						subTypesToCreate.Add(subTypeToCreate);
					}
					return new FuncSourceDescriptor.ParameterInfo(argName, typeName);
				})
				.ToList();
			bool isFireAndForget = type.Modes.Contains(FuncMode.Oneway);
			bool isQuery = type.Modes.Contains(FuncMode.Query);
			return  new FuncSourceDescriptor(name, isFireAndForget, isQuery, resolvedParameters, resolvedReturnParameters, subTypesToCreate);
		}

		private static string? ResolvePrimitive(PrimitiveType type)
		{
			Type? t = type switch
			{
				PrimitiveType.Text => typeof(string),
				PrimitiveType.Nat => typeof(UnboundedUInt),
				PrimitiveType.Nat8 => typeof(byte),
				PrimitiveType.Nat16 => typeof(ushort),
				PrimitiveType.Nat32 => typeof(uint),
				PrimitiveType.Nat64 => typeof(ulong),
				PrimitiveType.Int => typeof(UnboundedInt),
				PrimitiveType.Int8 => typeof(sbyte),
				PrimitiveType.Int16 => typeof(short),
				PrimitiveType.Int32 => typeof(int),
				PrimitiveType.Int64 => typeof(long),
				PrimitiveType.Float32 => typeof(float),
				PrimitiveType.Float64 => typeof(double),
				PrimitiveType.Bool => typeof(bool),
				PrimitiveType.Principal => typeof(Principal),
				PrimitiveType.Reserved => null,
				PrimitiveType.Empty => null,
				PrimitiveType.Null => null,
				_ => throw new NotImplementedException(),
			};
			if (t == null)
			{
				return null;
			}
			return t.FullName;
		}


		private static string? ResolveInnerTypeName(
			string variableName,
			CandidType type,
			Dictionary<CandidId, string> declaredFullTypeNames,
			out TypeSourceDescriptor? subTypeToCreate)
		{
			switch (type)
			{
				case CandidReferenceType r:
					subTypeToCreate = null;
					return declaredFullTypeNames[r.Id];
				case CandidPrimitiveType p:
					subTypeToCreate = null;
					return ResolvePrimitive(p.PrimitiveType);
				case CandidVectorType v:
					{
						string? innerTypeName = ResolveInnerTypeName(variableName, v.InnerType, declaredFullTypeNames, out subTypeToCreate);
						if (innerTypeName == null)
						{
							// TODO
							throw new Exception();
						}
						return $"List<{innerTypeName}>";
					}
				case CandidOptionalType o:
					{

						string? innerTypeName = ResolveInnerTypeName(variableName, o.Value, declaredFullTypeNames, out subTypeToCreate);
						if (innerTypeName == null)
						{
							// TODO
							throw new Exception();
						}
						return $"{innerTypeName}?";
					}
				case CandidRecordType r:
					{
						RecordSourceDescriptor record = ResolveRecord(variableName, r, declaredFullTypeNames);
						subTypeToCreate = record;
						return record.Name;
					}
				case CandidVariantType v:
					{
						VariantSourceDescriptor variant = ResolveVariant(variableName, v, declaredFullTypeNames);
						subTypeToCreate = variant;
						return variant.Name;
					}
				case CandidServiceType s:
					{
						ServiceSourceDescriptor service = ResolveService(variableName, s, declaredFullTypeNames);
						subTypeToCreate = service;
						return service.Name;
					}
				case CandidFuncType func:
					{
						subTypeToCreate = null;
						return "(Principal CanisterId, string Method)";
					}
				default:
					throw new NotImplementedException();
			}
		}

	}
}