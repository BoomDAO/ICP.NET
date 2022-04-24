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
			BigInteger v = LEB128.Decode(stream, (b, i) =>
			{
				var valueToAdd = new BigInteger(b & 0b0111_1111u); // Remove first bit, just an indicator if there are more bytes
				valueToAdd <<= (7 * i); // Shift over 7 * i bits to get value to add\
				return valueToAdd;
			});
			return new UnboundedUInt(v);
		}

		public static UnboundedInt DecodeSigned(Stream stream)
		{
			BigInteger v = LEB128.Decode(stream, (b, i) =>
			{
				var valueToAdd = new BigInteger(b & 0b0111_1111); // Remove first bit, just an indicator if there are more bytes
				// TODO correct value
				valueToAdd <<= (7 * i); // Shift over 7 * i bits to get value to add\
				return valueToAdd;
			});
			return new UnboundedInt(v);
		}

		private static BigInteger Decode(Stream stream, Func<byte, int, BigInteger> getValueFunc)
        {
			BigInteger v = 0;
			int i = 0;
			while (true)
			{
				int byteOrEnd = stream.ReadByte();
				if (byteOrEnd == -1)
				{
					throw new EndOfStreamException();
				}
				byte b = (byte)byteOrEnd;
				BigInteger valueToAdd = getValueFunc(b, i);
				v += valueToAdd;
				bool more = (b & 0b1000_0000) == 0b1000_0000; // first bit is a flag if there is more bytes
				if (!more)
				{
					return v;
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
			//     11110_00011101_11000000  Adding one (two's complement)
			// 1111000  0111011  1000000  Split into 7-bit groups
			//01111000 10111011 11000000  Add high 1 bits on all but last (most significant) group to form bytes

			// The way it is handled here is the BigInteger handles the heavy lifing of 
			// converting the value to 2's compliment 
			var bytes = new List<byte>();
			bool more = true;
			while (more)
			{
				byte byteValue = (value & 0b0111_1111).ToByteArray()[0]; // Get the last 7 bits
				value = value >> 7; // Shift over 7 bits to setup the next byte
				bool mostSignficantBitIsSet = (byteValue & 0b0100_0000) != 0;
				if (value == 0)
				{
					// No more values and tha last bit isnt a 1  => end
					// If the last bit is a 1, loop one more time, setting the next byte as 

                    if (mostSignficantBitIsSet)
					{
						AddByteWithMoreFlag(byteValue);
						bytes.Add(0b0000_0000);
					}
                    else
                    {
						bytes.Add(byteValue);
					}
					break;
				}
				if (value == -1)
				{
                    // -1 is equivalent to all 1's in the binary sequence/255 if unsigned
                    // AND since if the number is negative, 1 is used to fill vacated bit positions
                    // meaning the remaining value is just the sign bit

                    // IF the most signficant bit is not set, set the final byte to 0b0111_1111
                    // otherwise end

                    if (mostSignficantBitIsSet)
					{
						bytes.Add(byteValue);
					}
                    else
					{
						AddByteWithMoreFlag(byteValue);
						bytes.Add(0b1111_111);
					}
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
