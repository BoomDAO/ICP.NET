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
		public List<(CandidId Id, ITypeSourceDescriptor Type)> Types { get; }
		public List<(CandidId Alias, TypeName Type)> Aliases { get; }
		public ServiceSourceInfo(
			string name,
			ServiceSourceDescriptor service,
			List<(CandidId Name, ITypeSourceDescriptor Info)> types,
			List<(CandidId Alias, TypeName Type)> aliases)
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
			Dictionary<CandidId, CandidType> declaredTypes = service.DeclaredTypes
				.ToDictionary(t => t.Key, t => t.Value);

			if (service.ServiceReferenceId != null)
			{
				// If there is a reference to the service type. Remove it to not process it as a type
				declaredTypes.Remove(service.ServiceReferenceId);
			}



			TypeHelper typeHelper = TypeHelper.Load(declaredTypes);
			foreach ((CandidId id, CandidType type) in declaredTypes)
			{
				var a = typeHelper.Resolve(type);
				switch (type)
				{
					case CandidFuncType f:
						{
							// Func alias declaration
							// ex: type A = func
							// using A = Edjcase.ICP.Candid.Models.Values.CandidFunc;
							TypeName typeName = TypeName.FromType<CandidFunc>();
							aliases.Add((id, typeName));
							break;
						}
					case CandidPrimitiveType p:
						{
							TypeName typeName = p.Type;
							aliases.Add((id, typeName));
							break;
						}
					case CandidReferenceType r:
						{
							TypeName typeName = new TypeName(r.Id.ToString(), $"{baseNamespace}.Models");
							aliases.Add((id, typeName));
							break;
						}
					case CandidVectorType v:
						{
							TypeName innerType = v.InnerType;
							TypeName typeName = new TypeName("List", "System.Collections.Generic", innerType);
							break;
						}
					case CandidOptionalType o:
						{
							TypeName typeName = new TypeName("OptionalValue", "EdjCase.ICP.Candid.Models", o.InnerType);
							aliases.Add((id, typeName));
							break;
						}
						break;
					default:
						{
							types.Add((id, desc));
							break;
						}
				}
			}



			ServiceSourceDescriptor serviceSourceDesc = helper.ResolveService(service.Service);


			return new ServiceSourceInfo(serviceName, serviceSourceDesc, types, aliases);
		}





		private class TypeHelper
		{
			public class TypeInfo
			{
				public CandidId? Id { get; }
				public CandidType Type { get; }

				public TypeInfo(CandidType type)
				{
					this.Type = type;
				}
			}

			private Dictionary<CandidId, TypeInfo> types = new();
			public TypeHelper(Dictionary<CandidId, TypeInfo> types)
			{
				this.types = types;
			}

			public static TypeHelper Load(Dictionary<CandidId, CandidType> declaredTypes)
			{
				List<TypeInfo> types = new();
				foreach ((CandidId id, CandidType type) in declaredTypes)
				{
					L(type);
				}
			}

			private static void L(CandidId id, CandidType type, Dictionary<CandidId, TypeInfo> types)
			{
				var typeInfo = new TypeInfo(type);
				t.Add(id, typeInfo);

			}

			private static void M(Dictionary<CandidId, TypeInfo> types)
			{
				switch (type)
				{
					case CandidReferenceType r:
						{
							types.Add()
							return new ReferenceSourceDescriptor(r.Id);
						}
					case CandidPrimitiveType p:
						{
							return ResolvePrimitive(p.PrimitiveType);
						}
					case CandidVectorType v:
						{
							ITypeSourceDescriptor desc = this.Resolve(v.InnerType);
							return new VectorSourceDescriptor(desc);
						}
					case CandidOptionalType o:
						{
							ITypeSourceDescriptor desc = this.Resolve(o.Value);
							return new OptionalSourceDescriptor(desc);
						}
					case CandidRecordType r:
						{
							return ResolveRecord(r);
						}
					case CandidVariantType v:
						{
							return ResolveVariant(v);
						}
					case CandidServiceType s:
						{
							return ResolveService(s);
						}
					case CandidFuncType f:
						{
							return ResolveFunc(f);
						}
					default:
						throw new NotImplementedException();
				}
			}

			public ITypeSourceDescriptor Resolve(CandidType type)
			{
				switch (type)
				{
					case CandidReferenceType r:
						{
							return new ReferenceSourceDescriptor(r.Id);
						}
					case CandidPrimitiveType p:
						{
							return ResolvePrimitive(p.PrimitiveType);
						}
					case CandidVectorType v:
						{
							ITypeSourceDescriptor desc = this.Resolve(v.InnerType);
							return new VectorSourceDescriptor(desc);
						}
					case CandidOptionalType o:
						{
							ITypeSourceDescriptor desc = this.Resolve(o.Value);
							return new OptionalSourceDescriptor(desc);
						}
					case CandidRecordType r:
						{
							return ResolveRecord(r);
						}
					case CandidVariantType v:
						{
							return ResolveVariant(v);
						}
					case CandidServiceType s:
						{
							return ResolveService(s);
						}
					case CandidFuncType f:
						{
							return ResolveFunc(f);
						}
					default:
						throw new NotImplementedException();
				}
			}

			private RecordSourceDescriptor ResolveRecord(CandidRecordType type)
			{
				List<(ValueName Name, ITypeSourceDescriptor Type)> resolvedFields = type.Fields
					.Select(f =>
					{
						ValueName fieldName = ValueName.Default(f.Key); // Get field name or id

						ITypeSourceDescriptor desc = this.Resolve(f.Value);
						return (fieldName, desc);
					})
					.Select(f => (f.fieldName, f.desc))
					.ToList();
				return new RecordSourceDescriptor(resolvedFields);
			}


			private VariantSourceDescriptor ResolveVariant(CandidVariantType type)
			{
				List<(ValueName Name, ITypeSourceDescriptor Type)> resolvedOptions = type.Fields
					.Select(f =>
					{
						ValueName fieldName = ValueName.Default(f.Key);
						ITypeSourceDescriptor desc = this.Resolve(f.Value);
						return (fieldName, desc);
					})
					.ToList();
				return new VariantSourceDescriptor(resolvedOptions);
			}

			private ServiceSourceDescriptor ResolveService(CandidServiceType type)
			{
				List<(string Name, FuncSourceDescriptor FuncDesc)> resolvedMethods = type.Methods
					.Select(f =>
					{
						FuncSourceDescriptor func = this.ResolveFunc(f.Value);
						string methodName = f.Key.ToString();
						return (methodName, func);
					})
					.ToList();
				return new ServiceSourceDescriptor(resolvedMethods);
			}
			private FuncSourceDescriptor ResolveFunc(CandidFuncType type)
			{
				var subTypesToCreate = new List<ITypeSourceDescriptor>();

				FuncSourceDescriptor.ParameterInfo BuildParam(ValueName argName, CandidType paramType)
				{
					if (paramType is CandidOptionalType o)
					{
						paramType = o.Value;
					}
					ITypeSourceDescriptor desc = this.Resolve(paramType);
					return new FuncSourceDescriptor.ParameterInfo(argName, desc);
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
		}
	}
}