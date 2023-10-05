using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{

	internal class RecordMapper : ICandidValueMapper
	{
		public CandidRecordType CandidType { get; }
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
				object? fieldValue;
				if (!record.Fields.TryGetValue(tag, out CandidValue fieldCandidValue))
				{
					if (!this.CandidType.Fields.TryGetValue(tag, out CandidType fieldType))
					{
						// Record has property that is not in the candid type, skip
						continue;
					}
					if (fieldType is not CandidOptionalType)
					{
						// Only throw if fieldType is not an opt value since the value is unset (null)
						// or has an extra
						throw new Exception($"Could not map candid record to type '{this.Type}'. Record is missing field '{tag}'");
					}
					// Set to optional value if not specified in record
					fieldCandidValue = new CandidOptional(null);
				}
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
				if (propValue == null)
				{
					v = new CandidOptional(null);
				}
				else if (property.CustomMapper != null)
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
