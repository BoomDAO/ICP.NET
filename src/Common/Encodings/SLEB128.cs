
using ICP.Common.Candid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ICP.Common.Encodings
{
	public class SLEB128
	{
		public byte[] Raw { get; }

		private SLEB128(byte[] raw)
		{
			if (raw == null || raw.Length < 1)
			{
				throw new ArgumentException("LEB128 requires at least one byte");
			}
			this.Raw = raw ?? throw new Exception(nameof(raw));
		}

		public long ToInt64()
		{
			if (!this.TryToInt64(out long value))
			{
				throw new InvalidOperationException("SLEB128 is too large to fit in a UInt64");
			}
			return value;
		}

		public bool TryToInt64(out long value)
		{
			if (this.Raw.Length > 9)
			{
				// Larger than long.MaxValue
				value = 0;
				return false;
			}
			long v = 0;
			for (int i = 0; i < this.Raw.Length; i++)
			{
				byte b = this.Raw[i];
				long valueToAdd = (b & 0b0111_1111) << (7 * i); // Shift over 7 * i bits to get value to add
				v += valueToAdd;
			}
			value = v;
			return true;
		}

		public static SLEB128 FromInt64(long v)
        {
			return SLEB128.FromBigInteger(new BigInteger(v));
        }

		public static SLEB128 FromInt(UnboundedInt unboundedInt)
		{
			return SLEB128.FromBigInteger(unboundedInt.ToBigInteger());
		}

		public static SLEB128 FromBigInteger(BigInteger value)
		{
			if (value == 0)
			{
				return new SLEB128(new byte[] { 0b0 });
			}
			bool isNegative = value < 0;

			// Signed LEB128 - https://en.wikipedia.org/wiki/LEB128#Signed_LEB128
			//         11110001001000000  Binary encoding of 123456
			//     00001_11100010_01000000  As a 21-bit number (multiple of 7)
			//     11110_00011101_10111111  Negating all bits (one's complement)
			//     11110_00011101_11000000  Adding one (two's complement)
			// 1111000  0111011  1000000  Split into 7-bit groups
			//01111000 10111011 11000000  Add high 1 bits on all but last (most significant) group to form bytes
			

			long bitCount = value.GetBitLength(); // log2 gets bit count and there is an extra bit for sign
			if(bitCount > int.MaxValue) // Addresses the issue of bitcount has to be an int below
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
			return new SLEB128(bytes.ToArray());
		}
	}
}
