using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using ICP.ClientGenerator;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.ClientGenerator
{
	internal class ServiceSourceInfo
	{
		public string Name { get; }
		public ServiceSourceDescriptor Service { get; }
		public List<(TypeName Name, TypeSourceDescriptor Desc)> Types { get; }
		public List<(string Alias, TypeName Type)> Aliases { get; }
		public ServiceSourceInfo(string name, ServiceSourceDescriptor service, List<(TypeName Name, TypeSourceDescriptor Desc)> types, List<(string Alias, TypeName Type)> aliases)
		{
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
			this.Service = service ?? throw new ArgumentNullException(nameof(service));
			this.Types = types ?? throw new ArgumentNullException(nameof(types));
			this.Aliases = aliases ?? throw new ArgumentNullException(nameof(aliases));
		}
	}
	internal class TypeSourceConverter
	{
		public static ServiceSourceInfo ConvertService(string serviceName, string baseNamespace, CandidServiceDescription service)
		{
			// Mapping of A => Type
			// where candid is: type A = Type;
			Dictionary<CandidId, CandidType> declaredTypes = service.DeclaredTypes.ToDictionary(t => t.Key, t => t.Value);

			if (service.ServiceReferenceId != null)
			{
				// If there is a reference to the service type. Remove it to not process it as a type
				declaredTypes.Remove(service.ServiceReferenceId);
			}


			Dictionary<TypeName, TypeSourceDescriptor> resolvedDeclaredTypes = new();

			TypeName? GetType(TypeSourceDescriptor desc, string parentName)
			{
				// Id is null if empty, reserved or null
				TypeName? type;
				switch (desc)
				{
					case PrimitiveSourceDescriptor p:
						type = p.Type;
						break;
					case VectorSourceDescriptor v:
						type = new TypeName("List", "System.Collections.Generic", v.InnerType);
						break;
					case OptionalSourceDescriptor o:
						type = new TypeName("OptionalValue", "EdjCase.ICP.Candid.Models", o.InnerType);
						break;
					case VariantSourceDescriptor va:
						type = new TypeName(parentName, null);
						break;
					case RecordSourceDescriptor re:
						type = new TypeName(parentName, null);
						break;
					case FuncSourceDescriptor f:
						// Func alias declaration
						// ex: type A = func
						// using A = Edjcase.ICP.Candid.Models.Values.CandidFunc;
						type = TypeName.FromType<CandidFunc>();
						break;
					case ReferenceSourceDescriptor re:
						type = re.Reference; // TODO
						break;
					default:
						throw new NotImplementedException();
				}
				return type;
			}
			TypeName? AddType(TypeSourceDescriptor desc, string parentName)
			{
				TypeName? type = GetType(desc, parentName);
				if (type != null)
				{
					resolvedDeclaredTypes.Add(type, desc);
				}
				return type;
			}

			List<(TypeName Name, TypeSourceDescriptor Desc)> customTypes = new();
			List<(string Alias, TypeName Type)> aliases = new();
			foreach ((CandidId id, CandidType type) in declaredTypes)
			{
				string name = id.ToString();
				TypeSourceDescriptor desc = ResolveDescriptor(type, baseNamespace, name, AddType);

				TypeName? typeName = GetType(desc, name);
				if (typeName == null)
				{
					// Skip empty, reserved and null
					continue;
				}
				switch (desc)
				{
					case FuncSourceDescriptor f:
					case PrimitiveSourceDescriptor p:
					case ReferenceSourceDescriptor r:
					case VectorSourceDescriptor v:
					case OptionalSourceDescriptor o:
						aliases.Add((name, typeName));
						break;
					default:
						customTypes.Add((typeName, desc));
						break;
				}
			}


			ServiceSourceDescriptor serviceSourceDesc = ResolveService(service.Service, baseNamespace, serviceName, AddType);


			return new ServiceSourceInfo(serviceName, serviceSourceDesc, customTypes, aliases);
		}

		private static RecordSourceDescriptor ResolveRecord(
			CandidRecordType type,
			string baseNamespace,
			string parentName,
			Func<TypeSourceDescriptor, string, TypeName?> addTypeFunc)
		{
			List<(ValueName Name, TypeName Type)> resolvedFields = type.Fields
				.Select(f =>
				{
					ValueName fieldName = ValueName.Default(f.Key); // Get field name or id
					if (string.Equals(parentName, fieldName.PropertyName, StringComparison.OrdinalIgnoreCase))
					{
						// Cant match the class name
						fieldName = ValueName.Default(fieldName.PropertyName + "Item");
					}
					TypeSourceDescriptor desc = ResolveDescriptor(f.Value, baseNamespace, parentName, addTypeFunc);
					TypeName? fieldTypeId = addTypeFunc(desc, parentName);
					return (fieldName, fieldTypeId);
				})
				.Where(f => f.fieldTypeId != null)
				.Select(f => (f.fieldName, f.fieldTypeId!))
				.ToList();
			return new RecordSourceDescriptor(resolvedFields);
		}


		private static VariantSourceDescriptor ResolveVariant(
			CandidVariantType type,
			string baseNamespace,
			string parentName,
			Func<TypeSourceDescriptor, string, TypeName?> addTypeFunc)
		{
			List<(ValueName Name, TypeName? Type)> resolvedOptions = type.Fields
				.Select(f =>
				{
					ValueName fieldName = ValueName.Default(f.Key);
					TypeSourceDescriptor desc = ResolveDescriptor(f.Value, baseNamespace, parentName, addTypeFunc);
					TypeName? fieldTypeId = addTypeFunc(desc, parentName);
					return (fieldName, fieldTypeId);
				})
				.ToList();
			return new VariantSourceDescriptor(resolvedOptions);
		}

		private static ServiceSourceDescriptor ResolveService(
			CandidServiceType type,
			string baseNamespace,
			string parentName,
			Func<TypeSourceDescriptor, string, TypeName?> addTypeFunc)
		{
			List<(string Name, TypeName FuncType, FuncSourceDescriptor FuncDesc)> resolvedMethods = type.Methods
				.Select(f =>
				{
					FuncSourceDescriptor func = ResolveFunc(f.Value, baseNamespace, parentName, addTypeFunc);
					string methodName = f.Key.ToString();
					TypeName funcType = addTypeFunc(func, methodName)!;
					return (methodName, funcType, func);
				})
				.ToList();
			return new ServiceSourceDescriptor(resolvedMethods);
		}
		private static FuncSourceDescriptor ResolveFunc(
			CandidFuncType type,
			string baseNamespace,
			string parentName,
			Func<TypeSourceDescriptor, string, TypeName?> addTypeFunc)
		{
			var subTypesToCreate = new List<TypeSourceDescriptor>();

			FuncSourceDescriptor.ParameterInfo BuildParam(ValueName argName, CandidType paramType)
			{
				bool isOpt = false;
				if (paramType is CandidOptionalType o)
				{
					paramType = o.Value;
					isOpt = true;
				}
				TypeSourceDescriptor desc = ResolveDescriptor(paramType, baseNamespace, parentName, addTypeFunc);
				TypeName? typeId = addTypeFunc(desc, parentName);
				return new FuncSourceDescriptor.ParameterInfo(argName, typeId, isOpt);
			}

			List<FuncSourceDescriptor.ParameterInfo> resolvedParameters = type.ArgTypes
				.Select((f, i) =>
				{
					ValueName argName = ValueName.Default(f.Name == null ? $"arg{i}" : f.Name.Value);
					return BuildParam(argName, f.Type);
				})
				.ToList();
			List<FuncSourceDescriptor.ParameterInfo> resolvedReturnParameters = type.ReturnTypes
				.Select((f, i) =>
				{
					ValueName argName = ValueName.Default(f.Name == null ? $"r{i}" : f.Name.Value);
					return BuildParam(argName, f.Type);
				})
				.ToList();
			bool isFireAndForget = type.Modes.Contains(FuncMode.Oneway);
			bool isQuery = type.Modes.Contains(FuncMode.Query);
			return new FuncSourceDescriptor(isFireAndForget, isQuery, resolvedParameters, resolvedReturnParameters);
		}

		private static PrimitiveSourceDescriptor ResolvePrimitive(PrimitiveType type)
		{
			TypeName? t = type switch
			{
				PrimitiveType.Text => TypeName.FromType<string>(),
				PrimitiveType.Nat => TypeName.FromType<UnboundedUInt>(),
				PrimitiveType.Nat8 => TypeName.FromType<byte>(),
				PrimitiveType.Nat16 => TypeName.FromType<ushort>(),
				PrimitiveType.Nat32 => TypeName.FromType<uint>(),
				PrimitiveType.Nat64 => TypeName.FromType<ulong>(),
				PrimitiveType.Int => TypeName.FromType<UnboundedInt>(),
				PrimitiveType.Int8 => TypeName.FromType<sbyte>(),
				PrimitiveType.Int16 => TypeName.FromType<short>(),
				PrimitiveType.Int32 => TypeName.FromType<int>(),
				PrimitiveType.Int64 => TypeName.FromType<long>(),
				PrimitiveType.Float32 => TypeName.FromType<float>(),
				PrimitiveType.Float64 => TypeName.FromType<double>(),
				PrimitiveType.Bool => TypeName.FromType<bool>(),
				PrimitiveType.Principal => TypeName.FromType<Principal>(),
				PrimitiveType.Reserved => null,
				PrimitiveType.Empty => null,
				PrimitiveType.Null => null,
				_ => throw new NotImplementedException(),
			};
			return new PrimitiveSourceDescriptor(t);
		}



		private static TypeSourceDescriptor ResolveDescriptor(
			CandidType type,
			string baseNamespace,
			string parentName,
			Func<TypeSourceDescriptor, string, TypeName?> addTypeFunc)
		{
			switch (type)
			{
				case CandidReferenceType r:
					{
						TypeName typeName = new(r.Id.Value, null); // TODO?
						return new ReferenceSourceDescriptor(typeName);
					}
				case CandidPrimitiveType p:
					{
						return ResolvePrimitive(p.PrimitiveType);
					}
				case CandidVectorType v:
					{
						TypeSourceDescriptor desc = ResolveDescriptor(v.InnerType, baseNamespace, parentName, addTypeFunc);
						TypeName? innerId = addTypeFunc(desc, parentName);
						if (innerId == null)
						{
							// TODO?
							throw new NotImplementedException("Empty, reserved or null in a vector");
						}
						return new VectorSourceDescriptor(innerId);
					}
				case CandidOptionalType o:
					{
						TypeSourceDescriptor desc = ResolveDescriptor(o.Value, baseNamespace, parentName, addTypeFunc);
						TypeName? innerId = addTypeFunc(desc, parentName);
						if (innerId == null)
						{
							// TODO?
							throw new NotImplementedException("Empty, reserved or null in a vector");
						}
						return new OptionalSourceDescriptor(innerId);
					}
				case CandidRecordType r:
					{
						return ResolveRecord(r, baseNamespace, parentName, addTypeFunc);
					}
				case CandidVariantType v:
					{
						return ResolveVariant(v, baseNamespace, parentName, addTypeFunc);
					}
				case CandidServiceType s:
					{
						return ResolveService(s, baseNamespace, parentName, addTypeFunc);
					}
				case CandidFuncType f:
					{
						return ResolveFunc(f, baseNamespace, parentName, addTypeFunc);
					}
				default:
					throw new NotImplementedException();
			}
		}

	}
}