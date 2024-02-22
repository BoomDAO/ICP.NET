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

		private static readonly Fp B;

		static G1Affine()
		{
			B = new Fp(
				0xaa27_0000_000c_fff3,
				0x53cc_0032_fc34_000a,
				0x478f_e97a_6b0a_807f,
				0xb1d3_7ebe_e6ba_24d7,
				0x8ec9_733b_bf78_ab2f,
				0x09d6_4551_3d83_de7e
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
				Fp y = (x.Square() * x + B).SquareRoot();

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
