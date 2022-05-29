using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ICP.Candid.Tests
{
	public class CandidConverterTests
	{
		[Fact]
		public void Text_From_String()
		{
			string value = "Test";
			CandidValue expectedValue = CandidPrimitive.Text(value);
			CandidType expectedType = new CandidPrimitiveType(PrimitiveType.Text);
			CandidValueWithType expected = CandidValueWithType.FromValueAndType(expectedValue, expectedType);

			this.Test(value, expected);
		}

		[Fact]
		public void Vector_From_List()
		{
			List<string> values = new List<string>
			{
				"1",
				"2",
				"3"
			};
			CandidValue expectedValue = new CandidVector(values.Select(v => CandidPrimitive.Text(v)).ToArray());
			CandidType expectedType = new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Text));
			CandidValueWithType expected = CandidValueWithType.FromValueAndType(expectedValue, expectedType);

			this.Test(values, expected);
		}

		[Fact]
		public void Vector_From_Array()
		{
			var values = new string[]
			{
				"1",
				"2",
				"3"
			};
			CandidValue expectedValue = new CandidVector(values.Select(v => CandidPrimitive.Text(v)).ToArray());
			CandidType expectedType = new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Text));
			CandidValueWithType expected = CandidValueWithType.FromValueAndType(expectedValue, expectedType);

			this.Test(values, expected);
		}

		public class RecordClass
		{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public string StringField { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public int IntField { get; set; }

			public override bool Equals(object? obj)
			{
				if(obj is not RecordClass r)
				{
					return false;
				}
				return this.StringField == r.StringField && this.IntField == r.IntField;
			}

			public override int GetHashCode()
			{
				return HashCode.Combine(this.StringField, this.IntField);
			}
		}
		[Fact]
		public void Record_From_Class()
		{
			var values = new RecordClass
			{
				StringField = "StringValue",
				IntField = 2
			};
			CandidTag stringFieldName = CandidTag.FromName("string_field");
			CandidTag intFieldName = CandidTag.FromName("int_field");
			var fields = new Dictionary<CandidTag, CandidValue>
			{
				{stringFieldName, CandidPrimitive.Text("StringValue")},
				{intFieldName, CandidPrimitive.Int32(2)}
			};
			CandidValue expectedValue = new CandidRecord(fields);

			var fieldTypes = new Dictionary<CandidTag, CandidType>
			{
				{stringFieldName, new CandidPrimitiveType(PrimitiveType.Text)},
				{intFieldName, new CandidPrimitiveType(PrimitiveType.Int32)}
			};
			CandidType expectedType = new CandidRecordType(fieldTypes);
			CandidValueWithType expected = CandidValueWithType.FromValueAndType(expectedValue, expectedType);

			this.Test(values, expected);
		}






		private void Test<T>(T raw, CandidValueWithType candid)
		{
			CandidValueWithType actual = CandidConverter.Default.FromObject(raw);
			Assert.Equal(candid, actual);


			T? obj = CandidConverter.Default.ToObject<T>(candid.Value);
			Assert.Equal(raw, obj);
		}
	}
}
