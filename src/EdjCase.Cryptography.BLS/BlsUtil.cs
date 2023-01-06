using System;

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


		public static Lazy<bool> isDllInitialized = new Lazy<bool>(
			() => Interop.Init(Interop.MCL_BLS12_381, Interop.MCLBN_COMPILED_TIME_VAR) == 1,
			true
		);


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
			if (!isDllInitialized.Value)
			{
				throw new InvalidOperationException("Dll is not initialized");
			}

			var blsPublicKey = default(Interop.BlsPublicKey);
			int publicKeyBytesRead;
			unsafe
			{
				fixed (byte* publicKeyPtr = publicKey)
				{
					publicKeyBytesRead = Interop.PublicKeyDeserialize(ref blsPublicKey, publicKeyPtr, publicKey!.Length);
				}
			}
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
