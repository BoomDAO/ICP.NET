using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal static class CatchAllMapperFactory
	{
		private static ConcurrentDictionary<(Type, CandidConverterOptions), (CatchAllMapper, CandidType)> _cache = new();
		public static CatchAllMapper Build(Type objType, CandidConverterOptions options)
		{
			return BuildInternal(objType, options).Mapper;
		}

		private static (CatchAllMapper Mapper, CandidType Type) BuildInternal(Type objType, CandidConverterOptions options, List<Type> parentTypes)
		{
			return _cache.GetOrAdd((objType, options), (a) => BuildInternalNoCache(a.Item1, a.Item2, parentTypes));
		}

		private static (CatchAllMapper Mapper, CandidType Type) BuildInternalNoCache(Type objType, CandidConverterOptions options, List<Type> parentTypes)
		{
			if (objType == typeof(string))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Text),
					o => CandidPrimitive.Text((string)o),
					v => v.AsText()
				);
			}
			if (objType == typeof(byte))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Nat8),
					o => CandidPrimitive.Nat8((byte)o),
					v => v.AsNat8()
				);
			}
			if (objType == typeof(ushort))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Nat16),
					o => CandidPrimitive.Nat16((ushort)o),
					v => v.AsNat16()
				);
			}
			if (objType == typeof(uint))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Nat32),
					o => CandidPrimitive.Nat32((uint)o),
					v => v.AsNat32()
				);
			}
			if (objType == typeof(ulong))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Nat64),
					o => CandidPrimitive.Nat64((ulong)o),
					v => v.AsNat64()
				);
			}
			if (objType == typeof(UnboundedUInt))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Nat),
					o => CandidPrimitive.Nat((UnboundedUInt)o),
					v => v.AsNat()
				);
			}
			if (objType == typeof(sbyte))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Int8),
					o => CandidPrimitive.Int8((sbyte)o),
					v => v.AsInt8()
				);
			}
			if (objType == typeof(short))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Int16),
					o => CandidPrimitive.Int16((short)o),
					v => v.AsInt16()
				);
			}
			if (objType == typeof(int))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Int32),
					o => CandidPrimitive.Int32((int)o),
					v => v.AsInt32()
				);
			}
			if (objType == typeof(long))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Int64),
					o => CandidPrimitive.Int64((long)o),
					v => v.AsInt64()
				);
			}
			if (objType == typeof(UnboundedInt))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Int),
					o => CandidPrimitive.Int((UnboundedInt)o),
					v => v.AsInt()
				);
			}
			if (objType == typeof(double))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Float64),
					o => CandidPrimitive.Float64((double)o),
					v => v.AsFloat64()
				);
			}
			if (objType == typeof(decimal))
			{
				// TODO is this the best way to convert a decimal?
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Float64),
					o => CandidPrimitive.Float64((double)o),
					v => (decimal)v.AsFloat64()
				);
			}
			if (objType == typeof(Principal))
			{
				return BuildPrimitive(
					new CandidPrimitiveType(PrimitiveType.Principal),
					o => CandidPrimitive.Principal((Principal)o),
					v => v.AsPrincipal()
				);
			}
			if (objType.IsArray)
			{
				Type innerType = objType.GetElementType();
				return BuildVector(
					innerType,
					options,
					o => ((IList)o).Cast<object>(),
					v =>
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
				return BuildVariant(objType, options);
			}
			// Generics
			if (objType.IsGenericType)
			{
				Type genericTypeDefinition = objType.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(OptionalValue<>))
				{
					return BuildOpt(objType, options);
				}
				if (genericTypeDefinition == typeof(List<>))
				{
					Type innerType = objType.GenericTypeArguments[0];
					return BuildVector(
						innerType,
						options,
						o => ((IList)o).Cast<object>(),
						v =>
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
			return BuildRecord(objType, options);
		}

		private static (CatchAllMapper Mapper, CandidType Type) BuildPrimitive(
			CandidType type,
			Func<object, CandidValue> toCandid,
			Func<CandidValue, object> fromCandid)
		{
			var mapper = new CatchAllMapper(
				o =>
				{
					CandidValue value = toCandid(o);
					return new CandidTypedValue(value, type);
				},
				fromCandid
			);
			return (mapper, type);
		}

		private static (CatchAllMapper Mapper, CandidType Type) BuildOpt(Type objType, CandidConverterOptions options)
		{
			Type innerType = objType.GenericTypeArguments[0];
			(CatchAllMapper innerCatchAllMapper, CandidType t) = BuildInternal(innerType, options, parentTypes);
			var type = new CandidOptionalType(t);
			PropertyInfo hasValueProp = objType.GetProperty(nameof(OptionalValue<object>.HasValue));
			MethodInfo valueGetFunc = objType.GetMethod(nameof(OptionalValue<object>.GetValueOrThrow));
			var mapper = new CatchAllMapper(
				o =>
				{
					bool hasValue = (bool)hasValueProp.GetValue(o);
					CandidValue? v;
					if (!hasValue)
					{
						v = null;
					}
					else
					{
						object innerValue = valueGetFunc.Invoke(o, new object[0]);
						v = innerCatchAllMapper.ToCandidFunc(innerValue).Value;
					}
					return new CandidTypedValue(new CandidOptional(v), type);
				},
				v =>
				{
					CandidOptional opt = v.AsOptional();

					object? innerValue;
					bool hasValue;
					if (opt.Value != CandidPrimitive.Null())
					{
						innerValue = innerCatchAllMapper.FromCandidFunc(opt.Value);
						hasValue = true;
					}
					else
					{
						innerValue = null;
						hasValue = false;
					}
					return Activator.CreateInstance(objType, hasValue, innerValue);
				}
			);
			return (mapper, type);
		}

		private static (CatchAllMapper Mapper, CandidType Type) BuildVector(
			Type innerType,
			CandidConverterOptions options,
			Func<object, IEnumerable<object>> toEnumerableFunc,
			Func<IEnumerable<object>, object> fromEnumerableFunc)
		{
			(CatchAllMapper innerCatchAllMapper, CandidType t) = BuildInternal(innerType!, options);
			var type = new CandidVectorType(t);

			var mapper = new CatchAllMapper(
				o =>
				{
					CandidValue[] values = toEnumerableFunc(o)
						.Select(v => innerCatchAllMapper.ToCandidFunc(v).Value)
						.ToArray();
					return new CandidTypedValue(new CandidVector(values), type);
				},
				v =>
				{
					CandidVector vector = v.AsVector();
					IEnumerable<object> objEnumerable = vector.Values
						.Select(v => innerCatchAllMapper.FromCandidFunc(v));

					return fromEnumerableFunc(objEnumerable);
				}
			);
			return (mapper, type);
		}

		private static (CatchAllMapper info, CandidType t) BuildVariant(Type objType, CandidConverterOptions options)
		{
			ICandidVariantValue variant = (ICandidVariantValue)Activator.CreateInstance(objType, nonPublic: true);
			Dictionary<CandidTag, Type?> optionTypes = variant.GetOptions();
			Dictionary<CandidTag, (CatchAllMapper Mapper, CandidType Type)?> optionMappingMap = optionTypes
				.ToDictionary(
					t => t.Key,
					t =>
					{
						if (t.Value == null)
						{
							return (ValueTuple<CatchAllMapper, CandidType>?)null;
						}
						return BuildInternal(t.Value, options);
					}
				);
			Dictionary<CandidTag, CandidType> variantOptions = optionMappingMap
				.ToDictionary(
					t => t.Key,
					t =>
					{
						if (t.Value == null)
						{
							return new CandidPrimitiveType(PrimitiveType.Null);
						}
						return t.Value.Value.Type;
					});
			var type = new CandidVariantType(variantOptions);

			var mapper = new CatchAllMapper(
				o =>
				{
					ICandidVariantValue v = (ICandidVariantValue)o;
					(CandidTag innerTag, object? innerObj) = v.GetValue();

					CatchAllMapper? mapper = optionMappingMap[innerTag]?.Mapper;
					CandidType innerType = variantOptions[innerTag];
					CandidValue innerValue;
					if (mapper != null && innerObj != null)
					{
						innerValue = mapper.ToCandidFunc(innerObj).Value;
					}
					else
					{
						innerValue = CandidPrimitive.Null();
					}
					return new CandidTypedValue(new CandidVariant(innerTag, innerValue), type);
				},
				v =>
				{
					CandidVariant variant = v.AsVariant();
					ICandidVariantValue obj = (ICandidVariantValue)Activator.CreateInstance(objType, nonPublic: true);
					CatchAllMapper? optionCatchAllMapper = optionMappingMap[variant.Tag]?.Mapper;
					object? variantValue = optionCatchAllMapper?.FromCandidFunc(variant.Value);
					obj.SetValue(variant.Tag, variantValue);
					return obj;
				}
			);
			return (mapper, type);
		}

		private static (CatchAllMapper Mapper, CandidRecordType Type) BuildRecord(Type objType, CandidConverterOptions options)
		{
			List<PropertyInfo> properties = objType
				.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.ToList();
			var propertyMetaDataMap = new Dictionary<CandidTag, (PropertyInfo Property, IObjectMapper Mapper, CandidType Type)>();
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

				(IObjectMapper fieldMapper, CandidType fieldType) = BuildInternal(property.PropertyType, options);
				if (customMapperAttribute != null)
				{
					fieldMapper = customMapperAttribute.Mapper!;
				}
				propertyMetaDataMap.Add(tag, (property, fieldMapper, fieldType));
			}

			Dictionary<CandidTag, CandidType> fields = propertyMetaDataMap
				.ToDictionary(
					p => p.Key,
					p => p.Value.Type
				);

			CandidRecordType type = new CandidRecordType(fields);
			Func<object, CandidTypedValue> mapFromObjectFunc = (obj) =>
			{
				Dictionary<CandidTag, CandidValue> fields = new();
				foreach ((CandidTag tag, (PropertyInfo property, IObjectMapper mapper, CandidType resultType)) in propertyMetaDataMap)
				{
					object propValue = property.GetValue(obj);
					CandidTypedValue v = mapper.Map(propValue, options);
					fields.Add(tag, v.Value);
				}
				var r = new CandidRecord(fields);
				return new CandidTypedValue(
					r,
					type
				);
			};
			Func<CandidValue, object> mapToObjectFunc = (v) =>
			{
				CandidRecord t = v.AsRecord();
				object obj = Activator.CreateInstance(objType);
				foreach ((CandidTag tag, (PropertyInfo property, IObjectMapper mapper, CandidType type)) in propertyMetaDataMap)
				{
					CandidValue fieldCandidValue = t.Fields[tag];
					object? fieldValue = mapper.Map(fieldCandidValue, options);
					property.SetValue(obj, fieldValue);
				}
				return obj;
			};

			var mapper = new CatchAllMapper(mapFromObjectFunc, mapToObjectFunc);
			return (mapper, type);
		}


	}
	internal class CatchAllMapper : IObjectMapper
	{
		public Func<object, CandidTypedValue> ToCandidFunc { get; }
		public Func<CandidValue, object> FromCandidFunc { get; }
		public CatchAllMapper(
			Func<object, CandidTypedValue> toCandidFunc,
			Func<CandidValue, object> fromCandidFunc)
		{
			this.ToCandidFunc = toCandidFunc ?? throw new ArgumentNullException(nameof(toCandidFunc));
			this.FromCandidFunc = fromCandidFunc ?? throw new ArgumentNullException(nameof(fromCandidFunc));
		}

		public bool CanMap(Type type)
		{
			return true;
		}

		public object Map(CandidValue value, CandidConverterOptions options)
		{
			return this.FromCandidFunc(value);
		}

		public CandidTypedValue Map(object obj, CandidConverterOptions options)
		{
			return this.ToCandidFunc(obj);
		}
	}
}
