using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	internal class G1Affine
	{
		public Fp X { get; }
		public Fp Y { get; }
		public bool IsInfinity { get; }

		public static readonly Fp SSWU_XI;
		public static readonly Fp SSWU_ELLP_A;
		public static readonly Fp SSWU_ELLP_B;

		static G1Affine()
		{
			SSWU_XI = new Fp(
				0x886c_0000_0023_ffdc,
				0x0f70_008d_3090_001d,
				0x7767_2417_ed58_28c3,
				0x9dac_23e9_43dc_1740,
				0x5055_3f1b_9c13_1521,
				0x078c_712f_be0a_b6e8
			);
			SSWU_ELLP_A = new Fp(
				0x2f65_aa0e_9af5_aa51,
				0x8646_4c2d_1e84_16c3,
				0xb85c_e591_b7bd_31e2,
				0x27e1_1c91_b5f2_4e7c,
				0x2837_6eda_6bfc_1835,
				0x1554_55c3_e507_1d85
			);
			SSWU_ELLP_B = new Fp(
				0xfb99_6971_fe22_a1e0,
				0x9aa9_3eb3_5b74_2d6f,
				0x8c47_6013_de99_c5c4,
				0x873e_27c3_a221_e571,
				0xca72_b5e4_5a52_d888,
				0x0682_4061_418a_386b
			);
		}


		public G1Affine(Fp x, Fp y, bool isInfinity)
		{
			this.X = x;
			this.Y = y;
			this.IsInfinity = isInfinity;
		}

		public static G1Affine Identity()
		{
			return new G1Affine(Fp.Zero(), Fp.One(), true);
		}

		public G1Projective ToProjective()
		{
			return new G1Projective(this.X, this.Y, this.IsInfinity ? Fp.Zero() : Fp.One());
		}

		public byte[] ToCompressed()
		{
			byte[] bytes = (this.IsInfinity ? Fp.Zero() : this.X).ToBytes();
			bytes[0] |= 1 << 7;
			bytes[0] |= (byte)(this.IsInfinity ? 1 << 6 : 0);
			bytes[0] |= (byte)((!this.IsInfinity && this.Y.LexicographicallyLargest()) ? 1 << 5 : 0);
			return bytes;
		}


		internal static G1Affine FromCompressed(byte[] bytes)
		{
			if (bytes.Length != 48)
			{
				throw new ArgumentException("Byte length must be 48 for G1");
			}
			// Obtain the three flags from the start of the byte sequence
			bool compressionFlagSet = ((bytes[0] >> 7) & 1) == 1;
			bool infinityFlagSet = ((bytes[0] >> 6) & 1) == 1;
			bool sortFlagSet = ((bytes[0] >> 5) & 1) == 1;

			// Attempt to obtain the x-coordinate
			byte[] bytesWithoutFlagBits = new byte[48];
			Array.Copy(bytes, 0, bytesWithoutFlagBits, 0, 48);
			bytesWithoutFlagBits[0] &= 0b0001_1111;
			Fp x = Fp.FromBytes(bytesWithoutFlagBits);

			if (infinityFlagSet && compressionFlagSet && !sortFlagSet && x.IsZero())
			{
				return G1Affine.Identity();
			}
			else
			{
				// Recover a y-coordinate given x by y = sqrt(x^3 + 4)
				Fp y = (x.Square() * x + Fp.B).SquareRoot();

				// Switch to the correct y-coordinate if necessary.
				if (y.LexicographicallyLargest() ^ sortFlagSet)
				{
					y = y.Neg();
				}
				bool isValid = !infinityFlagSet && compressionFlagSet;
				if (!isValid)
				{
					throw new ArgumentException("Invalid compressed point");
				}
				return new G1Affine(x, y, infinityFlagSet);
			}
		}

		public bool IsIdentity()
		{
			return this.IsInfinity;
		}

		internal static G1Affine Generator()
		{
			Fp x = new Fp(
				0x5cb3_8790_fd53_0c16,
				0x7817_fc67_9976_fff5,
				0x154f_95c7_143b_a1c1,
				0xf0ae_6acd_f3d0_e747,
				0xedce_6ecc_21db_f440,
				0x1201_7741_9e0b_fb75
			);
			Fp y = new Fp(
				0xbaac_93d5_0ce7_2271,
				0x8c22_631a_7918_fd8e,
				0xdd59_5f13_5707_25ce,
				0x51ac_5829_5040_5194,
				0x0e1c_8c3f_ad00_59c0,
				0x0bbc_3efc_5008_a26a
			);
			return new G1Affine(x, y, false);
		}

		internal G1Affine Neg()
		{
			return new G1Affine(this.X, this.IsInfinity ? Fp.One() : this.Y.Neg(), this.IsInfinity);
		}
	}

}
