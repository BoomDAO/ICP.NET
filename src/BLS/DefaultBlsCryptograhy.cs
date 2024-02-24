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
		private static readonly byte[] DstG2;

		static DefaultBlsCryptograhy()
		{
			DstG2 = Encoding.UTF8.GetBytes("BLS_SIG_BLS12381G2_XMD:SHA-256_SSWU_RO_NUL_");
		}

		/// <inheritdoc />
		public bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature)
		{
			G1Projective[] g1Values = new[]
			{
				HashToCurveG1(messageHash)
			};
			G2Projective[] g2Values = new[]
			{
				G2Projective.FromCompressed(publicKey)
			};
			G1Affine sig = G1Affine.FromCompressed(signature);
			return this.Verify(sig, g2Values, g1Values);
		}

		private bool Verify(G1Affine signature, G2Projective[] g2Values, G1Projective[] g1Values)
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

			// Enforce distinct messages to counter BLS's rogue-key attack
			HashSet<byte[]> distinctHashes = g2Values
				.Select(h => h.ToCompressed())
				.ToHashSet(new ByteArrayComparer());
			if (distinctHashes.Count() != nHashes)
			{
				return false;
			}

			Fp12 millerLoopValue = Fp12.One();
			int i = 0;
			foreach ((G1Projective pk, G2Projective hash) in g1Values.Zip(g2Values, (pk, h) => (pk, h)))
			{
				G1Affine pkAffine = pk.ToAffine();
				G2Affine hAffine = hash.ToAffine();
				G2Prepared hPrepared = hAffine.ToPrepared();
				Fp12 result = BlsUtil.MillerLoop(
					Fp12.One(),
					(f) => DoublingStep(f, pkAffine, hPrepared, ref i),
					(f) => AddingStep(f, pkAffine, hPrepared, ref i),
					(f) => f.Square(),
					(f) => f.Conjugate()
				);
				millerLoopValue *= result;
			}

			G2Prepared g2Prepared = G2Affine.Generator().Neg().ToPrepared();
			i = 0;
			Fp12 r = BlsUtil.MillerLoop(
					Fp12.One(),
					(f) => DoublingStep(f, signature, g2Prepared, ref i),
					(f) => AddingStep(f, signature, g2Prepared, ref i),
					(f) => f.Square(),
					(f) => f.Conjugate()
				);
			millerLoopValue *= r;

			return FinalExponentiation(millerLoopValue).Equals(Fp12.One());
		}

		private static G1Projective HashToCurveG1(byte[] message)
		{
			int outputLength = 128;
			int hashSize = 32;
			(Fp u1, Fp u2) = HashToFieldFp(message, DstG2, outputLength, hashSize);
			G1Projective p1 = MapToCurveG1(u1);
			G1Projective p2 = MapToCurveG1(u2);
			return (p1 + p2).ClearH();
		}

		private static G1Projective MapToCurveG1(Fp u)
		{
			Fp usq = u.Square();
			Fp xi_usq = G1Affine.SSWU_XI * usq;
			Fp xisq_u4 = xi_usq.Square();
			Fp nd_common = xisq_u4 + xi_usq;
			Fp x_den = G1Affine.SSWU_ELLP_A * (nd_common.IsZero() ? G1Affine.SSWU_XI : nd_common.Neg());
			Fp x0_num = G1Affine.SSWU_ELLP_B * (Fp.One() + nd_common);
			Fp x_densq = x_den.Square();
			Fp gx_den = x_densq * x_den;
			Fp gx0_num = (x0_num.Square() + G1Affine.SSWU_ELLP_A * x_densq) * x0_num + G1Affine.SSWU_ELLP_B * gx_den;

			Fp uV = gx0_num * gx_den;
			Fp vsq = gx_den.Square();
			Fp sqrt_candidate = uV * ChainPm3div4(uV * vsq);

			bool gx0_square = (sqrt_candidate.Square() * gx_den).Equals(gx0_num);
			Fp x1_num = x0_num * xi_usq;
			Fp y1 = G1Affine.SQRT_M_XI_CUBED * usq * u * sqrt_candidate;

			Fp x_num = gx0_square ? x0_num : x1_num;
			Fp y = gx0_square ? sqrt_candidate : y1;
			if (y.Sgn0() ^ u.Sgn0())
			{
				y = y.Neg();
			}

			G1Projective v = new(x_num, y * x_den, x_den);

			return IsoMap(v);
		}

		private static Fp Square(Fp var0, int var1)
		{
			for (int i = 0; i < var1; i++)
			{
				var0 = var0.Square();
			}
			return var0;
		}

		private static Fp ChainPm3div4(Fp var0)
		{
			Fp var1 = var0.Square();
			Fp var9 = var1 * var0;
			Fp var5 = var1.Square();
			Fp var2 = var9 * var1;
			Fp var7 = var5 * var9;
			Fp var10 = var2 * var5;
			Fp var13 = var7 * var5;
			Fp var4 = var10 * var5;
			Fp var8 = var13 * var5;
			Fp var15 = var4 * var5;
			Fp var11 = var8 * var5;
			Fp var3 = var15 * var5;
			Fp var12 = var11 * var5;
			var1 = var4.Square();
			Fp var14 = var12 * var5;
			Fp var6 = var1 * var9;
			var5 = var1 * var2;
			var1 = Square(var1, 12);
			var1 *= var15;
			var1 = Square(var1, 7);
			var1 *= var8;
			var1 = Square(var1, 4);
			var1 *= var2;
			var1 = Square(var1, 6);
			var1 *= var7;
			var1 = Square(var1, 7);
			var1 *= var12;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 2);
			var1 *= var9;
			var1 = Square(var1, 6);
			var1 *= var4;
			var1 = Square(var1, 6);
			var1 *= var4;
			var1 = Square(var1, 6);
			var1 *= var10;
			var1 = Square(var1, 3);
			var1 *= var9;
			var1 = Square(var1, 7);
			var1 *= var4;
			var1 = Square(var1, 4);
			var1 *= var4;
			var1 = Square(var1, 6);
			var1 *= var8;
			var1 = Square(var1, 6);
			var1 *= var14;
			var1 = Square(var1, 3);
			var1 *= var0;
			var1 = Square(var1, 8);
			var1 *= var4;
			var1 = Square(var1, 7);
			var1 *= var12;
			var1 = Square(var1, 5);
			var1 *= var13;
			var1 = Square(var1, 6);
			var1 *= var4;
			var1 = Square(var1, 6);
			var1 *= var6;
			var1 = Square(var1, 4);
			var1 *= var10;
			var1 = Square(var1, 8);
			var1 *= var6;
			var1 = Square(var1, 4);
			var1 *= var4;
			var1 = Square(var1, 7);
			var1 *= var12;
			var1 = Square(var1, 9);
			var1 *= var11;
			var1 = Square(var1, 2);
			var1 *= var9;
			var1 = Square(var1, 5);
			var1 *= var7;
			var1 = Square(var1, 7);
			var1 *= var2;
			var1 = Square(var1, 7);
			var1 *= var10;
			var1 = Square(var1, 6);
			var1 *= var12;
			var1 = Square(var1, 5);
			var1 *= var6;
			var1 = Square(var1, 5);
			var1 *= var11;
			var1 = Square(var1, 5);
			var1 *= var11;
			var1 = Square(var1, 8);
			var1 *= var4;
			var1 = Square(var1, 7);
			var1 *= var3;
			var1 = Square(var1, 9);
			var1 *= var8;
			var1 = Square(var1, 5);
			var1 *= var4;
			var1 = Square(var1, 3);
			var1 *= var9;
			var1 = Square(var1, 8);
			var1 *= var8;
			var1 = Square(var1, 3);
			var1 *= var9;
			var1 = Square(var1, 7);
			var1 *= var10;
			var1 = Square(var1, 9);
			var1 *= var8;
			var1 = Square(var1, 6);
			var1 *= var3;
			var1 = Square(var1, 6);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 4);
			var1 *= var4;
			var1 = Square(var1, 3);
			var1 *= var9;
			var1 = Square(var1, 8);
			var1 *= var3;
			var1 = Square(var1, 7);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 4);
			var1 *= var8;
			var1 = Square(var1, 4);
			var1 *= var7;
			var1 = Square(var1, 7);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var6;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 4);
			var1 *= var4;
			var1 = Square(var1, 6);
			var1 *= var3;
			var1 = Square(var1, 4);
			var1 *= var2;
			var1 = var1.Square();
			return var1;
		}


		private static G1Projective IsoMap(G1Projective u)
		{
			Fp[][] coeffs = {
				G1Affine.ISO11_XNUM,
				G1Affine.ISO11_XDEN,
				G1Affine.ISO11_YNUM,
				G1Affine.ISO11_YDEN
			};

			Fp x = u.X;
			Fp y = u.Y;
			Fp z = u.Z;

			Fp[] mapvals = new[] { Fp.Zero(), Fp.Zero(), Fp.Zero(), Fp.Zero() };

			Fp[] zpows = new Fp[15];
			zpows[0] = z;
			for (int idx = 1; idx < zpows.Length; idx++)
			{
				zpows[idx] = zpows[idx - 1] * z;
			}

			for (int idx = 0; idx < 4; idx++)
			{
				Fp[] coeff = coeffs[idx];
				int clast = coeff.Length - 1;
				mapvals[idx] = coeff[clast];
				for (int jdx = 0; jdx < clast; jdx++)
				{
					mapvals[idx] = mapvals[idx] * x + zpows[jdx] * coeff[clast - 1 - jdx];
				}
			}

			mapvals[1] *= z;

			mapvals[2] *= y;
			mapvals[3] *= z;

			return new G1Projective(
				mapvals[0] * mapvals[3],
				mapvals[2] * mapvals[1],
				mapvals[1] * mapvals[3]
			);
		}

		private static (Fp, Fp) HashToFieldFp(byte[] message, byte[] dst, int outputLength, int hashSize)
		{
			const int byteLength = 256;
			Expander ex = Expander.Create(message, dst, byteLength, hashSize);
			byte[] a = ex.ReadInto(outputLength, hashSize);
			byte[] b = ex.ReadInto(outputLength, hashSize);
			return (
				FromOkmFp(a),
				FromOkmFp(b)
			);
		}


		private static Fp FromOkmFp(byte[] okm)
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


		private static Fp12 DoublingStep(Fp12 f, G1Affine g1, G2Prepared hash, ref int index)
		{
			try
			{
				bool eitherIdentity = g1.IsIdentity() || hash.IsInfinity;
				if (eitherIdentity)
				{
					return f;
				}
				Fp12 newF = Ell(f, hash.Coefficients[index], g1);
				return newF;
			}
			finally
			{
				index++;
			}
		}

		private static Fp12 AddingStep(Fp12 f, G1Affine g1, G2Prepared hash, ref int index)
		{
			try
			{
				bool eitherIdentity = g1.IsIdentity() || hash.IsInfinity;
				if (eitherIdentity)
				{
					return f;
				}
				Fp12 newF = Ell(f, hash.Coefficients[index], g1);
				return newF;
			}
			finally
			{
				index++;
			}
		}

		private static Fp12 Ell(Fp12 f, (Fp2, Fp2, Fp2) value, G1Affine publicKey)
		{
			(Fp2 c0, Fp2 c1, Fp2 c2) = value;

			c0 = new Fp2(c0.C0 * publicKey.Y, c0.C1 * publicKey.Y);
			c1 = new Fp2(c1.C0 * publicKey.X, c1.C1 * publicKey.X);

			return f.MultiplyBy014(c2, c1, c0);
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
