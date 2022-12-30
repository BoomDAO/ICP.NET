using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Mapping.Mappers;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EdjCase.ICP.Candid
{
	public class CandidConverter
	{
		public static CandidConverter Default { get; } = new CandidConverter();

		public CandidConverterOptions Options { get; }

		public CandidConverter(CandidConverterOptions? options = null)
		{
			this.Options = options ?? new CandidConverterOptions();
		}

		public CandidTypedValue FromObject(object? obj)
		{
			if (object.ReferenceEquals(obj, null))
			{
				throw new ArgumentNullException(nameof(obj));
			}
			IObjectMapper mapper = this.Options.ResolveMapper(obj.GetType());

			CandidTypedValue value = mapper!.Map(obj, this.Options);

			return value;
		}

		public T ToObject<T>(CandidValue value)
		{
			return (T)this.ToObject(typeof(T), value);
		}

		public OptionalValue<T> ToOptionalObject<T>(CandidValue value)
		{
			return this.ToOptionalObject(typeof(T), value).Cast<T>();
		}

		public object ToObject(Type objType, CandidValue value)
		{
			return this.ToOptionalObject(objType, value).GetValueOrThrow();
		}

		public OptionalValue<object> ToOptionalObject(Type objType, CandidValue value)
		{
			if (value.IsNull())
			{
				return OptionalValue<object>.NoValue(); // Handle null here for all mappers
			}
			IObjectMapper mapper = this.Options.ResolveMapper(objType);
			object v = mapper!.Map(value, this.Options);
			return new OptionalValue<object>(true, v);
		}

	}

	public class CandidConverterOptions
	{
		public Func<Type, IObjectMapper?>? CustomMappingResolver { get; }

		private readonly ConcurrentDictionary<Type, IObjectMapper> _typeToMapperCache;

		public CandidConverterOptions(Func<Type, IObjectMapper?>? customMappingResolver = null)
		{
			this._typeToMapperCache = new ConcurrentDictionary<Type, IObjectMapper>();
			this.CustomMappingResolver = customMappingResolver;
		}

		public IObjectMapper ResolveMapper(Type type)
		{
			return this._typeToMapperCache.GetOrAdd(type, this.ResolveUncached);
		}

		private IObjectMapper ResolveUncached(Type type)
		{
			CustomMapperAttribute? mapperAttr = type.GetCustomAttribute<CustomMapperAttribute>();
			if (mapperAttr != null)
			{
				return mapperAttr.Mapper;
			}
			IObjectMapper? mapper = this.CustomMappingResolver?.Invoke(type);

			return mapper == null
				// Create a dynamic mapper if no custom one
				? DefaultMapperFactory.Build(type)
				: mapper;
		}
	}
}
