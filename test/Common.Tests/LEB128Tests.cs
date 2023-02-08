using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.IO;
using Xunit;

namespace EdjCase.ICP.Candid.Tests
{
	public class LEB128Tests
	{
		[Theory]
		[InlineData(0, "00")]
		[InlineData(1, "01")]
		[InlineData(9, "09")]
		[InlineData(10, "0A")]
		[InlineData(127, "7F")]
		[InlineData(255, "FF01")]
		[InlineData(256, "8002")]
		[InlineData(624485, "E58E26")]
		[InlineData(10000000000000, "80C0CAF384A302")]
		[InlineData(ulong.MaxValue, "FFFFFFFFFFFFFFFFFF01")]
		public void Unsinged(ulong nat64, string expectedLEB128Hex)
		{
			byte[] value = LEB128.EncodeUnsigned(nat64);
			string hexValue = value.ToHexString();
			Assert.Equal(expectedLEB128Hex, hexValue);

			using (MemoryStream stream = new (value))
			{
				UnboundedUInt v = LEB128.DecodeUnsigned(stream);
				Assert.True(v.TryToUInt64(out ulong l));
				Assert.Equal(nat64, l);
			}
		}

		[Theory]
		[InlineData(-10000000000000, "80C0B58CFBDC7D")]
		[InlineData(-256, "807E")]
		[InlineData(-10, "76")]
		[InlineData(-9, "77")]
		[InlineData(-1, "7F")]
		[InlineData(0, "00")]
		[InlineData(1, "01")]
		[InlineData(9, "09")]
		[InlineData(10, "0A")]
		[InlineData(256, "8002")]
		[InlineData(10000000000000, "80C0CAF384A302")]
		[InlineData(long.MaxValue, "FFFFFFFFFFFFFFFFFF00")]
		public void Signed(long int64, string expectedLEB128Hex)
		{
			byte[] value = LEB128.EncodeSigned(int64);
			string hexValue = value.ToHexString();
			Assert.Equal(expectedLEB128Hex, hexValue);
			using (MemoryStream stream = new (value))
			{
				UnboundedInt v = LEB128.DecodeSigned(stream);
				Assert.True(v.TryToInt64(out long l));
				Assert.Equal(int64, l);
			}
		}

	}
}
