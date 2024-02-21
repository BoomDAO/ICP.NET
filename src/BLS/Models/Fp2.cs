using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	internal class Fp2
	{
		public readonly Fp C0;
		public readonly Fp C1;

		public Fp2(Fp c0, Fp c1)
		{
			this.C0 = c0;
			this.C1 = c1;
		}

		public static Fp2 Zero()
		{
			return new Fp2(Fp.Zero(), Fp.Zero());
		}

		public static Fp2 One()
		{
			return new Fp2(Fp.One(), Fp.Zero());
		}

		public bool IsZero()
		{
			return this.C0.IsZero() && this.C1.IsZero();
		}

		public Fp2 Square()
		{
			Fp a = this.C0.Add(this.C1);
			Fp b = this.C0.Subtract(this.C1);
			Fp c = this.C0.Add(this.C0);


			Fp newC0 = a.Multiply(b);
			Fp newC1 = c.Multiply(this.C1);
			return new Fp2(newC0, newC1);
		}

		public bool LexicographicallyLargest()
		{
			return this.C1.LexicographicallyLargest()
			|| (this.C1.IsZero() && this.C0.LexicographicallyLargest());
		}

		public Fp2 Add(Fp2 rhs)
		{
			return new Fp2(this.C0.Add(rhs.C0), this.C1.Add(rhs.C1));
		}

		public static Fp2 operator +(Fp2 lhs, Fp2 rhs)
		{
			return lhs.Add(rhs);
		}

		public Fp2 Multiply(Fp2 rhs)
		{
			Fp newC0 = Fp.SumOfProducts(new[] { this.C0, this.C1.Neg() }, new[] { rhs.C0, rhs.C1 });
			Fp newC1 = Fp.SumOfProducts(new[] { this.C0, this.C1 }, new[] { rhs.C1, rhs.C0 });
			return new Fp2(newC0, newC1);
		}

		public static Fp2 operator *(Fp2 lhs, Fp2 rhs)
		{
			return lhs.Multiply(rhs);
		}

		public Fp2 Neg()
		{
			return new Fp2(this.C0.Neg(), this.C1.Neg());
		}

		public Fp2 SquareRoot()
		{
			if (this.IsZero())
			{
				return Fp2.Zero();
			}
			Fp2 a1 = this.Pow(new Fp(
				0xee7f_bfff_ffff_eaaa,
				0x07aa_ffff_ac54_ffff,
				0xd9cc_34a8_3dac_3d89,
				0xd91d_d2e1_3ce1_44af,
				0x92c6_e9ed_90d2_eb35,
				0x0680_447a_8e5f_f9a6
			));
			Fp2 alpha = a1.Square() * this;
			Fp2 x0 = a1 * this;
			if (alpha == Fp2.One().Neg())
			{
				return new Fp2(x0.C1.Neg(), x0.C0);
			}
			Fp2 sqrt = (alpha + Fp2.One()).Pow(new Fp(
				0xdcff_7fff_ffff_d555,
				0x0f55_ffff_58a9_ffff,
				0xb398_6950_7b58_7b12,
				0xb23b_a5c2_79c2_895f,
				0x258d_d3db_21a5_d66b,
				0x0d00_88f5_1cbf_f34d
			)) * x0;
			if (sqrt.Square() != this)
			{
				throw new InvalidOperationException("No square root exists for this element.");
			}
			return sqrt;
		}

		public Fp2 Pow(Fp by)
		{
			Fp2 res = Fp2.One();
			for (int i = 5; i >= 0; i--)
			{
				for (int j = 63; j >= 0; j--)
				{
					res = res.Square();
					if (((by.Values[i] >> j) & 1) == 1)
					{
						res *= this;
					}
				}
			}
			return res;
		}

		public Fp2? Invert()
		{
			Fp t0 = this.C0.Square();
			Fp t1 = this.C1.Square();
			Fp t2 = t0.Add(t1);
			Fp? t3 = t2.Invert();
			if (t3 == null)
			{
				return null;
			}
			Fp t4 = this.C0.Multiply(t3);
			Fp t5 = this.C1.Multiply(t3.Neg());
			return new Fp2(t4, t5);
		}

	}
}
