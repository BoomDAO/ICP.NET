using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Utilities;

namespace EdjCase.ICP.Candid.Models
{
	public class ED25519PublicKey : IHashable, IPublicKey
	{
		private static byte[] ED25519_OID = new byte[]
		{
			0x30, 0x05, // SEQUENCE of 5 bytes
			0x06, 0x03, // OID with 3 bytes
			0x2b, 0x65, 0x70 // id-Ed25519 OID
		};


		public byte[] Value { get; }

		public ED25519PublicKey(byte[] value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		public byte[] GetDerEncodedBytes()
		{
			return DerEncodingUtil.EncodePublicKey(this.Value, ED25519_OID);
		}

		public static ED25519PublicKey FromDer(byte[] derEncodedPublicKey)
		{
			byte[] value = DerEncodingUtil.DecodePublicKey(derEncodedPublicKey, ED25519_OID);
			return new ED25519PublicKey(value);
		}

		public byte[] GetRawBytes()
		{
			return this.Value;
		}

		public byte[] GetOid()
		{
			return ED25519_OID;
		}
	}
}
