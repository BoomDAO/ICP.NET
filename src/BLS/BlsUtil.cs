using Dirichlet.Numerics;
using EdjCase.ICP.BLS.Models;
using System;
using System.Linq;

namespace EdjCase.ICP.BLS
{
	internal class BlsUtil
	{
		internal static (ulong, ulong) MultiplyAddCarry(ulong a, ulong b, ulong c, ulong carry)
		{
			UInt128.Multiply(out UInt128 product, b, c);
			UInt128 sum = UInt128.Add(UInt128.Add(product, a), carry);

			// Extract the lower and upper 64 bits of the result
			ulong lower = (ulong)(sum & 0xFFFFFFFFFFFFFFFF);
			ulong upper = (ulong)((sum >> 64) & 0xFFFFFFFFFFFFFFFF);

			return (lower, upper);
		}


		internal static (ulong sum, ulong carry) AddWithCarry(ulong a, ulong b)
		{
			ulong sum = a + b;
			// If sum is less than either a or b, then it means there was a wraparound, hence a carry.
			ulong carry = (sum < a || sum < b) ? 1UL : 0UL;

			return (sum, carry);
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
			// Start with the addition of 'a' and 'b'
			ulong lower = a + b;

			// If 'lower' is less than either 'a' or 'b', it means an overflow occurred
			// So, we need to account for this in the upper part
			ulong overflow = (lower < a || lower < b) ? 1UL : 0;

			// Add the carry to the lower part. If this addition overflows, it will also contribute to the upper part
			lower += carry;
			if (lower < carry) // Check for overflow after adding the carry
			{
				overflow++;
			}

			return (lower, overflow);
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

		public static Fp FromOkmFp(byte[] okm)
		{
			Fp F2256 = new(
				0x075b_3cd7_c5ce_820f,
				0x3ec6_ba62_1c3e_db0b,
				0x168a_13d8_2bff_6bce,
				0x8766_3c4b_f8c4_49d2,
				0x15f3_4c83_ddc8_d830,
				0x0f96_28b4_9caa_2e85
			);
			var bs = new byte[48];
			Array.Copy(okm, 0, bs, 16, 32);
			Fp db = Fp.FromBytes(bs);
			Array.Copy(okm, 32, bs, 16, 32);
			Fp da = Fp.FromBytes(bs);
			return db * F2256 + da;
		}

	}
}
