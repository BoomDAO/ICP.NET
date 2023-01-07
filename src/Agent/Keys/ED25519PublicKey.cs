using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;

namespace EdjCase.ICP.Agent.Keys
{
	public class ED25519PublicKey : IHashable, IPublicKey
	{
		public const string OID = "1.3.101.112";

		public byte[] Value { get; }

		public ED25519PublicKey(byte[] value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		public byte[] GetRawBytes()
		{
			return this.Value;
		}

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

		public static ED25519PublicKey FromDer(byte[] derEncodedPublicKey)
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
				throw new InvalidEd25519PublicKey();
			}
			if (oid != "1.3.101.112" || publicKey.Length != 32)
			{
				throw new InvalidEd25519PublicKey();
			}
			return new ED25519PublicKey(publicKey);
		}
	}


	public class InvalidEd25519PublicKey : Exception
	{
		public InvalidEd25519PublicKey()
		{
		}
	}
}
