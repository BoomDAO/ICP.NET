using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{

	internal class TupleMapper : ICandidValueMapper
	{
		public CandidType CandidType { get; }

		public Type Type { get; }
		public List<Type> Types { get; }

		public TupleMapper(Type type, List<(Type, CandidType)> tupleTypes)
		{
			Dictionary<CandidTag, CandidType> fields = tupleTypes
				.Select((t, i) => (Index: i, Type: t.Item2))
				.ToDictionary(t => CandidTag.FromId((uint)t.Index), t => t.Type);
			this.Type = type;
			this.CandidType = new CandidRecordType(fields);
			this.Types = tupleTypes.Select(t => t.Item1).ToList();
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			CandidRecord record = value.AsRecord();
			object obj = Activator.CreateInstance(this.Type);
			static void SetValue(object tuple, int itemIndex, object? itemValue)
			{
				if (itemIndex >= 7)
				{
					// Index 7+ is located in the "Rest" which is a nested tuple
					object restTuple = tuple.GetType().GetField("Rest").GetValue(tuple);
					SetValue(restTuple, itemIndex - 7, itemValue);
				}
				else
				{
					tuple.GetType().GetField("Item" + (itemIndex + 1)).SetValue(tuple, itemValue);
				}
			}
			uint i = 0;
			foreach (Type fieldType in this.Types)
			{
				CandidTag fieldId = CandidTag.FromId(i);
				if (!record.Fields.TryGetValue(fieldId, out CandidValue fieldCandidValue))
				{
					throw new Exception($"Could not map candid record to type '{this.Type}'. Record is missing field '{fieldId}'");
				}
				object? fieldValue = converter.ToObject(fieldType, fieldCandidValue);
				SetValue(obj, (int)i, fieldValue);
				i++;
			}
			return obj;
		}

		public CandidValue Map(object value, CandidConverter converter)
		{
			ITuple tuple = (ITuple)value;
			Dictionary<CandidTag, CandidValue> fields = new();
			int i = 0;
			foreach (Type type in this.Types)
			{
				object propValue = tuple[i];
				CandidValue v = converter.FromObject(propValue);
				fields.Add(CandidTag.FromId((uint)i), v);
				i++;
			}
			return new CandidRecord(fields);
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
		}
	}
}
