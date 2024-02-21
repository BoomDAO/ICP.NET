using EdjCase.ICP.BLS.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace EdjCase.ICP.BLS
{
	internal class BlsUtil
	{
		internal static (ulong, ulong) MultiplyAddCarry(ulong a, ulong b, ulong c, ulong carry)
		{
			// Convert to BigInteger for handling potential overflow
			BigInteger product = (BigInteger)b * c;
			BigInteger sum = product + a + carry;

			// Extract the lower and upper 64 bits of the result
			ulong lower = (ulong)(sum & 0xFFFFFFFFFFFFFFFF);
			ulong upper = (ulong)((sum >> 64) & 0xFFFFFFFFFFFFFFFF);

			return (lower, upper);
		}

		internal static (ulong, ulong) SubtractWithBorrow(ulong a, ulong b, ulong borrow)
		{
			ulong diff = a - b - (borrow >> 63);
			ulong borrowOut = (a < b || (borrow >> 63) == 1 && diff == ulong.MaxValue) ? 1UL : 0UL;
			return (diff, borrowOut);
		}

		internal static (ulong, ulong) AddWithCarry(ulong a, ulong b, ulong carry)
		{
			BigInteger sum = (BigInteger)a + b + carry;
			ulong lower = (ulong)(sum & 0xFFFFFFFFFFFFFFFF);
			ulong upper = (ulong)((sum >> 64) & 0xFFFFFFFFFFFFFFFF);
			return (lower, upper);
		}
	}
}
