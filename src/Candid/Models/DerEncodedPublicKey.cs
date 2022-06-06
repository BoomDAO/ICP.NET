using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Utilities;

namespace EdjCase.ICP.Candid.Models
{
	public class DerEncodedPublicKey : IHashable, IPublicKey, IEquatable<DerEncodedPublicKey>
	{
		public byte[] Value { get; }

		public DerEncodedPublicKey(byte[] value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		public DerEncodedPublicKey GetDerEncodedBytes()
		{
			return this;
		}

		public bool Equals(DerEncodedPublicKey? other)
		{
			if (other == null)
			{
				return false;
			}
			return this.Value.SequenceEqual(other.Value);
		}


		public override bool Equals(object obj)
		{
			if (obj is DerEncodedPublicKey k)
			{
				return this.Equals(k);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Value);
		}

		public override string ToString()
		{
			return ByteUtil.ToHexString(this.Value);
		}

		public byte[] Decode(byte[] oid)
		{
			using (var stream = new MemoryStream(oid))
			{
				using (var reader = new BinaryReader(stream))
				{
					byte firstByte = reader.ReadByte();
					if (firstByte != 0x30)
					{
						throw new InvalidDataException("Der encoding is expected to start with '0x30'");
					}
					int totalByteLength = DecodeLengthValue(reader);
					byte[] actualOid = reader.ReadBytes(oid.Length);
					if (!oid.SequenceEqual(actualOid))
					{
						string expectedOidString = ByteUtil.ToHexString(oid);
						string actualOidString = ByteUtil.ToHexString(actualOid);
						throw new InvalidDataException($"Der encoding OID '{expectedOidString}' does not match expected OID '{actualOidString}'");
					}
					byte bitStringByte = reader.ReadByte();
					if (bitStringByte != 0x03)
					{
						throw new InvalidDataException("Der encoding is expected to have '0x03' after the oid");
					}
					int publicKeyLength = DecodeLengthValue(reader);
					byte paddingByte = reader.ReadByte();
					if (paddingByte != 0x00)
					{
						throw new InvalidDataException("Der encoding is expected to have '0x00' (0 padding byte) after the key length");
					}
					byte[] publicKey = reader.ReadBytes(publicKeyLength);
					return publicKey;
				}
			}
		}

		public static DerEncodedPublicKey Encode(byte[] publicKey, byte[] oid)
		{
			// The Bit String header needs to include the unused bit count byte in its length
			byte[] encodedKeyLength = EncodeLengthValue(publicKey.Length + 1);

			int totalByteLength = oid.Length + encodedKeyLength.Length + 2 + publicKey.Length;
			byte[] encodedTotalByteLength = EncodeLengthValue(totalByteLength);

			var bytes = new List<byte>();
			bytes.Add(0x30); // Type: Sequence
			bytes.AddRange(encodedTotalByteLength); // Sequence Length
			bytes.AddRange(oid); // OID
			bytes.Add(0x03); // Type: Bit String
			bytes.AddRange(encodedKeyLength);
			bytes.Add(0x00); // 0 padding
			bytes.AddRange(publicKey);

			return new DerEncodedPublicKey(bytes.ToArray());
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

		private static int DecodeLengthValue(BinaryReader reader)
		{
			byte firstByte = reader.ReadByte();
			if (firstByte < 0x80)
			{
				return firstByte;
			}
			if(firstByte == 0x80)
			{
				throw new InvalidDataException("Invalid length 0");
			}
			if(firstByte == 0x81)
			{
				byte secondByte = reader.ReadByte();
				return secondByte;
			}
			if (firstByte == 0x82)
			{
				byte secondByte = reader.ReadByte();
				byte thirdByte = reader.ReadByte();
				return (secondByte << 8) + thirdByte;
			}
			if (firstByte == 0x83)
			{
				byte secondByte = reader.ReadByte();
				byte thirdByte = reader.ReadByte();
				byte fourthByte = reader.ReadByte();
				return (secondByte << 16) + (thirdByte << 8) + fourthByte;
			}
			throw new InvalidDataException("Length too long (> 4 bytes)");
		}
	}
}
