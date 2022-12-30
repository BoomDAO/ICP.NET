using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal delegate IObjectMapper ResolveMapper(Dictionary<Type, CandidType> resolvedMappings);

	internal interface IResolvableTypeInfo
	{
		Type ObjType { get; }
		List<Type> Dependencies { get; }
		IObjectMapper Resolve(Dictionary<Type, CandidType> resolvedDependencies);
	}

	internal class ResolvedTypeInfo : IResolvableTypeInfo
	{
		public Type ObjType { get; }
		public List<Type> Dependencies { get; }
		public IObjectMapper Mapper { get; }
		public ResolvedTypeInfo(Type objType, IObjectMapper mapper)
		{
			this.ObjType = objType;
			this.Mapper = mapper;
			this.Dependencies = new List<Type>();
		}

		public IObjectMapper Resolve(Dictionary<Type, CandidType> resolvedDependencies)
		{
			return this.Mapper;
		}
	}

	internal class ComplexTypeInfo : IResolvableTypeInfo
	{
		public Type ObjType { get; }
		public List<Type> Dependencies { get; }
		public ResolveMapper Resolver { get; }

		public ComplexTypeInfo(Type objType, List<Type> dependencies, ResolveMapper resolver)
		{
			this.ObjType = objType;
			this.Dependencies = dependencies;
			this.Resolver = resolver;
		}

		public IObjectMapper Resolve(Dictionary<Type, CandidType> resolvedDependencies)
		{
			return this.Resolver(resolvedDependencies);
		}
	}


	internal static class DefaultMapperFactory
	{
		public static IObjectMapper Build(Type objType)
		{
			Dictionary<Type, CandidType> resolvedDependencies = new();

			return ResolveDependencies(objType, resolvedDependencies);
		}

		private static IObjectMapper ResolveDependencies(Type objType, Dictionary<Type, CandidType> resolvedDependencies)
		{
			IResolvableTypeInfo info = BuildTypeInfo(objType);

			if (info.Dependencies.Any())
			{
				// Make any recursion stop
				resolvedDependencies[objType] = new CandidReferenceType(CandidId.Parse("ref"));

				foreach (Type depType in info.Dependencies)
				{
					if (resolvedDependencies.ContainsKey(depType))
					{
						// Skip already resolved dependencies
						continue;
					}
					ResolveDependencies(depType, resolvedDependencies);
				}
			}

			IObjectMapper objectMapper = info.Resolve(resolvedDependencies);
			resolvedDependencies[objType] = objectMapper.CandidType;
			return objectMapper;
		}

		private static IResolvableTypeInfo BuildTypeInfo(Type objType)
		{
			if (objType == typeof(string))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Text),
					(o, options) => CandidPrimitive.Text((string)o),
					(v, options) => v.AsText()
				);
			}
			if (objType == typeof(byte))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Nat8),
					(o, options) => CandidPrimitive.Nat8((byte)o),
					(v, options) => v.AsNat8()
				);
			}
			if (objType == typeof(ushort))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Nat16),
					(o, options) => CandidPrimitive.Nat16((ushort)o),
					(v, options) => v.AsNat16()
				);
			}
			if (objType == typeof(uint))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Nat32),
					(o, options) => CandidPrimitive.Nat32((uint)o),
					(v, options) => v.AsNat32()
				);
			}
			if (objType == typeof(ulong))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Nat64),
					(o, options) => CandidPrimitive.Nat64((ulong)o),
					(v, options) => v.AsNat64()
				);
			}
			if (objType == typeof(UnboundedUInt))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Nat),
					(o, options) => CandidPrimitive.Nat((UnboundedUInt)o),
					(v, options) => v.AsNat()
				);
			}
			if (objType == typeof(sbyte))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Int8),
					(o, options) => CandidPrimitive.Int8((sbyte)o),
					(v, options) => v.AsInt8()
				);
			}
			if (objType == typeof(short))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Int16),
					(o, options) => CandidPrimitive.Int16((short)o),
					(v, options) => v.AsInt16()
				);
			}
			if (objType == typeof(int))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Int32),
					(o, options) => CandidPrimitive.Int32((int)o),
					(v, options) => v.AsInt32()
				);
			}
			if (objType == typeof(long))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Int64),
					(o, options) => CandidPrimitive.Int64((long)o),
					(v, options) => v.AsInt64()
				);
			}
			if (objType == typeof(UnboundedInt))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Int),
					(o, options) => CandidPrimitive.Int((UnboundedInt)o),
					(v, options) => v.AsInt()
				);
			}
			if (objType == typeof(double))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Float64),
					(o, options) => CandidPrimitive.Float64((double)o),
					(v, options) => v.AsFloat64()
				);
			}
			if (objType == typeof(decimal))
			{
				// TODO is this the best way to convert a decimal?
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Float64),
					(o, options) => CandidPrimitive.Float64((double)o),
					(v, options) => (decimal)v.AsFloat64()
				);
			}
			if (objType == typeof(Principal))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Principal),
					(o, options) => CandidPrimitive.Principal((Principal)o),
					(v, options) => v.AsPrincipal()
				);
			}
			if (objType.IsArray)
			{
				Type innerType = objType.GetElementType();
				return BuildVector(
					objType,
					innerType,
					(o, options) => ((IList)o).Cast<object>(),
					(v, options) =>
					{
						object[] objArray = v.ToArray();
						Array array = Array.CreateInstance(innerType, objArray.Length);
						for (int i = 0; i < objArray.Length; i++)
						{
							array.SetValue(objArray[i], i);
						}
						return array;
					}
				);
			}
			if (typeof(ICandidVariantValue).IsAssignableFrom(objType))
			{
				return BuildVariant(objType);
			}
			// Generics
			if (objType.IsGenericType)
			{
				Type genericTypeDefinition = objType.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(OptionalValue<>))
				{
					return BuildOpt(objType);
				}
				if (genericTypeDefinition == typeof(List<>))
				{
					Type innerType = objType.GenericTypeArguments[0];
					return BuildVector(
						objType,
						innerType,
						(o, options) => ((IList)o).Cast<object>(),
						(v, options) =>
						{
							IList list = (IList)Activator.CreateInstance(objType);
							foreach (object innerValue in v)
							{
								list.Add(innerValue);
							}
							return list;
						}
					);
				}
			}
			// Assume anything else is a record
			return BuildRecord(objType);
		}

		private static IResolvableTypeInfo BuildPrimitive(
			Type objType,
			CandidType type,
			Func<object, CandidConverterOptions, CandidValue> toCandid,
			Func<CandidValue, CandidConverterOptions, object> fromCandid)
		{
			var mapper = new PrimitiveMapper(
				type,
				objType,
				(o, options) =>
				{
					CandidValue value = toCandid(o, options);
					return new CandidTypedValue(value, type);
				},
				fromCandid
			);
			return new ResolvedTypeInfo(objType, mapper);
		}

		private static IResolvableTypeInfo BuildOpt(Type objType)
		{
			Type innerType = objType.GenericTypeArguments[0];

			var dependencies = new List<Type> { innerType };
			return new ComplexTypeInfo(objType, dependencies, (resolvedDependencies) =>
			{
				CandidType innerCandidType = resolvedDependencies[innerType];
				return new OptMapper(innerCandidType, innerType, overrideInnerMapper: null); // TODO overrides?
			});
		}

		private static IResolvableTypeInfo BuildVector(
			Type objType,
			Type innerType,
			Func<object, CandidConverterOptions, IEnumerable<object>> toEnumerableFunc,
			Func<IEnumerable<object>, CandidConverterOptions, object> fromEnumerableFunc)
		{
			return new ComplexTypeInfo(objType, new List<Type> { innerType }, (resolvedMappings) =>
			{
				CandidType innerCandidType = resolvedMappings[innerType];
				var type = new CandidVectorType(innerCandidType);

				return new VectorMapper(
					type,
					objType,
					innerType,
					toEnumerableFunc,
					fromEnumerableFunc,
					null // TODO overrides?
				);
			});
		}

		private static IResolvableTypeInfo BuildVariant(Type objType)
		{
			ICandidVariantValue variant = (ICandidVariantValue)Activator.CreateInstance(objType, nonPublic: true);
			Dictionary<CandidTag, Type?> optionTypes = variant.GetOptions();

			List<Type> dependencies = optionTypes.Values
				.Where(v => v != null)
				.Select(v => v!)
				.ToList();
			Dictionary<CandidTag, (Type Type, IObjectMapper? OverrideMapper)?> options = optionTypes
				.ToDictionary(
					t => t.Key,
					t => t.Value == null ? (ValueTuple<Type, IObjectMapper?>?)null : (t.Value, null)); // TODO overrides?
			return new ComplexTypeInfo(objType, dependencies, (resolvedDependencies) =>
			{
				Dictionary<CandidTag, CandidType> optionCandidTypes = options
					.ToDictionary(
						o => o.Key,
						o =>
						{
							if (o.Value == null)
							{
								return new CandidPrimitiveType(PrimitiveType.Null);
							}
							return o.Value.Value.OverrideMapper?.CandidType ?? resolvedDependencies[o.Value.Value.Type];
						}
					);
				var type = new CandidVariantType(optionCandidTypes);

				return new VariantMapper(type, objType, options);
			});
		}

		private static IResolvableTypeInfo BuildRecord(Type objType)
		{
			List<PropertyInfo> properties = objType
				.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.ToList();
			var propertyMetaDataMap = new Dictionary<CandidTag, (PropertyInfo Property, IObjectMapper? OverrideMapper)>();
			foreach (PropertyInfo property in properties)
			{
				CandidIgnoreAttribute? ignoreAttribute = property.GetCustomAttribute<CandidIgnoreAttribute>();
				if (ignoreAttribute != null)
				{
					// Ignore property
					continue;
				}
				CandidNameAttribute? propertyAttribute = property.GetCustomAttribute<CandidNameAttribute>();
				string propertyName;
				if (propertyAttribute != null)
				{
					propertyName = propertyAttribute.Name;
				}
				else
				{
					propertyName = property.Name;
				}
				CandidTag tag = CandidTag.FromName(propertyName);
				CustomMapperAttribute? customMapperAttribute = property.GetCustomAttribute<CustomMapperAttribute>();

				propertyMetaDataMap.Add(tag, (property, customMapperAttribute?.Mapper));
			}
			List<Type> dependencies = propertyMetaDataMap
				.Where(p => p.Value.OverrideMapper == null) // Only resolve the ones that need to
				.Select(p => p.Value.Property.PropertyType)
				.ToList();
			return new ComplexTypeInfo(objType, dependencies, (resolvedMappings) =>
			{
				Dictionary<CandidTag, CandidType> fieldTypes = propertyMetaDataMap
					.ToDictionary(
						p => p.Key,
						p =>
						{
							if (p.Value.OverrideMapper != null)
							{
								return p.Value.OverrideMapper!.CandidType;
							}
							return resolvedMappings[p.Value.Property.PropertyType];
						}
					);
				CandidRecordType type = new CandidRecordType(fieldTypes);

				return new RecordMapper(type, objType, propertyMetaDataMap);
			});
		}


	}

}
