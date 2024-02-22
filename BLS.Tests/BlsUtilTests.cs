using EdjCase.ICP.BLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLS.Tests
{
	public class BlsUtilTests
	{
		[Fact]
		public void SubtractWithoutBorrow()
		{
			(ulong value, ulong borrow) = BlsUtil.SubtractWithBorrow(10, 5, 0);
			Assert.Equal(5UL, value);
			Assert.Equal(0UL, borrow);
		}

		[Fact]
		public void SubtractWithBorrow()
		{
			(ulong value, ulong borrow) = BlsUtil.SubtractWithBorrow(5, 10, 0);
			Assert.Equal(18446744073709551611UL, value); // 2^64 - 5
			Assert.Equal(1UL, borrow);
		}

		[Fact]
		public void SubtractWithZero()
		{
			(ulong value, ulong borrow) = BlsUtil.SubtractWithBorrow(0, 0, 0);
			Assert.Equal(0UL, value);
			Assert.Equal(0UL, borrow);
		}

		[Fact]
		public void SubtractMaxValues()
		{
			(ulong value, ulong borrow) = BlsUtil.SubtractWithBorrow(ulong.MaxValue, ulong.MaxValue, 0);
			Assert.Equal(0UL, value);
			Assert.Equal(0UL, borrow);
		}


		[Fact]
		public void SubtractWithEffectiveBorrow()
		{
			(ulong value, ulong borrow) = BlsUtil.SubtractWithBorrow(5UL, 4UL, 1UL); // Effective borrow
			Assert.Equal(0UL, value);
			Assert.Equal(0UL, borrow);
		}

		[Fact]
		public void SubtractLeadingToBorrowWithNonZeroBorrowInput()
		{
			// This case tests when the initial borrow itself causes a borrow.
			(ulong value, ulong borrow) = BlsUtil.SubtractWithBorrow(0UL, 0UL, 1UL); // Non-zero borrow on zero subtraction
			Assert.Equal(18446744073709551615UL, value); // 2^64 - 1, due to underflow
			Assert.Equal(1UL, borrow); // Borrow occurred
		}

		[Fact]
		public void SubtractWithZeroAndNonZeroBorrow()
		{
			// Testing subtraction of 0 with a non-zero borrow, expecting underflow
			(ulong value, ulong borrow) = BlsUtil.SubtractWithBorrow(0UL, 0UL, 12345UL); // Any non-zero borrow
			Assert.Equal(18446744073709551615UL, value); // 2^64 - 1, due to underflow
			Assert.Equal(1UL, borrow); // Borrow occurred
		}
	}
}
