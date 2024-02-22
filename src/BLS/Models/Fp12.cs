using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	internal class Fp12
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

			c1 = c1 * new Fp6(
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
			return new Fp12(this.C0.Multiply(t), this.C1.Multiply(t.Neg()));
		}

		public Fp12 Multiply(Fp12 t1)
		{
			// let aa = self.c0 * other.c0;
			//         let bb = self.c1 * other.c1;
			//         let o = other.c0 + other.c1;
			//         let c1 = self.c1 + self.c0;
			//         let c1 = c1 * o;
			//         let c1 = c1 - aa;
			//         let c1 = c1 - bb;
			//         let c0 = bb.mul_by_nonresidue();
			//         let c0 = c0 + aa;

			//         Fp12 { c0, c1 }
			Fp6 aa = this.C0.Multiply(t1.C0);
			Fp6 bb = this.C1.Multiply(t1.C1);
			Fp6 o = t1.C0.Add(t1.C1);
			Fp6 c1 = this.C1.Add(this.C0);
			c1 = c1.Multiply(o);
			c1 = c1.Subtract(aa);
			c1 = c1.Subtract(bb);
			Fp6 c0 = bb.MultiplyByNonresidue();
			c0 = c0.Add(aa);
			return new Fp12(c0, c1);
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
	}
}
