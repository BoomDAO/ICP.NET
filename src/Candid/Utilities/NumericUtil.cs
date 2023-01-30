using System.Numerics;

namespace EdjCase.ICP.Candid.Utilities
{
	internal static class NumericUtil
	{
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
