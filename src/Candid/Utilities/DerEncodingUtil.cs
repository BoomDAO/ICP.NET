using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Candid.Utilities
{
	public static class DerEncodingUtil
	{
		public static byte[] DecodePublicKey(byte[] derEncodedPublicKey, byte[] oid)
		{
			using (var stream = new MemoryStream(derEncodedPublicKey))
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

		public static byte[] EncodePublicKey(byte[] rawPublicKey, byte[] oid)
		{
			// The Bit String header needs to include the unused bit count byte in its length
			byte[] encodedKeyLength = EncodeLengthValue(rawPublicKey.Length + 1);

			int totalByteLength = oid.Length + encodedKeyLength.Length + 2 + rawPublicKey.Length;
			byte[] encodedTotalByteLength = EncodeLengthValue(totalByteLength);

			var bytes = new List<byte>();
			bytes.Add(0x30); // Type: Sequence
			bytes.AddRange(encodedTotalByteLength); // Sequence Length
			bytes.AddRange(oid); // OID
			bytes.Add(0x03); // Type: Bit String
			bytes.AddRange(encodedKeyLength);
			bytes.Add(0x00); // 0 padding
			bytes.AddRange(rawPublicKey);

			return bytes.ToArray();
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
			if (firstByte == 0x80)
			{
				throw new InvalidDataException("Invalid length 0");
			}
			if (firstByte == 0x81)
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
