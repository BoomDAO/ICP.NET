using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Linq;

namespace EdjCase.ICP.BLS.Models
{
	internal struct Fp
	{
		public ulong V0;
		public ulong V1;
		public ulong V2;
		public ulong V3;
		public ulong V4;
		public ulong V5;
		public ulong[] ToArray() => new ulong[] {this.V0, this.V1, this.V2, this.V3, this.V4, this.V5};

		// Constants
		public static readonly ulong[] MODULUS = new ulong[] {
			0xb9fe_ffff_ffff_aaab,
			0x1eab_fffe_b153_ffff,
			0x6730_d2a0_f6b0_f624,
			0x6477_4b84_f385_12bf,
			0x4b1b_a7b6_434b_acd7,
			0x1a01_11ea_397f_e69a
		};
		public static readonly ulong INV = 0x89f3_fffc_fffc_fffd;

		// Static readonly instances of Fp for R, R2, and R3
		public static readonly Fp R;
		public static readonly Fp R2;
		public static readonly Fp R3;
		public static readonly Fp B;

		static Fp()
		{
			R = new Fp(0x7609_0000_0002_fffd, 0xebf4_000b_c40c_0002, 0x5f48_9857_53c7_58ba, 0x77ce_5853_7052_5745, 0x5c07_1a97_a256_ec6d, 0x15f6_5ec3_fa80_e493);
			R2 = new Fp(0xf4df_1f34_1c34_1746, 0x0a76_e6a6_09d1_04f1, 0x8de5_476c_4c95_b6d5, 0x67eb_88a9_939d_83c0, 0x9a79_3e85_b519_952d, 0x1198_8fe5_92ca_e3aa);
			R3 = new Fp(0xed48_ac6b_d94c_a1e0, 0x315f_831e_03a7_adf8, 0x9a53_352a_615e_29dd, 0x34c0_4e5e_921e_1761, 0x2512_d435_6572_4728, 0x0aa6_3460_9175_5d4d);

			B = new Fp(
				0xaa27_0000_000c_fff3,
				0x53cc_0032_fc34_000a,
				0x478f_e97a_6b0a_807f,
				0xb1d3_7ebe_e6ba_24d7,
				0x8ec9_733b_bf78_ab2f,
				0x09d6_4551_3d83_de7e
			);
		}

		public Fp(ulong v0, ulong v1, ulong v2, ulong v3, ulong v4, ulong v5)
		{
			this.V0 = v0;
			this.V1 = v1;
			this.V2 = v2;
			this.V3 = v3;
			this.V4 = v4;
			this.V5 = v5;
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

		public override bool Equals(object obj)
		{
			if (obj is Fp fp)
			{
				return this.Equals(fp);
			}
			return false;
		}

		public bool Equals(Fp other)
		{
			return this.V0 == other.V0
				&& this.V1 == other.V1
				&& this.V2 == other.V2
				&& this.V3 == other.V3
				&& this.V4 == other.V4
				&& this.V5 == other.V5;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.V0, this.V1, this.V2, this.V3, this.V4, this.V5);
		}

		public bool IsZero()
		{
			return this.Equals(Fp.Zero());
		}

		public Fp Clone()
		{
			return new Fp(
				this.V0,
				this.V1,
				this.V2,
				this.V3,
				this.V4,
				this.V5
			);
		}

		public byte[] ToBytes()
		{
			// Turn into canonical form by computing
			// (a.R) / R = a
			Fp tmp = Fp.MontgomeryReduce(
				this.V0,
				this.V1,
				this.V2,
				this.V3,
				this.V4,
				this.V5,
				0,
				0,
				0,
				0,
				0,
				0
			);
			byte[] bytes = new byte[48];

			// Write bytes directly into the array, using a helper method to respect endianness.
			WriteBytes(bytes, 0, tmp.V5);
			WriteBytes(bytes, 8, tmp.V4);
			WriteBytes(bytes, 16, tmp.V3);
			WriteBytes(bytes, 24, tmp.V2);
			WriteBytes(bytes, 32, tmp.V1);
			WriteBytes(bytes, 40, tmp.V0);

			return bytes;
		}

		private static void WriteBytes(byte[] target, int start, ulong value)
		{
			BinaryPrimitives.WriteUInt64BigEndian(target.AsSpan(start, 8), value);
		}


		public Fp Add(Fp rhs)
		{
			ulong carry = 0;

			(ulong v0, carry) = BlsUtil.AddWithCarry(this.V0, rhs.V0, carry);
			(ulong v1, carry) = BlsUtil.AddWithCarry(this.V1, rhs.V1, carry);
			(ulong v2, carry) = BlsUtil.AddWithCarry(this.V2, rhs.V2, carry);
			(ulong v3, carry) = BlsUtil.AddWithCarry(this.V3, rhs.V3, carry);
			(ulong v4, carry) = BlsUtil.AddWithCarry(this.V4, rhs.V4, carry);
			(ulong v5, carry) = BlsUtil.AddWithCarry(this.V5, rhs.V5, carry);

			// Attempt to subtract the modulus, to ensure the value
			// is smaller than the modulus.
			return new Fp(
				v0,
				v1,
				v2,
				v3,
				v4,
				v5
			)
			.SubtractP();
		}

		public static Fp operator +(Fp a, Fp b)
		{
			return a.Add(b);
		}

		public Fp Subtract(Fp rhs)
		{
			return rhs.Neg().Add(this);
		}

		public static Fp operator -(Fp a, Fp b)
		{
			return a.Subtract(b);
		}

		public Fp Multiply(Fp rhs)
		{
			(ulong t0, ulong carry) = BlsUtil.MultiplyAddCarry(0, this.V0, rhs.V0, 0);
			(ulong t1, carry) = BlsUtil.MultiplyAddCarry(0, this.V0, rhs.V1, carry);
			(ulong t2, carry) = BlsUtil.MultiplyAddCarry(0, this.V0, rhs.V2, carry);
			(ulong t3, carry) = BlsUtil.MultiplyAddCarry(0, this.V0, rhs.V3, carry);
			(ulong t4, carry) = BlsUtil.MultiplyAddCarry(0, this.V0, rhs.V4, carry);
			(ulong t5, ulong t6) = BlsUtil.MultiplyAddCarry(0, this.V0, rhs.V5, carry);

			(t1, carry) = BlsUtil.MultiplyAddCarry(t1, this.V1, rhs.V0, 0);
			(t2, carry) = BlsUtil.MultiplyAddCarry(t2, this.V1, rhs.V1, carry);
			(t3, carry) = BlsUtil.MultiplyAddCarry(t3, this.V1, rhs.V2, carry);
			(t4, carry) = BlsUtil.MultiplyAddCarry(t4, this.V1, rhs.V3, carry);
			(t5, carry) = BlsUtil.MultiplyAddCarry(t5, this.V1, rhs.V4, carry);
			(t6, ulong t7) = BlsUtil.MultiplyAddCarry(t6, this.V1, rhs.V5, carry);

			(t2, carry) = BlsUtil.MultiplyAddCarry(t2, this.V2, rhs.V0, 0);
			(t3, carry) = BlsUtil.MultiplyAddCarry(t3, this.V2, rhs.V1, carry);
			(t4, carry) = BlsUtil.MultiplyAddCarry(t4, this.V2, rhs.V2, carry);
			(t5, carry) = BlsUtil.MultiplyAddCarry(t5, this.V2, rhs.V3, carry);
			(t6, carry) = BlsUtil.MultiplyAddCarry(t6, this.V2, rhs.V4, carry);
			(t7, ulong t8) = BlsUtil.MultiplyAddCarry(t7, this.V2, rhs.V5, carry);

			(t3, carry) = BlsUtil.MultiplyAddCarry(t3, this.V3, rhs.V0, 0);
			(t4, carry) = BlsUtil.MultiplyAddCarry(t4, this.V3, rhs.V1, carry);
			(t5, carry) = BlsUtil.MultiplyAddCarry(t5, this.V3, rhs.V2, carry);
			(t6, carry) = BlsUtil.MultiplyAddCarry(t6, this.V3, rhs.V3, carry);
			(t7, carry) = BlsUtil.MultiplyAddCarry(t7, this.V3, rhs.V4, carry);
			(t8, ulong t9) = BlsUtil.MultiplyAddCarry(t8, this.V3, rhs.V5, carry);

			(t4, carry) = BlsUtil.MultiplyAddCarry(t4, this.V4, rhs.V0, 0);
			(t5, carry) = BlsUtil.MultiplyAddCarry(t5, this.V4, rhs.V1, carry);
			(t6, carry) = BlsUtil.MultiplyAddCarry(t6, this.V4, rhs.V2, carry);
			(t7, carry) = BlsUtil.MultiplyAddCarry(t7, this.V4, rhs.V3, carry);
			(t8, carry) = BlsUtil.MultiplyAddCarry(t8, this.V4, rhs.V4, carry);
			(t9, ulong t10) = BlsUtil.MultiplyAddCarry(t9, this.V4, rhs.V5, carry);

			(t5, carry) = BlsUtil.MultiplyAddCarry(t5, this.V5, rhs.V0, 0);
			(t6, carry) = BlsUtil.MultiplyAddCarry(t6, this.V5, rhs.V1, carry);
			(t7, carry) = BlsUtil.MultiplyAddCarry(t7, this.V5, rhs.V2, carry);
			(t8, carry) = BlsUtil.MultiplyAddCarry(t8, this.V5, rhs.V3, carry);
			(t9, carry) = BlsUtil.MultiplyAddCarry(t9, this.V5, rhs.V4, carry);
			(t10, ulong t11) = BlsUtil.MultiplyAddCarry(t10, this.V5, rhs.V5, carry);

			return MontgomeryReduce(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
		}

		public static Fp operator *(Fp a, Fp b)
		{
			return a.Multiply(b);
		}


		public Fp Square()
		{
			// Initial declarations
			ulong t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11;
			ulong carry;

			// Operations
			(t1, carry) = BlsUtil.MultiplyAddCarry(0, this.V0, this.V1, 0);
			(t2, carry) = BlsUtil.MultiplyAddCarry(0, this.V0, this.V2, carry);
			(t3, carry) = BlsUtil.MultiplyAddCarry(0, this.V0, this.V3, carry);
			(t4, carry) = BlsUtil.MultiplyAddCarry(0, this.V0, this.V4, carry);
			(t5, t6) = BlsUtil.MultiplyAddCarry(0, this.V0, this.V5, carry);

			(t3, carry) = BlsUtil.MultiplyAddCarry(t3, this.V1, this.V2, 0);
			(t4, carry) = BlsUtil.MultiplyAddCarry(t4, this.V1, this.V3, carry);
			(t5, carry) = BlsUtil.MultiplyAddCarry(t5, this.V1, this.V4, carry);
			(t6, t7) = BlsUtil.MultiplyAddCarry(t6, this.V1, this.V5, carry);

			(t5, carry) = BlsUtil.MultiplyAddCarry(t5, this.V2, this.V3, 0);
			(t6, carry) = BlsUtil.MultiplyAddCarry(t6, this.V2, this.V4, carry);
			(t7, t8) = BlsUtil.MultiplyAddCarry(t7, this.V2, this.V5, carry);

			(t7, carry) = BlsUtil.MultiplyAddCarry(t7, this.V3, this.V4, 0);
			(t8, t9) = BlsUtil.MultiplyAddCarry(t8, this.V3, this.V5, carry);

			(t9, t10) = BlsUtil.MultiplyAddCarry(t9, this.V4, this.V5, 0);

			// Bit shifting operations
			t11 = t10 >> 63;
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

			// Final operations with carry management
			ulong t0;
			(t0, carry) = BlsUtil.MultiplyAddCarry(0, this.V0, this.V0, 0);
			(t1, carry) = BlsUtil.AddWithCarry(t1, 0, carry);
			(t2, carry) = BlsUtil.MultiplyAddCarry(t2, this.V1, this.V1, carry);
			(t3, carry) = BlsUtil.AddWithCarry(t3, 0, carry);
			(t4, carry) = BlsUtil.MultiplyAddCarry(t4, this.V2, this.V2, carry);
			(t5, carry) = BlsUtil.AddWithCarry(t5, 0, carry);
			(t6, carry) = BlsUtil.MultiplyAddCarry(t6, this.V3, this.V3, carry);
			(t7, carry) = BlsUtil.AddWithCarry(t7, 0, carry);
			(t8, carry) = BlsUtil.MultiplyAddCarry(t8, this.V4, this.V4, carry);
			(t9, carry) = BlsUtil.AddWithCarry(t9, 0, carry);
			(t10, carry) = BlsUtil.MultiplyAddCarry(t10, this.V5, this.V5, carry);
			(t11, _) = BlsUtil.AddWithCarry(t11, 0, carry);

			// Return the final result
			return MontgomeryReduce(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
		}


		public Fp SubtractP()
		{
			(ulong r0, ulong borrow) = BlsUtil.SubtractWithBorrow(this.V0, MODULUS[0], 0);
			(ulong r1, borrow) = BlsUtil.SubtractWithBorrow(this.V1, MODULUS[1], borrow);
			(ulong r2, borrow) = BlsUtil.SubtractWithBorrow(this.V2, MODULUS[2], borrow);
			(ulong r3, borrow) = BlsUtil.SubtractWithBorrow(this.V3, MODULUS[3], borrow);
			(ulong r4, borrow) = BlsUtil.SubtractWithBorrow(this.V4, MODULUS[4], borrow);
			(ulong r5, borrow) = BlsUtil.SubtractWithBorrow(this.V5, MODULUS[5], borrow);

			// If underflow occurred on the final limb, borrow = 0xfff...fff, otherwise
			// borrow = 0x000...000. Thus, we use it as a mask!
			ulong mask = borrow == 0 ? ulong.MaxValue : 0;

			r0 = (this.V0 & ~mask) | (r0 & mask);
			r1 = (this.V1 & ~mask) | (r1 & mask);
			r2 = (this.V2 & ~mask) | (r2 & mask);
			r3 = (this.V3 & ~mask) | (r3 & mask);
			r4 = (this.V4 & ~mask) | (r4 & mask);
			r5 = (this.V5 & ~mask) | (r5 & mask);

			return new Fp(r0, r1, r2, r3, r4, r5);
		}

		public Fp Neg()
		{
			(ulong d0, ulong borrow) = BlsUtil.SubtractWithBorrow(MODULUS[0], this.V0, 0);
			(ulong d1, borrow) = BlsUtil.SubtractWithBorrow(MODULUS[1], this.V1, borrow);
			(ulong d2, borrow) = BlsUtil.SubtractWithBorrow(MODULUS[2], this.V2, borrow);
			(ulong d3, borrow) = BlsUtil.SubtractWithBorrow(MODULUS[3], this.V3, borrow);
			(ulong d4, borrow) = BlsUtil.SubtractWithBorrow(MODULUS[4], this.V4, borrow);
			(ulong d5, _) = BlsUtil.SubtractWithBorrow(MODULUS[5], this.V5, borrow);

			// Let's use a mask if `self` was zero, which would mean
			// the result of the subtraction is p.
			ulong mask = ((this.V0 | this.V1 | this.V2 | this.V3 | this.V4 | this.V5) == 0)
				? 0
				: ulong.MaxValue;

			return new Fp(
				d0 & mask,
				d1 & mask,
				d2 & mask,
				d3 & mask,
				d4 & mask,
				d5 & mask
			);
		}


		public static Fp FromBytes(byte[] bytes)
		{
			if (bytes.Length != 48)
				throw new ArgumentException("Byte array must be exactly 48 bytes long.");

			ulong[] value = new ulong[6];
			for (int i = 0; i < 6; i++)
			{
				// Extract 8 bytes for each ulong, considering the position in the array
				byte[] ulongBytes = new byte[8];
				Array.Copy(bytes, i * 8, ulongBytes, 0, 8);

				// Check if the system is little-endian; reverse the array to ensure big-endian order
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(ulongBytes);
				}

				// Convert bytes to ulong considering they are now in big-endian format
				value[5 - i] = BitConverter.ToUInt64(ulongBytes, 0);
			}

			ulong borrow = 0;
			for (int i = 0; i < 6; i++)
			{
				(_, borrow) = BlsUtil.SubtractWithBorrow(value[i], MODULUS[i], borrow);
			}
			bool isValid = (borrow & 1) == 1;

			if (!isValid)
				throw new ArgumentException("The provided bytes represent a value that is not within the valid range of the field.");

			Fp fp = new Fp(value[0], value[1], value[2], value[3], value[4], value[5]);
			fp *= R2;
			return fp;
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

		public Fp SquareRoot()
		{
			Fp by = new(
				0xee7f_bfff_ffff_eaab,
				0x07aa_ffff_ac54_ffff,
				0xd9cc_34a8_3dac_3d89,
				0xd91d_d2e1_3ce1_44af,
				0x92c6_e9ed_90d2_eb35,
				0x0680_447a_8e5f_f9a6
			);
			return this.Pow(by);
		}

		public Fp Pow(Fp by)
		{
			Fp res = Fp.One();
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

		public Fp? Invert()
		{
			Fp t = this.Pow(new Fp(
				0xb9fe_ffff_ffff_aaa9,
				0x1eab_fffe_b153_ffff,
				0x6730_d2a0_f6b0_f624,
				0x6477_4b84_f385_12bf,
				0x4b1b_a7b6_434b_acd7,
				0x1a01_11ea_397f_e69a
			));
			if (this.IsZero())
			{
				return null;
			}
			return t;
		}

		public bool LexicographicallyLargest()
		{
			Fp tmp = Fp.MontgomeryReduce(
				this.V0,
				this.V1,
				this.V2,
				this.V3,
				this.V4,
				this.V5,
				0,
				0,
				0,
				0,
				0,
				0
			);
			(_, ulong borrow) = BlsUtil.SubtractWithBorrow(tmp.V0, 0xdcff_7fff_ffff_d556, 0);
			(_, borrow) = BlsUtil.SubtractWithBorrow(tmp.V1, 0x0f55_ffff_58a9_ffff, borrow);
			(_, borrow) = BlsUtil.SubtractWithBorrow(tmp.V2, 0xb398_6950_7b58_7b12, borrow);
			(_, borrow) = BlsUtil.SubtractWithBorrow(tmp.V3, 0xb23b_a5c2_79c2_895f, borrow);
			(_, borrow) = BlsUtil.SubtractWithBorrow(tmp.V4, 0x258d_d3db_21a5_d66b, borrow);
			(_, borrow) = BlsUtil.SubtractWithBorrow(tmp.V5, 0x0d00_88f5_1cbf_f34d, borrow);
			return (borrow & 1) != 1;
		}

		public static Fp SumOfProducts(Fp[] a, Fp[] b)
		{
			if (a.Length != b.Length)
			{
				throw new ArgumentException("Arrays must be of the same length.");
			};
			// Iterate Range from 0 -> 6

			(ulong u0, ulong u1, ulong u2, ulong u3, ulong u4, ulong u5) = Enumerable.Range(0, 6)
			.Aggregate((0ul, 0ul, 0ul, 0ul, 0ul, 0ul), (acc, j) =>
			{
				(ulong u0, ulong u1, ulong u2, ulong u3, ulong u4, ulong u5) = acc;
				(ulong t0, ulong t1, ulong t2, ulong t3, ulong t4, ulong t5, ulong t6) = a
					.Zip(b, (a, b) => (a, b))
					.Aggregate(
						(u0, u1, u2, u3, u4, u5, 0ul),
						(acc2, pair) =>
						{
							ulong vj = pair.a.GetValueByIndex(j);
							(ulong t0, ulong t1, ulong t2, ulong t3, ulong t4, ulong t5, ulong t6) = acc2;
							(t0, ulong carry) = BlsUtil.MultiplyAddCarry(t0, vj, pair.b.V0, 0);
							(t1, carry) = BlsUtil.MultiplyAddCarry(t1, vj, pair.b.V1, carry);
							(t2, carry) = BlsUtil.MultiplyAddCarry(t2, vj, pair.b.V2, carry);
							(t3, carry) = BlsUtil.MultiplyAddCarry(t3, vj, pair.b.V3, carry);
							(t4, carry) = BlsUtil.MultiplyAddCarry(t4, vj, pair.b.V4, carry);
							(t5, carry) = BlsUtil.MultiplyAddCarry(t5, vj, pair.b.V5, carry);
							(t6, _) = BlsUtil.AddWithCarry(t6, 0, carry);
							return (t0, t1, t2, t3, t4, t5, t6);
						});

				ulong k;
				unchecked
				{
					// Wrapping multiply
					k = t0 * INV;
				}
				(ulong _, ulong carry) = BlsUtil.MultiplyAddCarry(t0, k, MODULUS[0], 0);
				(ulong r1, carry) = BlsUtil.MultiplyAddCarry(t1, k, MODULUS[1], carry);
				(ulong r2, carry) = BlsUtil.MultiplyAddCarry(t2, k, MODULUS[2], carry);
				(ulong r3, carry) = BlsUtil.MultiplyAddCarry(t3, k, MODULUS[3], carry);
				(ulong r4, carry) = BlsUtil.MultiplyAddCarry(t4, k, MODULUS[4], carry);
				(ulong r5, carry) = BlsUtil.MultiplyAddCarry(t5, k, MODULUS[5], carry);
				(ulong r6, _) = BlsUtil.AddWithCarry(t6, 0, carry);
				return (r1, r2, r3, r4, r5, r6);
			});
			return new Fp(
				u0,
				u1,
				u2,
				u3,
				u4,
				u5
			)
			.SubtractP();

		}

		public ulong GetValueByIndex(int i)
		{
			switch (i)
			{
				case 0:
					return this.V0;
				case 1:
					return this.V1;
				case 2:
					return this.V2;
				case 3:
					return this.V3;
				case 4:
					return this.V4;
				case 5:
					return this.V5;
				default:
					throw new NotImplementedException();
			};
		}

		public override string ToString()
		{
			// Hex of bytes with 0x prefix
			byte[] bytes = this.ToBytes();
			Span<char> stringValue = stackalloc char[bytes.Length * 2 + 2];
			stringValue[0] = '0';
			stringValue[1] = 'x';
			int i = 1;
			foreach (byte b in bytes)
			{
				int charIndex = i++ * 2;
				int quotient = Math.DivRem(b, 16, out int remainder);
				stringValue[charIndex] = GetChar(quotient);
				stringValue[charIndex + 1] = GetChar(remainder);
			}

			return new string(stringValue);
		}

		private static char GetChar(int value)
		{
			if (value < 10)
			{
				return (char)(value + 48); // 0->9
			}
			return (char)(value + 65 - 10); // A->F ASCII
		}

		internal bool Sgn0()
		{
			Fp tmp = Fp.MontgomeryReduce(
				this.V0,
				this.V1,
				this.V2,
				this.V3,
				this.V4,
				this.V5,
				0,
				0,
				0,
				0,
				0,
				0
			);
			return (tmp.V0 & 1) != 0;
		}
	}
}
