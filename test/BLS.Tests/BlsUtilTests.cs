using EdjCase.ICP.BLS;
using EdjCase.ICP.BLS.Models;
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

	//	[Fact]
	//	public void MultiMillerLoop_Equal()
	//	{
	//		G1Affine a1 = G1Affine.Generator();
	//		G2Affine b1 = G2Affine.Generator();

	//		G1Affine a2 = new G1Affine(
	//			G1Affine.Generator() * Scalar.FromRaw(new ulong[] { 1, 2, 3, 4 }).Invert().Square()
	//		);
	//		G2Affine b2 = new G2Affine(
	//			G2Affine.Generator() * Scalar.FromRaw(new ulong[] { 4, 2, 2, 4 }).Invert().Square()
	//		);

	//		G1Affine a3 = G1Affine.Identity();
	//		G2Affine b3 = new G2Affine(
	//			G2Affine.Generator() * Scalar.FromRaw(new ulong[] { 9, 2, 2, 4 }).Invert().Square()
	//		);

	//		G1Affine a4 = new G1Affine(
	//			G1Affine.Generator() * Scalar.FromRaw(new ulong[] { 5, 5, 5, 5 }).Invert().Square()
	//		);
	//		G2Affine b4 = G2Affine.Identity();

	//		G1Affine a5 = new G1Affine(
	//			G1Affine.Generator() * Scalar.FromRaw(new ulong[] { 323, 32, 3, 1 }).Invert().Square()
	//		);
	//		G2Affine b5 = new G2Affine(
	//			G2Affine.Generator() * Scalar.FromRaw(new ulong[] { 4, 2, 2, 9099 }).Invert().Square()
	//		);

	//		G2Prepared b1Prepared = new G2Prepared(b1);
	//		G2Prepared b2Prepared = new G2Prepared(b2);
	//		G2Prepared b3Prepared = new G2Prepared(b3);
	//		G2Prepared b4Prepared = new G2Prepared(b4);
	//		G2Prepared b5Prepared = new G2Prepared(b5);

	//		var expected = Pairing.Pair(a1, b1)
	//			+ Pairing.Pair(a2, b2)
	//			+ Pairing.Pair(a3, b3)
	//			+ Pairing.Pair(a4, b4)
	//			+ Pairing.Pair(a5, b5);

	//		var test = MultiMillerLoop(new[]
	//		{
	//	(a1, b1Prepared),
	//	(a2, b2Prepared),
	//	(a3, b3Prepared),
	//	(a4, b4Prepared),
	//	(a5, b5Prepared)
	//}).FinalExponentiation();

	//		Assert.Equal(expected, test);
	//	}

	}
}
