using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EdjCase.ICP.Candid.Tests
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
		[Fact]
		public void Vector_From_Dict()
		{
			var values = new Dictionary<string, UnboundedUInt>
			{
				["1"] = 1,
				["2"] = 2,
				["3"] = 3
			};
			CandidValue[] elements = values
				.Select(v => new CandidRecord(new Dictionary<CandidTag, CandidValue>
				{
					[0] = CandidValue.Text(v.Key),
					[1] = CandidValue.Nat(v.Value)
				}))
				.ToArray();
			CandidValue expectedValue = new CandidVector(elements);
			CandidType expectedType = new CandidVectorType(new CandidRecordType(new Dictionary<CandidTag, CandidType>
			{
				[0] = CandidType.Text(),
				[1] = CandidType.Nat()
			}));
			CandidTypedValue expected = CandidTypedValue.FromValueAndType(expectedValue, expectedType);

			this.Test(values, expected, Enumerable.SequenceEqual);
		}

		public class RecordClass
		{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public OptionalValue<string> StringField { get; set; }
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
				StringField = OptionalValue<string>.WithValue("StringValue"),
				IntField = 2
			};
			CandidTag stringFieldName = CandidTag.FromName("StringField");
			CandidTag intFieldName = CandidTag.FromName("IntField");
			var fields = new Dictionary<CandidTag, CandidValue>
			{
				{stringFieldName, new CandidOptional(CandidValue.Text("StringValue"))},
				{intFieldName, CandidValue.Int32(2)}
			};
			CandidValue expectedValue = new CandidRecord(fields);

			var fieldTypes = new Dictionary<CandidTag, CandidType>
			{
				{stringFieldName, new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text))},
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


		public class RecordNullableFieldClass
		{
			public string? StringField { get; set; }
			public int IntField { get; set; }

			public override bool Equals(object? obj)
			{
				if (obj is not RecordNullableFieldClass r)
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




		[Variant]
		public class VariantValueClass
		{
			[VariantTagProperty]
			public VariantValueClassType Type { get; set; }
			[VariantValueProperty]
			public object? Value { get; set; }


			public int AsV3()
			{
				return (int)this.Value!;
			}
			public OptionalValue<string> AsV4()
			{
				return (OptionalValue<string>)this.Value!;
			}
		}

		public enum VariantValueClassType
		{
			[CandidName("v1")]
			V1,
			[CandidName("v2")]
			V2,
			[CandidName("v3")]
			V3,
			[CandidName("v4")]
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


		[Fact]
		public void Tuple_From_Class()
		{
			var tuple = new Arg0ItemRecord("f0Value", "f1Value");

			CandidRecord value = new CandidRecord(new Dictionary<CandidTag, CandidValue>
			{
				[0] = CandidValue.Text("f0Value"),
				[1] = CandidValue.Text("f1Value")
			});
			CandidType type = new CandidRecordType(new Dictionary<CandidTag, CandidType>
			{
				[0] = CandidType.Text(),
				[1] = CandidType.Text()
			});
			CandidTypedValue expected = CandidTypedValue.FromValueAndType(value, type);
			this.Test(
				tuple,
				expected,
				(x, y) =>
				{
					return x.F0 == y.F0 && x.F1 == y.F1;
				}
			);
		}

		public class Arg0ItemRecord
		{
			[CandidTag(0U)] public string F0 { get; set; }

			[CandidTag(1U)] public string F1 { get; set; }

			public Arg0ItemRecord(string f0, string f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public Arg0ItemRecord()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			{
			}
		}



		[Fact]
		public void Optional_Type_Coercion()
		{
			OptTypeCoercion o = new()
			{
				TextValue = "Text1",
				ListOfText = new List<string?> { "Text2", null }
			};

			CandidValue oT = new CandidOptional(
				new CandidRecord(
					new Dictionary<CandidTag, CandidValue>
					{
						["TextValue"] = new CandidOptional(
							CandidValue.Text("Text1")
						),
						["ListOfText"] = new CandidOptional(
							new CandidVector(new CandidValue[]
							{
								new CandidOptional(CandidValue.Text("Text2")),
								new CandidOptional()
							})
						)
					}
				)
			);

			OptTypeCoercion actual = CandidConverter.Default.ToObject<OptTypeCoercion>(oT);
			Assert.Equal(o.TextValue, actual.TextValue);
			Assert.Equal(o.ListOfText, actual.ListOfText);

		}

		public class OptOverride
		{

			[CandidOptional]
			public string? OptValue { get; set; }
			public OptionalValue<string> OptValue2 { get; set; } = OptionalValue<string>.NoValue();
		}

		[Fact]
		public void OptionalValueOverride_Record_WithValue()
		{
			var raw = new OptOverride { OptValue = "Test1", OptValue2 = OptionalValue<string>.WithValue("Test2") };
			var candidValue = new CandidRecord(new Dictionary<CandidTag, CandidValue>
			{
				["OptValue"] = new CandidOptional(CandidValue.Text("Test1")),
				["OptValue2"] = new CandidOptional(CandidValue.Text("Test2")),
			});
			var candidType = new CandidRecordType(new Dictionary<CandidTag, CandidType>
			{
				["OptValue"] = new CandidOptionalType(CandidType.Text()),
				["OptValue2"] = new CandidOptionalType(CandidType.Text()),
			});
			var candid = new CandidTypedValue(candidValue, candidType);
			this.Test(raw, candid, (a, b) => a.OptValue == b.OptValue && a.OptValue2 == b.OptValue2);
		}
		[Fact]
		public void OptionalValueOverride_Record_Null()
		{
			var raw = new OptOverride { OptValue = null, OptValue2 = OptionalValue<string>.NoValue() };
			var candidValue = new CandidRecord(new Dictionary<CandidTag, CandidValue>
			{
				["OptValue"] = new CandidOptional(),
				["OptValue2"] = new CandidOptional(),
			});
			var candidType = new CandidRecordType(new Dictionary<CandidTag, CandidType>
			{
				["OptValue"] = new CandidOptionalType(CandidType.Text()),
				["OptValue2"] = new CandidOptionalType(CandidType.Text()),
			});
			var candid = new CandidTypedValue(candidValue, candidType);
			this.Test(raw, candid, (a, b) => a.OptValue == b.OptValue && a.OptValue2 == b.OptValue2);
		}

		public class OptTypeCoercion
		{
			public string? TextValue { get; set; }
			public List<string?>? ListOfText { get; set; }

		}



		private void Test<T>(T raw, CandidTypedValue candid, Func<T, T, bool> areEqual)
			where T : notnull
		{
			CandidTypedValue actual = CandidConverter.Default.FromTypedObject(raw!);
			Assert.Equal(candid, actual);


			T? obj = CandidConverter.Default.ToObject<T>(candid.Value);
			Assert.NotNull(obj);
			Assert.True(areEqual(raw, obj!));
		}
	}
}
