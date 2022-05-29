using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;

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
		(CandidTag Tag, object Value) GetValue();
		void SetValue(CandidTag tag, object value);
	}
}