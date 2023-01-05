using Dahomey.Cbor;
using Dahomey.Cbor.Serialization.Converters;
using Dahomey.Cbor.Serialization.Converters.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Cbor
{
	internal class CborConverterProvider : ICborConverterProvider
	{
		private static readonly Lazy<Dictionary<Type, Func<CborOptions, ICborConverter>>> providerMapLazy = new(BuildMap, isThreadSafe: true);
		public ICborConverter? GetConverter(Type type, CborOptions options)
		{
			return CborConverterProvider.providerMapLazy.Value.GetValueOrDefault(type)?.Invoke(options);
		}

		private static Dictionary<Type, Func<CborOptions, ICborConverter>> BuildMap()
		{
			Type baseClass = typeof(CborConverterBase<>);
			return Assembly.GetExecutingAssembly()
				.GetTypes()
				.Select(t => (ConverterType: t, ObjectType: GetCborConverterType(t)))
				.Where(t => t.ObjectType != null)
				.ToDictionary(t => t.ObjectType!, t => CreateConverter(t.ConverterType));
		}

		private static Func<CborOptions, ICborConverter> CreateConverter(Type converterType)
		{
			return options =>
			{
				ConstructorInfo[] constructors = converterType.GetConstructors();
				if (!constructors.Any())
				{
					// Default parameterless
					return CastConverter(Activator.CreateInstance(converterType));
				}
				foreach (ConstructorInfo constructor in constructors.OrderByDescending(c => c.GetParameters().Length))
				{
					var parameters = constructor.GetParameters();
					if (parameters.Length == 0)
					{
						// Default parameterless
						return CastConverter(Activator.CreateInstance(converterType));
					}
					if (parameters.Length == 1 && parameters[0].ParameterType == typeof(CborOptions))
					{
						return CastConverter(Activator.CreateInstance(converterType, options));
					}
				}
				throw new InvalidOperationException("Converter constructor either needs to be empty or have `CborOptions` as the only parameter");

				ICborConverter CastConverter(object? instance)
				{
					return (ICborConverter)(instance ?? throw new InvalidOperationException($"Invalid cbor converter '{converterType}'"));
				}
			};
		}

		static Type? GetCborConverterType(Type type)
		{
			Type baseClass = typeof(CborConverterBase<>);
			if (type.IsGenericType && type.GetGenericTypeDefinition() == baseClass)
			{
				// Matched cbor converter base, get object type
				return type.GenericTypeArguments[0];
			}

			if (type.BaseType != null)
			{
				// Recursively check inheritance for the converter base
				return GetCborConverterType(type.BaseType);
			}
			// No match
			return null;
		}
	}
}
