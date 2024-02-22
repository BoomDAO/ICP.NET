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

		internal static (ulong Value, ulong BorrowOut) SubtractWithBorrow(ulong a, ulong b, ulong borrow)
		{
			unchecked
			{
				// Interpret any non-zero borrow value as true (1) for subtraction
				ulong effectiveBorrow = borrow > 0 ? 1UL : 0UL;
				ulong result = a - b - effectiveBorrow;

				// Determine if a borrow occurred. A borrow is needed if a is less than b,
				// or if a equals b and an effective borrow is subtracted.
				ulong borrowOut = (a < b || (a == b && effectiveBorrow > 0)) ? 1UL : 0UL;

				return (result, borrowOut);
			}
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
