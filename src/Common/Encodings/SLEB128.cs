
using Candid;
using System;

namespace Dfinity.Common.Encodings
{
	public class SLEB128
	{
		public byte[] Raw { get; }

		private SLEB128(byte[] raw)
		{
			if(raw == null || raw.Length < 1)
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
			byte[] bytes = GetBytes(v);
			return new SLEB128(bytes);
		}

		private static byte[] GetBytes(long value)
		{
			if (value == 0)
			{
				return new byte[] { 0b0 };
			}
			bool isNegative = value < 0;

			if (isNegative)
			{
				value = -value - 1;
			}

			// Signed LEB128 - https://en.wikipedia.org/wiki/LEB128#Signed_LEB128
			//         11110001001000000  Binary encoding of 123456
			//     000011110001001000000  As a 21-bit number
			//     111100001110110111111  Negating all bits (one's complement)
			//     111100001110111000000  Adding one (two's complement)
			// 1111000  0111011  1000000  Split into 7-bit groups
			//01111000 10111011 11000000  Add high 1 bits on all but last (most significant) group to form bytes

			int bitCount = (int)Math.Ceiling(Math.Log2(value)) + 1; // log2 gets bit count and there is an extra bit for sign
			int byteCount = (int)Math.Ceiling(bitCount / 7m); // 7, not 8, the 8th bit is to indicate end of number
			byte[] bytes = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				byte byteValue = Convert.ToByte(value & 0b0111_1111);
				value = value >> 7; // Shift over 7 bits to setup the next byte

				if (isNegative)
				{
					// two's complement for negative values
					byteValue = Convert.ToByte(0b1000_0000 - byteValue - 1);
				}
				if (value != 0)
				{
					// All bytes have 1 as the most left value except the last byte
					byteValue &= 0b1000_0000;
				}
				bytes[i] = byteValue;
			}
			return bytes;
		}

		public static SLEB128 FromInt(UnboundedInt unboundedInt)
		{
			// TODO 
			//return new SLEB128(raw);
			throw new NotImplementedException();
		}
	}
}
