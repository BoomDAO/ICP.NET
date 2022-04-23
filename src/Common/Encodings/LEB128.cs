using ICP.Common.Candid;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Numerics;

namespace ICP.Common.Encodings
{
	public static class LEB128
	{
		public static UnboundedUInt DecodeUnsigned(byte[] encodedValue)
		{
			BigInteger v = 0;
			for (int i = 0; i < encodedValue.Length; i++)
			{
				byte b = encodedValue[i];
				ulong valueToAdd = (b & 0b0111_1111ul) << (7 * i); // Shift over 7 * i bits to get value to add
				v += valueToAdd;
			}
			return new UnboundedUInt(v);
		}
		public static UnboundedUInt DecodeUnsigned(Stream stream)
        {

        }


		public static UnboundedInt DecodeSigned(byte[] encodedValue)
		{
			BigInteger v = 0;
			for (int i = 0; i < encodedValue.Length; i++)
			{
				byte b = encodedValue[i];
				long valueToAdd = (b & 0b0111_1111) << (7 * i); // Shift over 7 * i bits to get value to add
				v += valueToAdd;
			}
			return new UnboundedInt(v);
		}

		public static UnboundedInt DecodeSigned(Stream stream)
		{
			BigInteger v = 0;
			int i = 0;
			while(true)
			{
				int byteOrEnd = stream.ReadByte();
				if(byteOrEnd == -1)
                {
					// TODO end
					break;
                }
				byte b = (byte)byteOrEnd;
                if (endOfLEB)
                {
					// TODO detect end of LEB
					break;
                }
				long valueToAdd = (b & 0b0111_1111) << (7 * i); // Shift over 7 * i bits to get value to add
				v += valueToAdd;
			}
			return new UnboundedInt(v);
		}

		public static byte[] EncodeUnsigned(UnboundedUInt value)
		{
			return LEB128.EncodeUnsigned(value.ToBigInteger());
		}

		public static byte[] EncodeSigned(UnboundedInt unboundedInt)
		{
			return LEB128.EncodeSigned(unboundedInt.ToBigInteger());
		}

		private static byte[] EncodeUnsigned(BigInteger value)
		{
			if(value < 0)
            {
				throw new ArgumentOutOfRangeException(nameof(value), "Value must be 0 or greater");
            }
			if (value == 0)
			{
				return new byte[] { 0b0 };
			}

			// Unsigned LEB128 - https://en.wikipedia.org/wiki/LEB128#Unsigned_LEB128
			//       10011000011101100101  In raw binary
			//      010011000011101100101  Padded to a multiple of 7 bits
			//  0100110  0001110  1100101  Split into 7-bit groups
			// 00100110 10001110 11100101  Add high 1 bits on all but last (most significant) group to form bytes

			long bitCount = value.GetBitLength();
			long byteCount = (long)Math.Ceiling(bitCount / 7m); // 7, not 8, the 8th bit is to indicate end of number
			byte[] lebBytes = new byte[byteCount];

			for (int i = 0; i < byteCount; i++)
			{
				byte byteValue = (value & 0b0111_1111).ToByteArray()[0]; // Get the last 7 bits
				value = value >> 7; // Chop off last 7 bits
				if (value != 0)
				{
					// Have most left of byte be 1 if there is another byte
					byteValue |= 0b10000000;
				}
				lebBytes[i] = byteValue;
			}
			return lebBytes;
		}


		private static byte[] EncodeSigned(BigInteger value)
		{
			if (value == 0)
			{
				return new byte[] { 0b0 };
			}

			// Signed LEB128 - https://en.wikipedia.org/wiki/LEB128#Signed_LEB128
			//         11110001001000000  Binary encoding of 123456
			//     00001_11100010_01000000  As a 21-bit number (multiple of 7)
			//     11110_00011101_10111111  Negating all bits (one's complement)
			//     11110_00011101_11000000  Adding one (two's complement)
			// 1111000  0111011  1000000  Split into 7-bit groups
			//01111000 10111011 11000000  Add high 1 bits on all but last (most significant) group to form bytes


			long bitCount = value.GetBitLength(); // log2 gets bit count and there is an extra bit for sign
			if (bitCount > int.MaxValue) // Addresses the issue of bitcount has to be an int below
			{

				throw new InvalidOperationException("Big integer value too large to convert to a SLEB128");
			}
			var bytes = new List<byte>();
			bool more = true;
			while (more)
			{
				byte byteValue = (value & 0b0111_1111).ToByteArray()[0]; // Get the last 7 bits
				value = value >> 7; // Shift over 7 bits to setup the next byte
				if (value == 0 && (byteValue & 0b0100_0000) == 0)
				{
					more = false;
				}
				else if (value == -1 && (byteValue & 0b0100_0000) > 0)
				{
					more = false;
				}
				else
				{
					byteValue |= 0b1000_0000;
				}
				bytes.Add(byteValue);
			}
			return bytes.ToArray();
		}
	}
}
