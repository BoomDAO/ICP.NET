using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{

	internal class RecordMapper : IObjectMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }
		public Dictionary<CandidTag, (PropertyInfo Property, IObjectMapper? OverrideMapper)> Properties { get; }

		public RecordMapper(CandidRecordType candidType, Type type, Dictionary<CandidTag, (PropertyInfo Property, IObjectMapper? OverrideMapper)> properties)
		{
			this.CandidType = candidType ?? throw new ArgumentNullException(nameof(candidType));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.Properties = properties;
		}

		public object Map(CandidValue value, CandidConverterOptions options)
		{
			CandidRecord record = value.AsRecord();
			object obj = Activator.CreateInstance(this.Type);
			foreach ((CandidTag tag, (PropertyInfo property, IObjectMapper? overrideMapper)) in this.Properties)
			{
				CandidValue fieldCandidValue = record.Fields[tag];
				IObjectMapper mapper = overrideMapper ?? options.ResolveMapper(property.PropertyType);
				object? fieldValue = mapper.Map(fieldCandidValue, options);
				property.SetValue(obj, fieldValue);
			}
			return obj;
		}

		public CandidTypedValue Map(object obj, CandidConverterOptions options)
		{
			Dictionary<CandidTag, CandidValue> fields = new();
			foreach ((CandidTag tag, (PropertyInfo property, IObjectMapper? overrideMapper)) in this.Properties)
			{
				object propValue = property.GetValue(obj);
				IObjectMapper mapper = overrideMapper ?? options.ResolveMapper(property.PropertyType);
				CandidTypedValue v = mapper.Map(propValue, options);
				fields.Add(tag, v.Value);
			}
			var r = new CandidRecord(fields);
			return new CandidTypedValue(
				r,
				this.CandidType
			);
		}
	}
}
