using EdjCase.ICP.BLS.Models;


namespace BLS.Tests
{
	public class FpTests
	{
		[Theory]
		[InlineData(new ulong[] { 1, 2, 3, 4, 5, 6 }, new ulong[] { 1, 2, 3, 4, 5, 6 }, true)] // Equal
		[InlineData(new ulong[] { 7, 2, 3, 4, 5, 6 }, new ulong[] { 1, 2, 3, 4, 5, 6 }, false)] // Not equal
		[InlineData(new ulong[] { 1, 7, 3, 4, 5, 6 }, new ulong[] { 1, 2, 3, 4, 5, 6 }, false)] // Not equal
		[InlineData(new ulong[] { 1, 2, 7, 4, 5, 6 }, new ulong[] { 1, 2, 3, 4, 5, 6 }, false)] // Not equal
		[InlineData(new ulong[] { 1, 2, 3, 7, 5, 6 }, new ulong[] { 1, 2, 3, 4, 5, 6 }, false)] // Not equal
		[InlineData(new ulong[] { 1, 2, 3, 4, 7, 6 }, new ulong[] { 1, 2, 3, 4, 5, 6 }, false)] // Not equal
		[InlineData(new ulong[] { 1, 2, 3, 4, 5, 7 }, new ulong[] { 1, 2, 3, 4, 5, 6 }, false)] // Not equal
		public void Equals_Equal(ulong[] valuesA, ulong[] valuesB, bool expected)
		{
			// Assuming Fp constructor takes an array of ulongs
			Fp a = new (
				valuesA[0],
				valuesA[1],
				valuesA[2],
				valuesA[3],
				valuesA[4],
				valuesA[5]
			);
			Fp b = new(
				valuesB[0],
				valuesB[1],
				valuesB[2],
				valuesB[3],
				valuesB[4],
				valuesB[5]
			);

			Assert.Equal(expected, a.Equals(b));
		}
		[Fact]
		public void Multiply_Equal()
		{
			Fp a = new(
				0x0397_a383_2017_0cd4,
				0x734c_1b2c_9e76_1d30,
				0x5ed2_55ad_9a48_beb5,
				0x095a_3c6b_22a7_fcfc,
				0x2294_ce75_d4e2_6a27,
				0x1333_8bd8_7001_1ebb
			);
			Fp b = new(
				0xb9c3_c7c5_b119_6af7,
				0x2580_e208_6ce3_35c1,
				0xf49a_ed3d_8a57_ef42,
				0x41f2_81e4_9846_e878,
				0xe076_2346_c384_52ce,
				0x0652_e893_26e5_7dc0
			);
			Fp c = new(
				0xf96e_f3d7_11ab_5355,
				0xe8d4_59ea_00f1_48dd,
				0x53f7_354a_5f00_fa78,
				0x9e34_a4f3_125c_5f83,
				0x3fbe_0c47_ca74_c19e,
				0x01b0_6a8b_bd4a_dfe4
			);

			Assert.Equal(c, a * b);
		}

		[Fact]
		public void Add_Equal()
		{
			Fp a = new(
				0x5360_bb59_7867_8032,
				0x7dd2_75ae_799e_128e,
				0x5c5b_5071_ce4f_4dcf,
				0xcdb2_1f93_078d_bb3e,
				0xc323_65c5_e73f_474a,
				0x115a_2a54_89ba_be5b
			);
			Fp b = new(
				0x9fd2_8773_3d23_dda0,
				0xb16b_f2af_738b_3554,
				0x3e57_a75b_d3cc_6d1d,
				0x900b_c0bd_627f_d6d6,
				0xd319_a080_efb2_45fe,
				0x15fd_caa4_e4bb_2091
			);
			Fp c = new(
				0x3934_42cc_b58b_b327,
				0x1092_685f_3bd5_47e3,
				0x3382_252c_ab6a_c4c9,
				0xf946_94cb_7688_7f55,
				0x4b21_5e90_93a5_e071,
				0x0d56_e30f_34f5_f853
			);
			Assert.Equal(c, a + b);
		}

		[Fact]
		public void Subtract_Equal()
		{
			Fp a = new(
				0x5360_bb59_7867_8032,
				0x7dd2_75ae_799e_128e,
				0x5c5b_5071_ce4f_4dcf,
				0xcdb2_1f93_078d_bb3e,
				0xc323_65c5_e73f_474a,
				0x115a_2a54_89ba_be5b
			);
			Fp b = new(
				0x9fd2_8773_3d23_dda0,
				0xb16b_f2af_738b_3554,
				0x3e57_a75b_d3cc_6d1d,
				0x900b_c0bd_627f_d6d6,
				0xd319_a080_efb2_45fe,
				0x15fd_caa4_e4bb_2091
			);
			Fp c = new(
				0x6d8d_33e6_3b43_4d3d,
				0xeb12_82fd_b766_dd39,
				0x8534_7bb6_f133_d6d5,
				0xa21d_aa5a_9892_f727,
				0x3b25_6cfb_3ad8_ae23,
				0x155d_7199_de7f_8464
			);

			Assert.Equal(c, a - b);
		}

		[Fact]
		public void Square_Equal()
		{
			Fp a = new(
				0xd215_d276_8e83_191b,
				0x5085_d80f_8fb2_8261,
				0xce9a_032d_df39_3a56,
				0x3e9c_4fff_2ca0_c4bb,
				0x6436_b6f7_f4d9_5dfb,
				0x1060_6628_ad4a_4d90
			);
			Fp b = new(
				0x33d9_c42a_3cb3_e235,
				0xdad1_1a09_4c4c_d455,
				0xa2f1_44bd_729a_aeba,
				0xd415_0932_be9f_feac,
				0xe27b_c7c4_7d44_ee50,
				0x14b6_a78d_3ec7_a560
			);

			Assert.Equal(b, a.Square());
		}

		[Fact]
		public void Neg_Equal()
		{
			Fp a = new(
				0x5360_bb59_7867_8032,
				0x7dd2_75ae_799e_128e,
				0x5c5b_5071_ce4f_4dcf,
				0xcdb2_1f93_078d_bb3e,
				0xc323_65c5_e73f_474a,
				0x115a_2a54_89ba_be5b
			);
			Fp b = new(
				0x669e_44a6_8798_2a79,
				0xa0d9_8a50_37b5_ed71,
				0x0ad5_822f_2861_a854,
				0x96c5_2bf1_ebf7_5781,
				0x87f8_41f0_5c0c_658c,
				0x08a6_e795_afc5_283e
			);
			Assert.Equal(b, a.Neg());
		}

		[Fact]
		public void FromBytes_SquareEqual()
		{
			Fp a = new(
				0xdc90_6d9b_e3f9_5dc8,
				0x8755_caf7_4596_91a1,
				0xcff1_a7f4_e958_3ab3,
				0x9b43_821f_849e_2284,
				0xf575_54f3_a297_4f3f,
				0x085d_bea8_4ed4_7f79
			);

			for (int i = 0; i < 100; i++)
			{
				a = a.Square();
				byte[] tmp = a.ToBytes();
				Fp b = Fp.FromBytes(tmp);
				Assert.Equal(a, b);
			}
		}
		[Fact]
		public void FromBytes_NegativeOne()
		{

			var negativeOneBytes = new byte[]
			{
				26, 1, 17, 234, 57, 127, 230, 154, 75, 27, 167, 182, 67, 75, 172, 215, 100, 119, 75,
				132, 243, 133, 18, 191, 103, 48, 210, 160, 246, 176, 246, 36, 30, 171, 255, 254, 177,
				83, 255, 255, 185, 254, 255, 255, 255, 255, 170, 170
			};
			Assert.Equal(Fp.One().Neg(), Fp.FromBytes(negativeOneBytes));
		}

		[Fact]
		public void FromBytes_InvalidBytes_Error()
		{
			var invalidBytes = new byte[]
			{
				27, 1, 17, 234, 57, 127, 230, 154, 75, 27, 167, 182, 67, 75, 172, 215, 100, 119, 75,
				132, 243, 133, 18, 191, 103, 48, 210, 160, 246, 176, 246, 36, 30, 171, 255, 254, 177,
				83, 255, 255, 185, 254, 255, 255, 255, 255, 170, 170
			};
			Assert.Throws<ArgumentException>(() => Fp.FromBytes(invalidBytes));
		}

		[Fact]
		public void FromBytes_OverflowBytes_Error()
		{
			var overflowBytes = new byte[48];
			Array.Fill(overflowBytes, (byte)0xff);
			Assert.Throws<ArgumentException>(() => Fp.FromBytes(overflowBytes));
		}

		[Fact]
		public void Invert()
		{
			Fp a = new(
				0x43b4_3a50_78ac_2076,
				0x1ce0_7630_46f8_962b,
				0x724a_5276_486d_735c,
				0x6f05_c2a6_282d_48fd,
				0x2095_bd5b_b4ca_9331,
				0x03b3_5b38_94b0_f7da
			);
			Fp b = new(
				0x69ec_d704_0952_148f,
				0x985c_cc20_2219_0f55,
				0xe19b_ba36_a9ad_2f41,
				0x19bb_16c9_5219_dbd8,
				0x14dc_acfd_fb47_8693,
				0x115f_f58a_fff9_a8e1
			);
			Assert.Equal(b, a.Invert());
		}

		[Fact]
		public void Zero_NotLexicographicallyLargest()
		{
			var zero = Fp.Zero();
			Assert.False(zero.LexicographicallyLargest());
		}

		[Fact]
		public void One_NotLexicographicallyLargest()
		{
			var one = Fp.One();
			Assert.False(one.LexicographicallyLargest());
		}

		[Fact]
		public void SpecificValue1_NotLexicographicallyLargest()
		{
			Fp fp = new (
				0xa1fa_ffff_fffe_5557,
				0x995b_fff9_76a3_fffe,
				0x03f4_1d24_d174_ceb4,
				0xf654_7998_c199_5dbd,
				0x778a_468f_507a_6034,
				0x0205_5993_1f7f_8103
			);
			Assert.False(fp.LexicographicallyLargest());
		}

		[Fact]
		public void SpecificValue2_LexicographicallyLargest()
		{
			Fp fp = new (
				0x1804_0000_0001_5554,
				0x8550_0005_3ab0_0001,
				0x633c_b57c_253c_276f,
				0x6e22_d1ec_31eb_b502,
				0xd391_6126_f2d1_4ca2,
				0x17fb_b857_1a00_6596
			);
			Assert.True(fp.LexicographicallyLargest());
		}

		[Fact]
		public void SpecificValue3_LexicographicallyLargest()
		{
			Fp fp = new (
				0x43f5_ffff_fffc_aaae,
				0x32b7_fff2_ed47_fffd,
				0x07e8_3a49_a2e9_9d69,
				0xeca8_f331_8332_bb7a,
				0xef14_8d1e_a0f4_c069,
				0x040a_b326_3eff_0206
			);
			Assert.True(fp.LexicographicallyLargest());
		}

	}
}
