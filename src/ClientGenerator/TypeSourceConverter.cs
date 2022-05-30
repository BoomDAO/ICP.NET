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
		public List<DeclaredTypeSourceDescriptor> Types { get; }
		public Dictionary<string, string> Aliases { get; }
		public ServiceSourceInfo(string name, ServiceSourceDescriptor service, List<DeclaredTypeSourceDescriptor> types, Dictionary<string, string> aliases)
		{
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
			this.Service = service ?? throw new ArgumentNullException(nameof(service));
			this.Types = types ?? throw new ArgumentNullException(nameof(types));
			this.Aliases = aliases ?? throw new ArgumentNullException(nameof(aliases));
		}
	}
	internal class TypeSourceConverter
	{
		public static ServiceSourceInfo ConvertService(string serviceName, CandidServiceFile file)
		{
			Dictionary<CandidId, CandidType> declaredTypes = file.DeclaredTypes.ToDictionary(t => t.Key, t => t.Value);
			
			if (file.ServiceReferenceId != null)
			{
				// If there is a reference to the service type. Remove it to not process it as a type
				declaredTypes.Remove(file.ServiceReferenceId); 
			}


			Dictionary<CandidId, TypeInfo> declaredFullTypeNames = declaredTypes
				.ToDictionary(t => t.Key, t =>
				{
					string name = StringUtil.ToPascalCase(t.Key.ToString());
					string? @namespace = t.Value switch
					{
						CandidRecordType r => "EdjCase.ICP.Clients.Models",
						CandidVariantType v => "EdjCase.ICP.Clients.Models",
						CandidServiceType s => "EdjCase.ICP.Clients",
						CandidPrimitiveType p => null,
						CandidReferenceType p => null,
						_ => throw new NotImplementedException(),
					};
					return new TypeInfo(name, @namespace);
				});

			List<DeclaredTypeSourceDescriptor> resolvedTypes = new();
			Dictionary<string, string> aliases = new();

			foreach ((CandidId id, CandidType type) in declaredTypes)
			{
				TypeInfo typeName = declaredFullTypeNames[id];
				switch (type)
				{
					case CandidPrimitiveType p:
						{
							// Primitive alias declaration
							// ex: type A = nat8
							AddAlias("Primitive", typeName.Name, p);
							break;
						}
					case CandidReferenceType r:
						{
							// Reference alias declaration
							// ex: type A = B
							AddAlias("Reference", typeName.Name, r);
							break;
						}
					case CandidVectorType v:
						{
							// Vector alias declaration
							// ex: type A = vec nat8
							AddAlias("Item", typeName.Name, v);
							break;
						}
					case CandidOptionalType o:
						{
							// Opt alias declaration
							// ex: type A = opt nat8
							AddAlias("Value", typeName.Name, o);
							break;
						}
					default:
						{
							// Normal type declaration
							// ex: type A = record {}
							if (typeName.Namespace == null)
							{
								// TODO
								throw new Exception();
							}
							TypeSourceDescriptor resolvedType = type switch
							{
								CandidRecordType r => ResolveRecord(typeName.Name, r, declaredFullTypeNames),
								CandidVariantType v => ResolveVariant(typeName.Name, v, declaredFullTypeNames),
								CandidServiceType s => ResolveService(typeName.Name, s, declaredFullTypeNames),
								_ => throw new NotImplementedException()
							};
							resolvedTypes.Add(new DeclaredTypeSourceDescriptor(typeName.Namespace, resolvedType));
							break;
						}
				}
			}

			ServiceSourceDescriptor serviceSourceDesc = ResolveService(serviceName.ToString(), file.Service, declaredFullTypeNames);


			return new ServiceSourceInfo(serviceName, serviceSourceDesc, resolvedTypes, aliases);

			void AddAlias(string variableName, string typeName, CandidType type)
			{
				TypeInfo? innerFullTypeName = ResolveInnerTypeName(variableName, type, declaredFullTypeNames, out TypeSourceDescriptor? subTypeToCreate);
				if (innerFullTypeName == null)
				{
					// TOOD
					throw new Exception();
				}
				if (subTypeToCreate != null)
				{
					// TOOD
					throw new Exception();
				}
				aliases.Add(typeName, innerFullTypeName.FullTypeName);
			}
		}




		private static RecordSourceDescriptor ResolveRecord(string name, CandidRecordType type, Dictionary<CandidId, TypeInfo> declaredFullTypeNames)
		{
			var subTypesToCreate = new List<TypeSourceDescriptor>();
			List<(string Name, string FullTypeName)> resolvedFields = type.Fields
				.Select(f =>
				{
					string unmodifiedFieldName = f.Key.Name ?? "F" + f.Key.Id;
					string fieldName = StringUtil.ToPascalCase(unmodifiedFieldName);
					if (fieldName == name)
					{
						// TODO how to handle property and class name collision?
						fieldName += "_";
					}
					TypeInfo? typeName = ResolveInnerTypeName(fieldName + "Info", f.Value, declaredFullTypeNames, out TypeSourceDescriptor? subTypeToCreate);
					if (subTypeToCreate != null)
					{
						subTypesToCreate.Add(subTypeToCreate);
					}
					return (fieldName, typeName);
				})
				.Where(f => f.typeName != null)
				.Select(f => (f.fieldName, f.typeName!.FullTypeName))
				.ToList();
			return new RecordSourceDescriptor(name, resolvedFields, subTypesToCreate);
		}


		private static VariantSourceDescriptor ResolveVariant(string name, CandidVariantType type, Dictionary<CandidId, TypeInfo> declaredFullTypeNames)
		{
			var subTypesToCreate = new List<TypeSourceDescriptor>();
			List<(string Name, string? InfoFullTypeName)> resolvedOptions = type.Fields
				.Select(f =>
				{
					string unmodifiedFieldName = f.Key.Name ?? "O" + f.Key.Id;
					string fieldName = StringUtil.ToPascalCase(unmodifiedFieldName);
					TypeInfo? typeName = ResolveInnerTypeName(fieldName + "Info", f.Value, declaredFullTypeNames, out TypeSourceDescriptor? subTypeToCreate);
					if (subTypeToCreate != null)
					{
						subTypesToCreate.Add(subTypeToCreate);
					}
					return (fieldName, typeName?.FullTypeName);
				})
				.ToList();
			return new VariantSourceDescriptor(name, resolvedOptions, subTypesToCreate);
		}

		private static ServiceSourceDescriptor ResolveService(string name, CandidServiceType type, Dictionary<CandidId, TypeInfo> declaredFullTypeNames)
		{
			var subTypesToCreate = new List<TypeSourceDescriptor>();
			List<ServiceSourceDescriptor.Method> resolvedMethods = type.Methods
				.Select(f =>
				{
					string unmodifiedName = f.Key.ToString();
					string funcName = StringUtil.ToPascalCase(unmodifiedName);
					(ServiceSourceDescriptor.Method method, List<TypeSourceDescriptor> innerSubTypesToCreate) = ResolveFunc(funcName, unmodifiedName, f.Value, declaredFullTypeNames);
					subTypesToCreate.AddRange(innerSubTypesToCreate);
					return method;
				})
				.ToList();
			return new ServiceSourceDescriptor(name, resolvedMethods, subTypesToCreate);
		}
		private static (ServiceSourceDescriptor.Method Method, List<TypeSourceDescriptor> SubTypesToCreate) ResolveFunc(string name, string unmodifiedName, CandidFuncType type, Dictionary<CandidId, TypeInfo> declaredFullTypeNames)
		{
			var subTypesToCreate = new List<TypeSourceDescriptor>();
			List<ServiceSourceDescriptor.Method.ParameterInfo> resolvedParameters = type.ArgTypes
				.Select((f, i) =>
				{
					string argName = $"arg{i}"; // TODO better naming
					TypeInfo? typeName = ResolveInnerTypeName(argName, f, declaredFullTypeNames, out TypeSourceDescriptor? subTypeToCreate);
					if (subTypeToCreate != null)
					{
						subTypesToCreate.Add(subTypeToCreate);
					}
					return new ServiceSourceDescriptor.Method.ParameterInfo(argName, typeName);
				})
				.ToList();
			List<ServiceSourceDescriptor.Method.ParameterInfo> resolvedReturnParameters = type.ReturnTypes
				.Select((f, i) =>
				{
					string argName = $"R{i}"; // TODO better naming
					TypeInfo? typeName = ResolveInnerTypeName(argName, f, declaredFullTypeNames, out TypeSourceDescriptor? subTypeToCreate);
					if (subTypeToCreate != null)
					{
						subTypesToCreate.Add(subTypeToCreate);
					}
					return new ServiceSourceDescriptor.Method.ParameterInfo(argName, typeName);
				})
				.ToList();
			bool isFireAndForget = type.Modes.Contains(FuncMode.Oneway);
			bool isQuery = type.Modes.Contains(FuncMode.Query);
			var method = new ServiceSourceDescriptor.Method(name, unmodifiedName, isFireAndForget, isQuery, resolvedParameters, resolvedReturnParameters);
			return (method, subTypesToCreate);
		}

		private static TypeInfo? ResolvePrimitive(PrimitiveType type)
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
			return new TypeInfo(t.Name, t.Namespace);
		}


		private static TypeInfo? ResolveInnerTypeName(
			string variableName,
			CandidType type,
			Dictionary<CandidId, TypeInfo> declaredFullTypeNames,
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
						TypeInfo? innerTypeName = ResolveInnerTypeName(variableName, v.InnerType, declaredFullTypeNames, out subTypeToCreate);
						if (innerTypeName == null)
						{
							// TODO
							throw new Exception();
						}
						return new TypeInfo($"List<{innerTypeName.FullTypeName}>", "System.Collections.Generic");
					}
				case CandidOptionalType o:
					{

						TypeInfo? innerTypeName = ResolveInnerTypeName(variableName, o.Value, declaredFullTypeNames, out subTypeToCreate);
						if (innerTypeName == null)
						{
							// TODO
							throw new Exception();
						}
						return new TypeInfo($"{innerTypeName.FullTypeName}?", null);
					}
				case CandidRecordType r:
					{
						RecordSourceDescriptor record = ResolveRecord(variableName, r, declaredFullTypeNames);
						subTypeToCreate = record;
						return new TypeInfo(record.Name, null);
					}
				case CandidVariantType v:
					{
						VariantSourceDescriptor variant = ResolveVariant(variableName, v, declaredFullTypeNames);
						subTypeToCreate = variant;
						return new TypeInfo(variant.Name, null);
					}
				case CandidServiceType s:
					{
						ServiceSourceDescriptor service = ResolveService(variableName, s, declaredFullTypeNames);
						subTypeToCreate = service;
						return new TypeInfo(service.Name, null);
					}
				default:
					throw new NotImplementedException();
			}
		}


		public class TypeInfo
		{
			public string Name { get; }
			public string? Namespace { get; }

			public string FullTypeName => this.Namespace == null ? this.Name : $"{this.Namespace}.{this.Name}";

			public TypeInfo(string name, string? @namespace)
			{
				this.Name = name ?? throw new ArgumentNullException(nameof(name));
				this.Namespace = @namespace;
			}
		}

	}
}