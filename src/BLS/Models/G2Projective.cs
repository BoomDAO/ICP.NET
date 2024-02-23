using System;

namespace EdjCase.ICP.BLS.Models
{
	internal class G2Projective
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
	}
}
