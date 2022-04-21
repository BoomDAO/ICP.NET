using ICP.Common.Candid;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace ICP.Common.Encodings
{
	public class LEB128
	{
		public byte[] Raw { get; }

		private LEB128(byte[] raw)
		{
			if (raw == null || raw.Length < 1)
			{
				throw new ArgumentException("LEB128 requires at least one byte");
			}
			this.Raw = raw ?? throw new Exception(nameof(raw));
		}

		public ulong ToUInt64()
		{
			if (!this.TryToUInt64(out ulong value))
			{
				throw new InvalidOperationException("LEB128 is too large to fit in a UInt64");
			}
			return value;
		}

		public bool TryToUInt64(out ulong value)
		{
			ulong v = 0;
			for (int i = 0; i < this.Raw.Length; i++)
			{
				byte b = this.Raw[i];
				if (i >= 10 && b > 1)
				{
					// Larger than ulong.MaxValue
					value = 0;
					return false;
				}
				ulong valueToAdd = (b & 0b0111_1111ul) << (7 * i); // Shift over 7 * i bits to get value to add
				v += valueToAdd;
			}
			value = v;
			return true;
		}

		public static LEB128 FromUInt64(ulong v)
		{
			return LEB128.FromBigInteger(new BigInteger(v));
		}

		public static LEB128 FromNat(UnboundedUInt value)
		{
			return LEB128.FromBigInteger(value.ToBigInteger());
		}

        public static LEB128 FromBigInteger(BigInteger value)
		{
			if (value == 0)
			{
				return new LEB128(new byte[] { 0b0 });
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
			return new LEB128(lebBytes);
		}

        public static LEB128 FromRaw(byte[] value)
		{
			if (!LEB128.TryFromRaw(value, out LEB128? leb))
			{
				throw new ArgumentException($"Invalid byte array value for leb: {Convert.ToHexString(value)}");
			}
			return leb;
		}

		public static bool TryFromRaw(byte[] value, [NotNullWhen(true)] out LEB128? leb)
		{
			if (!LEB128.AreBytesValid(value))
			{
				leb = null;
				return false;
			}
			leb = new LEB128(value);
			return true;
		}

		private static bool AreBytesValid(byte[] value)
		{
			// TODO
			throw new NotImplementedException();
		}
	}
}
