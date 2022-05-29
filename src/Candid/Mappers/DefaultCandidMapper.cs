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

namespace EdjCase.ICP.Candid.Mappers
{
	public class DefaultCandidMapper : ICandidMapper
	{
		private ConcurrentDictionary<Type, MappingInfo?> _typeMapCache = new();


		public bool TryGetMappingInfo(Type objType, CandidConverter converter, out MappingInfo? mappingInfo)
		{
			mappingInfo = this._typeMapCache.GetOrAdd(objType, t => GetMappingInfoFromType(t, converter));
			return mappingInfo != null;
		}

		private static MappingInfo GetMappingInfoFromType(Type objType, CandidConverter converter)
		{
			Type? genericTypeDefintion = objType.IsGenericType ? objType.GetGenericTypeDefinition() : null;
			CandidType type;
			Func<object, CandidValue> mapFromObjectFunc;
			Func<CandidValue, object?> mapToObjectFunc;
			if (objType == typeof(string))
			{
				type = new CandidPrimitiveType(PrimitiveType.Text);
				mapFromObjectFunc = o => CandidPrimitive.Text((string)o);
				mapToObjectFunc = v => v.AsText();
			}
			else if (objType == typeof(byte))
			{
				type = new CandidPrimitiveType(PrimitiveType.Nat8);
				mapFromObjectFunc = o => CandidPrimitive.Nat8((byte)o);
				mapToObjectFunc = v => v.AsNat8();
			}
			else if (objType == typeof(ushort))
			{
				type = new CandidPrimitiveType(PrimitiveType.Nat16);
				mapFromObjectFunc = o => CandidPrimitive.Nat16((ushort)o);
				mapToObjectFunc = v => v.AsNat16();
			}
			else if (objType == typeof(uint))
			{
				type = new CandidPrimitiveType(PrimitiveType.Nat32);
				mapFromObjectFunc = o => CandidPrimitive.Nat32((uint)o);
				mapToObjectFunc = v => v.AsNat32();
			}
			else if (objType == typeof(ulong))
			{
				type = new CandidPrimitiveType(PrimitiveType.Nat64);
				mapFromObjectFunc = o => CandidPrimitive.Nat64((ulong)o);
				mapToObjectFunc = v => v.AsNat64();
			}
			else if (objType == typeof(UnboundedUInt))
			{
				type = new CandidPrimitiveType(PrimitiveType.Nat);
				mapFromObjectFunc = o => CandidPrimitive.Nat((UnboundedUInt)o);
				mapToObjectFunc = v => v.AsNat();
			}
			else if (objType == typeof(sbyte))
			{
				type = new CandidPrimitiveType(PrimitiveType.Int8);
				mapFromObjectFunc = o => CandidPrimitive.Int8((sbyte)o);
				mapToObjectFunc = v => v.AsInt8();
			}
			else if (objType == typeof(short))
			{
				type = new CandidPrimitiveType(PrimitiveType.Int16);
				mapFromObjectFunc = o => CandidPrimitive.Int16((short)o);
				mapToObjectFunc = v => v.AsInt16();
			}
			else if (objType == typeof(int))
			{
				type = new CandidPrimitiveType(PrimitiveType.Int32);
				mapFromObjectFunc = o => CandidPrimitive.Int32((int)o);
				mapToObjectFunc = v => v.AsInt32();
			}
			else if (objType == typeof(long))
			{
				type = new CandidPrimitiveType(PrimitiveType.Int64);
				mapFromObjectFunc = o => CandidPrimitive.Int64((long)o);
				mapToObjectFunc = v => v.AsInt64();
			}
			else if (objType == typeof(UnboundedInt))
			{
				type = new CandidPrimitiveType(PrimitiveType.Int);
				mapFromObjectFunc = o => CandidPrimitive.Int((UnboundedInt)o);
				mapToObjectFunc = v => v.AsInt();
			}
			else if (objType == typeof(double))
			{
				type = new CandidPrimitiveType(PrimitiveType.Float64);
				mapFromObjectFunc = o => CandidPrimitive.Float64((double)o);
				mapToObjectFunc = v => v.AsFloat64();
			}
			else if (objType == typeof(decimal))
			{
				// TODO is this the best way to convert a decimal?
				type = new CandidPrimitiveType(PrimitiveType.Float64);
				mapFromObjectFunc = o => CandidPrimitive.Float64((double)o);
				mapToObjectFunc = v => (decimal)v.AsFloat64();
			}
			else if (objType == typeof(Principal))
			{
				type = new CandidPrimitiveType(PrimitiveType.Principal);
				mapFromObjectFunc = o => CandidPrimitive.Principal((Principal)o);
				mapToObjectFunc = v => v.AsPrincipal()!; // TODO null?
			}
			else if (genericTypeDefintion == typeof(Nullable<>))
			{
				Type innerType = objType.GenericTypeArguments[0];
				MappingInfo innerMappingInfo = GetMappingInfoFromType(innerType, converter);
				type = new CandidOptionalType(innerMappingInfo.Type);
				PropertyInfo valueProp = objType.GetProperty("Value");
				mapFromObjectFunc = o =>
				{
					if (o == null)
					{
						return CandidPrimitive.Null();
					}
					object innerValue = valueProp.GetValue(o);
					return innerMappingInfo.MapFromObjectFunc(innerValue);
				};
				mapToObjectFunc = v =>
				{
					CandidOptional opt = v.AsOptional();
					return innerMappingInfo.MapToObjectFunc(opt.Value);
				};
			}
			else if (genericTypeDefintion == typeof(List<>))
			{
				Type innerType = objType.GenericTypeArguments[0];
				MappingInfo innerMappingInfo = GetMappingInfoFromType(innerType!, converter);
				type = new CandidVectorType(innerMappingInfo.Type);
				mapFromObjectFunc = o =>
				{
					CandidValue[] values = ((IEnumerable)o).Select(v => innerMappingInfo.MapFromObjectFunc(v)).ToArray();
					return new CandidVector(values);
				};

				mapToObjectFunc = v =>
				{
					IList list = (IList)Activator.CreateInstance(objType);
					foreach (CandidValue innerValue in v.AsVector().Values)
					{
						list.Add(innerMappingInfo.MapToObjectFunc(innerValue));
					}
					return list;
				};
			}
			else if (objType.IsArray)
			{
				Type innerType = objType.GetElementType();
				MappingInfo innerMappingInfo = GetMappingInfoFromType(innerType!, converter);
				type = new CandidVectorType(innerMappingInfo.Type);
				mapFromObjectFunc = o =>
				{
					CandidValue[] values = ((IEnumerable)o).Select(v => innerMappingInfo.MapFromObjectFunc(v)).ToArray();
					return new CandidVector(values);
				};

				mapToObjectFunc = v =>
				{
					CandidVector vector = v.AsVector();
					Array array = Array.CreateInstance(innerType, vector.Values.Length);
					for (int i = 0; i < vector.Values.Length; i++)
					{
						CandidValue innerValue = vector.Values[i];
						object? innerObj = innerMappingInfo.MapToObjectFunc(innerValue);
						array.SetValue(innerObj, i);
					}
					return array;
				};
			}
			else if (typeof(ICandidVariantValue).IsAssignableFrom(objType))
			{
				type = new CandidVariantType(options);
				mapFromObjectFunc = o =>
				{
					ICandidVariantValue v = (ICandidVariantValue)o;
					(CandidTag innerTag, object? innerObj) = v.GetValue();
					CandidValue innerValue;
					if (innerObj != null)
					{
						MappingInfo info = GetMappingInfoFromType(innerObj.GetType(), converter);
						innerValue = info.MapFromObjectFunc(innerObj);
						type = info.Type;
					}
					else
					{
						innerValue = CandidPrimitive.Null();
						type = new CandidPrimitiveType(PrimitiveType.Null);
					}
					return new CandidVariant(innerTag, innerValue);
				};

				mapToObjectFunc = v =>
				{
					CandidVariant variant = v.AsVariant();
					ICandidVariantValue obj = (ICandidVariantValue)Activator.CreateInstance(objType);
					obj.SetValue(variant.Tag, variant.Value);
					return obj;
				};
			}
			else
			{
				MappingInfo info = GetRecordMappingInfo(objType, converter);
				type = info.Type;
				mapFromObjectFunc = info.MapFromObjectFunc;
				mapToObjectFunc = info.MapToObjectFunc;
			}
			if (objType.IsClass)
			{
				var oldMap = mapToObjectFunc;
				mapToObjectFunc = v =>
				{
					if (v is CandidOptional o)
					{
						if (o.Value == CandidPrimitive.Null())
						{
							return null;
						}
						v = o.Value;
					}
					return oldMap(v);
				};
			}
			return new MappingInfo(type, mapFromObjectFunc, mapToObjectFunc);
		}

		private static MappingInfo GetRecordMappingInfo(Type objType, CandidConverter converter)
		{
			List<PropertyInfo> properties = objType
				.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.ToList();
			var propertyMetaDataMap = new Dictionary<CandidTag, (PropertyInfo Property, MappingInfo MappingInfo)>();
			foreach (PropertyInfo property in properties)
			{
				CandidIgnoreAttribute? ignoreAttribute = property.GetCustomAttribute<CandidIgnoreAttribute>();
				if (ignoreAttribute != null)
				{
					// Ignore property
					continue;
				}
				CandidPropertyNameAttribute? propertyAttribute = property.GetCustomAttribute<CandidPropertyNameAttribute>();
				string propertyName;
				if (propertyAttribute != null)
				{
					propertyName = propertyAttribute.Name;
				}
				else
				{
					propertyName = StringUtil.ToSnakeCase(property.Name);
				}
				CandidTag tag = CandidTag.FromName(propertyName);
				CustomMapperAttribute? customMapperAttribute = property.GetCustomAttribute<CustomMapperAttribute>();

				MappingInfo fieldMappingInfo;
				if (customMapperAttribute != null)
				{
					bool canGetMappingInfo = customMapperAttribute.Mapper.TryGetMappingInfo(property.PropertyType, converter, out MappingInfo? mappingInfo);
					if (!canGetMappingInfo)
					{
						// TODO
						throw new Exception();
					}
					fieldMappingInfo = mappingInfo!;
				}
				else
				{
					fieldMappingInfo = GetMappingInfoFromType(property.PropertyType, converter);
				}
				propertyMetaDataMap.Add(tag, (property, fieldMappingInfo));
			}


			Dictionary<CandidTag, CandidType> fieldTypes = propertyMetaDataMap
				.ToDictionary(p => p.Key, p => p.Value.MappingInfo.Type);
			CandidRecordType recordType = new(fieldTypes);

			Func<object, CandidValue> mapFromObjectFunc = (obj) =>
			{
				Dictionary<CandidTag, CandidValue> fields = new();
				foreach ((CandidTag tag, (PropertyInfo property, MappingInfo mappingInfo)) in propertyMetaDataMap)
				{
					object propValue = property.GetValue(obj);
					CandidValue v = mappingInfo.MapFromObjectFunc(propValue);
					fields.Add(tag, v);
				}
				return new CandidRecord(fields);
			};
			Func<CandidValue, object> mapToObjectFunc = (v) =>
			{
				CandidRecord recordType = v.AsRecord();
				object obj = Activator.CreateInstance(objType);
				foreach ((CandidTag tag, (PropertyInfo property, MappingInfo mappingInfo)) in propertyMetaDataMap)
				{
					CandidValue fieldCandidValue = recordType.Fields[tag];
					object? fieldValue = mappingInfo.MapToObjectFunc(fieldCandidValue);
					property.SetValue(obj, fieldValue);
				}
				return obj;
			};

			return new MappingInfo(recordType, mapFromObjectFunc, mapToObjectFunc);
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class CandidPropertyNameAttribute : Attribute
	{
		public string Name { get; }
		public CandidPropertyNameAttribute(string name)
		{
			this.Name = name;
		}
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property)]
	public class CustomMapperAttribute : Attribute
	{
		public ICandidMapper Mapper { get; }
		public CustomMapperAttribute(ICandidMapper mapper)
		{
			this.Mapper = mapper;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class CandidIgnoreAttribute : Attribute
	{
	}
}
