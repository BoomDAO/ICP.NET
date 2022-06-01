using EdjCase.ICP.Candid.Mappers;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

		public CandidValueWithType FromObject<T>(T obj)
		{
			return this.FromObject(obj as object);
		}

		public CandidValueWithType FromObject(object? obj)
		{
			if (object.ReferenceEquals(obj, null))
			{
				return CandidValueWithType.Null();
			}
			if(!this.TryGetMappingInfo(obj.GetType(), out MappingInfo? mappingInfo))
			{
				// TODO
				throw new Exception();
			}

			CandidValue value = mappingInfo!.MapFromObjectFunc(obj);
			return CandidValueWithType.FromValueAndType(value, mappingInfo.Type);
		}

		public bool TryGetMappingInfo<T>(out MappingInfo? mappingInfo)
		{
			return this.TryGetMappingInfo(typeof(T), out mappingInfo);
		}

		public bool TryGetMappingInfo(Type objType, out MappingInfo? mappingInfo)
		{
			ICandidMapper mapper = this.Options.ResolveMapper(objType, this);
			return mapper.TryGetMappingInfo(objType, this, out mappingInfo);
		}

		public T ToObject<T>(CandidValue value)
		{
			object? obj = this.ToObject(typeof(T), value);
			return (T)obj;
		}

		public object ToObject(Type objType, CandidValue value)
		{
			if (!this.TryGetMappingInfo(objType, out MappingInfo? mappingInfo))
			{
				// TODO
				throw new Exception();
			}
			object? obj = mappingInfo!.MapToObjectFunc(value);
			if(obj == null)
			{
				// TODO
				throw new Exception();
			}
			return obj;
		}

	}

	public class CandidConverterOptions
	{
		public IReadOnlyList<ICandidMapper> Mappers { get; }
		public CandidMappingSettings Settings { get; }

		private readonly ConcurrentDictionary<Type, ICandidMapper> _typeToMapperCache;

		public CandidConverterOptions(CandidMappingSettings settings, params ICandidMapper[] mappers) : this(settings, (IEnumerable<ICandidMapper>)mappers)
		{

		}
		public CandidConverterOptions(CandidMappingSettings settings, IEnumerable<ICandidMapper> mappers)
		{
			this.Settings = settings;
			this.Mappers = mappers?.ToList() ?? throw new ArgumentNullException(nameof(mappers));
			this._typeToMapperCache = new ConcurrentDictionary<Type, ICandidMapper>();
		}

		public static CandidConverterOptions Default { get; } = new CandidConverterOptions(
			settings: CandidMappingSettings.Default,
			new DefaultCandidMapper()
		);

		internal ICandidMapper ResolveMapper(Type type, CandidConverter converter)
		{
			return this._typeToMapperCache.GetOrAdd(type, t => this.ResolveUncached(t, converter));
		}

		private ICandidMapper ResolveUncached(Type type, CandidConverter converter)
		{
			ICandidMapper? mapper = this.Mappers
				.FirstOrDefault(m => m.TryGetMappingInfo(type, converter, out _));
			if (mapper == null)
			{
				// TODO
				throw new Exception();
			}
			return mapper;
		}
	}

	public class CandidMappingSettings
	{
		public static CandidMappingSettings Default { get; } = new CandidMappingSettings(
		);
	}
}
