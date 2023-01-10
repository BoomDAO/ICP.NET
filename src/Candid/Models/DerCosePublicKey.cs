using System;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Utilities;

namespace EdjCase.ICP.Candid.Models
{
	public class DerCosePublicKey : IHashable, IPublicKey
	{
		private static byte[] DER_COSE_OID = new byte[]
		{
			0x30, 0x0c, // SEQUENCE 
			0x06, 0x0a, // OID with 10 bytes
			0x2b, 0x06, 0x01, 0x04, 0x01, 0x83, 0xb8, 0x43, 0x01, 0x01, // DER encoded COSE
		};

		public byte[] Value { get; }

		public DerCosePublicKey(byte[] value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		public byte[] GetDerEncodedBytes()
		{
			return DerEncodingUtil.EncodePublicKey(this.Value, DER_COSE_OID);
		}

		public static DerCosePublicKey FromDer(byte[] derEncodedPublicKey)
		{
			byte[] value = DerEncodingUtil.DecodePublicKey(derEncodedPublicKey, DER_COSE_OID);
			return new DerCosePublicKey(value);
		}

		public byte[] GetRawBytes()
		{
			return this.Value;
		}

		public byte[] GetOid()
		{
			return DER_COSE_OID;
		}
	}
}

