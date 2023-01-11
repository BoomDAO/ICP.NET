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

		protected DerCosePublicKey(byte[]? rawBytes, byte[]? derEncodedBytes)
		{
			this._rawBytes = rawBytes;
			this._derEncodedBytes = derEncodedBytes;
		}

		public static DerCosePublicKey FromDer(byte[] derEncodedPublicKey)
		{
			return new DerCosePublicKey(null, derEncodedPublicKey);
		}

		public static DerCosePublicKey FromRaw(byte[] rawBytes)
		{
			return new DerCosePublicKey(rawBytes, null);
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			throw new System.NotImplementedException("TODO: unclear how this is used");
			//return hashFunction.ComputeHash(this.Value);
		}

		public byte[] GetDerEncodedBytes()
		{
			return this._derEncodedBytes = this._derEncodedBytes ?? DerEncodingUtil.EncodePublicKey(this._rawBytes!, DER_COSE_OID);
		}

		public byte[] GetRawBytes()
		{
			return this._rawBytes = this._rawBytes ?? DerEncodingUtil.DecodePublicKey(this._derEncodedBytes!, DER_COSE_OID);
		}

		public byte[] GetOid()
		{
			return DER_COSE_OID;
		}

		private byte[]? _rawBytes;
		private byte[]? _derEncodedBytes;
	}
}

