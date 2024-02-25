using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	internal struct Fp12
	{
		public Fp6 C0 { get; }
		public Fp6 C1 { get; }

		public Fp12(Fp6 c0, Fp6 c1)
		{
			this.C0 = c0;
			this.C1 = c1;
		}

		public Fp12 FrobeniusMap()
		{
			Fp6 c0 = this.C0.FrobeniusMap();
			Fp6 c1 = this.C1.FrobeniusMap();

			c1 *= new Fp6(
				new Fp2(
					new Fp(
						0x0708_9552_b319_d465,
						0xc669_5f92_b50a_8313,
						0x97e8_3ccc_d117_228f,
						0xa35b_aeca_b2dc_29ee,
						0x1ce3_93ea_5daa_ce4d,
						0x08f2_220f_b0fb_66eb
					),
					new Fp(
						0xb2f6_6aad_4ce5_d646,
						0x5842_a06b_fc49_7cec,
						0xcf48_95d4_2599_d394,
						0xc11b_9cba_40a8_e8d0,
						0x2e38_13cb_e5a0_de89,
						0x110e_efda_8884_7faf
					)
				),
				Fp2.Zero(),
				Fp2.Zero()
			);
			return new Fp12(c0, c1);
		}

		public Fp12? Invert()
		{
			Fp6? t = (this.C0.Square() - this.C1.Square().MultiplyByNonresidue()).Invert();
			if (t == null)
			{
				return null;
			}
			return new Fp12(this.C0.Multiply(t.Value), this.C1.Multiply(t.Value.Neg()));
		}

		public Fp12 Multiply(Fp12 rhs)
		{
			Fp6 aa = this.C0.Multiply(rhs.C0);
			Fp6 bb = this.C1.Multiply(rhs.C1);
			Fp6 o = rhs.C0.Add(rhs.C1);
			Fp6 c1 = this.C1.Add(this.C0);
			c1 = c1.Multiply(o);
			c1 = c1.Subtract(aa);
			c1 = c1.Subtract(bb);
			Fp6 c0 = bb.MultiplyByNonresidue();
			c0 = c0.Add(aa);
			return new Fp12(c0, c1);
		}

		public static Fp12 operator *(Fp12 a, Fp12 b)
		{
			return a.Multiply(b);
		}

		public Fp12 Conjugate()
		{
			return new Fp12(this.C0, this.C1.Neg());
		}

		public static Fp12 Zero()
		{
			return new Fp12(Fp6.Zero(), Fp6.Zero());
		}

		public static Fp12 One()
		{
			return new Fp12(Fp6.One(), Fp6.Zero());
		}

		internal Fp12 Square()
		{
			Fp6 ab = this.C0 * this.C1;
			Fp6 c0c1 = this.C0 + this.C1;
			Fp6 c0 = this.C1.MultiplyByNonresidue();
			c0 += this.C0;
			c0 *= c0c1;
			c0 -= ab;
			Fp6 c1 = ab + ab;
			c0 -= ab.MultiplyByNonresidue();

			return new Fp12(c0, c1);
		}

		internal Fp12 MultiplyBy014(Fp2 c0, Fp2 c1, Fp2 c4)
		{
			Fp6 aa = this.C0.MultiplyBy01(c0, c1);
			Fp6 bb = this.C1.MultiplyBy1(c4);
			Fp2 o = c1 + c4;
			Fp6 newC1 = this.C1 + this.C0;
			newC1 = newC1.MultiplyBy01(c0, o);
			newC1 = newC1 - aa - bb;
			Fp6 newC0 = bb;
			newC0 = newC0.MultiplyByNonresidue();
			newC0 = newC0 + aa;

			return new Fp12(newC0, newC1);
		}


		public Fp12 Add(Fp12 rhs)
		{
			return new Fp12(this.C0 + rhs.C0, this.C1 + rhs.C1);
		}

		public override bool Equals(object obj)
		{
			if (obj is Fp12 fp12)
			{
				return this.Equals(fp12);
			}
			return false;
		}

		public bool Equals(Fp12 rhs)
		{
			return this.C0.Equals(rhs.C0) && this.C1.Equals(rhs.C1);
		}

		public override int GetHashCode()
		{
			return this.C0.GetHashCode() ^ this.C1.GetHashCode();
		}

		public override string ToString()
		{
			return $"{this.C0} + {this.C1}";
		}
	}
}
