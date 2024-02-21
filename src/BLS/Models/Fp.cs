using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	internal class Fp
	{
		public readonly ulong[] Values;

		// Constants
		public static readonly ulong[] MODULUS = new ulong[] { 0xb9fe_ffff_ffff_aaab, 0x1eab_fffe_b153_ffff, 0x6730_d2a0_f6b0_f624, 0x6477_4b84_f385_12bf, 0x4b1b_a7b6_434b_acd7, 0x1a01_11ea_397f_e69a };
		public static readonly ulong INV = 0x89f3_fffc_fffc_fffd;

		// Static readonly instances of Fp for R, R2, and R3
		public static readonly Fp R;
		public static readonly Fp R2;
		public static readonly Fp R3;

		static Fp()
		{
			R = new Fp(0x7609_0000_0002_fffd, 0xebf4_000b_c40c_0002, 0x5f48_9857_53c7_58ba, 0x77ce_5853_7052_5745, 0x5c07_1a97_a256_ec6d, 0x15f6_5ec3_fa80_e493);
			R2 = new Fp(0xf4df_1f34_1c34_1746, 0x0a76_e6a6_09d1_04f1, 0x8de5_476c_4c95_b6d5, 0x67eb_88a9_939d_83c0, 0x9a79_3e85_b519_952d, 0x1198_8fe5_92ca_e3aa);
			R3 = new Fp(0xed48_ac6b_d94c_a1e0, 0x315f_831e_03a7_adf8, 0x9a53_352a_615e_29dd, 0x34c0_4e5e_921e_1761, 0x2512_d435_6572_4728, 0x0aa6_3460_9175_5d4d);
		}

		public Fp(ulong v0, ulong v1, ulong v2, ulong v3, ulong v4, ulong v5)
		{
			this.Values = new ulong[] {
				v0,
				v1,
				v2,
				v3,
				v4,
				v5
			};
		}

		public static Fp Zero()
		{
			return new Fp(0, 0, 0, 0, 0, 0);
		}

		public static Fp Default()
		{
			return Fp.Zero();
		}

		internal static Fp One()
		{
			return R.Clone();
		}

		public Fp Clone()
		{
			return new Fp(
				this.Values[0],
				this.Values[1],
				this.Values[2],
				this.Values[3],
				this.Values[4],
				this.Values[5]
			);
		}

		public byte[] ToBytes()
		{
			// Turn into canonical form by computing
			// (a.R) / R = a
			Fp tmp = Fp.MontgomeryReduce(
				this.Values[0], this.Values[1], this.Values[2], this.Values[3], this.Values[4], this.Values[5], 0, 0, 0, 0, 0, 0
			);

			byte[] res = new byte[48];
			Array.Copy(BitConverter.GetBytes(tmp.Values[5]).Reverse().ToArray(), 0, res, 0, 8);
			Array.Copy(BitConverter.GetBytes(tmp.Values[4]).Reverse().ToArray(), 0, res, 8, 8);
			Array.Copy(BitConverter.GetBytes(tmp.Values[3]).Reverse().ToArray(), 0, res, 16, 8);
			Array.Copy(BitConverter.GetBytes(tmp.Values[2]).Reverse().ToArray(), 0, res, 24, 8);
			Array.Copy(BitConverter.GetBytes(tmp.Values[1]).Reverse().ToArray(), 0, res, 32, 8);
			Array.Copy(BitConverter.GetBytes(tmp.Values[0]).Reverse().ToArray(), 0, res, 40, 8);

			return res;
		}


		public Fp Square()
		{
			(ulong t1, ulong carry) = BlsUtil.MultiplyAddCarry(0, this.Values[0], this.Values[1], 0);
			(ulong t2, carry) = BlsUtil.MultiplyAddCarry(0, this.Values[0], this.Values[2], carry);
			(ulong t3, carry) = BlsUtil.MultiplyAddCarry(0, this.Values[0], this.Values[3], carry);
			(ulong t4, carry) = BlsUtil.MultiplyAddCarry(0, this.Values[0], this.Values[4], carry);
			(ulong t5, ulong t6) = BlsUtil.MultiplyAddCarry(0, this.Values[0], this.Values[5], carry);

			(t3, carry) = BlsUtil.MultiplyAddCarry(t3, this.Values[1], this.Values[2], 0);
			(t4, carry) = BlsUtil.MultiplyAddCarry(t4, this.Values[1], this.Values[3], carry);
			(t5, carry) = BlsUtil.MultiplyAddCarry(t5, this.Values[1], this.Values[4], carry);
			(t6, ulong t7) = BlsUtil.MultiplyAddCarry(t6, this.Values[1], this.Values[5], carry);

			(t5, carry) = BlsUtil.MultiplyAddCarry(t5, this.Values[2], this.Values[3], 0);
			(t6, carry) = BlsUtil.MultiplyAddCarry(t6, this.Values[2], this.Values[4], carry);
			(t7, ulong t8) = BlsUtil.MultiplyAddCarry(t7, this.Values[2], this.Values[5], carry);

			(t7, carry) = BlsUtil.MultiplyAddCarry(t7, this.Values[3], this.Values[4], 0);
			(t8, ulong t9) = BlsUtil.MultiplyAddCarry(t8, this.Values[3], this.Values[5], carry);

			(t9, ulong t10) = BlsUtil.MultiplyAddCarry(t9, this.Values[4], this.Values[5], 0);

			ulong t11 = t10 >> 63;
			t10 = (t10 << 1) | (t9 >> 63);
			t9 = (t9 << 1) | (t8 >> 63);
			t8 = (t8 << 1) | (t7 >> 63);
			t7 = (t7 << 1) | (t6 >> 63);
			t6 = (t6 << 1) | (t5 >> 63);
			t5 = (t5 << 1) | (t4 >> 63);
			t4 = (t4 << 1) | (t3 >> 63);
			t3 = (t3 << 1) | (t2 >> 63);
			t2 = (t2 << 1) | (t1 >> 63);
			t1 = t1 << 1;

			(ulong t0, carry) = BlsUtil.MultiplyAddCarry(0, this.Values[0], this.Values[0], 0);
			(t1, carry) = BlsUtil.AddWithCarry(t1, 0, carry);
			(t2, carry) = BlsUtil.MultiplyAddCarry(t2, this.Values[1], this.Values[1], carry);
			(t3, carry) = BlsUtil.AddWithCarry(t3, 0, carry);
			(t4, carry) = BlsUtil.MultiplyAddCarry(t4, this.Values[2], this.Values[2], carry);
			(t5, carry) = BlsUtil.AddWithCarry(t5, 0, carry);
			(t6, carry) = BlsUtil.MultiplyAddCarry(t6, this.Values[3], this.Values[3], carry);
			(t7, carry) = BlsUtil.AddWithCarry(t7, 0, carry);
			(t8, carry) = BlsUtil.MultiplyAddCarry(t8, this.Values[4], this.Values[4], carry);
			(t9, carry) = BlsUtil.AddWithCarry(t9, 0, carry);
			(t10, carry) = BlsUtil.MultiplyAddCarry(t10, this.Values[5], this.Values[5], carry);
			(t11, _) = BlsUtil.AddWithCarry(t11, 0, carry);

			return MontgomeryReduce(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
		}

		public Fp SubtractP()
		{
			(ulong r0, ulong borrow) = BlsUtil.SubtractWithBorrow(this.Values[0], MODULUS[0], 0);
			(ulong r1, borrow) = BlsUtil.SubtractWithBorrow(this.Values[1], MODULUS[1], borrow);
			(ulong r2, borrow) = BlsUtil.SubtractWithBorrow(this.Values[2], MODULUS[2], borrow);
			(ulong r3, borrow) = BlsUtil.SubtractWithBorrow(this.Values[3], MODULUS[3], borrow);
			(ulong r4, borrow) = BlsUtil.SubtractWithBorrow(this.Values[4], MODULUS[4], borrow);
			(ulong r5, borrow) = BlsUtil.SubtractWithBorrow(this.Values[5], MODULUS[5], borrow);

			// If underflow occurred on the final limb, borrow = 0xfff...fff, otherwise
			// borrow = 0x000...000. Thus, we use it as a mask!
			r0 = (this.Values[0] & borrow) | (r0 & ~borrow);
			r1 = (this.Values[1] & borrow) | (r1 & ~borrow);
			r2 = (this.Values[2] & borrow) | (r2 & ~borrow);
			r3 = (this.Values[3] & borrow) | (r3 & ~borrow);
			r4 = (this.Values[4] & borrow) | (r4 & ~borrow);
			r5 = (this.Values[5] & borrow) | (r5 & ~borrow);

			return new Fp(r0, r1, r2, r3, r4, r5);
		}

		public Fp Neg()
		{
			(ulong d0, ulong borrow) = Sbb(MODULUS[0], this.Values[0], 0);
			(ulong d1, borrow) = Sbb(MODULUS[1], this.Values[1], borrow);
			(ulong d2, borrow) = Sbb(MODULUS[2], this.Values[2], borrow);
			(ulong d3, borrow) = Sbb(MODULUS[3], this.Values[3], borrow);
			(ulong d4, borrow) = Sbb(MODULUS[4], this.Values[4], borrow);
			(ulong d5, _) = Sbb(MODULUS[5], this.Values[5], borrow);

			// Let's use a mask if `self` was zero, which would mean
			// the result of the subtraction is p.
			ulong mask = ((this.Values[0] | this.Values[1] | this.Values[2] | this.Values[3] | this.Values[4] | this.Values[5]) == 0)
				? ulong.MaxValue
				: 0;

			return new Fp(new ulong[] { d0 & mask, d1 & mask, d2 & mask, d3 & mask, d4 & mask, d5 & mask });
		}


		public static Fp FromBytes(byte[] bytes)
		{
			if (bytes.Length != 48)
				throw new ArgumentException("Byte array must be exactly 48 bytes long.");

			ulong[] value = new ulong[6];
			for (int i = 0; i < 6; i++)
			{
				value[5 - i] = BitConverter.ToUInt64(bytes, i * 8);
			}

			ulong borrow = 0;
			for (int i = 0; i < 6; i++)
			{
				(_, borrow) = BlsUtil.SubtractWithBorrow(value[i], MODULUS[i], borrow);
			}
			bool isValid = (borrow & 1) == 0;


			if (!isValid)
				throw new ArgumentException("The provided bytes represent a value that is not within the valid range of the field.");

			return new Fp(value[0], value[1], value[2], value[3], value[4], value[5]);
		}


		public static Fp MontgomeryReduce(
			ulong t0,
			ulong t1,
			ulong t2,
			ulong t3,
			ulong t4,
			ulong t5,
			ulong t6,
			ulong t7,
			ulong t8,
			ulong t9,
			ulong t10,
			ulong t11
		)
		{
			ulong k = t0 * INV;
			(ulong _, ulong carry) = BlsUtil.MultiplyAddCarry(t0, k, MODULUS[0], 0);
			(ulong r1, carry) = BlsUtil.MultiplyAddCarry(t1, k, MODULUS[1], carry);
			(ulong r2, carry) = BlsUtil.MultiplyAddCarry(t2, k, MODULUS[2], carry);
			(ulong r3, carry) = BlsUtil.MultiplyAddCarry(t3, k, MODULUS[3], carry);
			(ulong r4, carry) = BlsUtil.MultiplyAddCarry(t4, k, MODULUS[4], carry);
			(ulong r5, carry) = BlsUtil.MultiplyAddCarry(t5, k, MODULUS[5], carry);
			(ulong r6, ulong r7) = BlsUtil.AddWithCarry(t6, 0, carry);

			k = r1 * INV;
			(_, carry) = BlsUtil.MultiplyAddCarry(r1, k, MODULUS[0], 0);
			(r2, carry) = BlsUtil.MultiplyAddCarry(r2, k, MODULUS[1], carry);
			(r3, carry) = BlsUtil.MultiplyAddCarry(r3, k, MODULUS[2], carry);
			(r4, carry) = BlsUtil.MultiplyAddCarry(r4, k, MODULUS[3], carry);
			(r5, carry) = BlsUtil.MultiplyAddCarry(r5, k, MODULUS[4], carry);
			(r6, carry) = BlsUtil.MultiplyAddCarry(r6, k, MODULUS[5], carry);
			(r7, ulong r8) = BlsUtil.AddWithCarry(t7, r7, carry);

			k = r2 * INV;
			(_, carry) = BlsUtil.MultiplyAddCarry(r2, k, MODULUS[0], 0);
			(r3, carry) = BlsUtil.MultiplyAddCarry(r3, k, MODULUS[1], carry);
			(r4, carry) = BlsUtil.MultiplyAddCarry(r4, k, MODULUS[2], carry);
			(r5, carry) = BlsUtil.MultiplyAddCarry(r5, k, MODULUS[3], carry);
			(r6, carry) = BlsUtil.MultiplyAddCarry(r6, k, MODULUS[4], carry);
			(r7, carry) = BlsUtil.MultiplyAddCarry(r7, k, MODULUS[5], carry);
			(r8, ulong r9) = BlsUtil.AddWithCarry(t8, r8, carry);

			k = r3 * INV;
			(_, carry) = BlsUtil.MultiplyAddCarry(r3, k, MODULUS[0], 0);
			(r4, carry) = BlsUtil.MultiplyAddCarry(r4, k, MODULUS[1], carry);
			(r5, carry) = BlsUtil.MultiplyAddCarry(r5, k, MODULUS[2], carry);
			(r6, carry) = BlsUtil.MultiplyAddCarry(r6, k, MODULUS[3], carry);
			(r7, carry) = BlsUtil.MultiplyAddCarry(r7, k, MODULUS[4], carry);
			(r8, carry) = BlsUtil.MultiplyAddCarry(r8, k, MODULUS[5], carry);
			(r9, ulong r10) = BlsUtil.AddWithCarry(t9, r9, carry);

			k = r4 * INV;
			(_, carry) = BlsUtil.MultiplyAddCarry(r4, k, MODULUS[0], 0);
			(r5, carry) = BlsUtil.MultiplyAddCarry(r5, k, MODULUS[1], carry);
			(r6, carry) = BlsUtil.MultiplyAddCarry(r6, k, MODULUS[2], carry);
			(r7, carry) = BlsUtil.MultiplyAddCarry(r7, k, MODULUS[3], carry);
			(r8, carry) = BlsUtil.MultiplyAddCarry(r8, k, MODULUS[4], carry);
			(r9, carry) = BlsUtil.MultiplyAddCarry(r9, k, MODULUS[5], carry);
			(r10, ulong r11) = BlsUtil.AddWithCarry(t10, r10, carry);

			k = r5 * INV;
			(_, carry) = BlsUtil.MultiplyAddCarry(r5, k, MODULUS[0], 0);
			(r6, carry) = BlsUtil.MultiplyAddCarry(r6, k, MODULUS[1], carry);
			(r7, carry) = BlsUtil.MultiplyAddCarry(r7, k, MODULUS[2], carry);
			(r8, carry) = BlsUtil.MultiplyAddCarry(r8, k, MODULUS[3], carry);
			(r9, carry) = BlsUtil.MultiplyAddCarry(r9, k, MODULUS[4], carry);
			(r10, carry) = BlsUtil.MultiplyAddCarry(r10, k, MODULUS[5], carry);
			(r11, _) = BlsUtil.AddWithCarry(t11, r11, carry);

			return new Fp(r6, r7, r8, r9, r10, r11).SubtractP();
		}
	}
}
