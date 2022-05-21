using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;

namespace EdjCase.ICP.Candid.Encodings
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
			BigInteger v = LEB128.Decode(stream, isUnsigned: true);
			return new UnboundedUInt(v);
		}

		public static UnboundedInt DecodeSigned(Stream stream)
		{
			BigInteger v = LEB128.Decode(stream, isUnsigned: false);
			return new UnboundedInt(v);
		}

		private static BigInteger Decode(Stream stream, bool isUnsigned)
		{
            byte[] bytes = LEB128.GetValueBits(stream, isUnsigned)
				.Chunk(8)
				.Select(bits =>
				{
					byte b = 0;
                    for (int i = bits.Length - 1; i >= 0; i--)
					{
						bool bit = bits[i];
						b |= bit ? (byte)1 : (byte)0;
						if (i > 0)
						{
							b <<= 1;
						}
                    }
					return b;
				})
				.ToArray();
			return bytes.ToBigInteger(isUnsigned: isUnsigned, isBigEndian: false);
		}
		private static IEnumerable<bool> GetValueBits(Stream stream, bool isUnsigned)
		{
			int i = 0;
			while (true)
			{
				int byteOrEnd = stream.ReadByte();
				if (byteOrEnd == -1)
				{
					throw new EndOfStreamException();
				}

				bool more = (byteOrEnd & 0b1000_0000) == 0b1000_0000; // first bit is a flag if there is more bytes

				// See which of the 7 bits are set
				yield return GetBit(0b0000_0001);
				yield return GetBit(0b0000_0010);
				yield return GetBit(0b0000_0100);
				yield return GetBit(0b0000_1000);
				yield return GetBit(0b0001_0000);
				yield return GetBit(0b0010_0000);

				bool lastBitSet = GetBit(0b0100_0000);
				yield return lastBitSet;
				if (!more)
				{
					bool paddingValue;
					if (isUnsigned)
                    {
						paddingValue = false;
                    }
                    else
                    {
						paddingValue = lastBitSet; // 1 if signed and last bit is 1, otherwise 0
                    }
					while (i % 8 != 0)
					{
						// Round out the 
						yield return paddingValue;
						i++;
					}
					break;
				}
				bool GetBit(byte bitMask)
				{
					i++;
					return (byteOrEnd & bitMask) == bitMask;
				}
			}
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
			//     11110_00011101_11000000  Adding one (two's complement) (Binary encoding of signed -123456)
			// 1111000  0111011  1000000  Split into 7-bit groups
			//01111000 10111011 11000000  Add high 1 bits on all but last (most significant) group to form bytes

			var bytes = new List<byte>();
			bool more = true;
			while (more)
			{
				byte byteValue = (value & 0b0111_1111).ToByteArray()[0]; // Get the last 7 bits
				value = value >> 7; // Shift over 7 bits to setup the next byte
				bool mostSignficantBitIsSet = (byteValue & 0b0100_0000) != 0;
				if (value == 0) // no more bits => end
				{
                    if (mostSignficantBitIsSet)
					{
						// If last bit is a 1, add another 0 value byte
						AddByteWithMoreFlag(byteValue);
						bytes.Add(0b0000_0000);
						break;
					}
					bytes.Add(byteValue);
					break;
				}
				if (value == -1) // -1 == only bit remaining is a sign bit (with backfilled 1's from a right shift) => end
				{
                    // -1 is equivalent to all 1's in the binary sequence/255 if unsigned
                    // AND since if the number is negative, 1 is used to fill vacated bit positions
                    // meaning the remaining value is just the sign bit

                    // IF the most signficant bit is not set, set the final byte to 0b0111_1111
                    // otherwise end

                    if (!mostSignficantBitIsSet)
					{
						// If last bit is a 1, add another 111_1111 value byte
						AddByteWithMoreFlag(byteValue);
                        bytes.Add(0b0111_1111);
						break;
                    }
					bytes.Add(byteValue);
					break;
				}
				AddByteWithMoreFlag(byteValue);
			}
			return bytes.ToArray();

			void AddByteWithMoreFlag(byte byteValue)
			{
				bytes.Add((byte)(byteValue | 0b1000_0000));
			}
		}
	}
}
