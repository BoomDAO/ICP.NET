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
			this.Options = options ?? CandidConverterOptions.Default;
		}

		public CandidValue FromObject(object obj, bool isOpt)
		{
			if (object.ReferenceEquals(obj, null))
			{
				throw new ArgumentNullException(nameof(obj));
			}
			ICustomCandidMapper mapper = this.Options.ResolveMapper(obj.GetType());

			CandidValue value = mapper!.Map(obj, this.Options);
			return isOpt ? new CandidOptional(value) : value;
		}

		public T ToObject<T>(CandidValue value)
		{
			return (T)this.ToObject(typeof(T), value);
		}

		public T? ToObjectOrNull<T>(CandidValue value)
		{
			return (T?)this.ToObjectOrNull(typeof(T), value);
		}

		public object ToObject(Type objType, CandidValue value)
		{
			return this.ToObjectOrNull(objType, value) ?? throw new InvalidOperationException("Value cannot be null");
		}

		public object? ToObjectOrNull(Type objType, CandidValue value)
		{
			if (value.IsNull())
			{
				return null; // Handle null here for all mappers
			}
			ICustomCandidMapper mapper = this.Options.ResolveMapper(objType);
			return mapper!.Map(value, this.Options);
		}

	}

	public class CandidConverterOptions
	{
		public IReadOnlyList<ICustomCandidMapper> CustomMappers { get; }
		public ICaseConverter? CaseConverter { get; }

		private readonly ConcurrentDictionary<Type, ICustomCandidMapper> _typeToMapperCache;

		public CandidConverterOptions(
			IEnumerable<ICustomCandidMapper> mappers,
			ICaseConverter? caseConverter = null)
		{
			this.CaseConverter = caseConverter;
			this.CustomMappers = mappers?.ToList() ?? throw new ArgumentNullException(nameof(mappers));
			this._typeToMapperCache = new ConcurrentDictionary<Type, ICustomCandidMapper>();
		}

		public static CandidConverterOptions Default { get; } = new CandidConverterOptions(
			mappers: Enumerable.Empty<ICustomCandidMapper>(),
			caseConverter: null
		);

		public ICustomCandidMapper ResolveMapper(Type type)
		{
			return this._typeToMapperCache.GetOrAdd(type, this.ResolveUncached);
		}

		private ICustomCandidMapper ResolveUncached(Type type)
		{
			CustomMapperAttribute? mapperAttr = type.GetCustomAttribute<CustomMapperAttribute>();
			if (mapperAttr != null)
			{
				return mapperAttr.Mapper;
			}
			ICustomCandidMapper? mapper = this.CustomMappers
				.FirstOrDefault(m => m.CanMap(type));
			return mapper == null
				// Create a dynamic mapper if no custom one
				? CatchAllMapperFactory.GetMappingInfoFromType(type, this)
				: mapper;
		}
	}
}
