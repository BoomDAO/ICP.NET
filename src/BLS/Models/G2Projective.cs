using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace EdjCase.ICP.BLS.Models
{
	internal struct G2Projective
	{
		public Fp2 X { get; }
		public Fp2 Y { get; }
		public Fp2 Z { get; }

		public G2Projective(Fp2 x, Fp2 y, Fp2 z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}


		public G2Affine ToAffine()
		{
			Fp2 zinv = this.Z.Invert() ?? Fp2.Zero();
			if (zinv.IsZero())
			{
				return G2Affine.Identity();
			}
			Fp2 x = this.X.Multiply(zinv);
			Fp2 y = this.Y.Multiply(zinv);
			return new(x, y, false);
		}

		public static G2Projective FromCompressed(byte[] bytes)
		{
			return G2Affine.FromCompressed(bytes).ToProjective();
		}

		internal byte[] ToCompressed()
		{
			return this.ToAffine().ToCompressed();
		}

		public G2Projective Add(G2Projective rhs)
		{
			Fp2 t0 = this.X * rhs.X;
			Fp2 t1 = this.Y * rhs.Y;
			Fp2 t2 = this.Z * rhs.Z;
			Fp2 t3 = this.X + this.Y;
			Fp2 t4 = rhs.X + rhs.Y;
			t3 = t3 * t4;
			t4 = t0 + t1;
			t3 = t3 - t4;
			t4 = this.Y + this.Z;
			Fp2 x3 = rhs.Y + rhs.Z;
			t4 = t4 * x3;
			x3 = t1 + t2;
			t4 = t4 - x3;
			x3 = this.X + this.Z;
			Fp2 y3 = rhs.X + rhs.Z;
			x3 = x3 * y3;
			y3 = t0 + t2;
			y3 = x3 - y3;
			x3 = t0 + t0;
			t0 = x3 + t0;
			t2 = t2 * Fp2.B3;
			Fp2 z3 = t1 + t2;
			t1 = t1 - t2;
			y3 = y3 * Fp2.B3;
			x3 = t4 * y3;
			t2 = t3 * t1;
			x3 = t2 - x3;
			y3 = y3 * t0;
			t1 = t1 * z3;
			y3 = t1 + y3;
			t0 = t0 * t3;
			z3 = z3 * t4;
			z3 = z3 + t0;

			return new G2Projective(x3, y3, z3);
		}

		public static G2Projective operator +(G2Projective lhs, G2Projective rhs)
		{
			return lhs.Add(rhs);
		}


		private G2Projective Subtract(G2Projective rhs)
		{
			return this.Add(rhs.Neg());
		}

		public static G2Projective operator -(G2Projective lhs, G2Projective rhs)
		{
			return lhs.Subtract(rhs);
		}

		public override bool Equals(object obj)
		{
			if (obj is G2Projective p)
			{
				return this.Equals(p);
			}
			return false;
		}

		public bool Equals(G2Projective other)
		{
			Fp2 x1 = this.X * other.Z;
			Fp2 x2 = other.X * this.Z;

			Fp2 y1 = this.Y * other.Z;
			Fp2 y2 = other.Y * this.Z;

			bool thisIsZero = this.Z.IsZero();
			bool otherIsZero = other.Z.IsZero();

			return (thisIsZero && otherIsZero)
				|| (!thisIsZero && !otherIsZero && x1.Equals(x2) && y1.Equals(y2));
		}


		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
		}

		internal bool IsIdentity()
		{
			return this.Z.IsZero();
		}

		internal G2Projective ClearH()
		{
			G2Projective t1 = this.MultiplyByX();
			G2Projective t2 = this.Psi();
			return this.Double()
			.Psi2()
			.Add(t1
				.Add(t2)
				.MultiplyByX()
				.Subtract(t1)
				.Subtract(t2)
				.Subtract(this)
			);
		}

		private G2Projective Psi2()
		{
			Fp2 psi2CoeffX = new(
				new Fp(
					0xcd03c9e48671f071,
					0x5dab22461fcda5d2,
					0x587042afd3851b95,
					0x8eb60ebe01bacb9e,
					0x03f97d6e83d050d2,
					0x18f0206554638741
				),
				Fp.Zero()
			);

			return new G2Projective(
				this.X * psi2CoeffX,
				this.Y.Neg(),
				this.Z
			);
		}

		private G2Projective Psi()
		{
			Fp2 psiCoeffX = new(
				Fp.Zero(),
				new Fp(
					0x890dc9e4867545c3,
					0x2af322533285a5d5,
					0x50880866309b7e2c,
					0xa20d1b8c7e881024,
					0x14e4f04fe2db9068,
					0x14e56d3f1564853a
				)
			);
			Fp2 psiCoeffY = new(
				new Fp(
					0x3e2f585da55c9ad1,
					0x4294213d86c18183,
					0x382844c88b623732,
					0x92ad2afd19103e18,
					0x1d794e4fac7cf0b9,
					0x0bd592fc7d825ec8
				),
				new Fp(
					0x7bcfa7a25aa30fda,
					0xdc17dec12a927e7c,
					0x2f088dd86b4ebef1,
					0xd1ca2087da74d4a7,
					0x2da2596696cebc1d,
					0x0e2b7eedbbfd87d2
				)
			);

			return new G2Projective(
				this.X.FrobeniusMap() * psiCoeffX,
				this.Y.FrobeniusMap() * psiCoeffY,
				this.Z.FrobeniusMap()
			);
		}

		private G2Projective MultiplyByX()
		{
			G2Projective xself = G2Projective.Identity();
			ulong x = Constants.BLS_X >> 1;
			G2Projective acc = this;
			while (x != 0)
			{
				acc = acc.Double();
				if (x % 2 == 1)
				{
					xself += acc;
				}
				x >>= 1;
			}
			xself = xself.Neg();
			return xself;
		}

		private G2Projective Neg()
		{
			return new G2Projective(this.X, this.Y.Neg(), this.Z);
		}

		private static G2Projective Identity()
		{
			return new G2Projective(Fp2.Zero(), Fp2.One(), Fp2.Zero());
		}

		private G2Projective Double()
		{
			Fp2 t0 = this.Y.Square();
			Fp2 z3 = t0 + t0;
			z3 = z3 + z3;
			z3 = z3 + z3;
			Fp2 t1 = this.Y * this.Z;
			Fp2 t2 = this.Z.Square();
			t2 = t2 * Fp2.B3;
			Fp2 x3 = t2 * z3;
			Fp2 y3 = t0 + t2;
			z3 = t1 * z3;
			t1 = t2 + t2;
			t2 = t1 + t2;
			t0 = t0 - t2;
			y3 = t0 * y3;
			y3 = x3 + y3;
			t1 = this.X * this.Y;
			x3 = t0 * t1;
			x3 = x3 + x3;


			G2Projective a = new(x3, y3, z3);
			if (a.IsIdentity())
			{
				return G2Projective.Identity();
			}
			return a;
		}



		public static G2Projective HashToCurve(byte[] message, byte[] dst)
		{
			int outputLength = 128;
			int hashSize = 32;
			(Fp2 u1, Fp2 u2) = HashToFieldF2(message, dst, outputLength, hashSize);
			G2Projective p1 = MapToCurveG2(u1);
			G2Projective p2 = MapToCurveG2(u2);
			return (p1 + p2).ClearH();
		}

		private static G2Projective MapToCurveG2(Fp2 u)
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
			if (u.Sgn0() ^ y.Sgn0())
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
			var1 = Square(var1, 2);
			var1 *= var0;
			var1 = Square(var1, 9);
			var1 *= var12;
			var1 = Square(var1, 4);
			var1 *= var5;
			var1 = Square(var1, 6);
			var1 *= var14;
			var1 = Square(var1, 4);
			var1 *= var3;
			var1 = Square(var1, 5);
			var1 *= var2;
			var1 = Square(var1, 8);
			var1 *= var5;
			var1 = Square(var1, 4);
			var1 *= var3;
			var1 = Square(var1, 4);
			var1 *= var10;
			var1 = Square(var1, 8);
			var1 *= var8;
			var1 = Square(var1, 6);
			var1 *= var13;
			var1 = Square(var1, 4);
			var1 *= var5;
			var1 = Square(var1, 3);
			var1 *= var0;
			var1 = Square(var1, 6);
			var1 *= var10;
			var1 = Square(var1, 8);
			var1 *= var8;
			var1 = Square(var1, 6);
			var1 *= var4;
			var1 = Square(var1, 8);
			var1 *= var9;
			var1 = Square(var1, 5);
			var1 *= var10;
			var1 = Square(var1, 6);
			var1 *= var14;
			var1 = Square(var1, 5);
			var1 *= var10;
			var1 = Square(var1, 2);
			var1 *= var0;
			var1 = Square(var1, 6);
			var1 *= var10;
			var1 = Square(var1, 7);
			var1 *= var13;
			var1 = Square(var1, 4);
			var1 *= var3;
			var1 = Square(var1, 6);
			var1 *= var14;
			var1 = Square(var1, 7);
			var1 *= var3;
			var1 = Square(var1, 5);
			var1 *= var15;
			var1 = Square(var1, 7);
			var1 *= var3;
			var1 = Square(var1, 5);
			var1 *= var3;
			var1 = Square(var1, 10);
			var1 *= var9;
			var1 = Square(var1, 3);
			var1 *= var15;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 8);
			var1 *= var6;
			var1 = Square(var1, 5);
			var1 *= var7;
			var1 = Square(var1, 6);
			var1 *= var13;
			var1 = Square(var1, 6);
			var1 *= var10;
			var1 = Square(var1, 6);
			var1 *= var14;
			var1 = Square(var1, 7);
			var1 *= var16;
			var1 = Square(var1, 5);
			var1 *= var14;
			var1 = Square(var1, 6);
			var1 *= var10;
			var1 = Square(var1, 6);
			var1 *= var9;
			var1 = Square(var1, 5);
			var1 *= var10;
			var1 = Square(var1, 2);
			var1 *= var0;
			var1 = Square(var1, 8);
			var1 *= var15;
			var1 = Square(var1, 7);
			var1 *= var15;
			var1 = Square(var1, 4);
			var1 *= var2;
			var1 = Square(var1, 7);
			var1 *= var13;
			var1 = Square(var1, 6);
			var1 *= var10;
			var1 = Square(var1, 4);
			var1 *= var5;
			var1 = Square(var1, 7);
			var1 *= var13;
			var1 = Square(var1, 6);
			var1 *= var12;
			var1 = Square(var1, 5);
			var1 *= var7;
			var1 = Square(var1, 5);
			var1 *= var15;
			var1 = Square(var1, 7);
			var1 *= var12;
			var1 = Square(var1, 5);
			var1 *= var7;
			var1 = Square(var1, 5);
			var1 *= var4;
			var1 = Square(var1, 4);
			var1 *= var2;
			var1 = Square(var1, 6);
			var1 *= var15;
			var1 = Square(var1, 6);
			var1 *= var14;
			var1 = Square(var1, 4);
			var1 *= var2;
			var1 = Square(var1, 4);
			var1 *= var2;
			var1 = Square(var1, 8);
			var1 *= var14;
			var1 = Square(var1, 5);
			var1 *= var10;
			var1 = Square(var1, 6);
			var1 *= var3;
			var1 = Square(var1, 5);
			var1 *= var10;
			var1 = Square(var1, 12);
			var1 *= var9;
			var1 = Square(var1, 4);
			var1 *= var5;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 6);
			var1 *= var2;
			var1 = Square(var1, 9);
			var1 *= var6;
			var1 = Square(var1, 5);
			var1 *= var6;
			var1 = Square(var1, 6);
			var1 *= var2;
			var1 = Square(var1, 6);
			var1 *= var2;
			var1 = Square(var1, 9);
			var1 *= var7;
			var1 = Square(var1, 7);
			var1 *= var10;
			var1 = Square(var1, 6);
			var1 *= var6;
			var1 = Square(var1, 5);
			var1 *= var14;
			var1 = Square(var1, 7);
			var1 *= var7;
			var1 = Square(var1, 2);
			var1 *= var0;
			var1 = Square(var1, 8);
			var1 *= var13;
			var1 = Square(var1, 4);
			var1 *= var15;
			var1 = Square(var1, 7);
			var1 *= var3;
			var1 = Square(var1, 8);
			var1 *= var14;
			var1 = Square(var1, 7);
			var1 *= var5;
			var1 = Square(var1, 10);
			var1 *= var14;
			var1 = Square(var1, 6);
			var1 *= var13;
			var1 = Square(var1, 6);
			var1 *= var5;
			var1 = Square(var1, 6);
			var1 *= var11;
			var1 = Square(var1, 5);
			var1 *= var6;
			var1 = Square(var1, 7);
			var1 *= var10;
			var1 = Square(var1, 5);
			var1 *= var5;
			var1 = Square(var1, 7);
			var1 *= var11;
			var1 = Square(var1, 5);
			var1 *= var3;
			var1 = Square(var1, 8);
			var1 *= var12;
			var1 = Square(var1, 6);
			var1 *= var8;
			var1 = Square(var1, 6);
			var1 *= var2;
			var1 = Square(var1, 7);
			var1 *= var13;
			var1 = Square(var1, 7);
			var1 *= var13;
			var1 = Square(var1, 6);
			var1 *= var2;
			var1 = Square(var1, 5);
			var1 *= var3;
			var1 = Square(var1, 10);
			var1 *= var12;
			var1 = Square(var1, 4);
			var1 *= var0;
			var1 = Square(var1, 9);
			var1 *= var9;
			var1 = Square(var1, 6);
			var1 *= var10;
			var1 = Square(var1, 7);
			var1 *= var11;
			var1 = Square(var1, 5);
			var1 *= var4;
			var1 = Square(var1, 4);
			var1 *= var10;
			var1 = Square(var1, 7);
			var1 *= var8;
			var1 = Square(var1, 5);
			var1 *= var4;
			var1 = Square(var1, 5);
			var1 *= var4;
			var1 = Square(var1, 5);
			var1 *= var9;
			var1 = Square(var1, 4);
			var1 *= var5;
			var1 = Square(var1, 6);
			var1 *= var8;
			var1 = var1.Square();
			var1 *= var0;
			var1 = Square(var1, 6);
			var1 *= var3;
			var1 = Square(var1, 10);
			var1 *= var7;
			var1 = Square(var1, 6);
			var1 *= var4;
			var1 = Square(var1, 6);
			var1 *= var6;
			var1 = Square(var1, 6);
			var1 *= var5;
			var1 = Square(var1, 6);
			var1 *= var4;
			var1 = Square(var1, 23);
			var1 *= var3;
			var1 = Square(var1, 6);
			var1 *= var3;
			var1 = Square(var1, 5);
			var1 *= var2;
			var1 = Square(var1, 6);
			var1 *= var3;
			var1 = Square(var1, 5);
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

			return new G2Projective(mapvals[0] * mapvals[3], mapvals[2] * mapvals[1], mapvals[1] * mapvals[3]);
		}

		private static (Fp2, Fp2) HashToFieldF2(byte[] message, byte[] dst, int outputLength, int hashSize)
		{
			const int byteLength = 256;
			Expander ex = Expander.Create(message, dst, byteLength, hashSize);
			byte[] a = ex.ReadInto(outputLength, hashSize);
			byte[] b = ex.ReadInto(outputLength, hashSize);
			return (
				FromOkmFp2(a),
				FromOkmFp2(b)
			);
		}

		private static Fp2 FromOkmFp2(byte[] okm)
		{
			if (okm.Length != 128)
			{
				throw new ArgumentException("Invalid OKM length");
			}
			Fp c0 = BlsUtil.FromOkmFp(okm[..64]);
			Fp c1 = BlsUtil.FromOkmFp(okm[64..]);
			return new Fp2(c0, c1);
		}

		internal G2Prepared ToPrepared()
		{
			G2Affine q = this.IsIdentity() ? G2Affine.Generator() : this.ToAffine();
			List<(Fp2, Fp2, Fp2)> coeffs = new();
			G2Projective cur = this;
			BlsUtil.MillerLoop(
			0, // Dummy value instead of void
			(f) =>
			{
				cur = G2Prepared.DoublingStep(cur, out var values);
				coeffs.Add(values);
				return f; // Dummy
			},
			(f) =>
			{
				cur = G2Prepared.AdditionStep(cur, q, out var values);
				coeffs.Add(values);
				return f; // Dummy
			},
				(f) => f, // Dummy
				(f) => f // Dummy
			);

			return new G2Prepared(q.IsInfinity, coeffs.ToArray());
		}
	}
}
