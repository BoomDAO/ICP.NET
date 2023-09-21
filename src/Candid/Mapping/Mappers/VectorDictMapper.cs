using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
			IDictionary obj = (IDictionary)Activator.CreateInstance(this.Type);

			foreach(CandidValue element in vector.Values)
			{
				CandidRecord r = element.AsRecord();
				object k = converter.ToObject(this.KeyType, r.Fields[0]);
				object v = converter.ToObject(this.ValueType, r.Fields[1]);
				obj.Add(k, v);
			}
			return obj;
		}

		public CandidValue Map(object obj, CandidConverter converter)
		{
			List<CandidValue> values = new ();
			foreach (DictionaryEntry value in (IDictionary)obj)
			{
				Dictionary<CandidTag, CandidValue> fields = new()
				{
					[0] = converter.FromObject(value.Key),
					[1] = converter.FromObject(value.Value)
				};
				values.Add(new CandidRecord(fields));
			}
			return new CandidVector(values.ToArray());
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
		}
	}
}