using EdjCase.ICP.BLS;
using EdjCase.ICP.Candid.Utilities;
using System.Security.Cryptography.X509Certificates;

namespace BLS.Tests
{
	public class DefaultBlsCryptographyTests
	{
		[Theory]
		// TODO ICP BLS
		//[InlineData(
		//	"814c0e6ec71fab583b08bd81373c255c3c371b2e84863c98a4f1e08b74235d14fb5d9c0cd546d9685f913a0c0b2cc5341583bf4b4392e467db96d65b9bb4cb717112f8472e0d5a4d14505ffd7484b01291091c5f87b98883463f98091a0baaae",
		//	"0d69632d73746174652d726f6f74e6c01e909b4923345ce5970962bcfe3004bfd8474a21dae28f50692502f46d90",
		//	"ace9fcdd9bc977e05d6328f889dc4e7c99114c737a494653cb27a1f55c06f4555e0f160980af5ead098acc195010b2f7"
		//)]
		//[InlineData(
		//	"9933e1f89e8a3c4d7fdcccdbd518089e2bd4d8180a261f18d9c247a52768ebce98dc7328a39814a8f911086a1dd50cbe015e2a53b7bf78b55288893daa15c346640e8831d72a12bdedd979d28470c34823b8d1c3f4795d9c3984a247132e94fe",
		//	"0d69632d73746174652d726f6f74b294b418b11ebe5dd7dd1dcb099e4e0372b9a42aef7a7a37fb4f25667d705ea9",
		//	"89a2be21b5fa8ac9fab1527e041327ce899d7da971436a1f2165393947b4d942365bfe5488710e61a619ba48388a21b1"
		//)]
		[InlineData(
			// G1 84c5ba8cd2507bdde36e4697d027f0b895079871c587eec363cd7dba7fbb05fc4abc78c0150f4696aaf88e1043a973c3
			// G2 a8aab303e33ed14f4a904004a92bd26ffc969c1d1e7d4b7f0c04150a73e1845a911e51a2b2d369d5cef06560c5ac9f5715c01566993d4469805df3e1f29b536481a832bf2751b6908faed6776d062d585521889232999d72b679d6e38bb5cfff
			// Sig G2 b53cfdf8b488a286df1ed20432e2bbc4e6361003757dfda3a4fd6cd98de95e5513f7c448d70b2681e14547a6ced47e7c10e28432e8abcb34de1dc28f39328fd2a13db12a4c6a30bd17b0e42881a429003e4c24583ba0f29a40fd836cf05e1a40
			//"84c5ba8cd2507bdde36e4697d027f0b895079871c587eec363cd7dba7fbb05fc4abc78c0150f4696aaf88e1043a973c3",
			"b2be11dc8e54ee74dbc07569fd74fe03b5f52ad71cd49a8579b6c6387891f5a20ad980ec2747618c1b9ad35846a68a3e",
			"",
			"b53cfdf8b488a286df1ed20432e2bbc4e6361003757dfda3a4fd6cd98de95e5513f7c448d70b2681e14547a6ced47e7c10e28432e8abcb34de1dc28f39328fd2a13db12a4c6a30bd17b0e42881a429003e4c24583ba0f29a40fd836cf05e1a40"
		)]
		public void VerifySignature(string publicKeyHex, string msgHex, string signatureHex)
		{
			byte[] publicKey = ByteUtil.FromHexString(publicKeyHex);
			byte[] msg = ByteUtil.FromHexString(msgHex);
			byte[] signature = ByteUtil.FromHexString(signatureHex);
			
			bool isValid = new DefaultBlsCryptograhy().VerifySignature(publicKey, msg, signature);
			Assert.True(isValid);
		}

		//[Fact]
		//public void BasicAggregation()
		//{
		//	var rng = new ChaCha8Rng(12); // Assuming ChaCha8Rng is initialized with a seed like this

		//	int numMessages = 10;

		//	// Generate private keys
		//	List<PrivateKey> privateKeys = Enumerable.Range(0, numMessages)
		//		.Select(_ => PrivateKey.Generate(rng))
		//		.ToList();

		//	// Generate messages
		//	List<byte[]> messages = Enumerable.Range(0, numMessages)
		//		.Select(_ => Enumerable.Range(0, 64).Select(__ => rng.Gen<byte>()).ToArray()) // Assuming rng.Gen<byte>() generates a random byte
		//		.ToList();

		//	// Sign messages
		//	List<Signature> sigs = messages.Zip(privateKeys, (message, pk) => pk.Sign(message)).ToList();

		//	Signature aggregatedSignature = Signature.Aggregate(sigs); // Assuming Aggregate method exists and does not return null

		//	// Hash messages
		//	List<Hash> hashes = messages.Select(message => Hash.Compute(message)).ToList(); // Assuming Hash.Compute method exists

		//	// Extract public keys
		//	List<PublicKey> publicKeys = privateKeys.Select(pk => pk.PublicKey).ToList(); // Assuming PrivateKey has a PublicKey property

		//	// Verify aggregated signature
		//	bool isVerified = aggregatedSignature.Verify(hashes, publicKeys); // Assuming Verify method exists and matches the required signature
		//	Assert.True(isVerified, "Failed to verify");

		//	// Verify messages directly, assuming VerifyMessages method exists and matches the required signature
		//	Assert.True(aggregatedSignature.VerifyMessages(messages, publicKeys));
		//}

	}
}
