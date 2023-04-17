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
		public void SubjectPublicKeyInfo__FromEd25519(string publicKeyHex)
		{
			byte[] publicKeyBytes = ByteUtil.FromHexString(publicKeyHex);
			var key = SubjectPublicKeyInfo.Ed25519(publicKeyBytes);
			Snapshot.Match(key);
		}

		[Theory]
		[InlineData("302a300506032b65700321005987fc68902ecf4644fe6c7d82b9f0e957817a8f9f8c58da5d2c9d3d19915229", "d8c5d3d2bcb82f16b399d40ed6ff1801bc5eb31fc301dc1ad517e2ed8c3f268e5987fc68902ecf4644fe6c7d82b9f0e957817a8f9f8c58da5d2c9d3d19915229")]
		public void ED25519Identity_Sign(string publicKeyHex, string privateKeyHex)
		{
			byte[] publicKeyBytes = ByteUtil.FromHexString(publicKeyHex);
			byte[] privateKeyBytes = ByteUtil.FromHexString(privateKeyHex);
			var identity = new Ed25519Identity(publicKeyBytes, privateKeyBytes);
			byte[] signature = identity.Sign(new byte[] { 1, 2, 3, 4 });
			Snapshot.Match(signature);
		}

		[Theory]
		[InlineData("303c300c060a2b0601040183b8430102032c000a00000000000000070101a451d1829b843e2aabdd49ea590668978a73612067bdde0b8502f844452a7558")]
		public void SubjectPublicKeyInfo__FromSecp256k1(string publicKeyHex)
		{
			byte[] publicKeyBytes = ByteUtil.FromHexString(publicKeyHex);
			var key = SubjectPublicKeyInfo.Secp256k1(publicKeyBytes);
			Snapshot.Match(key);
		}


		//[Theory]
		//[InlineData(
		//	"04A6D4E6FEA95AF9771EEBFA3684B3D7F2AF5D5A5A5F3A3F5F2A94A5D7DC5A5EEDC7BCBFE5D18C7F2DF9DA4E7D3D34E54F7BEA18D8E3827E2D1C447E7F16DF9D5C7",
		//	"79afbf7147841fca72b45a1978dd7669470ba67abbe5c220062924380c9c364b",
		//	"B5B11B455B339DE49EC8E942B813AB2E1AFA9AA40C9E511950EFD1BDBEF21D38",
		//	"3045022100AE40C8E1FB0A349F54A3D6165A2A5F5F7A72B5D5A1F9B8CFA6F0CE0F33D0071B0220571D4B7F53B4C3F7C2E8FAF6F2C6F48A6A48AC9548C2006E06F6E9D6D9C6E1B6"
		//)]
		//public void Secp256k1Identity_Sign(
		//	string publicKeyHex,
		//	string privateKeyHex,
		//	string messageHex,
		//	string expectedSignatureHex
		//)
		//{
		//	byte[] publicKeyBytes = ByteUtil.FromHexString(publicKeyHex);
		//	byte[] privateKeyBytes = ByteUtil.FromHexString(privateKeyHex);
		//	var identity = new Secp256k1Identity(publicKeyBytes, privateKeyBytes);
		//	byte[] signature = identity.Sign(new byte[] { 1, 2, 3, 4 });
		//	Snapshot.Match(signature);
		//}
	}
}
