using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Mapping.Mappers;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace EdjCase.ICP.Candid
{
	/// <summary>
	/// A class that converts to and from C# and Candid types
	/// </summary>
	public class CandidConverter
	{
		/// <summary>
		/// A candid converter with the default settings
		/// </summary>
		public static CandidConverter Default { get; } = new CandidConverter();

		/// <summary>
		/// The options for the converter
		/// </summary>
		public CandidConverterOptions Options { get; }

		/// <param name="options">Optional. The options for the converter. If not set, will use defaults</param>
		public CandidConverter(CandidConverterOptions? options = null)
		{
			this.Options = options ?? CandidConverterOptions.Default();
		}

		/// <summary>
		/// Converts a C# object into a typed candid value
		/// </summary>
		/// <param name="obj">The object to convert</param>
		/// <returns></returns>
		public CandidTypedValue FromObject(object obj)
		{
			if (ReferenceEquals(obj, null))
			{
				throw new ArgumentNullException(nameof(obj));
			}
			IObjectMapper mapper = this.Options.ResolveMapper(obj.GetType());

			CandidTypedValue value = mapper!.Map(obj, this.Options);

			return value;
		}

		/// <summary>
		/// Converts a candid value into a C# object
		/// </summary>
		/// <typeparam name="T">The C# type to convert to</typeparam>
		/// <param name="value">The candid value to convert</param>
		/// <returns>A C# object of the value</returns>
		public T ToObject<T>(CandidValue value)
		{
			return (T)this.ToObject(typeof(T), value);
		}

		/// <summary>
		/// Converts a candid opt value to an OptionalValue
		/// </summary>
		/// <typeparam name="T">The C# type to convert the inner type to</typeparam>
		/// <param name="value">The opt value</param>
		/// <returns>An optional value</returns>
		public OptionalValue<T> ToOptionalObject<T>(CandidOptional value)
		{
			return this.ToOptionalObject(typeof(T), value).Cast<T>();
		}


		/// <summary>
		/// Converts a candid value into a C# object
		/// </summary>
		/// <param name="objType">The C# type to convert to</param>
		/// <param name="value">The candid value to convert</param>
		/// <returns>A C# object of the value</returns>
		public object ToObject(Type objType, CandidValue value)
		{
			IObjectMapper mapper = this.Options.ResolveMapper(objType);
			return mapper!.Map(value, this.Options);
		}

		/// <summary>
		/// Converts a candid opt value to an OptionalValue
		/// </summary>
		/// <param name="objType">The C# type to convert the inner type to</param>
		/// <param name="value">The opt value</param>
		/// <returns>An optional value</returns>
		public OptionalValue<object> ToOptionalObject(Type objType, CandidOptional value)
		{
			if (value.IsNull())
			{
				return OptionalValue<object>.NoValue(); // Handle null here for all mappers
			}
			object v = this.ToObject(objType, value.Value);
			return OptionalValue<object>.WithValue(v);
		}

	}

	/// <summary>
	/// Options for configuring how candid is convertered
	/// </summary>
	public class CandidConverterOptions
	{
		private Func<Type, IObjectMapper?>? CustomMappingResolver { get; }

		private readonly ConcurrentDictionary<Type, IObjectMapper> _typeToMapperCache;

		private CandidConverterOptions(Func<Type, IObjectMapper?>? customMappingResolver = null)
		{
			this._typeToMapperCache = new ConcurrentDictionary<Type, IObjectMapper>();
			this.CustomMappingResolver = customMappingResolver;
		}

		/// <summary>
		/// Resolves a mapper from the specified type
		/// </summary>
		/// <param name="type">The C# type to convert to and from</param>
		/// <returns>A mapper for conversion</returns>
		public IObjectMapper ResolveMapper(Type type)
		{
			return this._typeToMapperCache.GetOrAdd(type, this.ResolveUncached);
		}

		/// <summary>
		/// Creates the default options with its own mapper resolver
		/// </summary>
		/// <returns>Default converter options</returns>
		public static CandidConverterOptions Default()
		{
			return new CandidConverterOptions();
		}

		/// <summary>
		/// Creates options with a mapper resolver function, falls back to default if no mappers are found
		/// </summary>
		/// <param name="resolver">A function to resolve a mapper from a type</param>
		/// <returns>Converter options with resolver</returns>
		public static CandidConverterOptions FromResolver(Func<Type, IObjectMapper?> resolver)
		{
			return new CandidConverterOptions(resolver);
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
