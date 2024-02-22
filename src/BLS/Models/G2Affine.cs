using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	internal class G2Affine
	{
		public Fp2 X { get; }
		public Fp2 Y { get; }
		public bool IsInfinity { get; }

		static readonly Fp2 B;

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

		public G2Prepared ToPrepared()
		{
			G2Projective cur = this.ToProjective();
			G2Affine q = this.IsIdentity() ? G2Affine.Generator() : this;
			List<(Fp2, Fp2, Fp2)> coeffs = new();


			BlsUtil.MillerLoop(
				0, // Dummy value instead of void
				(f) =>
				{
					cur = G2Projective.DoublingStep(cur, out var values);
					coeffs.Add(values);
					return f; // Dummy
				},
				(f) =>
				{
					cur = G2Projective.AdditionStep(cur, q, out var values);
					coeffs.Add(values);
					return f; // Dummy
				},
				(f) => f, // Dummy
				(f) => f // Dummy
			);

			return new G2Prepared(q.IsInfinity, coeffs);
		}

		private static G2Affine Generator()
		{
			Fp2 x = new Fp2(
				new Fp(
					0xf5f2_8fa2_0294_0a10,
					0xb3f5_fb26_87b4_961a,
					0xa1a8_93b5_3e2a_e580,
					0x9894_999d_1a3c_aee9,
					0x6f67_b763_1863_366b,
					0x0581_9192_4350_bcd7
				),
				new Fp(
					0xa5a9_c075_9e23_f606,
					0xaaa0_c59d_bccd_60c3,
					0x3bb1_7e18_e286_7806,
					0x1b1a_b6cc_8541_b367,
					0xc2b6_ed0e_f215_8547,
					0x1192_2a09_7360_edf3
				)
			);
			Fp2 y = new Fp2(
				new Fp(
					0x4c73_0af8_6049_4c4a,
					0x597c_fa1f_5e36_9c5a,
					0xe7e6_856c_aa0a_635a,
					0xbbef_b5e9_6e0d_495f,
					0x07d3_a975_f0ef_25a2,
					0x0083_fd8e_7e80_dae5
				),
				new Fp(
					0xadc0_fc92_df64_b05d,
					0x18aa_270a_2b14_61dc,
					0x86ad_ac6a_3be4_eba0,
					0x7949_5c4e_c93d_a33a,
					0xe717_5850_a43c_caed,
					0x0b2b_c2a1_63de_1bf2
				)
			);
			return new G2Affine(x, y, false);
		}

		private bool IsIdentity()
		{
			return this.IsInfinity;
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

		internal byte[] ToCompressed()
		{
			Fp2 x = this.IsInfinity ? Fp2.Zero() : this.X;
			byte[] bytes = new byte[96];

			Array.Copy(x.C1.ToBytes(), 0, bytes, 0, 48);
			Array.Copy(x.C0.ToBytes(), 0, bytes, 48, 48);

			bytes[0] |= (byte)(1 << 7);

			bytes[0] |= (byte)(this.IsInfinity ? 1 << 6 : 0);

			bytes[0] |= (byte)(this.IsInfinity ? 0 : (this.Y.LexicographicallyLargest() ? 1 << 5 : 0));

			return bytes;
		}
	}

}
