using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ICP.Candid.Tests
{
	public class CandidConverterTests
	{
		[Fact]
		public void Text_From_String()
		{
			string value = "Test";
			CandidValue expectedValue = CandidValue.Text(value);
			CandidType expectedType = new CandidPrimitiveType(PrimitiveType.Text);
			CandidTypedValue expected = CandidTypedValue.FromValueAndType(expectedValue, expectedType);

			this.Test(value, expected, (x, y) => x == y);
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
			CandidValue expectedValue = new CandidVector(values.Select(v => CandidValue.Text(v)).ToArray());
			CandidType expectedType = new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Text));
			CandidTypedValue expected = CandidTypedValue.FromValueAndType(expectedValue, expectedType);

			this.Test(values, expected, Enumerable.SequenceEqual);
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
			CandidValue expectedValue = new CandidVector(values.Select(v => CandidValue.Text(v)).ToArray());
			CandidType expectedType = new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Text));
			CandidTypedValue expected = CandidTypedValue.FromValueAndType(expectedValue, expectedType);

			this.Test(values, expected, Enumerable.SequenceEqual);
		}

		public class RecordClass
		{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public string StringField { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public int IntField { get; set; }

			public override bool Equals(object? obj)
			{
				if (obj is not RecordClass r)
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
			CandidTag stringFieldName = CandidTag.FromName("StringField");
			CandidTag intFieldName = CandidTag.FromName("IntField");
			var fields = new Dictionary<CandidTag, CandidValue>
			{
				{stringFieldName, CandidValue.Text("StringValue")},
				{intFieldName, CandidValue.Int32(2)}
			};
			CandidValue expectedValue = new CandidRecord(fields);

			var fieldTypes = new Dictionary<CandidTag, CandidType>
			{
				{stringFieldName, new CandidPrimitiveType(PrimitiveType.Text)},
				{intFieldName, new CandidPrimitiveType(PrimitiveType.Int32)}
			};
			CandidType expectedType = new CandidRecordType(fieldTypes);
			CandidTypedValue expected = CandidTypedValue.FromValueAndType(expectedValue, expectedType);

			this.Test(values, expected, (x, y) =>
			{
				return x.IntField == y.IntField
					&& x.StringField == y.StringField;
			});
		}



		[Variant(typeof(VariantValueClassType))]
		public class VariantValueClass
		{
			[VariantTagProperty]
			public VariantValueClassType Type { get; set; }
			[VariantValueProperty]
			public object? Value { get; set; }

		}

		public enum VariantValueClassType
		{
			[CandidName("v1")]
			V1,
			[CandidName("v2")]
			[VariantOptionType(typeof(string))]
			V2,
			[CandidName("v3")]
			[VariantOptionType(typeof(int))]
			V3,
			[CandidName("v4")]
			[VariantOptionType(typeof(OptionalValue<string>))]
			V4
		}

		[Fact]
		public void Variant_From_Class()
		{
			var variant = new VariantValueClass
			{
				Type = VariantValueClassType.V4,
				Value = OptionalValue<string>.WithValue("text")
			};
			CandidValue expectedValue = new CandidVariant("v4", new CandidOptional(CandidValue.Text("text")));

			var optionTypes = new Dictionary<CandidTag, CandidType>
			{
				{CandidTag.FromName("v1"), new CandidPrimitiveType(PrimitiveType.Null)},
				{CandidTag.FromName("v2"), new CandidPrimitiveType(PrimitiveType.Text)},
				{CandidTag.FromName("v3"), new CandidPrimitiveType(PrimitiveType.Int32)},
				{CandidTag.FromName("v4"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text))}
			};
			CandidType expectedType = new CandidVariantType(optionTypes);
			CandidTypedValue expected = CandidTypedValue.FromValueAndType(expectedValue, expectedType);

			this.Test(
				variant,
				expected,
				(x, y) =>
				{
					if (!ReferenceEquals(x.Value, null))
					{
						if (!ReferenceEquals(y.Value, null))
						{
							return x.Type == y.Type && x.Value!.Equals(y.Value);
						}
					}
					return false;
				});
		}






		private void Test<T>(T raw, CandidTypedValue candid, Func<T, T, bool> areEqual)
		{
			CandidTypedValue actual = CandidConverter.Default.FromObject(raw!);
			Assert.Equal(candid, actual);


			T? obj = CandidConverter.Default.ToObject<T>(candid.Value);
			Assert.NotNull(obj);
			Assert.True(areEqual(raw, obj!));
		}
	}
}
