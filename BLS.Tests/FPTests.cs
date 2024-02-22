using EdjCase.ICP.BLS.Models;


namespace BLS.Tests
{
	public class FpTests
	{
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

	}
}
