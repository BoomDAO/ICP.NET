using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{

	internal class RecordMapper : ICandidValueMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }
		public Dictionary<CandidTag, (PropertyInfo Property, ICandidValueMapper? OverrideMapper)> Properties { get; }

		public RecordMapper(CandidRecordType candidType, Type type, Dictionary<CandidTag, (PropertyInfo Property, ICandidValueMapper? OverrideMapper)> properties)
		{
			this.CandidType = candidType ?? throw new ArgumentNullException(nameof(candidType));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.Properties = properties;
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			CandidRecord record = value.AsRecord();
			object obj = Activator.CreateInstance(this.Type);
			foreach ((CandidTag tag, (PropertyInfo property, ICandidValueMapper? overrideMapper)) in this.Properties)
			{
				CandidValue fieldCandidValue = record.Fields[tag];
				object? fieldValue;
				if (overrideMapper != null)
				{
					fieldValue = overrideMapper.Map(fieldCandidValue, converter);
				}
				else
				{
					fieldValue = converter.ToObject(property.PropertyType, fieldCandidValue);
				}
				property.SetValue(obj, fieldValue);
			}
			return obj;
		}

		public CandidValue Map(object value, CandidConverter converter)
		{
			Dictionary<CandidTag, CandidValue> fields = new();
			foreach ((CandidTag tag, (PropertyInfo property, ICandidValueMapper? overrideMapper)) in this.Properties)
			{
				object propValue = property.GetValue(value);
				CandidValue v;
				if (overrideMapper != null)
				{
					v = overrideMapper.Map(propValue, converter);
				}
				else
				{
					v = converter.FromObject(propValue);
				}
				fields.Add(tag, v);
			}
			return new CandidRecord(fields);
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
		}
	}
}
