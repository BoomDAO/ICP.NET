using System;

namespace EdjCase.ICP.BLS.Models
{
	internal class G1Projective
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
	}
}
