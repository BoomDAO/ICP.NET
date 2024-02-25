using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	internal struct Fp6
	{
		public Fp2 C0 { get; }
		public Fp2 C1 { get; }
		public Fp2 C2 { get; }

		public Fp6(Fp2 c0, Fp2 c1, Fp2 c2)
		{
			this.C0 = c0;
			this.C1 = c1;
			this.C2 = c2;
		}

		public Fp6 Add(Fp6 rhs)
		{
			return new Fp6(this.C0.Add(rhs.C0), this.C1.Add(rhs.C1), this.C2.Add(rhs.C2));
		}

		public static Fp6 operator +(Fp6 lhs, Fp6 rhs)
		{
			return lhs.Add(rhs);
		}

		public Fp6 Subtract(Fp6 rhs)
		{
			Fp2 c0 = this.C0.Subtract(rhs.C0);
			Fp2 c1 = this.C1.Subtract(rhs.C1);
			Fp2 c2 = this.C2.Subtract(rhs.C2);
			return new Fp6(c0, c1, c2);
		}

		public static Fp6 operator -(Fp6 lhs, Fp6 rhs)
		{
			return lhs.Subtract(rhs);
		}

		public Fp6 Multiply(Fp6 rhs)
		{
			Fp6 a = this;
			Fp b10_p_b11 = rhs.C1.C0.Add(rhs.C1.C1);
			Fp b10_m_b11 = rhs.C1.C0.Subtract(rhs.C1.C1);
			Fp b20_p_b21 = rhs.C2.C0.Add(rhs.C2.C1);
			Fp b20_m_b21 = rhs.C2.C0.Subtract(rhs.C2.C1);

			Fp2 c0 = new Fp2(
				Fp.SumOfProducts(
					new Fp[] { a.C0.C0, a.C0.C1.Neg(), a.C1.C0, a.C1.C1.Neg(), a.C2.C0, a.C2.C1.Neg() },
					new Fp[] { rhs.C0.C0, rhs.C0.C1, b20_m_b21, b20_p_b21, b10_m_b11, b10_p_b11 }
				),
				Fp.SumOfProducts(
					new Fp[] { a.C0.C0, a.C0.C1, a.C1.C0, a.C1.C1, a.C2.C0, a.C2.C1 },
					new Fp[] { rhs.C0.C1, rhs.C0.C0, b20_p_b21, b20_m_b21, b10_p_b11, b10_m_b11 }
				)
			);

			Fp2 c1 = new Fp2(
				Fp.SumOfProducts(
					new Fp[] { a.C0.C0, a.C0.C1.Neg(), a.C1.C0, a.C1.C1.Neg(), a.C2.C0, a.C2.C1.Neg() },
					new Fp[] { rhs.C1.C0, rhs.C1.C1, rhs.C0.C0, rhs.C0.C1, b20_m_b21, b20_p_b21 }
				),
				Fp.SumOfProducts(
					new Fp[] { a.C0.C0, a.C0.C1, a.C1.C0, a.C1.C1, a.C2.C0, a.C2.C1 },
					new Fp[] { rhs.C1.C1, rhs.C1.C0, rhs.C0.C1, rhs.C0.C0, b20_p_b21, b20_m_b21 }
				)
			);

			Fp2 c2 = new Fp2(
				Fp.SumOfProducts(
					new Fp[] { a.C0.C0, a.C0.C1.Neg(), a.C1.C0, a.C1.C1.Neg(), a.C2.C0, a.C2.C1.Neg() },
					new Fp[] { rhs.C2.C0, rhs.C2.C1, rhs.C1.C0, rhs.C1.C1, rhs.C0.C0, rhs.C0.C1 }
				),
				Fp.SumOfProducts(
					new Fp[] { a.C0.C0, a.C0.C1, a.C1.C0, a.C1.C1, a.C2.C0, a.C2.C1 },
					new Fp[] { rhs.C2.C1, rhs.C2.C0, rhs.C1.C1, rhs.C1.C0, rhs.C0.C1, rhs.C0.C0 }
				)
			);

			return new Fp6(c0, c1, c2);
		}

		public static Fp6 operator *(Fp6 lhs, Fp6 rhs)
		{
			return lhs.Multiply(rhs);
		}

		public Fp6 FrobeniusMap()
		{
			Fp2 c0 = this.C0.FrobeniusMap();
			Fp2 c1 = this.C1.FrobeniusMap();
			Fp2 c2 = this.C2.FrobeniusMap();

			c1 *= new Fp2(
				Fp.Zero(),
				new Fp(
					0xcd03_c9e4_8671_f071,
					0x5dab_2246_1fcd_a5d2,
					0x5870_42af_d385_1b95,
					0x8eb6_0ebe_01ba_cb9e,
					0x03f9_7d6e_83d0_50d2,
					0x18f0_2065_5463_8741
				)
			);

			c2 *= new Fp2(
				new Fp(
					0x890d_c9e4_8675_45c3,
					0x2af3_2253_3285_a5d5,
					0x5088_0866_309b_7e2c,
					0xa20d_1b8c_7e88_1024,
					0x14e4_f04f_e2db_9068,
					0x14e5_6d3f_1564_853a
				),
				Fp.Zero()
			);

			return new Fp6(c0, c1, c2);
		}

		internal Fp6 Square()
		{
			Fp2 s0 = this.C0.Square();
			Fp2 ab = this.C0 * this.C1;
			Fp2 s1 = ab + ab;
			Fp2 s2 = (this.C0 - this.C1 + this.C2).Square();
			Fp2 bc = this.C1 * this.C2;
			Fp2 s3 = bc + bc;
			Fp2 s4 = this.C2.Square();

			Fp2 c0 = s3.MultiplyByNonresidue() + s0;
			Fp2 c1 = s4.MultiplyByNonresidue() + s1;
			Fp2 c2 = s1 + s2 + s3 - s0 - s4;

			return new Fp6(c0, c1, c2);
		}

		public Fp6 MultiplyByNonresidue()
		{
			Fp2 c0 = this.C2.MultiplyByNonresidue();
			Fp2 c1 = this.C0;
			Fp2 c2 = this.C1;
			return new Fp6(c0, c1, c2);
		}

		public Fp6? Invert()
		{
			Fp2 c0 = (this.C1 * this.C2).MultiplyByNonresidue();
			c0 = this.C0.Square() - c0;

			Fp2 c1 = this.C2.Square().MultiplyByNonresidue();
			c1 -= this.C0 * this.C1;

			Fp2 c2 = this.C1.Square();
			c2 -= this.C0 * this.C2;

			Fp2 tmp = ((this.C1 * c2) + (this.C2 * c1)).MultiplyByNonresidue();
			tmp += this.C0 * c0;

			Fp2? t = tmp.Invert();
			if (t == null)
			{
				return null;
			}
			return new Fp6(c0.Multiply(t.Value), c1.Multiply(t.Value), c2.Multiply(t.Value));
		}

		public Fp6 Neg()
		{
			return new Fp6(this.C0.Neg(), this.C1.Neg(), this.C2.Neg());
		}

		public static Fp6 Zero()
		{
			return new Fp6(Fp2.Zero(), Fp2.Zero(), Fp2.Zero());
		}

		public static Fp6 One()
		{
			return new Fp6(Fp2.One(), Fp2.Zero(), Fp2.Zero());
		}

		internal Fp6 MultiplyBy01(Fp2 c0, Fp2 c1)
		{
			Fp2 a_a = this.C0.Multiply(c0);
			Fp2 b_b = this.C1.Multiply(c1);

			Fp2 t1 = (this.C2.Multiply(c1).MultiplyByNonresidue()) + a_a;

			Fp2 t2 = (c0 + c1).Multiply(this.C0 + this.C1) - a_a - b_b;

			Fp2 t3 = this.C2.Multiply(c0) + b_b;

			return new Fp6(t1, t2, t3);
		}

		internal Fp6 MultiplyBy1(Fp2 c1)
		{
			return new Fp6(
				this.C2.Multiply(c1).MultiplyByNonresidue(),
				this.C0.Multiply(c1),
				this.C1.Multiply(c1)
			);
		}


		public override bool Equals(object obj)
		{
			if (obj is Fp6 fp6)
			{
				return this.Equals(fp6);
			}
			return false;
		}

		public bool Equals(Fp6 rhs)
		{
			return this.C0.Equals(rhs.C0) && this.C1.Equals(rhs.C1) && this.C2.Equals(rhs.C2);
		}

		public override int GetHashCode()
		{
			return this.C0.GetHashCode() ^ this.C1.GetHashCode() ^ this.C2.GetHashCode();
		}

		public override string ToString()
		{
			return $"{this.C0} + ({this.C1})*v + ({this.C2})*v^2";
		}
	}
}
