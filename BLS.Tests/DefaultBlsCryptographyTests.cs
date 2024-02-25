using EdjCase.ICP.BLS;
using EdjCase.ICP.BLS.Models;
using EdjCase.ICP.Candid.Utilities;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace BLS.Tests
{
	public class DefaultBlsCryptographyTests
	{
		[Theory]
		[InlineData(
			"814c0e6ec71fab583b08bd81373c255c3c371b2e84863c98a4f1e08b74235d14fb5d9c0cd546d9685f913a0c0b2cc5341583bf4b4392e467db96d65b9bb4cb717112f8472e0d5a4d14505ffd7484b01291091c5f87b98883463f98091a0baaae",
			"0d69632d73746174652d726f6f74e6c01e909b4923345ce5970962bcfe3004bfd8474a21dae28f50692502f46d90",
			"ace9fcdd9bc977e05d6328f889dc4e7c99114c737a494653cb27a1f55c06f4555e0f160980af5ead098acc195010b2f7"
		)]
		[InlineData(
			"9933e1f89e8a3c4d7fdcccdbd518089e2bd4d8180a261f18d9c247a52768ebce98dc7328a39814a8f911086a1dd50cbe015e2a53b7bf78b55288893daa15c346640e8831d72a12bdedd979d28470c34823b8d1c3f4795d9c3984a247132e94fe",
			"0d69632d73746174652d726f6f74b294b418b11ebe5dd7dd1dcb099e4e0372b9a42aef7a7a37fb4f25667d705ea9",
			"89a2be21b5fa8ac9fab1527e041327ce899d7da971436a1f2165393947b4d942365bfe5488710e61a619ba48388a21b1"
		)]
		public void VerifySignature(string publicKeyHex, string msgHex, string signatureHex)
		{
			byte[] publicKey = ByteUtil.FromHexString(publicKeyHex);
			byte[] msg = ByteUtil.FromHexString(msgHex);
			byte[] signature = ByteUtil.FromHexString(signatureHex);

			bool isValid = new DefaultBlsCryptograhy().VerifySignature(publicKey, msg, signature);
			Assert.True(isValid);
		}


		[Theory]
		[InlineData(
			"b2be11dc8e54ee74dbc07569fd74fe03b5f52ad71cd49a8579b6c6387891f5a20ad980ec2747618c1b9ad35846a68a3e",
			"",
			"b53cfdf8b488a286df1ed20432e2bbc4e6361003757dfda3a4fd6cd98de95e5513f7c448d70b2681e14547a6ced47e7c10e28432e8abcb34de1dc28f39328fd2a13db12a4c6a30bd17b0e42881a429003e4c24583ba0f29a40fd836cf05e1a40"

		)]
		public void VerifyG1PubKey(string publicKeyHex, string msgHex, string signatureHex)
		{
			byte[] publicKey = ByteUtil.FromHexString(publicKeyHex);
			byte[] msg = ByteUtil.FromHexString(msgHex);
			byte[] signature = ByteUtil.FromHexString(signatureHex);

			G1Projective[] g1Values = new[]
			{
				G1Projective.FromCompressed(publicKey)
			};
			G2Projective[] g2Values = new[]
			{
				G2Projective.HashToCurve(msg, DefaultBlsCryptograhy.DstG2)
			};
			G2Affine sig = G2Affine.FromCompressed(signature);
			bool isValid = new DefaultBlsCryptograhy().VerifyG2Signature(sig, g2Values, g1Values);
			Assert.True(isValid);
		}


		[Theory]
		[InlineData(
			"814c0e6ec71fab583b08bd81373c255c3c371b2e84863c98a4f1e08b74235d14fb5d9c0cd546d9685f913a0c0b2cc5341583bf4b4392e467db96d65b9bb4cb717112f8472e0d5a4d14505ffd7484b01291091c5f87b98883463f98091a0baaae",
			"0d69632d73746174652d726f6f74e6c01e909b4923345ce5970962bcfe3004bfd8474a21dae28f50692502f46d90",
			"ace9fcdd9bc977e05d6328f889dc4e7c99114c737a494653cb27a1f55c06f4555e0f160980af5ead098acc195010b2f7"
		)]
		[InlineData(
			"9933e1f89e8a3c4d7fdcccdbd518089e2bd4d8180a261f18d9c247a52768ebce98dc7328a39814a8f911086a1dd50cbe015e2a53b7bf78b55288893daa15c346640e8831d72a12bdedd979d28470c34823b8d1c3f4795d9c3984a247132e94fe",
			"0d69632d73746174652d726f6f74b294b418b11ebe5dd7dd1dcb099e4e0372b9a42aef7a7a37fb4f25667d705ea9",
			"89a2be21b5fa8ac9fab1527e041327ce899d7da971436a1f2165393947b4d942365bfe5488710e61a619ba48388a21b1"
		)]
		[InlineData(
			"a7623a93cdb56c4d23d99c14216afaab3dfd6d4f9eb3db23d038280b6d5cb2caaee2a19dd92c9df7001dede23bf036bc0f33982dfb41e8fa9b8e96b5dc3e83d55ca4dd146c7eb2e8b6859cb5a5db815db86810b8d12cee1588b5dbf34a4dc9a5",
			"hello",
			"b89e13a212c830586eaa9ad53946cd968718ebecc27eda849d9232673dcd4f440e8b5df39bf14a88048c15e16cbcaabe"
		)]
		public void VerifyG2PubKey(string publicKeyHex, string msgHex, string signatureHex)
		{
			byte[] publicKey = ByteUtil.FromHexString(publicKeyHex);
			byte[] msg = ByteUtil.FromHexString(msgHex);
			byte[] signature = ByteUtil.FromHexString(signatureHex);

			G1Projective[] g1Values = new[]
			{
				G1Projective.HashToCurve(msg, DefaultBlsCryptograhy.DstG1)
			};
			G2Projective[] g2Values = new[]
			{
				G2Projective.FromCompressed(publicKey)
			};
			G1Affine sig = G1Affine.FromCompressed(signature);
			bool isValid = new DefaultBlsCryptograhy().VerifyG1Signature(sig, g2Values, g1Values);
			Assert.True(isValid);
		}

		[Theory]
		[InlineData(
			"",
			"hMW6jNJQe93jbkaX0CfwuJUHmHHFh+7DY819un+7BfxKvHjAFQ9Glqr4jhBDqXPD"
		)]
		public void HashToCurveG1(
			string msgHex,
			string g1CompressedBase64
		)
		{
			byte[] msg = ByteUtil.FromHexString(msgHex);
			byte[] g1Compressed = Convert.FromBase64String(g1CompressedBase64);
			G1Projective g1Expected = G1Projective.FromCompressed(g1Compressed);

			G1Projective g1Actual = G1Projective.HashToCurve(msg, DefaultBlsCryptograhy.DstG2);

			Assert.True(g1Expected.Equals(g1Actual));
		}
		[Theory]
		[InlineData(
			"",
			"qKqzA+M+0U9KkEAEqSvSb/yWnB0efUt/DAQVCnPhhFqRHlGistNp1c7wZWDFrJ9XFcAVZpk9RGmAXfPh8ptTZIGoMr8nUbaQj67Wd20GLVhVIYiSMpmdcrZ51uOLtc//"
		)]
		public void HashToCurveG2(
			string msgHex,
			string g2CompressedBase64
		)
		{
			byte[] msg = ByteUtil.FromHexString(msgHex);
			byte[] g2Compressed = Convert.FromBase64String(g2CompressedBase64);
			G2Projective g2Expected = G2Projective.FromCompressed(g2Compressed);

			G2Projective g2Actual = G2Projective.HashToCurve(msg, DefaultBlsCryptograhy.DstG2);

			Assert.True(g2Expected.Equals(g2Actual));
		}


	}
}
