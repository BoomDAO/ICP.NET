using System;
using System.Linq;
using EdjCase.Cryptography.BLS;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;

namespace EdjCase.ICP.Agent.Keys
{
	/// <summary>
	/// A public key using the BLS algorithm
	/// </summary>
	public class BlsPublicKey : IHashable, IPublicKey
	{
		/// <summary>
		/// The raw bytes value
		/// </summary>
		public byte[] Value { get; }

		/// <param name="value">The raw bytes value</param>
		public BlsPublicKey(byte[] value)
		{
			this.Value = value;
		}

		/// <inheritdoc/>
		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		//1.3.6.1.4.1.44668.5.3.1.2.1
		static byte[] algorithmOid = new byte[]
		{
			0x2B, 0x06, 0x01, 0x04, 0x01, 0x82, 0xDC, 0x7C, 0x05, 0x03, 0x01, 0x02, 0x01
		};
		//1.3.6.1.4.1.44668.5.3.2.1
		static byte[] curveOid = new byte[]
		{
			0x2B, 0x06, 0x01, 0x04, 0x01, 0x82, 0xDC, 0x7C, 0x05, 0x03, 0x02, 0x01
		};

		/// <inheritdoc/>
		public byte[] GetDerEncodedBytes()
		{
			throw new NotImplementedException(); // TODO
		}

		/// <summary>
		/// Converts a DER encoded public key into a public key object
		/// </summary>
		/// <param name="derEncodedPublicKey">Public key with a DER encoding</param>
		/// <returns>Key object from the specified public key</returns>
		public static BlsPublicKey FromDer(byte[] derEncodedPublicKey)
		{
			// DER encoding
			// SEQUENCE (2 elem)
			//   SEQUENCE(2 elem)
			//     OBJECT IDENTIFIER 1.3.6.1.4.1.44668.5.3.1.2.1
			//     OBJECT IDENTIFIER 1.3.6.1.4.1.44668.5.3.2.1
			//   BIT STRING(768 bit) …
			string a = "308182301d060d2b0601040182dc7c0503010201060c2b0601040182dc7c05030201036100";
			byte[] prefix = ByteUtil.FromHexString(a); // TODO
			return new BlsPublicKey(derEncodedPublicKey.Skip(prefix.Length).ToArray());
		}

		/// <summary>
		/// Gets the raw bytes of the public key
		/// </summary>
		/// <returns>Public key bytes</returns>
		public byte[] GetRawBytes()
		{
			return this.Value;
		}

		/// <summary>
		/// Validates the specified signature against the specified hash value
		/// </summary>
		/// <param name="hash">The hash digest of some data</param>
		/// <param name="signature">The signature for the hashed data</param>
		/// <returns>True if the signature is valid, otherwise false</returns>
		public bool ValidateSignature(byte[] hash, byte[] signature)
		{
			return BlsUtil.VerifyHash(this.Value, hash, signature);
		}
	}
}
