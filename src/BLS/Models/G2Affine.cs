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

		public static readonly Fp2 B;

		public static readonly Fp2 SSWU_XI;
		public static readonly Fp2 SSWU_ELLP_A;
		public static readonly Fp2 SSWU_ELLP_B;
		public static readonly Fp2 SSWU_RV1;
		public static readonly Fp2[] SSWU_ETAS;
		public static readonly Fp2[] ISO3_XNUM;
		public static readonly Fp2[] ISO3_XDEN;
		public static readonly Fp2[] ISO3_YNUM;
		public static readonly Fp2[] ISO3_YDEN;

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
			SSWU_XI = new(
				new Fp(
					0x87eb_ffff_fff9_555c,
					0x656f_ffe5_da8f_fffa,
					0x0fd0_7493_45d3_3ad2,
					0xd951_e663_0665_76f4,
					0xde29_1a3d_41e9_80d3,
					0x0815_664c_7dfe_040d

				),
				new Fp(
					0x43f5_ffff_fffc_aaae,
					0x32b7_fff2_ed47_fffd,
					0x07e8_3a49_a2e9_9d69,
					0xeca8_f331_8332_bb7a,
					0xef14_8d1e_a0f4_c069,
					0x040a_b326_3eff_0206

				)
			);
			SSWU_ELLP_A = new Fp2(
				Fp.Zero(),
				new Fp(
					0xe53a_0000_0313_5242,
					0x0108_0c0f_def8_0285,
					0xe788_9edb_e340_f6bd,
					0x0b51_3751_2631_0601,
					0x02d6_9857_17c7_44ab,
					0x1220_b4e9_79ea_5467
				)
			);
			SSWU_ELLP_B = new Fp2(
				new Fp(
					0x22ea_0000_0cf8_9db2,
					0x6ec8_32df_7138_0aa4,
					0x6e1b_9440_3db5_a66e,
					0x75bf_3c53_a794_73ba,
					0x3dd3_a569_412c_0a34,
					0x125c_db5e_74dc_4fd1
				),
				new Fp(
					0x22ea_0000_0cf8_9db2,
					0x6ec8_32df_7138_0aa4,
					0x6e1b_9440_3db5_a66e,
					0x75bf_3c53_a794_73ba,
					0x3dd3_a569_412c_0a34,
					0x125c_db5e_74dc_4fd1
				)
			);
			ISO3_XNUM = new[]
			{
				new Fp2(
					new Fp(
						0x47f6_71c7_1ce0_5e62,
						0x06dd_5707_1206_393e,
						0x7c80_cd2a_f3fd_71a2,
						0x0481_03ea_9e6c_d062,
						0xc545_16ac_c8d0_37f6,
						0x1380_8f55_0920_ea41
					),
					new Fp(
						0x47f6_71c7_1ce0_5e62,
						0x06dd_5707_1206_393e,
						0x7c80_cd2a_f3fd_71a2,
						0x0481_03ea_9e6c_d062,
						0xc545_16ac_c8d0_37f6,
						0x1380_8f55_0920_ea41
					)
				),
				new Fp2(
					Fp.Zero(),
					new Fp(
						0x5fe5_5555_554c_71d0,
						0x873f_ffdd_236a_aaa3,
						0x6a6b_4619_b26e_f918,
						0x21c2_8884_0887_4945,
						0x2836_cda7_028c_abc5,
						0x0ac7_3310_a7fd_5abd
					)
				),
				new Fp2(
					new Fp(
						0x0a0c_5555_5559_71c3,
						0xdb0c_0010_1f9e_aaae,
						0xb1fb_2f94_1d79_7997,
						0xd396_0742_ef41_6e1c,
						0xb700_40e2_c205_56f4,
						0x149d_7861_e581_393b
					),
					new Fp(
						0xaff2_aaaa_aaa6_38e8,
						0x439f_ffee_91b5_5551,
						0xb535_a30c_d937_7c8c,
						0x90e1_4442_0443_a4a2,
						0x941b_66d3_8146_55e2,
						0x0563_9988_53fe_ad5e
					)
				),
				new Fp2(
					new Fp(
						0x40aa_c71c_71c7_25ed,
						0x1909_5555_7a84_e38e,
						0xd817_050a_8f41_abc3,
						0xd864_85d4_c87f_6fb1,
						0x696e_b479_f885_d059,
						0x198e_1a74_3280_02d2
					),
					Fp.Zero()
				)
			};
			ISO3_XDEN = new[]
			{
				new Fp2(
					Fp.Zero(),
					new Fp(
						0x1f3a_ffff_ff13_ab97,
						0xf25b_fc61_1da3_ff3e,
						0xca37_57cb_3819_b208,
						0x3e64_2736_6f8c_ec18,
						0x0397_7bc8_6095_b089,
						0x04f6_9db1_3f39_a952
					)
				),
				new Fp2(
					new Fp(
						0x4476_0000_0027_552e,
						0xdcb8_009a_4348_0020,
						0x6f7e_e9ce_4a6e_8b59,
						0xb103_30b7_c0a9_5bc6,
						0x6140_b1fc_fb1e_54b7,
						0x0381_be09_7f0b_b4e1
					),
					new Fp(
						0x7588_ffff_ffd8_557d,
						0x41f3_ff64_6e0b_ffdf,
						0xf7b1_e8d2_ac42_6aca,
						0xb374_1acd_32db_b6f8,
						0xe9da_f5b9_482d_581f,
						0x167f_53e0_ba74_31b8
					)
				),
				Fp2.Zero()
			};
			ISO3_YNUM = new[]
			{
				new Fp2(
					new Fp(
						0x96d8_f684_bdfc_77be,
						0xb530_e4f4_3b66_d0e2,
						0x184a_88ff_3796_52fd,
						0x57cb_23ec_fae8_04e1,
						0x0fd2_e39e_ada3_eba9,
						0x08c8_055e_31c5_d5c3
					),
					new Fp(
						0x96d8_f684_bdfc_77be,
						0xb530_e4f4_3b66_d0e2,
						0x184a_88ff_3796_52fd,
						0x57cb_23ec_fae8_04e1,
						0x0fd2_e39e_ada3_eba9,
						0x08c8_055e_31c5_d5c3
					)
				),
				new Fp2(
					Fp.Zero(),
					new Fp(
						0xbf0a_71c7_1c91_b406,
						0x4d6d_55d2_8b76_38fd,
						0x9d82_f98e_5f20_5aee,
						0xa27a_a27b_1d1a_18d5,
						0x02c3_b2b2_d293_8e86,
						0x0c7d_1342_0b09_807f
					)
				),
				new Fp2(
					new Fp(
						0xd7f9_5555_5553_1c74,
						0x21cf_fff7_48da_aaa8,
						0x5a9a_d186_6c9b_be46,
						0x4870_a221_0221_d251,
						0x4a0d_b369_c0a3_2af1,
						0x02b1_ccc4_29ff_56af
					),
					new Fp(
						0xe205_aaaa_aaac_8e37,
						0xfcdc_0007_6879_5556,
						0x0c96_011a_8a15_37dd,
						0x1c06_a963_f163_406e,
						0x010d_f44c_82a8_81e6,
						0x174f_4526_0f80_8feb
					)
				),
				new Fp2(
					new Fp(
						0xa470_bda1_2f67_f35c,
						0xc0fe_38e2_3327_b425,
						0xc9d3_d0f2_c6f0_678d,
						0x1c55_c993_5b5a_982e,
						0x27f6_c0e2_f074_6764,
						0x117c_5e6e_28aa_9054
					),
					Fp.Zero()
				)
			};
			ISO3_YDEN = new[]
			{
				new Fp2(
					new Fp(
						0x0162_ffff_fa76_5adf,
						0x8f7b_ea48_0083_fb75,
						0x561b_3c22_59e9_3611,
						0x11e1_9fc1_a9c8_75d5,
						0xca71_3efc_0036_7660,
						0x03c6_a03d_41da_1151
					),
					new Fp(
						0x0162_ffff_fa76_5adf,
						0x8f7b_ea48_0083_fb75,
						0x561b_3c22_59e9_3611,
						0x11e1_9fc1_a9c8_75d5,
						0xca71_3efc_0036_7660,
						0x03c6_a03d_41da_1151
					)
				),
				new Fp2(
					Fp.Zero(),
					new Fp(
						0x5db0_ffff_fd3b_02c5,
						0xd713_f523_58eb_fdba,
						0x5ea6_0761_a84d_161a,
						0xbb2c_75a3_4ea6_c44a,
						0x0ac6_7359_21c1_119b,
						0x0ee3_d913_bdac_fbf6
					)
				),
				new Fp2(
					new Fp(
						0x66b1_0000_003a_ffc5,
						0xcb14_00e7_64ec_0030,
						0xa73e_5eb5_6fa5_d106,
						0x8984_c913_a0fe_09a9,
						0x11e1_0afb_78ad_7f13,
						0x0542_9d0e_3e91_8f52
					),
					new Fp(
						0x534d_ffff_ffc4_aae6,
						0x5397_ff17_4c67_ffcf,
						0xbff2_73eb_870b_251d,
						0xdaf2_8271_5287_0915,
						0x393a_9cba_ca9e_2dc3,
						0x14be_74db_faee_5748
					)
				),
				Fp2.One()
			};
			SSWU_RV1 = new Fp2(
				new Fp(
					0x7bcf_a7a2_5aa3_0fda,
					0xdc17_dec1_2a92_7e7c,
					0x2f08_8dd8_6b4e_bef1,
					0xd1ca_2087_da74_d4a7,
					0x2da2_5966_96ce_bc1d,
					0x0e2b_7eed_bbfd_87d2
				),
				new Fp(
					0x7bcf_a7a2_5aa3_0fda,
					0xdc17_dec1_2a92_7e7c,
					0x2f08_8dd8_6b4e_bef1,
					0xd1ca_2087_da74_d4a7,
					0x2da2_5966_96ce_bc1d,
					0x0e2b_7eed_bbfd_87d2
				)
			);
			SSWU_ETAS = new Fp2[]
			{
				new Fp2(
					new Fp(
						0x05e5_1466_8ac7_36d2,
						0x9089_b4d6_b84f_3ea5,
						0x603c_384c_224a_8b32,
						0xf325_7909_536a_fea6,
						0x5c5c_dbab_ae65_6d81,
						0x075b_fa08_63c9_87e9
					),
					new Fp(
						0x338d_9bfe_0808_7330,
						0x7b8e_48b2_bd83_cefe,
						0x530d_ad5d_306b_5be7,
						0x5a4d_7e8e_6c40_8b6d,
						0x6258_f7a6_232c_ab9b,
						0x0b98_5811_cce1_4db5
					)
				),
				new Fp2(
					new Fp(
						0x8671_6401_f7f7_377b,
						0xa31d_b74b_f3d0_3101,
						0x1423_2543_c645_9a3c,
						0x0a29_ccf6_8744_8752,
						0xe8c2_b010_201f_013c,
						0x0e68_b9d8_6c9e_98e4
					),
					new Fp(
						0x05e5_1466_8ac7_36d2,
						0x9089_b4d6_b84f_3ea5,
						0x603c_384c_224a_8b32,
						0xf325_7909_536a_fea6,
						0x5c5c_dbab_ae65_6d81,
						0x075b_fa08_63c9_87e9
					)
				),
				new Fp2(
					new Fp(
						0x718f_dad2_4ee1_d90f,
						0xa58c_025b_ed82_76af,
						0x0c3a_1023_0ab7_976f,
						0xf0c5_4df5_c8f2_75e1,
						0x4ec2_478c_28ba_f465,
						0x1129_373a_90c5_08e6
					),
					new Fp(
						0x019a_f5f9_80a3_680c,
						0x4ed7_da0e_6606_3afa,
						0x6003_5472_3b5d_9972,
						0x8b2f_958b_20d0_9d72,
						0x0474_938f_02d4_61db,
						0x0dcf_8b9e_0684_ab1c
					)
				),
				new Fp2(
					new Fp(
						0xb864_0a06_7f5c_429f,
						0xcfd4_25f0_4b4d_c505,
						0x072d_7e2e_bb53_5cb1,
						0xd947_b5f9_d2b4_754d,
						0x46a7_1427_4077_4afb,
						0x0c31_864c_32fb_3b7e
					),
					new Fp(
						0x718f_dad2_4ee1_d90f,
						0xa58c_025b_ed82_76af,
						0x0c3a_1023_0ab7_976f,
						0xf0c5_4df5_c8f2_75e1,
						0x4ec2_478c_28ba_f465,
						0x1129_373a_90c5_08e6
					)
				)
			};
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
					cur = G2Prepared.DoublingStep(cur, out var values);
					coeffs.Add(values);
					return f; // Dummy
				},
				(f) =>
				{
					cur = G2Prepared.AdditionStep(cur, q, out var values);
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

		public bool IsIdentity()
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
				Fp2 y = (x.Square() * x + B).SquareRoot() ?? throw new Exception("Cant calculate square root");

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
