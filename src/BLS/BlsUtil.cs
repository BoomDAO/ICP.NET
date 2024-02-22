using EdjCase.ICP.BLS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Wasmtime;

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

		internal static (ulong Value, ulong Borrow) SubtractWithBorrow(ulong a, ulong b, ulong borrow)
		{
			// Perform the subtraction and borrow in two steps for clarity, using unchecked
			// to allow wrapping without throwing an overflow exception
			ulong retLow;
			ulong retHigh;
			unchecked
			{
				// First, subtract b and the borrow from a, treating them as 128-bit integers for the operation
				// Since C# does not support 128-bit integers directly, we handle the low and high parts separately
				ulong lowPart = a - b - (borrow >> 63);
				ulong highPart = 0;

				// If a borrow occurs during the subtraction, increment the high part
				if (a < b + (borrow >> 63))
				{
					highPart = 1;
				}

				retLow = lowPart;
				retHigh = highPart;
			}

			// The second element of the tuple indicates if there was a borrow, which is true if retHigh is not 0
			return (retLow, retHigh);
		}


		internal static (ulong, ulong) AddWithCarry(ulong a, ulong b, ulong carry)
		{
			BigInteger sum = (BigInteger)a + b + carry;
			ulong lower = (ulong)(sum & 0xFFFFFFFFFFFFFFFF);
			ulong upper = (ulong)((sum >> 64) & 0xFFFFFFFFFFFFFFFF);
			return (lower, upper);
		}

		internal static T MillerLoop<T>(
			T f,
			Func<T, T> doublingStep,
			Func<T, T> additionStep,
			Func<T, T> square,
			Func<T, T> conjugate
		)
		{
			bool foundOne = false;

			var values = Enumerable.Range(0, 64)
			.Reverse()
			.Select(b => ((Constants.BLS_X >> 1 >> b) & 1) == 1);

			foreach (bool i in values)
			{
				if (!foundOne)
				{
					foundOne = i;
					continue;
				}

				f = doublingStep(f);

				if (i)
				{
					f = additionStep(f);
				}
				f = square(f);
			}

			f = doublingStep(f);

			f = conjugate(f);
			return f;

		}
	}
}
