using ICP.Common;
using ICP.Common.Candid;
using System;
using System.Numerics;
using Xunit;

namespace Common.Tests
{
	public class NatTests
	{
		[Theory]
		[InlineData(0, "00")]
		[InlineData(16, "10")]
		[InlineData(543210, "EA9321")]
		[InlineData(624485, "E58E26")]
		public void Encode_Nat(ulong natValue, string expectedHex)
		{
			var nat = UnboundedUInt.FromUInt64(natValue);
			var candidNat = CandidPrimitive.Nat(nat);
			byte[] encodedValue = candidNat.EncodeValue();
			NatTests.AssertMatchesBytes(expectedHex, encodedValue);
		}

		[Fact]
		public void Encode_Nat_Big()
		{
			BigInteger bigInteger = BigInteger.Pow(2, 100) - 1;
			var nat = UnboundedUInt.FromBigInteger(bigInteger);
			var candidNat = CandidPrimitive.Nat(nat);
			byte[] encodedValue = candidNat.EncodeValue();
			NatTests.AssertMatchesBytes("FFFFFFFFFFFFFFFFFFFFFFFFFFFF03", encodedValue);
		}

		[Theory]
		[InlineData(0, "00")]
		[InlineData(16, "10")]
		[InlineData(543210, "EA4908")]
		public void Encode_Nat64(ulong natValue, string expectedHex)
		{
			var candidNat = CandidPrimitive.Nat64(natValue);
			byte[] encodedValue = candidNat.EncodeValue();
			NatTests.AssertMatchesBytes(expectedHex, encodedValue);
		}

		[Theory]
		[InlineData(0, "00")]
		[InlineData(16, "10")]
		[InlineData(543210, "EA4908")]
		public void Encode_Nat32(uint natValue, string expectedHex)
		{
			var candidNat = CandidPrimitive.Nat32(natValue);
			byte[] encodedValue = candidNat.EncodeValue();
			NatTests.AssertMatchesBytes(expectedHex, encodedValue);
		}

		[Theory]
		[InlineData(0, "00")]
		[InlineData(16, "10")]
		[InlineData(9999, "0F27")]
		public void Encode_Nat16(ushort natValue, string expectedHex)
		{
			var candidNat = CandidPrimitive.Nat16(natValue);
			byte[] encodedValue = candidNat.EncodeValue();
			NatTests.AssertMatchesBytes(expectedHex, encodedValue);
		}

		[Theory]
		[InlineData(0, "00")]
		[InlineData(16, "10")]
		[InlineData(99, "63")]
		public void Encode_Nat8(byte natValue, string expectedHex)
		{
			var candidNat = CandidPrimitive.Nat8(natValue);
			byte[] encodedValue = candidNat.EncodeValue();
			NatTests.AssertMatchesBytes(expectedHex, encodedValue);
		}

		private static void AssertMatchesBytes(string expectedHex, byte[] actualValue)
		{
			string actualHex = Convert.ToHexString(actualValue);
			Assert.Equal(expectedHex, actualHex);
		}
    }
}