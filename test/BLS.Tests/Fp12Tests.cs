using EdjCase.ICP.BLS.Models;


namespace BLS.Tests
{
	public class Fp12Tests
	{
		[Fact]
		public void TestArithmetic()
		{
			var a = new Fp12(
				new Fp6(
					new Fp2(
						new Fp(
							0x47f9_cb98_b1b8_2d58,
							0x5fe9_11eb_a3aa_1d9d,
							0x96bf_1b5f_4dd8_1db3,
							0x8100_d272_c925_9f5b,
							0xafa2_0b96_7464_0eab,
							0x09bb_cea7_d8d9_497d
						),
						new Fp(
							0x0303_cb98_b166_2daa,
							0xd931_10aa_0a62_1d5a,
							0xbfa9_820c_5be4_a468,
							0x0ba3_643e_cb05_a348,
							0xdc35_34bb_1f1c_25a6,
							0x06c3_05bb_19c0_e1c1
						)
					),
					new Fp2(
						new Fp(
							0x46f9_cb98_b162_d858,
							0x0be9_109c_f7aa_1d57,
							0xc791_bc55_fece_41d2,
							0xf84c_5770_4e38_5ec2,
							0xcb49_c1d9_c010_e60f,
							0x0acd_b8e1_58bf_e348
						),
						new Fp(
							0x8aef_cb98_b15f_8306,
							0x3ea1_108f_e4f2_1d54,
							0xcf79_f69f_a1b7_df3b,
							0xe4f5_4aa1_d16b_1a3c,
							0xba5e_4ef8_6105_a679,
							0x0ed8_6c07_97be_e5cf
						)
					),
					new Fp2(
						new Fp(
							0xcee5_cb98_b15c_2db4,
							0x7159_1082_d23a_1d51,
							0xd762_30e9_44a1_7ca4,
							0xd19e_3dd3_549d_d5b6,
							0xa972_dc17_01fa_66e3,
							0x12e3_1f2d_d6bd_e7d6
						),
						new Fp(
							0xad2a_cb98_b173_2d9d,
							0x2cfd_10dd_0696_1d64,
							0x0739_6b86_c6ef_24e8,
							0xbd76_e2fd_b1bf_c820,
							0x6afe_a7f6_de94_d0d5,
							0x1099_4b0c_5744_c040
						)
					)
				),
				new Fp6(
					new Fp2(
						new Fp(
							0x47f9_cb98_b1b8_2d58,
							0x5fe9_11eb_a3aa_1d9d,
							0x96bf_1b5f_4dd2_1db3,
							0x8100_d27c_c925_9f5b,
							0xafa2_0b96_7464_0eab,
							0x09bb_cea7_d8d9_497d
						),
						new Fp(
							0x0303_cb98_b166_2daa,
							0xd931_10aa_0a62_1d5a,
							0xbfa9_820c_5be4_a468,
							0x0ba3_643e_cb05_a348,
							0xdc35_34bb_1f1c_25a6,
							0x06c3_05bb_19c0_e1c1
						)
					),
					new Fp2(
						new Fp(
							0x46f9_cb98_b162_d858,
							0x0be9_109c_f7aa_1d57,
							0xc791_bc55_fece_41d2,
							0xf84c_5770_4e38_5ec2,
							0xcb49_c1d9_c010_e60f,
							0x0acd_b8e1_58bf_e3c8
						),
						new Fp(
							0x8aef_cb98_b15f_8306,
							0x3ea1_108f_e4f2_1d54,
							0xcf79_f69f_a117_df3b,
							0xe4f5_4aa1_d16b_1a3c,
							0xba5e_4ef8_6105_a679,
							0x0ed8_6c07_97be_e5cf
						)
					),
					new Fp2(
						new Fp(
							0xcee5_cb98_b15c_2db4,
							0x7159_1082_d23a_1d51,
							0xd762_30e9_44a1_7ca4,
							0xd19e_3dd3_549d_d5b6,
							0xa972_dc17_01fa_66e3,
							0x12e3_1f2d_d6bd_e7d6
						),
						new Fp(
							0xad2a_cb98_b173_2d9d,
							0x2cfd_10dd_0696_1d64,
							0x0739_6b86_c6ef_24e8,
							0xbd76_e2fd_b1bf_c820,
							0x6afe_a7f6_de94_d0d5,
							0x1099_4b0c_5744_c040
						)
					)
				)
			);
			var b = new Fp12(
				new Fp6(
					new Fp2(
						new Fp(
							0x47f9_cb98_b1b8_2d58,
							0x5fe9_11eb_a3aa_1d9d,
							0x96bf_1b5f_4dd8_1db3,
							0x8100_d272_c925_9f5b,
							0xafa2_0b96_7464_0eab,
							0x09bb_cea7_d8d9_497d
						),
						new Fp(
							0x0303_cb98_b166_2daa,
							0xd931_10aa_0a62_1d5a,
							0xbfa9_820c_5be4_a468,
							0x0ba3_643e_cb05_a348,
							0xdc35_34bb_1f1c_25a6,
							0x06c3_05bb_19c0_e1c1
						)
					),
					new Fp2(
						new Fp(
							0x46f9_cb98_b162_d858,
							0x0be9_109c_f7aa_1d57,
							0xc791_bc55_fece_41d2,
							0xf84c_5770_4e38_5ec2,
							0xcb49_c1d9_c010_e60f,
							0x0acd_b8e1_58bf_e348
						),
						new Fp(
							0x8aef_cb98_b15f_8306,
							0x3ea1_108f_e4f2_1d54,
							0xcf79_f69f_a1b7_df3b,
							0xe4f5_4aa1_d16b_1a3c,
							0xba5e_4ef8_6105_a679,
							0x0ed8_6c07_97be_e5cf
						)
					),
					new Fp2(
						new Fp(
							0xcee5_cb98_b15c_2db4,
							0x7159_1082_d23a_1d51,
							0xd762_30e9_44a1_7ca4,
							0xd19e_3dd3_549d_d5b6,
							0xa972_dc17_01fa_66e3,
							0x12e3_1f2d_d6bd_e7d6
						),
						new Fp(
							0xad2a_cb98_b173_2d9d,
							0x2cfd_10dd_0696_1d64,
							0x0739_6b86_c6ef_24e8,
							0xbd76_e2fd_b1bf_c820,
							0x6afe_a7f6_de94_d0d5,
							0x1099_4b0c_5744_c040
						)
					)
				),
				new Fp6(
					new Fp2(
						new Fp(
							0x47f9_cb98_b1b8_2d58,
							0x5fe9_11eb_a3aa_1d9d,
							0x96bf_1b5f_4dd2_1db3,
							0x8100_d27c_c925_9f5b,
							0xafa2_0b96_7464_0eab,
							0x09bb_cea7_d8d9_497d
						),
						new Fp(
							0x0303_cb98_b166_2daa,
							0xd931_10aa_0a62_1d5a,
							0xbfa9_820c_5be4_a468,
							0x0ba3_643e_cb05_a348,
							0xdc35_34bb_1f1c_25a6,
							0x06c3_05bb_19c0_e1c1
						)
					),
					new Fp2(
						new Fp(
							0x46f9_cb98_b162_d858,
							0x0be9_109c_f7aa_1d57,
							0xc791_bc55_fece_41d2,
							0xf84c_5770_4e38_5ec2,
							0xcb49_c1d9_c010_e60f,
							0x0acd_b8e1_58bf_e3c8
						),
						new Fp(
							0x8aef_cb98_b15f_8306,
							0x3ea1_108f_e4f2_1d54,
							0xcf79_f69f_a117_df3b,
							0xe4f5_4aa1_d16b_1a3c,
							0xba5e_4ef8_6105_a679,
							0x0ed8_6c07_97be_e5cf
						)
					),
					new Fp2(
						new Fp(
							0xcee5_cb98_b15c_2db4,
							0x7159_1082_d23a_1d51,
							0xd762_30e9_44a1_7ca4,
							0xd19e_3dd3_549d_d5b6,
							0xa972_dc17_01fa_66e3,
							0x12e3_1f2d_d6bd_e7d6
						),
						new Fp(
							0xad2a_cb98_b173_2d9d,
							0x2cfd_10dd_0696_1d64,
							0x0739_6b86_c6ef_24e8,
							0xbd76_e2fd_b1bf_c820,
							0x6afe_a7f6_de94_d0d5,
							0x1099_4b0c_5744_c040
						)
					)
				)
			);

			var c = new Fp12(
				new Fp6(
					new Fp2(
						new Fp(
							0x47f9_cb98_b1b8_2d58,
							0x5fe9_11eb_a3aa_1d9d,
							0x96bf_1b5f_4dd8_1db3,
							0x8100_d272_c925_9f5b,
							0xafa2_0b96_7464_0eab,
							0x09bb_cea7_d8d9_497d
						),
						new Fp(
							0x0303_cb98_b166_2daa,
							0xd931_10aa_0a62_1d5a,
							0xbfa9_820c_5be4_a468,
							0x0ba3_643e_cb05_a348,
							0xdc35_34bb_1f1c_25a6,
							0x06c3_05bb_19c0_e1c1
						)
					),
					new Fp2(
						new Fp(
							0x46f9_cb98_b162_d858,
							0x0be9_109c_f7aa_1d57,
							0xc791_bc55_fece_41d2,
							0xf84c_5770_4e38_5ec2,
							0xcb49_c1d9_c010_e60f,
							0x0acd_b8e1_58bf_e348
						),
						new Fp(
							0x8aef_cb98_b15f_8306,
							0x3ea1_108f_e4f2_1d54,
							0xcf79_f69f_a1b7_df3b,
							0xe4f5_4aa1_d16b_1a3c,
							0xba5e_4ef8_6105_a679,
							0x0ed8_6c07_97be_e5cf
						)
					),
					new Fp2(
						new Fp(
							0xcee5_cb98_b15c_2db4,
							0x7159_1082_d23a_1d51,
							0xd762_30e9_44a1_7ca4,
							0xd19e_3dd3_549d_d5b6,
							0xa972_dc17_01fa_66e3,
							0x12e3_1f2d_d6bd_e7d6
						),
						new Fp(
							0xad2a_cb98_b173_2d9d,
							0x2cfd_10dd_0696_1d64,
							0x0739_6b86_c6ef_24e8,
							0xbd76_e2fd_b1bf_c820,
							0x6afe_a7f6_de94_d0d5,
							0x1099_4b0c_5744_c040
						)
					)
				),
				new Fp6(
					new Fp2(
						new Fp(
							0x47f9_cb98_b1b8_2d58,
							0x5fe9_11eb_a3aa_1d9d,
							0x96bf_1b5f_4dd2_1db3,
							0x8100_d27c_c925_9f5b,
							0xafa2_0b96_7464_0eab,
							0x09bb_cea7_d8d9_497d
						),
						new Fp(
							0x0303_cb98_b166_2daa,
							0xd931_10aa_0a62_1d5a,
							0xbfa9_820c_5be4_a468,
							0x0ba3_643e_cb05_a348,
							0xdc35_34bb_1f1c_25a6,
							0x06c3_05bb_19c0_e1c1
						)
					),
					new Fp2(
						new Fp(
							0x46f9_cb98_b162_d858,
							0x0be9_109c_f7aa_1d57,
							0xc791_bc55_fece_41d2,
							0xf84c_5770_4e38_5ec2,
							0xcb49_c1d9_c010_e60f,
							0x0acd_b8e1_58bf_e3c8
						),
						new Fp(
							0x8aef_cb98_b15f_8306,
							0x3ea1_108f_e4f2_1d54,
							0xcf79_f69f_a117_df3b,
							0xe4f5_4aa1_d16b_1a3c,
							0xba5e_4ef8_6105_a679,
							0x0ed8_6c07_97be_e5cf
						)
					),
					new Fp2(
						new Fp(
							0xcee5_cb98_b15c_2db4,
							0x7159_1082_d23a_1d51,
							0xd762_30e9_44a1_7ca4,
							0xd19e_3dd3_549d_d5b6,
							0xa972_dc17_01fa_66e3,
							0x12e3_1f2d_d6bd_e7d6
						),
						new Fp(
							0xad2a_cb98_b173_2d9d,
							0x2cfd_10dd_0696_1d64,
							0x0739_6b86_c6ef_24e8,
							0xbd76_e2fd_b1bf_c820,
							0x6afe_a7f6_de94_d0d5,
							0x1099_4b0c_5744_c040
						)
					)
				)
			);


			a = a.Square().Invert()!.Value.Square().Add(c);
			b = b.Square().Invert()!.Value.Square().Add(a);
			c = c.Square().Invert()!.Value.Square().Add(b);

			Assert.Equal(a.Square(), a.Multiply(a));
			Assert.Equal(b.Square(), b.Multiply(b));
			Assert.Equal(c.Square(), c.Multiply(c));

			Assert.Equal((a.Add(b)).Multiply(c.Square()), (c.Square().Multiply(a)).Add(c.Square().Multiply(b)));

			Assert.Equal(a.Invert()!.Value.Multiply(b.Invert()!.Value), (a.Multiply(b)).Invert());
			Assert.Equal(a.Invert()!.Value.Multiply(a), Fp12.One());

			Assert.NotEqual(a, a.FrobeniusMap());
			Assert.Equal(a, a.FrobeniusMap().FrobeniusMap().FrobeniusMap().FrobeniusMap().FrobeniusMap().FrobeniusMap().FrobeniusMap().FrobeniusMap().FrobeniusMap().FrobeniusMap().FrobeniusMap().FrobeniusMap());
		}
	}
}
