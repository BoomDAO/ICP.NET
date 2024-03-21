using System;
using System.Collections.Generic;

namespace EdjCase.ICP.BLS.Models
{
	internal struct G1Projective
	{
		public Fp X { get; }
		public Fp Y { get; }
		public Fp Z { get; }

		public G1Projective(Fp x, Fp y, Fp z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}


		public G1Affine ToAffine()
		{
			Fp zinv = this.Z.Invert() ?? Fp.Zero();
			if (zinv.IsZero())
			{
				return G1Affine.Identity();
			}
			Fp x = this.X.Multiply(zinv);
			Fp y = this.Y.Multiply(zinv);
			return new(x, y, false);
		}
		public static G1Projective FromCompressed(byte[] bytes)
		{
			return G1Affine.FromCompressed(bytes).ToProjective();
		}

		public bool IsIdentity()
		{
			return this.Z.IsZero();
		}

		public override bool Equals(object obj)
		{
			if (obj is G2Projective p)
			{
				return this.Equals(p);
			}
			return false;
		}

		public bool Equals(G1Projective other)
		{
			Fp x1 = this.X * other.Z;
			Fp x2 = other.X * this.Z;

			Fp y1 = this.Y * other.Z;
			Fp y2 = other.Y * this.Z;

			bool thisIsZero = this.Z.IsZero();
			bool otherIsZero = other.Z.IsZero();

			return (thisIsZero && otherIsZero)
				|| (!thisIsZero && !otherIsZero && x1.Equals(x2) && y1.Equals(y2));
		}

		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
		}

		public G1Projective Add(G1Projective rhs)
		{
			Fp t0 = this.X * rhs.X;
			Fp t1 = this.Y * rhs.Y;
			Fp t2 = this.Z * rhs.Z;
			Fp t3 = this.X + this.Y;
			Fp t4 = rhs.X + rhs.Y;
			t3 = t3 * t4;
			t4 = t0 + t1;
			t3 = t3 - t4;
			t4 = this.Y + this.Z;
			Fp x3 = rhs.Y + rhs.Z;
			t4 = t4 * x3;
			x3 = t1 + t2;
			t4 = t4 - x3;
			x3 = this.X + this.Z;
			Fp y3 = rhs.X + rhs.Z;
			x3 = x3 * y3;
			y3 = t0 + t2;
			y3 = x3 - y3;
			x3 = t0 + t0;
			t0 = x3 + t0;
			t2 = MultiplyBy3B(t2);
			Fp z3 = t1 + t2;
			t1 = t1 - t2;
			y3 = MultiplyBy3B(y3);
			x3 = t4 * y3;
			t2 = t3 * t1;
			x3 = t2 - x3;
			y3 = y3 * t0;
			t1 = t1 * z3;
			y3 = t1 + y3;
			t0 = t0 * t3;
			z3 = z3 * t4;
			z3 = z3 + t0;

			return new G1Projective(x3, y3, z3);
		}

		public static G1Projective operator +(G1Projective lhs, G1Projective rhs)
		{
			return lhs.Add(rhs);
		}

		public G1Projective Subtract(G1Projective rhs)
		{
			return this + rhs.Neg();
		}

		public static G1Projective operator -(G1Projective lhs, G1Projective rhs)
		{
			return lhs.Subtract(rhs);
		}

		public static Fp MultiplyBy3B(Fp a)
		{
			a = a + a;
			a = a + a;
			return a + a + a;
		}

		internal G1Projective ClearH()
		{
			return this - this.MultiplyByX();
		}

		private G1Projective MultiplyByX()
		{
			G1Projective xself = G1Projective.Identity();

			ulong x = Constants.BLS_X >> 1;
			G1Projective tmp = this;
			while (x != 0)
			{
				tmp = tmp.Double();

				if (x % 2 == 1)
				{
					xself += tmp;
				}
				x >>= 1;
			}
			return xself.Neg();
		}

		private G1Projective Neg()
		{
			return new G1Projective(this.X, this.Y.Neg(), this.Z);
		}

		private G1Projective Double()
		{
			Fp t0 = this.Y.Square();
			Fp z3 = t0 + t0;
			z3 = z3 + z3;
			z3 = z3 + z3;
			Fp t1 = this.Y * this.Z;
			Fp t2 = this.Z.Square();
			t2 = MultiplyBy3B(t2);
			Fp x3 = t2 * z3;
			Fp y3 = t0 + t2;
			z3 = t1 * z3;
			t1 = t2 + t2;
			t2 = t1 + t2;
			t0 = t0 - t2;
			y3 = t0 * y3;
			y3 = x3 + y3;
			t1 = this.X * this.Y;
			x3 = t0 * t1;
			x3 = x3 + x3;

			if (this.IsIdentity())
			{
				return G1Projective.Identity();
			}

			return new G1Projective(x3, y3, z3);
		}

		private static G1Projective Identity()
		{
			return new G1Projective(Fp.Zero(), Fp.One(), Fp.Zero());
		}


		public static G1Projective HashToCurve(byte[] message, byte[] dst)
		{
			int outputLength = 64;
			int hashSize = 32;
			(Fp u1, Fp u2) = HashToFieldFp(message, dst, outputLength, hashSize);
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
			int byteLength = outputLength * 2;
			Expander ex = Expander.Create(message, dst, byteLength, hashSize);
			byte[] a = ex.ReadInto(outputLength, hashSize);
			byte[] b = ex.ReadInto(outputLength, hashSize);
			return (
				BlsUtil.FromOkmFp(a),
				BlsUtil.FromOkmFp(b)
			);
		}

		// TODO BLSICP
		//internal G1Prepared ToPrepared()
		//{
		//	G1Affine q = this.IsIdentity() ? G1Affine.Generator() : this.ToAffine();
		//	List<(Fp, Fp, Fp)> coeffs = new();
		//	G1Projective cur = this;
		//	BlsUtil.MillerLoop(
		//	0, // Dummy value instead of void
		//	(f) =>
		//	{
		//		cur = G1Prepared.DoublingStep(cur, out var values);
		//		coeffs.Add(values);
		//		return f; // Dummy
		//	},
		//	(f) =>
		//	{
		//		cur = G1Prepared.AdditionStep(cur, q, out var values);
		//		coeffs.Add(values);
		//		return f; // Dummy
		//	},
		//		(f) => f, // Dummy
		//		(f) => f // Dummy
		//	);

		//	return new G1Prepared(q.IsInfinity, coeffs);
		//}


	}
}
