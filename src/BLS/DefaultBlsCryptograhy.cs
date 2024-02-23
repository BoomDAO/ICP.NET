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
		/// <inheritdoc />
		public bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature)
		{
			G2Projective[] hashes = new[]
			{
				G2Projective.FromCompressed(messageHash)
			};
			G1Projective[] publicKeys = new[]
			{
				G1Projective.FromCompressed(publicKey)
			};
			G2Affine sig = G2Affine.FromCompressed(signature);
			return this.Verify(sig, hashes, publicKeys);
		}

		private static void HashToCurve(byte[] message, byte[] dst)
		{
			(Fp2 u1, Fp2 u2) = HashToField(message, dst);
			G2Projective p1 = MapToCurve(u1);
			G2Projective p2 = MapToCurve(u2);
			(p1 + p2).ClearH();
		}

		private static G2Projective MapToCurve(Fp2 u)
		{
			Fp2 usq = u.Square();
			Fp2 xi_usq = G2Affine.SSWU_XI * usq;
			Fp2 xisq_u4 = xi_usq.Square();
			Fp2 nd_common = xisq_u4 + xi_usq;
			Fp2 x_den = G2Affine.SSWU_ELLP_A * (nd_common.IsZero() ? G2Affine.SSWU_XI : nd_common.Neg());
			Fp2 x0_num = G2Affine.SSWU_ELLP_B * (Fp2.One() + nd_common);
			Fp2 x_densq = x_den.Square();
			Fp2 gx_den = x_densq * x_den;
			Fp2 gx0_num = (x0_num.Square() + G2Affine.SSWU_ELLP_A * x_densq) * x0_num + G2Affine.SSWU_ELLP_B * gx_den;
			Fp2 vsq = gx_den.Square();
			Fp2 v_3 = vsq * gx_den;
			Fp2 v_4 = vsq.Square();
			Fp2 uv_7 = gx0_num * v_3 * v_4;
			Fp2 uv_15 = uv_7 * v_4.Square();
			Fp2 sqrt_candidate = uv_7 * ChainP2m9div16(uv_15);
			Fp2 y = sqrt_candidate;
			Fp2 tmp = new Fp2(sqrt_candidate.C1.Neg(), sqrt_candidate.C0);
			if ((tmp.Square() * gx_den).Equals(gx0_num))
			{
				y = tmp;
			}
			tmp = sqrt_candidate * G2Affine.SSWU_RV1;
			if ((tmp.Square() * gx_den).Equals(gx0_num))
			{
				y = tmp;
			}
			tmp = new Fp2(tmp.C1, tmp.C0.Neg());
			if ((tmp.Square() * gx_den).Equals(gx0_num))
			{
				y = tmp;
			}
			Fp2 gx1_num = gx0_num * xi_usq * xisq_u4;
			sqrt_candidate = sqrt_candidate * usq * u;
			bool eta_found = false;
			foreach (Fp2 eta in G2Affine.SSWU_ETAS)
			{
				tmp = sqrt_candidate * eta;
				if ((tmp.Square() * gx_den).Equals(gx1_num))
				{
					y = tmp;
					eta_found = true;
				}
			}
			Fp2 x_num = eta_found ? x0_num * xi_usq : x0_num;
			if (x_num.Sgn0() ^ y.Sgn0())
			{
				y = y.Neg();
			}

			G2Projective v = new(x_num, y * x_den, x_den);
			return IsoMap(v);
		}

		private static Fp2 Square(Fp2 var0, int var1)
		{
			for (int i = 0; i < var1; i++)
			{
				var0 = var0.Square();
			}
			return var0;
		}

		private static Fp2 ChainP2m9div16(Fp2 var0)
		{
			Fp2 var1 = var0.Square();
			Fp2 var2 = var1 * var0;
			Fp2 var15 = var2 * var1;
			Fp2 var3 = var15 * var1;
			Fp2 var14 = var3 * var1;
			Fp2 var13 = var14 * var1;
			Fp2 var5 = var13 * var1;
			Fp2 var10 = var5 * var1;
			Fp2 var9 = var10 * var1;
			Fp2 var16 = var9 * var1;
			Fp2 var4 = var16 * var1;
			Fp2 var7 = var4 * var1;
			Fp2 var6 = var7 * var1;
			Fp2 var12 = var6 * var1;
			Fp2 var8 = var12 * var1;
			Fp2 var11 = var8 * var1;
			var1 = var4.Square();
			Square(var1, 2);
			var1 *= var0;
			Square(var1, 9);
			var1 *= var12;
			Square(var1, 4);
			var1 *= var5;
			Square(var1, 6);
			var1 *= var14;
			Square(var1, 4);
			var1 *= var3;
			Square(var1, 5);
			var1 *= var2;
			Square(var1, 8);
			var1 *= var5;
			Square(var1, 4);
			var1 *= var3;
			Square(var1, 4);
			var1 *= var10;
			Square(var1, 8);
			var1 *= var8;
			Square(var1, 6);
			var1 *= var13;
			Square(var1, 4);
			var1 *= var5;
			Square(var1, 3);
			var1 *= var0;
			Square(var1, 6);
			var1 *= var10;
			Square(var1, 8);
			var1 *= var8;
			Square(var1, 6);
			var1 *= var4;
			Square(var1, 8);
			var1 *= var9;
			Square(var1, 5);
			var1 *= var10;
			Square(var1, 6);
			var1 *= var14;
			Square(var1, 5);
			var1 *= var10;
			Square(var1, 2);
			var1 *= var0;
			Square(var1, 6);
			var1 *= var10;
			Square(var1, 7);
			var1 *= var13;
			Square(var1, 4);
			var1 *= var3;
			Square(var1, 6);
			var1 *= var14;
			Square(var1, 7);
			var1 *= var3;
			Square(var1, 5);
			var1 *= var15;
			Square(var1, 7);
			var1 *= var3;
			Square(var1, 5);
			var1 *= var3;
			Square(var1, 10);
			var1 *= var9;
			Square(var1, 3);
			var1 *= var15;
			Square(var1, 5);
			var1 *= var5;
			Square(var1, 8);
			var1 *= var6;
			Square(var1, 5);
			var1 *= var7;
			Square(var1, 6);
			var1 *= var13;
			Square(var1, 6);
			var1 *= var10;
			Square(var1, 6);
			var1 *= var14;
			Square(var1, 7);
			var1 *= var16;
			Square(var1, 5);
			var1 *= var14;
			Square(var1, 6);
			var1 *= var10;
			Square(var1, 6);
			var1 *= var9;
			Square(var1, 5);
			var1 *= var10;
			Square(var1, 2);
			var1 *= var0;
			Square(var1, 8);
			var1 *= var15;
			Square(var1, 7);
			var1 *= var15;
			Square(var1, 4);
			var1 *= var2;
			Square(var1, 7);
			var1 *= var13;
			Square(var1, 6);
			var1 *= var10;
			Square(var1, 4);
			var1 *= var5;
			Square(var1, 7);
			var1 *= var13;
			Square(var1, 6);
			var1 *= var12;
			Square(var1, 5);
			var1 *= var7;
			Square(var1, 5);
			var1 *= var15;
			Square(var1, 7);
			var1 *= var12;
			Square(var1, 5);
			var1 *= var7;
			Square(var1, 5);
			var1 *= var4;
			Square(var1, 4);
			var1 *= var2;
			Square(var1, 6);
			var1 *= var15;
			Square(var1, 6);
			var1 *= var14;
			Square(var1, 4);
			var1 *= var2;
			Square(var1, 4);
			var1 *= var2;
			Square(var1, 8);
			var1 *= var14;
			Square(var1, 5);
			var1 *= var10;
			Square(var1, 6);
			var1 *= var3;
			Square(var1, 5);
			var1 *= var10;
			Square(var1, 12);
			var1 *= var9;
			Square(var1, 4);
			var1 *= var5;
			Square(var1, 5);
			var1 *= var5;
			Square(var1, 6);
			var1 *= var2;
			Square(var1, 9);
			var1 *= var6;
			Square(var1, 5);
			var1 *= var6;
			Square(var1, 6);
			var1 *= var2;
			Square(var1, 6);
			var1 *= var2;
			Square(var1, 9);
			var1 *= var7;
			Square(var1, 7);
			var1 *= var10;
			Square(var1, 6);
			var1 *= var6;
			Square(var1, 5);
			var1 *= var14;
			Square(var1, 7);
			var1 *= var7;
			Square(var1, 2);
			var1 *= var0;
			Square(var1, 8);
			var1 *= var13;
			Square(var1, 4);
			var1 *= var15;
			Square(var1, 7);
			var1 *= var3;
			Square(var1, 8);
			var1 *= var14;
			Square(var1, 7);
			var1 *= var5;
			Square(var1, 10);
			var1 *= var14;
			Square(var1, 6);
			var1 *= var13;
			Square(var1, 6);
			var1 *= var5;
			Square(var1, 6);
			var1 *= var11;
			Square(var1, 5);
			var1 *= var6;
			Square(var1, 7);
			var1 *= var10;
			Square(var1, 5);
			var1 *= var5;
			Square(var1, 7);
			var1 *= var11;
			Square(var1, 5);
			var1 *= var3;
			Square(var1, 8);
			var1 *= var12;
			Square(var1, 6);
			var1 *= var8;
			Square(var1, 6);
			var1 *= var2;
			Square(var1, 7);
			var1 *= var13;
			Square(var1, 7);
			var1 *= var13;
			Square(var1, 6);
			var1 *= var2;
			Square(var1, 5);
			var1 *= var3;
			Square(var1, 10);
			var1 *= var12;
			Square(var1, 4);
			var1 *= var0;
			Square(var1, 9);
			var1 *= var9;
			Square(var1, 6);
			var1 *= var10;
			Square(var1, 7);
			var1 *= var11;
			Square(var1, 5);
			var1 *= var4;
			Square(var1, 4);
			var1 *= var10;
			Square(var1, 7);
			var1 *= var8;
			Square(var1, 5);
			var1 *= var4;
			Square(var1, 5);
			var1 *= var4;
			Square(var1, 5);
			var1 *= var9;
			Square(var1, 4);
			var1 *= var5;
			Square(var1, 6);
			var1 *= var8;
			var1 = var1.Square();
			var1 *= var0;
			Square(var1, 6);
			var1 *= var3;
			Square(var1, 10);
			var1 *= var7;
			Square(var1, 6);
			var1 *= var4;
			Square(var1, 6);
			var1 *= var6;
			Square(var1, 6);
			var1 *= var5;
			Square(var1, 6);
			var1 *= var4;
			Square(var1, 23);
			var1 *= var3;
			Square(var1, 6);
			var1 *= var3;
			Square(var1, 5);
			var1 *= var2;
			Square(var1, 6);
			var1 *= var3;
			Square(var1, 5);
			return var1 * var2;
		}

		private static G2Projective IsoMap(G2Projective u)
		{
			Fp2[][] coeffs = {
				G2Affine.ISO3_XNUM,
				G2Affine.ISO3_XDEN,
				G2Affine.ISO3_YNUM,
				G2Affine.ISO3_YDEN
			};

			Fp2 x = u.X;
			Fp2 y = u.Y;
			Fp2 z = u.Z;

			Fp2[] mapvals = new[] { Fp2.Zero(), Fp2.Zero(), Fp2.Zero(), Fp2.Zero() };

			Fp2 zsq = z.Square();
			Fp2[] zpows = new[] { z, zsq, zsq * z };

			for (int idx = 0; idx < 4; idx++)
			{
				Fp2[] coeff = coeffs[idx];
				Fp2 clast = coeff[coeff.Length - 1];
				mapvals[idx] = clast;
				for (int jdx = 0; jdx < coeff.Length - 1; jdx++)
				{
					mapvals[idx] = mapvals[idx] * x + zpows[jdx] * coeff[coeff.Length - 2 - jdx];
				}
			}

			mapvals[1] *= z;

			mapvals[2] *= y;
			mapvals[3] *= z;

			return new G2Projective(mapvals[0] * mapvals[3], mapvals[2] * mapvals[1], mapvals[1] * mapvals[3]);
		}

		private static (Fp2, Fp2) HashToField(byte[] message, byte[] dst)
		{
			const int byteLength = 128;
			(Fp2, Fp2) result = (Fp2.Zero(), Fp2.Zero());
			Expander ex = Expander.Create(message, dst, byteLength);
			(int aa, byte[] a) = ex.ReadInto();
			(int bb, byte[] b) = ex.ReadInto();
			return (
				FromOkm(a),
				FromOkm(b)
			);

			//let mut buf = GenericArray::< u8, Self::InputLength >::default();
			//output.iter_mut().for_each(| item | {
			//	expander.read_into(&mut buf[..]);
			//	*item = Self::from_okm(&buf);
			//});
		}


		private static Fp FromOkm(byte[] okm)
		{
			if (okm.Length != 64)
			{
				throw new ArgumentException("Invalid OKM length");
			}
			Fp F2256 = new(
				0x075b_3cd7_c5ce_820f,
				0x3ec6_ba62_1c3e_db0b,
				0x168a_13d8_2bff_6bce,
				0x8766_3c4b_f8c4_49d2,
				0x15f3_4c83_ddc8_d830,
				0x0f96_28b4_9caa_2e85
			);
			var bs = new byte[48];
			Array.Copy(okm, 0, bs, 16, 48);
			Fp db = Fp.FromBytes(bs);
			Array.Copy(okm, 32, bs, 16, 48);
			Fp da = Fp.FromBytes(bs);
			return db * F2256 + da;
		}

		private bool Verify(G2Affine signature, G2Projective[] g2Values, G1Projective[] g1Values)
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

			G1Affine g1Neg = G1Affine.Generator().Neg();
			G2Prepared signaturePrepared = signature.ToPrepared();
			i = 0;
			Fp12 r = BlsUtil.MillerLoop(
					Fp12.One(),
					(f) => DoublingStep(f, g1Neg, signaturePrepared, ref i),
					(f) => AddingStep(f, g1Neg, signaturePrepared, ref i),
					(f) => f.Square(),
					(f) => f.Conjugate()
				);
			millerLoopValue *= r;

			return FinalExponentiation(millerLoopValue).Equals(Fp12.One());
		}


		private static Fp12 DoublingStep(Fp12 f, G1Affine publicKey, G2Prepared hash, ref int index)
		{
			try
			{
				bool eitherIdentity = publicKey.IsIdentity() || hash.IsInfinity;
				if (eitherIdentity)
				{
					return f;
				}
				Fp12 newF = Ell(f, hash.Coefficients[index], publicKey);
				return newF;
			}
			finally
			{
				index++;
			}
		}

		private static Fp12 AddingStep(Fp12 f, G1Affine publicKey, G2Prepared hash, ref int index)
		{
			try
			{
				bool eitherIdentity = publicKey.IsIdentity() || hash.IsInfinity;
				if (eitherIdentity)
				{
					return f;
				}
				Fp12 newF = Ell(f, hash.Coefficients[index], publicKey);
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
