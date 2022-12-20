using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal static class CatchAllMapperFactory
	{
		public static CatchAllMapper GetCatchAllMapperFromType(Type objType, CandidConverterOptions options)
		{
			CandidType type;
			Func<CandidValue, object> mapToObjectFunc;
			Func<object, CandidValue> innerMapFromObjectFunc;
			if (objType == typeof(string))
			{
				type = new CandidPrimitiveType(PrimitiveType.Text);
				innerMapFromObjectFunc = o => CandidPrimitive.Text((string)o);
				mapToObjectFunc = v => v.AsText();
			}
			else if (objType == typeof(byte))
			{
				type = new CandidPrimitiveType(PrimitiveType.Nat8);
				innerMapFromObjectFunc = o => CandidPrimitive.Nat8((byte)o);
				mapToObjectFunc = v => v.AsNat8();
			}
			else if (objType == typeof(ushort))
			{
				type = new CandidPrimitiveType(PrimitiveType.Nat16);
				innerMapFromObjectFunc = o => CandidPrimitive.Nat16((ushort)o);
				mapToObjectFunc = v => v.AsNat16();
			}
			else if (objType == typeof(uint))
			{
				type = new CandidPrimitiveType(PrimitiveType.Nat32);
				innerMapFromObjectFunc = o => CandidPrimitive.Nat32((uint)o);
				mapToObjectFunc = v => v.AsNat32();
			}
			else if (objType == typeof(ulong))
			{
				type = new CandidPrimitiveType(PrimitiveType.Nat64);
				innerMapFromObjectFunc = o => CandidPrimitive.Nat64((ulong)o);
				mapToObjectFunc = v => v.AsNat64();
			}
			else if (objType == typeof(UnboundedUInt))
			{
				type = new CandidPrimitiveType(PrimitiveType.Nat);
				innerMapFromObjectFunc = o => CandidPrimitive.Nat((UnboundedUInt)o);
				mapToObjectFunc = v => v.AsNat();
			}
			else if (objType == typeof(sbyte))
			{
				type = new CandidPrimitiveType(PrimitiveType.Int8);
				innerMapFromObjectFunc = o => CandidPrimitive.Int8((sbyte)o);
				mapToObjectFunc = v => v.AsInt8();
			}
			else if (objType == typeof(short))
			{
				type = new CandidPrimitiveType(PrimitiveType.Int16);
				innerMapFromObjectFunc = o => CandidPrimitive.Int16((short)o);
				mapToObjectFunc = v => v.AsInt16();
			}
			else if (objType == typeof(int))
			{
				type = new CandidPrimitiveType(PrimitiveType.Int32);
				innerMapFromObjectFunc = o => CandidPrimitive.Int32((int)o);
				mapToObjectFunc = v => v.AsInt32();
			}
			else if (objType == typeof(long))
			{
				type = new CandidPrimitiveType(PrimitiveType.Int64);
				innerMapFromObjectFunc = o => CandidPrimitive.Int64((long)o);
				mapToObjectFunc = v => v.AsInt64();
			}
			else if (objType == typeof(UnboundedInt))
			{
				type = new CandidPrimitiveType(PrimitiveType.Int);
				innerMapFromObjectFunc = o => CandidPrimitive.Int((UnboundedInt)o);
				mapToObjectFunc = v => v.AsInt();
			}
			else if (objType == typeof(double))
			{
				type = new CandidPrimitiveType(PrimitiveType.Float64);
				innerMapFromObjectFunc = o => CandidPrimitive.Float64((double)o);
				mapToObjectFunc = v => v.AsFloat64();
			}
			else if (objType == typeof(decimal))
			{
				// TODO is this the best way to convert a decimal?
				type = new CandidPrimitiveType(PrimitiveType.Float64);
				innerMapFromObjectFunc = o => CandidPrimitive.Float64((double)o);
				mapToObjectFunc = v => (decimal)v.AsFloat64();
			}
			else if (objType == typeof(Principal))
			{
				type = new CandidPrimitiveType(PrimitiveType.Principal);
				innerMapFromObjectFunc = o => CandidPrimitive.Principal((Principal)o);
				mapToObjectFunc = v => v.AsPrincipal()!; // TODO null?
			}
			else if (genericTypeDefintion == typeof(Nullable<>))
			{
				Type innerType = objType.GenericTypeArguments[0];
				CatchAllMapper innerCatchAllMapper = GetCatchAllMapperFromType(innerType, converter);
				type = new CandidOptionalType(innerCatchAllMapper.Type);
				PropertyInfo valueProp = objType.GetProperty("Value");
				innerMapFromObjectFunc = o =>
				{
					object innerValue = valueProp.GetValue(o);
					return innerCatchAllMapper.MapFromObjectFunc(innerValue);
				};
				mapToObjectFunc = v =>
				{
					CandidOptional opt = v.AsOptional();
					return innerCatchAllMapper.MapToObjectFunc(opt.Value);
				};
			}
			else if (genericTypeDefintion == typeof(List<>))
			{
				Type innerType = objType.GenericTypeArguments[0];
				CatchAllMapper innerCatchAllMapper = GetCatchAllMapperFromType(innerType!, converter);
				type = new CandidVectorType(innerCatchAllMapper.Type);
				innerMapFromObjectFunc = o =>
				{
					CandidValue[] values = ((IEnumerable)o).Select(v => innerCatchAllMapper.MapFromObjectFunc(v)).ToArray();
					return new CandidVector(values);
				};

				mapToObjectFunc = v =>
				{
					IList list = (IList)Activator.CreateInstance(objType);
					foreach (CandidValue innerValue in v.AsVector().Values)
					{
						list.Add(innerCatchAllMapper.MapToObjectFunc(innerValue));
					}
					return list;
				};
			}
			else if (objType.IsArray)
			{
				Type innerType = objType.GetElementType();
				CatchAllMapper innerCatchAllMapper = GetCatchAllMapperFromType(innerType!, converter);
				type = new CandidVectorType(innerCatchAllMapper.Type);
				innerMapFromObjectFunc = o =>
				{
					CandidValue[] values = ((IEnumerable)o).Select(v => innerCatchAllMapper.MapFromObjectFunc(v)).ToArray();
					return new CandidVector(values);
				};

				mapToObjectFunc = v =>
				{
					CandidVector vector = v.AsVector();
					Array array = Array.CreateInstance(innerType, vector.Values.Length);
					for (int i = 0; i < vector.Values.Length; i++)
					{
						CandidValue innerValue = vector.Values[i];
						object? innerObj = innerCatchAllMapper.MapToObjectFunc(innerValue);
						array.SetValue(innerObj, i);
					}
					return array;
				};
			}
			else if (typeof(ICandidVariantValue).IsAssignableFrom(objType))
			{
				ICandidVariantValue variant = (ICandidVariantValue)Activator.CreateInstance(objType, nonPublic: true);
				Dictionary<CandidTag, (Type Type, bool IsOpt)?> optionTypes = variant.GetOptions();
				Dictionary<CandidTag, CatchAllMapper?> optionMappingMap = optionTypes
					.ToDictionary(
						t => t.Key,
						t =>
						{
							if (t.Value == null)
							{
								return null;
							}
							CatchAllMapper CatchAllMapper = GetCatchAllMapperFromType(t.Value.Value.Type, options);
							if (t.Value.Value.IsOpt)
							{
								CatchAllMapper = CatchAllMapper.ToOpt();
							}
							return CatchAllMapper;
						}
					);
				Dictionary<CandidTag, CandidType> options = optionMappingMap
					.ToDictionary(
						t => t.Key,
						t =>
						{
							if (t.Value == null)
							{
								return new CandidPrimitiveType(PrimitiveType.Null);
							}
							return t.Value.Type;
						});
				type = new CandidVariantType(options);
				innerMapFromObjectFunc = o =>
				{
					ICandidVariantValue v = (ICandidVariantValue)o;
					(CandidTag innerTag, object? innerObj) = v.GetValue();

					CatchAllMapper? innerCatchAllMapper = optionMappingMap[innerTag];
					CandidType innerType = options[innerTag];
					CandidValue innerValue;
					if (innerCatchAllMapper != null)
					{
						innerValue = innerCatchAllMapper.MapFromObjectFunc(innerObj);
					}
					else
					{
						innerValue = CandidPrimitive.Null();
					}
					return new CandidVariant(innerTag, innerValue);
				};

				mapToObjectFunc = v =>
				{
					CandidVariant variant = v.AsVariant();
					ICandidVariantValue obj = (ICandidVariantValue)Activator.CreateInstance(objType, nonPublic: true);
					CatchAllMapper? optionCatchAllMapper = optionMappingMap[variant.Tag];
					object? variantValue = optionCatchAllMapper?.MapToObjectFunc(variant.Value);
					obj.SetValue(variant.Tag, variantValue);
					return obj;
				};
			}
			else
			{
				CatchAllMapper info = GetRecordCatchAllMapper(objType, converter);
				type = info.Type;
				innerMapFromObjectFunc = info.MapFromObjectFunc;
				mapToObjectFunc = info.MapToObjectFunc;
			}
			Func<object?, CandidValue> mapFromObjectFunc = o =>
			{
				if (o == null)
				{
					return CandidPrimitive.Null();
				}
				return innerMapFromObjectFunc(o);
			};
			return new CatchAllMapper(mapFromObjectFunc, mapToObjectFunc);
		}



		private static CatchAllMapper GetRecordCatchAllMapper(Type objType, CandidConverterOptions options)
		{
			List<PropertyInfo> properties = objType
				.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.ToList();
			var propertyMetaDataMap = new Dictionary<CandidTag, (PropertyInfo Property, ICustomCandidMapper Mapper)>();
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

				ICustomCandidMapper mapper;
				if (customMapperAttribute != null)
				{
					mapper = customMapperAttribute.Mapper!;
				}
				else
				{
					mapper = options.ResolveMapper(property.PropertyType);
				}
				bool isOpt = TypeUtil.IsNullable(property);
				if (isOpt)
				{
					// TODO
					//mapper = 
				}
				propertyMetaDataMap.Add(tag, (property, mapper));
			}


			Func<object, CandidValue> mapFromObjectFunc = (obj) =>
			{
				if (obj == null)
				{
					return CandidPrimitive.Null();
				}
				Dictionary<CandidTag, CandidValue> fields = new();
				foreach ((CandidTag tag, (PropertyInfo property, ICustomCandidMapper mapper)) in propertyMetaDataMap)
				{
					object propValue = property.GetValue(obj);
					CandidValue v = mapper.Map(propValue, options);
					fields.Add(tag, v);
				}
				return new CandidRecord(fields);
			};
			Func<CandidValue, object> mapToObjectFunc = (v) =>
			{
				CandidRecord t = v.AsRecord();
				object obj = Activator.CreateInstance(objType);
				foreach ((CandidTag tag, (PropertyInfo property, ICustomCandidMapper mapper)) in propertyMetaDataMap)
				{
					CandidValue fieldCandidValue = t.Fields[tag];
					object? fieldValue = mapper.Map(fieldCandidValue, options);
					property.SetValue(obj, fieldValue);
				}
				return obj;
			};

			return new CatchAllMapper(mapFromObjectFunc, mapToObjectFunc);
		}


	}
	internal class CatchAllMapper : ICustomCandidMapper
	{
		public Func<object, CandidValue> MapFromObjectFunc { get; }
		public Func<CandidValue, object> MapToObjectFunc { get; }
		public CatchAllMapper(
			Func<object, CandidValue> mapFromObjectFunc,
			Func<CandidValue, object> mapToObjectFunc)
		{
			this.MapFromObjectFunc = mapFromObjectFunc ?? throw new ArgumentNullException(nameof(mapFromObjectFunc));
			this.MapToObjectFunc = mapToObjectFunc ?? throw new ArgumentNullException(nameof(mapToObjectFunc));
		}

		public bool CanMap(Type type)
		{
			return true;
		}

		public object Map(CandidValue value, CandidConverterOptions options)
		{
			return this.MapToObjectFunc(value);
		}

		public CandidValue Map(object obj, CandidConverterOptions options)
		{
			return this.MapFromObjectFunc(obj);
		}
	}
}
