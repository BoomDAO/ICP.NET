using ICP.Common;
using ICP.Common.Candid;
using System;
using System.Numerics;
using Xunit;

namespace Common.Tests
{
	public class IntTests
	{
		[Theory]
		[InlineData(0, "00")]
		[InlineData(16, "10")]
		[InlineData(-15, "71")]
		[InlineData(624485, "E58E26")]
		[InlineData(-123456, "C0BB78")]
		public void Encode_Int(long intValue, string expectedHex)
		{
			var @int = UnboundedInt.FromInt64(intValue);
			var candidInt = CandidPrimitive.Int(@int);
			byte[] encodedValue = candidInt.EncodeValue();
			IntTests.AssertMatchesBytes(expectedHex, encodedValue);
		}

		[Fact]
		public void Encode_Int_Big()
		{
			BigInteger bigInteger = BigInteger.Pow(2, 100) - 1;
			var @int = UnboundedInt.FromBigInteger(bigInteger);
			var candidInt = CandidPrimitive.Int(@int);
			byte[] encodedValue = candidInt.EncodeValue();
			IntTests.AssertMatchesBytes("FFFFFFFFFFFFFFFFFFFFFFFFFFFF03", encodedValue);
		}

		[Theory]
		[InlineData(0, "00")]
		[InlineData(16, "10")]
		[InlineData(-15, "F1")]
		[InlineData(543210, "EA4908")]
		public void Encode_Int64(long intValue, string expectedHex)
		{
			var candidInt = CandidPrimitive.Int64(intValue);
			byte[] encodedValue = candidInt.EncodeValue();
			IntTests.AssertMatchesBytes(expectedHex, encodedValue);
		}

		[Theory]
		[InlineData(0, "00")]
		[InlineData(16, "10")]
		[InlineData(-15, "F1")]
		[InlineData(543210, "EA4908")]
		public void Encode_Int32(int intValue, string expectedHex)
		{
			var candidInt = CandidPrimitive.Int32(intValue);
			byte[] encodedValue = candidInt.EncodeValue();
			IntTests.AssertMatchesBytes(expectedHex, encodedValue);
		}

		[Theory]
		[InlineData(0, "00")]
		[InlineData(16, "10")]
		[InlineData(-15, "F1")]
		[InlineData(9999, "0F27")]
		public void Encode_Int16(short intValue, string expectedHex)
		{
			var candidInt = CandidPrimitive.Int16(intValue);
			byte[] encodedValue = candidInt.EncodeValue();
			IntTests.AssertMatchesBytes(expectedHex, encodedValue);
		}

		[Theory]
		[InlineData(0, "00")]
		[InlineData(16, "10")]
		[InlineData(99, "63")]
		public void Encode_Int8(byte intValue, string expectedHex)
		{
			var candidInt = CandidPrimitive.Int8((sbyte)intValue);
			byte[] encodedValue = candidInt.EncodeValue();
			IntTests.AssertMatchesBytes(expectedHex, encodedValue);
		}
		[Fact]
		public void Encode_Int8_Neg()
		{
			var candidInt = CandidPrimitive.Int8((sbyte)-15);
			byte[] encodedValue = candidInt.EncodeValue();
			IntTests.AssertMatchesBytes("F1", encodedValue);
		}

		private static void AssertMatchesBytes(string expectedHex, byte[] actualValue)
		{
			string actualHex = Convert.ToHexString(actualValue);
			Assert.Equal(expectedHex, actualHex);
		}
    }
}