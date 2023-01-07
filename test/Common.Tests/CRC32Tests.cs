using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ICP.Candid.Tests
{
	public class CRC32Tests
	{
		[Theory]
		[InlineData("000102030405060708", "BCE14302")]
		[InlineData("00", "D202EF8D")]
		[InlineData("", "00000000")]
		[InlineData("0102030405060708091011121314151617181920212223242526272829", "421A4315")]
		public void ComputeHash(string hexData, string expectedHashHex)
		{
			byte[] data = ByteUtil.FromHexString(hexData);
			byte[] actualHash = CRC32.ComputeHash(data);
			string actualHashHex = ByteUtil.ToHexString(actualHash);

			Assert.Equal(expectedHashHex, actualHashHex);
		}

		// TODO
		//[Theory]
		//[InlineData("0")]
		//[InlineData("000")]
		//[InlineData("010203040506070809101112131415161718192021222324252627282930")]
		//public void ComputeHash_Error_Length(string hexData)
		//{
		//
		//}

		// TODO
		//[Theory]
		//[InlineData("0g")]
		//public void ComputeHash_Error_InvalidCharacters(string hexData)
		//{
		//
		//}
	}
}
