
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ICP.Candid.Tests
{
	public class HashTreeTests
	{
		[Fact]
		public void CborDecode()
		{
			var tests = new Dictionary<string, (string RootHashHex, HashTree Tree)>
			{
				{
					"8301830183024161830183018302417882034568656c6c6f810083024179820345776f726c6483024162820344676f6f648301830241638100830241648203476d6f726e696e67",
					(
						"eb5c5b2195e62d996b84c9bcc8259d19a83786a2f59e0878cec84c811f669aa0",
						HashTree.Fork(
							HashTree.Fork(
								HashTree.Labeled(
									"a",
									HashTree.Fork(
										HashTree.Fork(
											HashTree.Labeled(
												"x",
												HashTree.Leaf("hello")
											),
											HashTree.Empty()
										),
										HashTree.Labeled(
											"y",
											HashTree.Leaf("world")
										)
									)
								),
								HashTree.Labeled(
									"b",
									HashTree.Leaf("good")
								)
							),
							HashTree.Fork(
								HashTree.Labeled(
									"c",
									HashTree.Empty()
								),
								HashTree.Labeled(
									"d",
									HashTree.Leaf("morning")
								)
							)
						)
					)
				},
				{
					"83018301830241618301820458201b4feff9bef8131788b0c9dc6dbad6e81e524249c879e9f10f71ce3749f5a63883024179820345776f726c6483024162820458207b32ac0c6ba8ce35ac82c255fc7906f7fc130dab2a090f80fe12f9c2cae83ba6830182045820ec8324b8a1f1ac16bd2e806edba78006479c9877fed4eb464a25485465af601d830241648203476d6f726e696e67",
					(
						"eb5c5b2195e62d996b84c9bcc8259d19a83786a2f59e0878cec84c811f669aa0",
						HashTree.Fork(
							HashTree.Fork(
								HashTree.Labeled(
									"a",
									HashTree.Fork(
										HashTree.Pruned(ByteUtil.FromHexString("1b4feff9bef8131788b0c9dc6dbad6e81e524249c879e9f10f71ce3749f5a638")),
										HashTree.Labeled(
											"y",
											HashTree.Leaf("world")
										)
									)
								),
								HashTree.Labeled(
									"b",
									HashTree.Pruned(ByteUtil.FromHexString("7b32ac0c6ba8ce35ac82c255fc7906f7fc130dab2a090f80fe12f9c2cae83ba6"))
								)
							),
							HashTree.Fork(
								HashTree.Pruned(ByteUtil.FromHexString("ec8324b8a1f1ac16bd2e806edba78006479c9877fed4eb464a25485465af601d")),
								HashTree.Labeled(
									"d",
									HashTree.Leaf("morning")
								)
							)
						)
					)
				}
			};
			foreach ((string cborHex, (string expectedRootHashHex, HashTree expectedTree)) in tests)
			{
				byte[] cborBytes = ByteUtil.FromHexString(cborHex);
				var cborReader = new CborReader(cborBytes);
				HashTree tree = Certificate.TreeFromCbor(cborReader);
				Assert.Equal(expectedTree, tree);

				byte[] rootHash = tree.BuildRootHash();
				string rootHashHex = ByteUtil.ToHexString(rootHash);
				Assert.Equal(expectedRootHashHex, rootHashHex, ignoreCase: true);
			}
		}

		//[Theory]
		//[InlineData("8301830182045820250f5e26868d9c1ea7ab29cbe9c15bf1c47c0d7605e803e39e375a7fe09c6ebb830183024e726571756573745f7374617475738301820458204b268227774ec77ff2b37ecb12157329d54cf376694bdd59ded7803efd82386f83025820edad510eaaa08ed2acd4781324e6446269da6753ec17760f206bbe81c465ff528301830183024b72656a6563745f636f64658203410383024e72656a6563745f6d6573736167658203584443616e69737465722069766733372d71696161612d61616161622d61616167612d63616920686173206e6f20757064617465206d6574686f64202772656769737465722783024673746174757382034872656a65637465648204582097232f31f6ab7ca4fe53eb6568fc3e02bc22fe94ab31d010e5fb3c642301f1608301820458203a48d1fc213d49307103104f7d72c2b5930edba8787b90631f343b3aa68a5f0a83024474696d65820349e2dc939091c696eb1669")]
		//public void BuildRootHash(string certHex)
		//{
		//	byte[] cborBytes = ByteUtil.FromHexString(certHex);
		//	var cborReader = new CborReader(cborBytes);
		//	HashTree tree = Certificate.ReadTreeCbor(cborReader);
		//	byte[] rootHash = tree.BuildRootHash();
		//	Assert.True(rootHash != null);
		//}
	}
}
