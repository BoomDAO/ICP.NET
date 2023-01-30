using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
				resolvedDependencies[objType] = new CandidReferenceType(CandidId.Create("ref"));

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
					(o, options) => CandidValue.Text((string)o),
					(v, options) => v.AsText()
				);
			}
			if (objType == typeof(bool))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Bool),
					(o, options) => CandidValue.Bool((bool)o),
					(v, options) => v.AsBool()
				);
			}
			if (objType == typeof(byte))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Nat8),
					(o, options) => CandidValue.Nat8((byte)o),
					(v, options) => v.AsNat8()
				);
			}
			if (objType == typeof(ushort))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Nat16),
					(o, options) => CandidValue.Nat16((ushort)o),
					(v, options) => v.AsNat16()
				);
			}
			if (objType == typeof(uint))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Nat32),
					(o, options) => CandidValue.Nat32((uint)o),
					(v, options) => v.AsNat32()
				);
			}
			if (objType == typeof(ulong))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Nat64),
					(o, options) => CandidValue.Nat64((ulong)o),
					(v, options) => v.AsNat64()
				);
			}
			if (objType == typeof(UnboundedUInt))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Nat),
					(o, options) => CandidValue.Nat((UnboundedUInt)o),
					(v, options) => v.AsNat()
				);
			}
			if (objType == typeof(sbyte))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Int8),
					(o, options) => CandidValue.Int8((sbyte)o),
					(v, options) => v.AsInt8()
				);
			}
			if (objType == typeof(short))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Int16),
					(o, options) => CandidValue.Int16((short)o),
					(v, options) => v.AsInt16()
				);
			}
			if (objType == typeof(int))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Int32),
					(o, options) => CandidValue.Int32((int)o),
					(v, options) => v.AsInt32()
				);
			}
			if (objType == typeof(long))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Int64),
					(o, options) => CandidValue.Int64((long)o),
					(v, options) => v.AsInt64()
				);
			}
			if (objType == typeof(UnboundedInt))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Int),
					(o, options) => CandidValue.Int((UnboundedInt)o),
					(v, options) => v.AsInt()
				);
			}
			if (objType == typeof(float))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Float32),
					(o, options) => CandidValue.Float32((float)o),
					(v, options) => v.AsFloat32()
				);
			}
			if (objType == typeof(double))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Float64),
					(o, options) => CandidValue.Float64((double)o),
					(v, options) => v.AsFloat64()
				);
			}
			if (objType == typeof(decimal))
			{
				// TODO is this the best way to convert a decimal?
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Float64),
					(o, options) => CandidValue.Float64((double)o),
					(v, options) => (decimal)v.AsFloat64()
				);
			}
			if (objType == typeof(Principal))
			{
				return BuildPrimitive(
					objType,
					new CandidPrimitiveType(PrimitiveType.Principal),
					(o, options) => CandidValue.Principal((Principal)o),
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
			// Variants are objects with a [Variant] on the class
			VariantAttribute? variantAttribute = objType.GetCustomAttribute<VariantAttribute>();
			if (variantAttribute != null)
			{
				return BuildVariant(objType, variantAttribute);
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

		private static IResolvableTypeInfo BuildVariant(Type objType, VariantAttribute attribute)
		{
			object? variant = Activator.CreateInstance(objType, nonPublic: true);

			Dictionary<CandidTag, VariantMapper.Option> options = attribute.TagType.GetEnumValues()
				.Cast<Enum>()
				.Select(tagEnum =>
				{
					string tagName = Enum.GetName(attribute.TagType, tagEnum);
					MemberInfo field = attribute.TagType.GetMember(tagName).First();
					var typeAttribute = field.GetCustomAttribute<VariantOptionTypeAttribute>();
					Type? optionType = typeAttribute?.OptionType;
					var nameAttribute = field.GetCustomAttribute<CandidNameAttribute>();
					string name = nameAttribute?.Name ?? tagName;
					VariantMapper.ValueInfo? valueInfo = null;
					if (optionType != null)
					{
						IObjectMapper? overrideMapper = null;// TODO overrides?
						valueInfo = new VariantMapper.ValueInfo(optionType, overrideMapper);
					}
					return (CandidTag.FromName(name), new VariantMapper.Option(tagEnum, valueInfo));
				})
				.ToDictionary(k => k.Item1, k => k.Item2);

			List<Type> dependencies = options.Values
				.Where(v => v.ValueInfo != null)
				.Select(v => v.ValueInfo!.Type)
				.Distinct()
				.ToList();

			List<PropertyInfo> properties = objType
				.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.ToList();

			const string defaultTypePropertyName = "Tag";
			PropertyInfo? typeProperty = properties
				.FirstOrDefault(p => p.GetCustomAttribute<VariantTagPropertyAttribute>() != null)
				?? properties.FirstOrDefault(p => p.Name == defaultTypePropertyName);
			if (typeProperty == null)
			{
				throw new InvalidOperationException($"Variant type '{objType.FullName}' must include a property named '{defaultTypePropertyName}' or have the '{typeof(VariantTagPropertyAttribute).FullName}' attribute on a property");
			}
			const string defaultValuePropertyName = "Value";
			PropertyInfo? valueProperty = properties
				.FirstOrDefault(p => p.GetCustomAttribute<VariantValuePropertyAttribute>() != null)
				?? properties.FirstOrDefault(p => p.Name == defaultValuePropertyName);
			if (valueProperty == null)
			{
				throw new InvalidOperationException($"Variant type '{objType.FullName}' must include a property named '{defaultValuePropertyName}' or have the '{typeof(VariantValuePropertyAttribute).FullName}' attribute on a property");
			}
			return new ComplexTypeInfo(objType, dependencies, (resolvedDependencies) =>
			{
				Dictionary<CandidTag, CandidType> optionCandidTypes = options
					.ToDictionary(
						o => o.Key,
						o =>
						{
							if (o.Value.ValueInfo == null)
							{
								return new CandidPrimitiveType(PrimitiveType.Null);
							}
							return o.Value.ValueInfo.OverrideMapper?.CandidType ?? resolvedDependencies[o.Value.ValueInfo.Type];
						}
					);
				var type = new CandidVariantType(optionCandidTypes);

				return new VariantMapper(type, objType, typeProperty, valueProperty, options);
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
