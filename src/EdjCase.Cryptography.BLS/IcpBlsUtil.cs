using System;
using System.IO;

namespace EdjCase.Cryptography.BLS
{
	/// <summary>
	/// Class with functions around BLS signatures (ICP flavor only)
	/// </summary>
	public static class IcpBlsUtil
	{
		private const int PublicKeyLength = 96;
		private const int SignatureLength = 48;

		private static object intializeLock = new object();
		private static bool isInitialized = false;

		/// <summary>
		/// Verifies a BLS signature (ICP flavor only)
		/// </summary>
		/// <param name="publicKey">The signer public key</param>
		/// <param name="messageHash">The SHA256 hash of the message</param>
		/// <param name="signature">The signature of the message</param>
		/// <returns>True if the signature is valid, otherwise false</returns>
		public static bool VerifySignature(
			byte[] publicKey,
			byte[] messageHash,
			byte[] signature
		)
		{
			if (signature.Length != SignatureLength)
			{
				throw new ArgumentOutOfRangeException(nameof(signature), signature.Length, $"Signature must be {SignatureLength} bytes long.");
			}
			if (publicKey.Length != PublicKeyLength)
			{
				throw new ArgumentOutOfRangeException(nameof(publicKey), publicKey.Length, $"Public Key must be {PublicKeyLength} bytes long.");
			}
			
			EnsureInitialized();

			var blsPublicKey = default(Interop.PublicKey);
			ulong publicKeyBytesRead = Interop.blsPublicKeyDeserialize(ref blsPublicKey, publicKey, (ulong)publicKey!.LongLength);

			if (publicKeyBytesRead != (ulong)publicKey.Length)
			{
				throw new Exception($"Error deserializing BLS public key");
			}

			var blsSignature = default(Interop.Signature);
			ulong signatureBytesRead = Interop.blsSignatureDeserialize(ref blsSignature, signature, (ulong)signature.LongLength);
			if (signatureBytesRead != (ulong)signature.LongLength)
			{
				throw new Exception($"Error deserializing BLS signature, length: {signatureBytesRead}");
			}

			int result = Interop.blsVerify(in blsSignature, in blsPublicKey, messageHash, (ulong)messageHash.Length);

			return result == 1;
		}


		private static void EnsureInitialized()
		{
			lock (IcpBlsUtil.intializeLock)
			{
				if (!IcpBlsUtil.isInitialized)
				{
					Interop.Init(Interop.MCL_BLS12_381);
					Interop.SetETHserialization(true);
					Interop.SetMapToMode(Interop.MapToMode.HashToCurve);
					Interop.PublicKey gen = new Interop.PublicKey();
					gen.SetStr("1 0x24aa2b2f08f0a91260805272dc51051c6e47ad4fa403b02b4510b647ae3d1770bac0326a805bbefd48056c8c121bdb8 0x13e02b6052719f607dacd3a088274f65596bd0d09920b61ab5da61bbdc7f5049334cf11213945d57e5ac7d055d042b7e 0x0ce5d527727d6e118cc9cdc6da2e351aadfd9baa8cbdd3a76d429a695160d12c923ac9cc3baca289e193548608b82801 0x0606c4a02ea734cc32acd2b02bc28b99cb3e287e85a763af267492ab572e99ab3f370d275cec1da1aaa9075ff05f79be");
					Interop.SetGeneratorOfPublicKey(gen);
					Interop.SetDstG1("BLS_SIG_BLS12381G1_XMD:SHA-256_SSWU_RO_NUL_");
					IcpBlsUtil.isInitialized = true;
				}
			}
		}
	}
}
