using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EdjCase.ICP.Candid.Utilities
{
	public static class NumericUtil
	{
		public static BinarySequence ToBits(this BigInteger value, bool unsignedBits = false)
		{
			byte[] bytes = value.ToByteArray(); // BigInteger returns twos compliment (signed) bytes
			BinarySequence bits = BinarySequence.FromBytes(bytes, isBigEndian: false);
			if (unsignedBits && bits.MostSignificantBit)
			{
				bits = bits.ToReverseTwosCompliment();
			}
			return bits;
		}
		public static byte[] ToByteArray(this BigInteger value, bool unsignedBits = false, bool bigEndian = false)
		{
			BinarySequence bits = value.ToBits(unsignedBits);
			return bits.ToByteArray(bigEndian);
		}

		public static long GetBitLength(this BigInteger value)
		{
			int bitWidth = 1;
			while ((value >>= 1) > 0)
			{
				bitWidth++;
			}
			return bitWidth;
		}
	}
}
