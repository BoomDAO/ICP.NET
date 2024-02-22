using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	internal class G2Affine
	{
		public Fp2 X { get; }
		public Fp2 Y { get; }
		public bool IsInfinity { get; }

		static readonly Fp2 B;

		// const B: Fp2 = Fp2 {
		//     c0: Fp::from_raw_unchecked([
		//         0xaa27_0000_000c_fff3,
		//         0x53cc_0032_fc34_000a,
		//         0x478f_e97a_6b0a_807f,
		//         0xb1d3_7ebe_e6ba_24d7,
		//         0x8ec9_733b_bf78_ab2f,
		//         0x09d6_4551_3d83_de7e,
		//     ]),
		//     c1: Fp::from_raw_unchecked([
		//         0xaa27_0000_000c_fff3,
		//         0x53cc_0032_fc34_000a,
		//         0x478f_e97a_6b0a_807f,
		//         0xb1d3_7ebe_e6ba_24d7,
		//         0x8ec9_733b_bf78_ab2f,
		//         0x09d6_4551_3d83_de7e,
		//     ]),
		// };
		static G2Affine()
		{
			Fp c0 = new(
				0xaa27_0000_000c_fff3,
				0x53cc_0032_fc34_000a,
				0x478f_e97a_6b0a_807f,
				0xb1d3_7ebe_e6ba_24d7,
				0x8ec9_733b_bf78_ab2f,
				0x09d6_4551_3d83_de7e
			);
			Fp c1 = new(
				0xaa27_0000_000c_fff3,
				0x53cc_0032_fc34_000a,
				0x478f_e97a_6b0a_807f,
				0xb1d3_7ebe_e6ba_24d7,
				0x8ec9_733b_bf78_ab2f,
				0x09d6_4551_3d83_de7e
			);
			B = new Fp2(c0, c1);
		}

		public G2Affine(Fp2 x, Fp2 y, bool isInfinity)
		{
			this.X = x;
			this.Y = y;
			this.IsInfinity = isInfinity;
		}

		public static G2Affine Identity()
		{
			return new G2Affine(Fp2.Zero(), Fp2.One(), true);
		}

		public G2Projective ToProjective()
		{
			return new G2Projective(this.X, this.Y, this.IsInfinity ? Fp2.Zero() : Fp2.One());
		}

		public static G2Affine FromCompressed(byte[] bytes)
		{
			if (bytes.Length != 96)
				throw new ArgumentException("Byte array must be exactly 96 bytes long.");

			// Obtain the three flags from the start of the byte sequence
			bool compressionFlagSet = ((bytes[0] >> 7) & 1) == 1;
			bool infinityFlagSet = ((bytes[0] >> 6) & 1) == 1;
			bool sortFlagSet = ((bytes[0] >> 5) & 1) == 1;

			// Attempt to obtain the x-coordinate components
			byte[] xc1Bytes = new byte[48];
			Array.Copy(bytes, 0, xc1Bytes, 0, 48);
			xc1Bytes[0] &= 0b0001_1111; // Mask away the flag bits
			Fp xc1 = Fp.FromBytes(xc1Bytes);

			byte[] xc0Bytes = new byte[48];
			Array.Copy(bytes, 48, xc0Bytes, 0, 48); // No need to mask flags for xc0
			Fp xc0 = Fp.FromBytes(xc0Bytes);

			Fp2 x = new Fp2(xc0, xc1);

			if (infinityFlagSet && compressionFlagSet && !sortFlagSet && x.IsZero())
			{
				return G2Affine.Identity();
			}
			else
			{
				// Recover a y-coordinate given x by y = sqrt(x^3 + 4)
				Fp2 y = (x.Square() * x + B).SquareRoot();

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

				return new G2Affine(x, y, infinityFlagSet);
			}
		}

	}

}
