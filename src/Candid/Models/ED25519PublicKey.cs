using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdjCase.ICP.Candid.Crypto;

namespace EdjCase.ICP.Candid.Models
{
	public class ED25519PublicKey : IHashable, IPublicKey
	{
		private static byte[] ED25519_OID = new byte[]
		{
			0x30, 0x05, // SEQUENCE
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

		public DerEncodedPublicKey GetDerEncodedBytes()
		{
			// The Bit String header needs to include the unused bit count byte in its length
			byte[] encodedKeyLength = EncodeLengthValue(this.Value.Length + 1);

			int totalByteLength = ED25519_OID.Length + encodedKeyLength.Length + 2 + this.Value.Length;
			byte[] encodedTotalByteLength = EncodeLengthValue(totalByteLength);

			var bytes = new List<byte>();
			bytes.Add(0x30); // Type: Sequence
			bytes.AddRange(encodedTotalByteLength); // Sequence Length
			bytes.AddRange(ED25519_OID); // OID
			bytes.Add(0x03); // Type: Bit String
			bytes.AddRange(encodedKeyLength);
			bytes.Add(0x00); // 0 padding
			bytes.AddRange(this.Value);

			return new DerEncodedPublicKey(bytes.ToArray());
		}

		public static ED25519PublicKey FromDer(DerEncodedPublicKey der)
		{
			byte[] value = DecodeDerPublicKey(der);
			return new ED25519PublicKey(value);
		}

		public static byte[] DecodeDerPublicKey(DerEncodedPublicKey der)
		{
			// TODO 
			throw new NotImplementedException();
		}

		private static byte[] EncodeLengthValue(int number)
		{
			if (number <= 0b0111_1111)
			{
				return new byte[] { (byte)number };
			}
			if (number <= 0b1111_1111)
			{
				return new byte[] { 0x81, (byte)number };
			}
			if (number <= 0b1111_1111_1111_1111)
			{
				return new byte[] { 0x82, (byte)(number >> 8), (byte)number };
			}
			if (number <= 0b1111_1111_1111_1111_1111_1111)
			{
				return new byte[] { 0x83, (byte)(number >> 16), (byte)(number >> 8), (byte)number };
			}
			throw new Exception("Length too long (> 4 bytes)");
		}
	}
}
