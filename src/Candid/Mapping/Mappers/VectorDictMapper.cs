using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class VectorDictMapper : ICandidValueMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }
		public Type KeyType { get; }
		public Type ValueType { get; }

		public VectorDictMapper(
			CandidVectorType candidType,
			Type type,
			Type keyType,
			Type valueType)
		{
			this.CandidType = candidType;
			this.Type = type;
			this.KeyType = keyType;
			this.ValueType = valueType;
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			CandidVector vector = value.AsVector();
			return vector.Values
				.Select(v =>
				{
					return v.AsRecord<(object, object)>(r =>
					{
						object key = converter.ToObject(this.KeyType, r.Fields[0]);
						object value = converter.ToObject(this.ValueType, r.Fields[1]);
						return (key, value);
					});
				})
				.ToDictionary(v => v.Item1, v => v.Item2);
		}

		public CandidValue Map(object obj, CandidConverter converter)
		{
			CandidValue[] values = ((Dictionary<object, object>)obj)
				.Select(d =>
				{
					Dictionary<CandidTag, CandidValue> fields = new()
					{
						[0] = converter.FromObject(d.Key),
						[1] = converter.FromObject(d.Value)
					};
					return new CandidRecord(fields);
				})
				.ToArray();
			return new CandidVector(values);
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
		}
	}
}