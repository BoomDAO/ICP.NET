using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Asn1DecoderNet5.Interfaces;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Utilities;

namespace EdjCase.ICP.Candid.Models.Keys
{
	public class BlsPublicKey : IHashable, IPublicKey
	{
		private static byte[] OID = new byte[]
		{
			0x30, 0x81, 0x82, 
			0x30, 0x1D, 0x06, 0x0D, 0x2B, 0x06, 0x01, 0x04, 0x01, 0x82, 0xDC, 0x7C, 0x05
			//, 0x03, 0x01, 0x02, 0x01, 0x06, 0x0C, 0x2B, 0x06, 0x01, 0x04, 0x01, 0x82, 0xDC, 0x7C, 0x05, 0x03, 0x02, 0x01, 0x03, 0x61, 0x00, 0x81, 0x4C, 0x0E, 0x6E, 0xC7, 0x1F, 0xAB, 0x58, 0x3B, 0x08, 0xBD, 0x81, 0x37, 0x3C, 0x25, 0x5C, 0x3C, 0x37, 0x1B, 0x2E, 0x84, 0x86, 0x3C, 0x98, 0xA4, 0xF1, 0xE0, 0x8B, 0x74, 0x23, 0x5D, 0x14, 0xFB, 0x5D, 0x9C, 0x0C, 0xD5, 0x46, 0xD9, 0x68, 0x5F, 0x91, 0x3A, 0x0C, 0x0B, 0x2C, 0xC5, 0x34, 0x15, 0x83, 0xBF, 0x4B, 0x43, 0x92, 0xE4, 0x67, 0xDB, 0x96, 0xD6, 0x5B, 0x9B, 0xB4, 0xCB, 0x71, 0x71, 0x12, 0xF8, 0x47, 0x2E, 0x0D, 0x5A, 0x4D, 0x14, 0x50, 0x5F, 0xFD, 0x74, 0x84, 0xB0, 0x12, 0x91, 0x09, 0x1C, 0x5F, 0x87, 0xB9, 0x88, 0x83, 0x46, 0x3F, 0x98, 0x09, 0x1A, 0x0B, 0xAA, 0xAE
		};


		public byte[] Value { get; }

		public BlsPublicKey(byte[] value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		public byte[] GetDerEncodedBytes()
		{
			return DerEncodingUtil.EncodePublicKey(this.Value, OID);
		}

		public static BlsPublicKey FromDer(byte[] derEncodedPublicKey)
		{
			ITag derTag = Asn1DecoderNet5.Decoder.Decode(derEncodedPublicKey);
			const int sequenceTagNumber = 48;
			const int oidTagNumber = 6;
			if (derTag.TagNumber != sequenceTagNumber || derTag.Childs.Count != 2)
			{
				throw new InvalidBlsPublicKey();
			}
			ITag oids = derTag.Childs.First();
			if (oids.Childs.Count != 2)
			{
				throw new InvalidBlsPublicKey();
			}
			ITag oid1 = oids.Childs[0];
			ITag oid2 = oids.Childs[1];
			if (oid1.TagNumber != oidTagNumber
				|| oid2.TagNumber != oidTagNumber)
			{
				throw new InvalidBlsPublicKey();
			}
			oid1.ConvertContentToReadableContent();
			oid2.ConvertContentToReadableContent();

			if (oid1.ReadableContent != "1.3.6.1.4.1.44668.5.3.1.2.1"
				|| oid2.ReadableContent != "1.3.6.1.4.1.44668.5.3.2.1")
			{
				throw new InvalidBlsPublicKey();
			}

			byte[] value = derTag.Childs[1].Content;
			return new BlsPublicKey(value);
		}

		public byte[] GetRawBytes()
		{
			return this.Value;
		}

		public byte[] GetOid()
		{
			return OID;
		}

		public bool ValidateSignature(byte[] value, byte[] signature)
		{
			// TODO
			return true;
		}
	}


	public class InvalidBlsPublicKey : Exception
	{
		public InvalidBlsPublicKey()
		{
		}
	}
}
