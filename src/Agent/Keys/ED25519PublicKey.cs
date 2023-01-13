using System;
using System.Formats.Asn1;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Keys
{
	/// <summary>
	/// A public key using the Ed25519 algorithm
	/// </summary>
	public class Ed25519PublicKey : IHashable, IPublicKey
	{
		private const string OID = "1.3.101.112";

		/// <summary>
		/// The raw key bytes
		/// </summary>
		public byte[] Value { get; }

		/// <param name="value">The raw key bytes</param>
		/// <exception cref="ArgumentNullException">Throws if the value is null</exception>
		public Ed25519PublicKey(byte[] value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Computes the hash of the key with the specified hash function
		/// </summary>
		/// <param name="hashFunction">A hash function to hash the key with</param>
		/// <returns>The hash of the key bytes</returns>
		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		/// <summary>
		/// The raw bytes of the key
		/// </summary>
		/// <returns></returns>
		public byte[] GetRawBytes()
		{
			return this.Value;
		}

		/// <summary>
		/// The bytes of the key with DER encoding
		/// </summary>
		/// <returns>DER encoded key bytes</returns>
		public byte[] GetDerEncodedBytes()
		{
			AsnWriter writer = new(AsnEncodingRules.DER);
			// SEQUENCE (2 elem)
			using (writer.PushSequence())
			{
				// SEQUENCE(1 elem)
				using (writer.PushSequence())
				{
					// OBJECT IDENTIFIER 1.3.101.112
					writer.WriteObjectIdentifier(OID);
				}
				// BIT STRING(256 bit)
				writer.WriteBitString(this.Value);
			}

			return writer.Encode();
		}

		/// <summary>
		/// Builds a key from the DER encoded bytes
		/// </summary>
		/// <param name="derEncodedPublicKey">The DER encoded bytes of the key</param>
		/// <returns>A decoded key</returns>
		/// <exception cref="InvalidPublicKey">Throws if the key bytes is invalid</exception>
		public static Ed25519PublicKey FromDer(byte[] derEncodedPublicKey)
		{
			// DER encoding
			// SEQUENCE (2 elem)
			//   SEQUENCE(1 elem)
			//     OBJECT IDENTIFIER 1.3.101.112, EdDSA 25519 signature algorithm
			//   BIT STRING(256 bit) …

			string oid;
			byte[] publicKey;
			try
			{
				AsnReader reader = new (derEncodedPublicKey, AsnEncodingRules.DER);
				AsnReader seqReader = reader.ReadSequence();
				AsnReader seq2Reader = seqReader.ReadSequence();

				oid = seq2Reader.ReadObjectIdentifier();
				publicKey = seqReader.ReadBitString(out int _);
			}
			catch
			{
				throw new InvalidPublicKey();
			}
			if (oid != "1.3.101.112" || publicKey.Length != 32)
			{
				throw new InvalidPublicKey();
			}
			return new Ed25519PublicKey(publicKey);
		}
	}
}
