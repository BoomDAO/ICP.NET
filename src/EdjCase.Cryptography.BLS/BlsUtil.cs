using System;
using System.Runtime.CompilerServices;

namespace EdjCase.Cryptography.BLS
{
	// Modified from https://github.com/NethermindEth/cortex-cryptography-bls/blob/641f25297465e494dfcf62cd31ec44d6dc86a927/src/Cortex.Cryptography.Bls/BLSHerumi.cs
	public static class BlsUtil
	{
		private const int DomainLength = 8;
		private const int HashLength = 32;
		private const int PrivateKeyLength = 32;
		private const int PublicKeyLength = 96;
		private const int SignatureLength = 48;

		private static object intializeLock = new object();
		private static bool isInitialized = false;

		private static void EnsureInitialized()
		{
			lock (BlsUtil.intializeLock)
			{
				if (!BlsUtil.isInitialized)
				{
					int result = Interop.Init(Interop.MCL_BLS12_381, Interop.MCLBN_COMPILED_TIME_VAR);
					if (result != 0)
					{
						throw new InvalidOperationException("Dll failed to initialize. Error Code: " + result);
					}
					BlsUtil.isInitialized = true;
				}
			}
		}

		public static bool VerifyHash(
			byte[] publicKey,
			byte[] hash,
			byte[] signature
		)
		{
			if (signature.Length != SignatureLength)
			{
				throw new ArgumentOutOfRangeException(nameof(signature), signature.Length, $"Signature must be {SignatureLength} bytes long.");
			}
			EnsureInitialized();

			var blsPublicKey = default(Interop.BlsPublicKey);
			int publicKeyBytesRead = Interop.PublicKeyDeserialize(ref blsPublicKey, publicKey, (ulong)publicKey!.Length);
			if (publicKeyBytesRead != publicKey.Length)
			{
				throw new Exception($"Error deserializing BLS public key, length: {publicKeyBytesRead}");
			}

			var blsSignature = default(Interop.BlsSignature);
			int signatureBytesRead;
			unsafe
			{
				fixed (byte* signaturePtr = signature)
				{
					signatureBytesRead = Interop.SignatureDeserialize(ref blsSignature, signaturePtr, SignatureLength);
				}
			}
			if (signatureBytesRead != signature.Length)
			{
				throw new Exception($"Error deserializing BLS signature, length: {signatureBytesRead}");
			}

			int result;

			unsafe
			{
				fixed (byte* hashPtr = hash)
				{
					result = Interop.VerifyHash(ref blsSignature, ref blsPublicKey, hashPtr, hash.Length);
				}
			}

			return result == 1;
		}
	}
}
