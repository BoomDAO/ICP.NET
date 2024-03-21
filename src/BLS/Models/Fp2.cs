using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	internal struct Fp2
	{
		public static readonly Fp2 B;
		public static readonly Fp2 B3;
		static Fp2()
		{
			B = new Fp2(
				new Fp(
					0xaa27_0000_000c_fff3,
					0x53cc_0032_fc34_000a,
					0x478f_e97a_6b0a_807f,
					0xb1d3_7ebe_e6ba_24d7,
					0x8ec9_733b_bf78_ab2f,
					0x09d6_4551_3d83_de7e
				),
				new Fp(
					0xaa27_0000_000c_fff3,
					0x53cc_0032_fc34_000a,
					0x478f_e97a_6b0a_807f,
					0xb1d3_7ebe_e6ba_24d7,
					0x8ec9_733b_bf78_ab2f,
					0x09d6_4551_3d83_de7e
				)
			);
			B3 = B + B + B;
		}
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

		public Fp2 Subtract(Fp2 rhs)
		{
			return new Fp2(this.C0.Subtract(rhs.C0), this.C1.Subtract(rhs.C1));
		}

		public static Fp2 operator -(Fp2 lhs, Fp2 rhs)
		{
			return lhs.Subtract(rhs);
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

		public Fp2? SquareRoot()
		{
			if (this.IsZero())
			{
				return null;
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
			if (alpha.Equals(Fp2.One().Neg()))
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
			if (!sqrt.Square().Equals(this))
			{
				return null;
			}
			return sqrt;
		}

		public Fp2 Pow(Fp by)
		{
			Fp2 res = Fp2.One();
			for (int i = 5; i >= 0; i--)
			{
				ulong v = by.GetValueByIndex(i);
				for (int j = 63; j >= 0; j--)
				{
					res = res.Square();
					if (((v >> j) & 1) == 1)
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
			Fp t4 = this.C0.Multiply(t3.Value);
			Fp t5 = this.C1.Multiply(t3.Value.Neg());
			return new Fp2(t4, t5);
		}

		public Fp2 FrobeniusMap()
		{
			return this.Conjugate();
		}

		public Fp2 Conjugate()
		{
			return new Fp2(this.C0, this.C1.Neg());
		}

		public Fp2 MultiplyByNonresidue()
		{
			return new Fp2(this.C0 - this.C1, this.C0 + this.C1);
		}


		public override bool Equals(object obj)
		{
			if (obj is Fp2 fp2)
			{
				return this.Equals(fp2);
			}
			return false;
		}

		public bool Equals(Fp2 rhs)
		{
			return this.C0.Equals(rhs.C0) && this.C1.Equals(rhs.C1);
		}

		public override int GetHashCode()
		{
			return this.C0.GetHashCode() ^ this.C1.GetHashCode();
		}

		public override string ToString()
		{
			return $"{this.C0} + ({this.C1})*u";
		}

		internal static Fp Default()
		{
			return Fp.Zero();
		}

		internal bool Sgn0()
		{
			bool sign0 = this.C0.Sgn0();
			bool zero0 = this.C0.IsZero();
			bool sign1 = this.C1.Sgn0();
			return sign0 || (zero0 && sign1);
		}
	}
}
