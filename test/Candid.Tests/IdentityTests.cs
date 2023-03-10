using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Utilities;
using Snapshooter.Xunit;
using System.Threading.Tasks;
using Xunit;

namespace EdjCase.ICP.Candid.Tests
{
	public class IdentityTests
	{
		[Theory]
		[InlineData("303c300c060a2b0601040183b8430102032c000a00000000000000070101a451d1829b843e2aabdd49ea590668978a73612067bdde0b8502f844452a7558")]
		public void DerEncodedPublicKey__FromEd25519(string publicKeyHex)
		{
			byte[] publicKeyBytes = ByteUtil.FromHexString(publicKeyHex);
			var key = DerEncodedPublicKey.FromEd25519(publicKeyBytes);
			Snapshot.Match(key.Value);
		}
		[Theory]
		[InlineData("302a300506032b65700321005987fc68902ecf4644fe6c7d82b9f0e957817a8f9f8c58da5d2c9d3d19915229", "d8c5d3d2bcb82f16b399d40ed6ff1801bc5eb31fc301dc1ad517e2ed8c3f268e5987fc68902ecf4644fe6c7d82b9f0e957817a8f9f8c58da5d2c9d3d19915229")]
		public void ED25519Identity_Sign(string publicKeyHex, string privateKeyHex)
		{
			byte[] publicKeyBytes = ByteUtil.FromHexString(publicKeyHex);
			var publicKey = DerEncodedPublicKey.FromEd25519(publicKeyBytes);
			byte[] privateKeyBytes = ByteUtil.FromHexString(privateKeyHex);
			var identity = new Ed25519Identity(publicKey, privateKeyBytes);
			byte[] signature = identity.Sign(new byte[] { 1, 2, 3, 4 });
			Snapshot.Match(signature);
		}
	}
}
