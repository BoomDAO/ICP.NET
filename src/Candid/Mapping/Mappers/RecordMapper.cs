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
		public Dictionary<CandidTag, PropertyMetaData> Properties { get; }

		public RecordMapper(CandidRecordType candidType, Type type, Dictionary<CandidTag, PropertyMetaData> properties)
		{
			this.CandidType = candidType ?? throw new ArgumentNullException(nameof(candidType));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.Properties = properties;
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			CandidRecord record = value.AsRecord();
			object obj = Activator.CreateInstance(this.Type);
			foreach ((CandidTag tag, PropertyMetaData property) in this.Properties)
			{
				if (!record.Fields.TryGetValue(tag, out CandidValue fieldCandidValue))
				{
					throw new Exception($"Could not map candid record to type '{this.Type}'. Record is missing field '{tag}'");
				}
				object? fieldValue;
				if (property.CustomMapper != null)
				{
					fieldValue = property.CustomMapper.Map(fieldCandidValue, converter);
				}
				else
				{
					fieldValue = converter.ToObject(property.PropertyInfo.PropertyType, fieldCandidValue);
				}
				property.PropertyInfo.SetValue(obj, fieldValue);
			}
			return obj;
		}

		public CandidValue Map(object value, CandidConverter converter)
		{
			Dictionary<CandidTag, CandidValue> fields = new();
			foreach ((CandidTag tag, PropertyMetaData property) in this.Properties)
			{
				object propValue = property.PropertyInfo.GetValue(value);
				CandidValue v;
				if (property.CustomMapper != null)
				{
					v = property.CustomMapper.Map(propValue, converter);
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
