using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EdjCase.Cryptography.BLS;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;

namespace EdjCase.ICP.Agent.Keys
{
	public class BlsPublicKey : IHashable, IPublicKey
	{
		public byte[] Value { get; }

		public BlsPublicKey(byte[] value)
		{
			this.Value = value;
		}


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

		public byte[] GetDerEncodedBytes()
		{
			return new byte[0];
		}

		public static BlsPublicKey FromDer(byte[] derEncodedPublicKey)
		{
			// DER encoding
			// SEQUENCE (2 elem)
			//   SEQUENCE(2 elem)
			//     OBJECT IDENTIFIER 1.3.6.1.4.1.44668.5.3.1.2.1
			//     OBJECT IDENTIFIER 1.3.6.1.4.1.44668.5.3.2.1
			//   BIT STRING(768 bit) …
			string a = "308182301d060d2b0601040182dc7c0503010201060c2b0601040182dc7c05030201036100";
			byte[] prefix = ByteUtil.FromHexString(a);
			return new BlsPublicKey(derEncodedPublicKey.Skip(prefix.Length).ToArray());
		}

		public byte[] GetRawBytes()
		{
			return this.Value;
		}

		public bool ValidateSignature(byte[] hash, byte[] signature)
		{
			return BlsUtil.VerifyHash(this.Value, hash, signature);
		}
	}


	public class InvalidBlsPublicKey : Exception
	{
		public InvalidBlsPublicKey()
		{
		}
	}
}
