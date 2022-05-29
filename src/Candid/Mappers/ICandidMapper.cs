using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EdjCase.ICP.Candid
{
	public interface ICandidMapper
	{
		bool TryGetMappingInfo(Type objType, CandidConverter converter, out MappingInfo? mappingInfo);
	}

	public class MappingInfo
	{
		public CandidType Type { get; }
		public Func<object, CandidValue> MapFromObjectFunc { get; }
		public Func<CandidValue, object?> MapToObjectFunc { get; }
		public MappingInfo(CandidType type, Func<object, CandidValue> mapFromObjectFunc, Func<CandidValue, object?> mapToObjectFunc)
		{
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.MapFromObjectFunc = mapFromObjectFunc ?? throw new ArgumentNullException(nameof(mapFromObjectFunc));
			this.MapToObjectFunc = mapToObjectFunc ?? throw new ArgumentNullException(nameof(mapToObjectFunc));
		}
	}

	public abstract class CandidMapperBase<T> : ICandidMapper
	{
		public virtual bool CanMap(Type type)
		{
			return type == typeof(T);
		}

		public abstract bool TryGetMappingInfo(Type objType, CandidConverter converter, out MappingInfo? mappingInfo);
	}

	public interface ICandidVariantValue
	{
		Dictionary<CandidTag, Type?> GetOptions();
		(CandidTag Tag, object Value) GetValue();
		void SetValue(CandidTag tag, object? value);
	}

	public abstract class CandidVariantValueBase<TEnum> : ICandidVariantValue
		where TEnum : struct, Enum
	{
		private Lazy<Dictionary<TEnum, Type?>> lazyOptions = new Lazy<Dictionary<TEnum, Type?>>(BuildOptions);

		public TEnum Type { get; private set; }
		protected object? value;

		protected CandidVariantValueBase()
		{

		}
		protected CandidVariantValueBase(TEnum type, object? value)
		{
			this.Type = type;
			this.value = value;
		}


		public Dictionary<CandidTag, Type?> GetOptions()
		{
			return this.lazyOptions.Value
				.ToDictionary(v => CandidTag.FromName(v.Key.ToString()), v => v.Value);
		}

		private static Dictionary<TEnum, Type?> BuildOptions()
		{
			return typeof(TEnum)
				.GetMembers(BindingFlags.Public | BindingFlags.Static)
				.ToDictionary(
					m => (TEnum)Enum.Parse(typeof(TEnum), m.Name),
					m => m.GetCustomAttribute<VariantOptionTypeAttribute>()?.Type
				);
		}

		public (CandidTag Tag, object Value) GetValue()
		{
			return (CandidTag.FromName(this.Type.ToString()), this.value);
		}


		public void SetValue(TEnum type, object? value)
		{
			this.ValidateValue(type, value);
			this.Type = type;
			this.value = value;
		}

		public void SetValue(CandidTag tag, object? value)
		{
			TEnum type = this.GetEnumFromTag(tag);
			this.SetValue(type, value);
		}

		private void ValidateValue(TEnum e, object? value)
		{
			if (!this.lazyOptions.Value.TryGetValue(e, out Type? type))
			{
				throw new NotImplementedException($"No implementation of enum type '{typeof(TEnum)}' with value '{e}' has a type mapping");
			}
			if (value == null)
			{
				if (type == null)
				{
					// Valid if both are null
					return;
				}
			}
			else if (type == value.GetType())
			{
				// Valid if both the same type
				return;
			}
			throw new InvalidOperationException($"Invalid value type '{value?.GetType().FullName ?? "null"}' being set for variant option '{e}'. Expected value type '{type?.FullName ?? "null"}'");
		}


		private TEnum GetEnumFromTag(CandidTag tag)
		{
			if (tag.Name == null)
			{
				// TODO cache
				foreach(string name in Enum.GetNames(typeof(TEnum)))
				{
					CandidTag t = CandidTag.FromName(name);
					if(t.Id == tag.Id)
					{
						tag = t;
						break;
					}
				}
			}
			if (!Enum.TryParse(tag.Name!, out TEnum e) || !Enum.IsDefined(typeof(TEnum), e))
			{
				throw new InvalidOperationException($"Unable to convert tag '{tag}' into enum type '{typeof(TEnum)}'");
			}
			return e;
		}

		protected void ValidateType(TEnum type)
		{
			if (!this.Type.Equals(type))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}


	public class VariantOptionTypeAttribute : Attribute
	{
		public Type Type { get; }
		public VariantOptionTypeAttribute(Type type)
		{
			this.Type = type;
		}
	}
}