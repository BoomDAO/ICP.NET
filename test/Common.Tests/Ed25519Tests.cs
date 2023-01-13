using EdjCase.ICP.Agent.Keys;
using EdjCase.ICP.Candid.Utilities;
using Xunit;

namespace ICP.Candid.Tests
{
	public class Ed25519Tests
	{
		[Theory]
		[InlineData("B3997656BA51FF6DA37B61D8D549EC80717266ECF48FB5DA52B654412634844C", "302A300506032B6570032100B3997656BA51FF6DA37B61D8D549EC80717266ECF48FB5DA52B654412634844C")]
		[InlineData("A5AFB5FEB6DFB6DDF5DD6563856FFF5484F5FE304391D9ED06697861F220C610", "302A300506032B6570032100A5AFB5FEB6DFB6DDF5DD6563856FFF5484F5FE304391D9ED06697861F220C610")]
		[InlineData("C8413108F121CB794A10804D15F613E40ECC7C78A4EC567040DDF78467C71DFF", "302A300506032B6570032100C8413108F121CB794A10804D15F613E40ECC7C78A4EC567040DDF78467C71DFF")]
		public void PubliKey_Der_Encoding(string publicKeyHex, string derEncodedPublicKeyHex)
		{
			byte[] publicKeyBytes = ByteUtil.FromHexString(publicKeyHex);
			var publicKey = new Ed25519PublicKey(publicKeyBytes);

			byte[] expectedDer = ByteUtil.FromHexString(derEncodedPublicKeyHex);

			Ed25519PublicKey key = Ed25519PublicKey.FromDer(expectedDer);

			Assert.Equal(publicKeyBytes, key.GetRawBytes());

			byte[] actualDer = publicKey.GetDerEncodedBytes();

			Assert.Equal(expectedDer, actualDer);
		}
	}
}
