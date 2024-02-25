using EdjCase.ICP.BLS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EdjCase.ICP.BLS
{

	/// <summary>
	/// Represents the default implementation of the IBlsCryptography interface for BLS cryptography.
	/// </summary>
	public class DefaultBlsCryptograhy : IBlsCryptography
	{
		internal static readonly byte[] DstG1;
		internal static readonly byte[] DstG2;

		static DefaultBlsCryptograhy()
		{
			DstG1 = Encoding.UTF8.GetBytes("BLS_SIG_BLS12381G1_XMD:SHA-256_SSWU_RO_NUL_");
			DstG2 = Encoding.UTF8.GetBytes("BLS_SIG_BLS12381G2_XMD:SHA-256_SSWU_RO_NUL_");
		}

		/// <inheritdoc />
		public bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature)
		{
			G1Projective[] g1Values = new[]
			{
				G1Projective.HashToCurve(messageHash, DstG1)
			};
			G2Projective[] g2Values = new[]
			{
				G2Projective.FromCompressed(publicKey)
			};
			G1Affine sig = G1Affine.FromCompressed(signature);
			return this.VerifyG1Signature(sig, g2Values, g1Values);
		}

		internal bool VerifyG1Signature(
			G1Affine signature,
			G2Projective[] g2Values,
			G1Projective[] g1Values
		)
		{
			G1Projective g1 = signature.ToProjective(); // TODO BLSICP
			G2Projective g2 = G2Affine.Generator().Neg().ToProjective(); // TODO BLSICP
			G2Prepared g2Prepared = g2.ToPrepared();

			// TODO BLSICP
			Fp12 Ell(Fp12 f, G1Projective g1, G2Projective g2, int index)
			{
				(Fp2 c0, Fp2 c1, Fp2 c2) = g2Prepared.Coefficients[index];

				c0 = new Fp2(c0.C0 * g1.Y, c0.C1 * g1.Y);
				c1 = new Fp2(c1.C0 * g1.X, c1.C1 * g1.X);

				Fp12 newF = f.MultiplyBy014(c2, c1, c0);
				return newF;
			}

			/// ???
			//static Fp12 Ell(Fp12 f, G1Projective g1, G2Projective g2, int index)
			//{
			//	//TODO BLSICP
			//   (Fp c0, Fp c1, Fp c2) = g1Prepared.Coefficients[index];

			//	// Convert Fp to Fp2 by using c0 and c1 as the real part and setting the imaginary part to 0
			//	Fp2 convertedC0 = new Fp2(c0, Fp.Zero());
			//	Fp2 convertedC1 = new Fp2(c1, Fp.Zero());
			//	Fp2 convertedC2 = new Fp2(c2, Fp.Zero());

			//	// Now you can use convertedC0, convertedC1, and convertedC2 with MultiplyBy014
			//	Fp12 newF = f.MultiplyBy014(convertedC0, convertedC1, convertedC2);


			//	return newF;
			//	throw new NotImplementedException();
			//}


			return this.VerifyInternal(g2Values, g1Values, g1, g2, Ell);
		}

		internal bool VerifyG2Signature(
			G2Affine signature,
			G2Projective[] g2Values,
			G1Projective[] g1Values
		)
		{
			G1Projective g1 = G1Affine.Generator().Neg().ToProjective();
			G2Projective g2 = signature.ToProjective();
			G2Prepared g2Prepared = g2.ToPrepared();

			Fp12 Ell(Fp12 f, G1Projective g1, G2Projective g2, int index)
			{
				(Fp2 c0, Fp2 c1, Fp2 c2) = g2Prepared.Coefficients[index];

				c0 = new Fp2(c0.C0 * g1.Y, c0.C1 * g1.Y);
				c1 = new Fp2(c1.C0 * g1.X, c1.C1 * g1.X);

				Fp12 newF = f.MultiplyBy014(c2, c1, c0);
				return newF;
			}

			return this.VerifyInternal(g2Values, g1Values, g1, g2, Ell);
		}

		private bool VerifyInternal(
			G2Projective[] g2Values,
			G1Projective[] g1Values,
			G1Projective lastG1,
			G2Projective lastG2,
			Func<Fp12, G1Projective, G2Projective, int, Fp12> step
		)
		{
			if (g2Values.Length == 0 || g1Values.Length == 0)
			{
				return false;
			}

			int nHashes = g2Values.Length;

			if (nHashes != g1Values.Length)
			{
				return false;
			}

			// Zero keys should always fail
			if (g1Values.Any(pk => pk.IsIdentity()))
			{
				return false;
			}

			// TODO?
			// Enforce distinct messages to counter BLS's rogue-key attack
			//HashSet<byte[]> distinctHashes = g2Values
			//	.Select(h => h.ToCompressed())
			//	.ToHashSet(new ByteArrayComparer());
			//if (distinctHashes.Count() != nHashes)
			//{
			//	return false;
			//}

			Fp12 millerLoopValue = g1Values.Zip(g2Values, (g1, g2) => (g1, g2))
				.AsParallel()
				.Select((pair, i) => BlsUtil.MillerLoop(
					Fp12.One(),
					(f) => Step(f, pair.g1, pair.g2, step, i),
					(f) => Step(f, pair.g1, pair.g2, step, i),
					(f) => f.Square(),
					(f) => f.Conjugate()
				))
				.Aggregate(Fp12.One(), (acc, result) => acc * result);


			Fp12 r = BlsUtil.MillerLoop(
					Fp12.One(),
					(f) => Step(f, lastG1, lastG2, step, 0),
					(f) => Step(f, lastG1, lastG2, step, 0),
					(f) => f.Square(),
					(f) => f.Conjugate()
				);
			millerLoopValue *= r;

			return FinalExponentiation(millerLoopValue).Equals(Fp12.One());
		}


		private static Fp12 Step(Fp12 f, G1Projective g1, G2Projective g2, Func<Fp12, G1Projective, G2Projective, int, Fp12> step, int index)
		{
			bool eitherIdentity = g1.IsIdentity() || g2.IsIdentity();
			if (eitherIdentity)
			{
				return f;
			}
			return step(f, g1, g2, index);
		}


		private static Fp12 FinalExponentiation(Fp12 f)
		{
			Fp12 t0 = f
				.FrobeniusMap()
				.FrobeniusMap()
				.FrobeniusMap()
				.FrobeniusMap()
				.FrobeniusMap()
				.FrobeniusMap();
			Fp12 t1 = f.Invert() ?? throw new Exception("Failed to invert");
			Fp12 t2 = t0.Multiply(t1);
			t1 = t2;
			t2 = t2.FrobeniusMap().FrobeniusMap();
			t2 = t2.Multiply(t1);
			t1 = CyclotonmicSquare(t2).Conjugate();
			Fp12 t3 = CyclotomicExp(t2);
			Fp12 t4 = CyclotonmicSquare(t3);
			Fp12 t5 = t1.Multiply(t3);
			t1 = CyclotomicExp(t5);
			t0 = CyclotomicExp(t1);
			Fp12 t6 = CyclotomicExp(t0);
			t6 = t6.Multiply(t4);
			t4 = CyclotomicExp(t6);
			t5 = t5.Conjugate();
			t4 = t4.Multiply(t5).Multiply(t2);
			t5 = t2.Conjugate();
			t1 = t1.Multiply(t2);
			t1 = t1.FrobeniusMap().FrobeniusMap().FrobeniusMap();
			t6 = t6.Multiply(t5);
			t6 = t6.FrobeniusMap();
			t3 = t3.Multiply(t0);
			t3 = t3.FrobeniusMap().FrobeniusMap();
			t3 = t3.Multiply(t1);
			t3 = t3.Multiply(t6);
			f = t3.Multiply(t4);
			return f;
		}

		private static (Fp2, Fp2) Fp4Square(Fp2 a, Fp2 b)
		{
			Fp2 t0 = a.Square();
			Fp2 t1 = b.Square();
			Fp2 t2 = t1.MultiplyByNonresidue();
			Fp2 c0 = t2.Add(t0);
			t2 = a.Add(b);
			t2 = t2.Square();
			t2 = t2.Subtract(t0);
			Fp2 c1 = t2.Subtract(t1);

			return (c0, c1);
		}

		private static Fp12 CyclotonmicSquare(Fp12 f)
		{
			Fp2 z0 = f.C0.C0;
			Fp2 z4 = f.C0.C1;
			Fp2 z3 = f.C0.C2;
			Fp2 z2 = f.C1.C0;
			Fp2 z1 = f.C1.C1;
			Fp2 z5 = f.C1.C2;

			(Fp2 t0, Fp2 t1) = Fp4Square(z0, z1);

			// For A
			z0 = t0.Subtract(z0);
			z0 = z0.Add(z0).Add(t0);

			z1 = t1.Add(z1);
			z1 = z1.Add(z1).Add(t1);

			(t0, t1) = Fp4Square(z2, z3);
			(Fp2 t2, Fp2 t3) = Fp4Square(z4, z5);

			// For C
			z4 = t0.Subtract(z4);
			z4 = z4.Add(z4).Add(t0);

			z5 = t1.Add(z5);
			z5 = z5.Add(z5).Add(t1);

			// For B
			t0 = t3.MultiplyByNonresidue();
			z2 = t0.Add(z2);
			z2 = z2.Add(z2).Add(t0);

			z3 = t2.Subtract(z3);
			z3 = z3.Add(z3).Add(t2);
			return new Fp12(new Fp6(z0, z4, z3), new Fp6(z2, z1, z5));

		}

		private static Fp12 CyclotomicExp(Fp12 f)
		{
			ulong x = Constants.BLS_X;
			Fp12 tmp = Fp12.One();
			bool foundOne = false;
			for (int i = 63; i >= 0; i--)
			{
				bool b = ((x >> i) & 1) == 1;
				if (foundOne)
				{
					tmp = CyclotonmicSquare(tmp);
				}
				else
				{
					foundOne = b;
				}

				if (b)
				{
					tmp = tmp.Multiply(f);
				}
			}

			return tmp.Conjugate();
		}

		class ByteArrayComparer : IEqualityComparer<byte[]>
		{
			public bool Equals(byte[] x, byte[] y)
			{
				return x.SequenceEqual(y);
			}

			public int GetHashCode(byte[] obj)
			{
				return obj.GetHashCode();
			}
		}
	}

}
