using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using Xunit;

namespace ICP.Candid.Tests
{
	public class CertificateTests
	{
		[Fact]
		public void DeserializeCert()
		{
			var cases = new Dictionary<string, Certificate>
			{
				{
					"d9d9f7a364747265658301830182045820250f5e26868d9c1ea7ab29cbe9c15bf1c47c0d7605e803e39e375a7fe09c6ebb830183024e726571756573745f7374617475738301820458204b268227774ec77ff2b37ecb12157329d54cf376694bdd59ded7803efd82386f83025820edad510eaaa08ed2acd4781324e6446269da6753ec17760f206bbe81c465ff528301830183024b72656a6563745f636f64658203410383024e72656a6563745f6d6573736167658203584443616e69737465722069766733372d71696161612d61616161622d61616167612d63616920686173206e6f20757064617465206d6574686f64202772656769737465722783024673746174757382034872656a65637465648204582097232f31f6ab7ca4fe53eb6568fc3e02bc22fe94ab31d010e5fb3c642301f1608301820458203a48d1fc213d49307103104f7d72c2b5930edba8787b90631f343b3aa68a5f0a83024474696d65820349e2dc939091c696eb16697369676e6174757265583089a2be21b5fa8ac9fab1527e041327ce899d7da971436a1f2165393947b4d942365bfe5488710e61a619ba48388a21b16a64656c65676174696f6ea2697375626e65745f6964581dd77b2a2f7199b9a8aec93fe6fb588661358cf12223e9a3af7b4ebac4026b6365727469666963617465590231d9d9f7a26474726565830182045820ae023f28c3b9d966c8fb09f9ed755c828aadb5152e00aaf700b18c9c067294b483018302467375626e6574830182045820e83bb025f6574c8f31233dc0fe289ff546dfa1e49bd6116dd6e8896d90a4946e830182045820e782619092d69d5bebf0924138bd4116b0156b5a95e25c358ea8cf7e7161a661830183018204582062513fa926c9a9ef803ac284d620f303189588e1d3904349ab63b6470856fc4883018204582060e9a344ced2c9c4a96a0197fd585f2d259dbd193e4eada56239cac26087f9c58302581dd77b2a2f7199b9a8aec93fe6fb588661358cf12223e9a3af7b4ebac402830183024f63616e69737465725f72616e6765738203581bd9d9f781824a000000000020000001014a00000000002fffff010183024a7075626c69635f6b657982035885308182301d060d2b0601040182dc7c0503010201060c2b0601040182dc7c050302010361009933e1f89e8a3c4d7fdcccdbd518089e2bd4d8180a261f18d9c247a52768ebce98dc7328a39814a8f911086a1dd50cbe015e2a53b7bf78b55288893daa15c346640e8831d72a12bdedd979d28470c34823b8d1c3f4795d9c3984a247132e94fe82045820996f17bb926be3315745dea7282005a793b58e76afeb5d43d1a28ce29d2d158583024474696d6582034995b8aac0e4eda2ea16697369676e61747572655830ace9fcdd9bc977e05d6328f889dc4e7c99114c737a494653cb27a1f55c06f4555e0f160980af5ead098acc195010b2f7",
					new Certificate(
						HashTree.Fork(
							HashTree.Fork(
								HashTree.Pruned(ByteUtil.FromHexString("250F5E26868D9C1EA7AB29CBE9C15BF1C47C0D7605E803E39E375A7FE09C6EBB")),
								HashTree.Fork(
									HashTree.Labeled(
										"request_status",
										HashTree.Fork(
											HashTree.Pruned(ByteUtil.FromHexString("4B268227774EC77FF2B37ECB12157329D54CF376694BDD59DED7803EFD82386F")),
											HashTree.Labeled(
												"��Q\u000e���Ҭ�x\u0013$�Dbi�gS�\u0017v\u000f k���e�R",
												HashTree.Fork(
													HashTree.Fork(
														HashTree.Labeled(
															"reject_code",
															HashTree.Leaf(ByteUtil.FromHexString("03"))
														),
														HashTree.Labeled(
															"reject_message",
															HashTree.Leaf(ByteUtil.FromHexString("43616E69737465722069766733372D71696161612D61616161622D61616167612D63616920686173206E6F20757064617465206D6574686F642027726567697374657227"))
														)
													),
													HashTree.Labeled(
														"status",
														HashTree.Leaf(ByteUtil.FromHexString("72656A6563746564"))
													)
												)
											)
										)
									),
									HashTree.Pruned(ByteUtil.FromHexString("97232F31F6AB7CA4FE53EB6568FC3E02BC22FE94AB31D010E5FB3C642301F160"))
								)
							),
							HashTree.Fork(
								HashTree.Pruned(ByteUtil.FromHexString("3A48D1FC213D49307103104F7D72C2B5930EDBA8787B90631F343B3AA68A5F0A")),
								HashTree.Labeled(
									"time",
									HashTree.Leaf(ByteUtil.FromHexString("E2DC939091C696EB16"))
								)
							)
						),
						signature: ByteUtil.FromHexString("89A2BE21B5FA8AC9FAB1527E041327CE899D7DA971436A1F2165393947B4D942365BFE5488710E61A619BA48388A21B1"),
						delegation: new CertificateDelegation(
							Principal.FromText("qxesv-zoxpm-vc64m-zxguk-5sj74-35vrb-tbgwg-pcird-5gr26-62oxl-cae"),
							new Certificate(
								HashTree.Fork(
									HashTree.Pruned(ByteUtil.FromHexString("AE023F28C3B9D966C8FB09F9ED755C828AADB5152E00AAF700B18C9C067294B4")),
									HashTree.Fork(
										HashTree.Labeled(
											ByteUtil.FromHexString("7375626E6574"),
											HashTree.Fork(
												HashTree.Pruned(ByteUtil.FromHexString("E83BB025F6574C8F31233DC0FE289FF546DFA1E49BD6116DD6E8896D90A4946E")),
												HashTree.Fork(
													HashTree.Pruned(ByteUtil.FromHexString("E782619092D69D5BEBF0924138BD4116B0156B5A95E25C358EA8CF7E7161A661")),
													HashTree.Fork(
														HashTree.Fork(
															HashTree.Pruned(ByteUtil.FromHexString("62513FA926C9A9EF803AC284D620F303189588E1D3904349AB63B6470856FC48")),
															HashTree.Fork(
																HashTree.Pruned(ByteUtil.FromHexString("60E9A344CED2C9C4A96A0197FD585F2D259DBD193E4EADA56239CAC26087F9C5")),
																HashTree.Labeled(
																	ByteUtil.FromHexString("D77B2A2F7199B9A8AEC93FE6FB588661358CF12223E9A3AF7B4EBAC402"),
																	HashTree.Fork(
																		HashTree.Labeled(
																			ByteUtil.FromHexString("63616E69737465725F72616E676573"),
																			HashTree.Leaf(ByteUtil.FromHexString("D9D9F781824A000000000020000001014A00000000002FFFFF0101"))
																		),
																		HashTree.Labeled(
																			ByteUtil.FromHexString("7075626C69635F6B6579"),
																			HashTree.Leaf(ByteUtil.FromHexString("308182301D060D2B0601040182DC7C0503010201060C2B0601040182DC7C050302010361009933E1F89E8A3C4D7FDCCCDBD518089E2BD4D8180A261F18D9C247A52768EBCE98DC7328A39814A8F911086A1DD50CBE015E2A53B7BF78B55288893DAA15C346640E8831D72A12BDEDD979D28470C34823B8D1C3F4795D9C3984A247132E94FE"))
																		)
																	)
																)
															)
														),
														HashTree.Pruned(ByteUtil.FromHexString("996F17BB926BE3315745DEA7282005A793B58E76AFEB5D43D1A28CE29D2D1585"))
													)
												)
											)
										),
										HashTree.Labeled(
											ByteUtil.FromHexString("74696D65"),
											HashTree.Leaf(ByteUtil.FromHexString("95B8AAC0E4EDA2EA16"))
										)
									)
								),
								signature: ByteUtil.FromHexString("ACE9FCDD9BC977E05D6328F889DC4E7C99114C737A494653CB27A1F55C06F4555E0F160980AF5EAD098ACC195010B2F7"),
								delegation: null
							)
						)
					)
				},
				//{
				//	"D9D9F7A364747265658301820458209A69D8FA48CEF4FDBF77F13289764571B959BA45AF9C216B4B913A76567435EA830182045820CC578109FC5DBDE591CF79CB3FA6326CDCCDD9FA88EA27592CA888824366AFE383024474696D65820349918093F0ACECC6A917697369676E61747572655830AF2A3822D5AEF52D490440039E0AC07D4EB2D62E9D2996C3D2EF4927B5BE4D24F76858EBF7073E5E9CFCA8CF63DA9C0C6A64656C65676174696F6EA2697375626E65745F6964581D17F67590355E7123AEFFABCE4C1BDDF3A0EFD53A2C2E3F6228FC3ADF026B6365727469666963617465590257D9D9F7A2647472656583018204582055DC418C4218D3FFE213EE8B525A67040E831A8E0C61068C4DDA6C07F7DB811C83018302467375626E657483018301830183018204582035BC207266AA1F9A1B4EEA393EFE91AE33ED4CE77069ED8E881D86716ADF7B6B830183018302581D17F67590355E7123AEFFABCE4C1BDDF3A0EFD53A2C2E3F6228FC3ADF02830183024F63616E69737465725F72616E6765738203581BD9D9F781824A0000000000D0000001014A0000000000DFFFFF010183024A7075626C69635F6B657982035885308182301D060D2B0601040182DC7C0503010201060C2B0601040182DC7C05030201036100850E5EDFB0685D36757643BE011F5D3378FEF0BFB9B0EF67429912EBB9237F5028979EB33779DF7C63ACF59B78EBA4D904C6B4ABAD529B9E8E2195321E287F8C51900F5B440872719C7AC56E381C8063BC6176F8E4B74D06DE669493E49055678204582055F5888D9CC859A79010675FA0EC66D5E9641834B316193E60DD90C0B27E38EA8204582069DE50894E2E942C019ABE7B468408237A53B47FF15449AFFBD005ED5CDB1C3F82045820028FC5E5F70868254E7215E7FC630DBD29EEFC3619AF17CE231909E1FAF97E9582045820A7F251951EED726811460449388214773C94153C758AFE3AAA54F9B51704268682045820DF1124435DF1C9BAE1F1344EF3FDA6A60F8FAF7D06720E35F01349D8A64FC96483024474696D65820349FD9DF0838EB0C3A917697369676E61747572655830B0C2962686EB5E3D8DE2CBD81591F8AB29DC01B3727F6B2641EA60FD147204B4EF5F214D02CEF8D5B0A21A0847F4CCE9",
				//	new Certificate(
				//		HashTree.Empty(),
				//		signature: ByteUtil.FromHexString(""),
				//		delegation: null
				//	)
				//}
			};
			foreach ((string cborHex, Certificate expected) in cases)
			{
				byte[] cborBytes = ByteUtil.FromHexString(cborHex);
				var cborReader = new CborReader(cborBytes);
				Certificate c = Certificate.ReadCbor(cborReader);
				Assert.Equal(expected.Tree, c.Tree);
				Assert.Equal(ByteUtil.ToHexString(expected.Signature), ByteUtil.ToHexString(c.Signature));
				this.CompareDelegation(expected.Delegation, c.Delegation);
			}
		}

		private void CompareDelegation(CertificateDelegation? expected, CertificateDelegation? c)
		{
			if (expected != null)
			{
				Assert.NotNull(c);
				Assert.Equal(expected.SubnetId, c.SubnetId);
				Assert.Equal(ByteUtil.ToHexString(expected.Certificate.Signature), ByteUtil.ToHexString(c.Certificate.Signature));
				Assert.Equal(expected.Certificate.Tree, c.Certificate.Tree);
				this.CompareDelegation(expected.Certificate.Delegation, c.Certificate.Delegation);
			}
			else
			{
				Assert.Null(c);
			}
		}

		//[Theory]
		//[InlineData("D9D9F7A16B636572746966696361746559033FD9D9F7A364747265658301820458209A69D8FA48CEF4FDBF77F13289764571B959BA45AF9C216B4B913A76567435EA830182045820CC578109FC5DBDE591CF79CB3FA6326CDCCDD9FA88EA27592CA888824366AFE383024474696D65820349918093F0ACECC6A917697369676E61747572655830AF2A3822D5AEF52D490440039E0AC07D4EB2D62E9D2996C3D2EF4927B5BE4D24F76858EBF7073E5E9CFCA8CF63DA9C0C6A64656C65676174696F6EA2697375626E65745F6964581D17F67590355E7123AEFFABCE4C1BDDF3A0EFD53A2C2E3F6228FC3ADF026B6365727469666963617465590257D9D9F7A2647472656583018204582055DC418C4218D3FFE213EE8B525A67040E831A8E0C61068C4DDA6C07F7DB811C83018302467375626E657483018301830183018204582035BC207266AA1F9A1B4EEA393EFE91AE33ED4CE77069ED8E881D86716ADF7B6B830183018302581D17F67590355E7123AEFFABCE4C1BDDF3A0EFD53A2C2E3F6228FC3ADF02830183024F63616E69737465725F72616E6765738203581BD9D9F781824A0000000000D0000001014A0000000000DFFFFF010183024A7075626C69635F6B657982035885308182301D060D2B0601040182DC7C0503010201060C2B0601040182DC7C05030201036100850E5EDFB0685D36757643BE011F5D3378FEF0BFB9B0EF67429912EBB9237F5028979EB33779DF7C63ACF59B78EBA4D904C6B4ABAD529B9E8E2195321E287F8C51900F5B440872719C7AC56E381C8063BC6176F8E4B74D06DE669493E49055678204582055F5888D9CC859A79010675FA0EC66D5E9641834B316193E60DD90C0B27E38EA8204582069DE50894E2E942C019ABE7B468408237A53B47FF15449AFFBD005ED5CDB1C3F82045820028FC5E5F70868254E7215E7FC630DBD29EEFC3619AF17CE231909E1FAF97E9582045820A7F251951EED726811460449388214773C94153C758AFE3AAA54F9B51704268682045820DF1124435DF1C9BAE1F1344EF3FDA6A60F8FAF7D06720E35F01349D8A64FC96483024474696D65820349FD9DF0838EB0C3A917697369676E61747572655830B0C2962686EB5E3D8DE2CBD81591F8AB29DC01B3727F6B2641EA60FD147204B4EF5F214D02CEF8D5B0A21A0847F4CCE9")]
		//public void A(string cborHex)
		//{

		//	byte[] cborBytes = ByteUtil.FromHexString(cborHex);
		//	var cborReader = new CborReader(cborBytes);
		//	var r = ReadStateResponse.ReadCbor(cborReader);
		//}
	}
}
