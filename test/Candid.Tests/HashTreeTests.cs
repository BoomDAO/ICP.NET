
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
		//[Fact]
		//public void CborDecode()
		//{
		//	var tests = new Dictionary<string, (string RootHashHex, HashTree Tree)>
		//	{
		//		{
		//			"8301830183024161830183018302417882034568656c6c6f810083024179820345776f726c6483024162820344676f6f648301830241638100830241648203476d6f726e696e67",
		//			(
		//				"",
		//				HashTree.Fork(
		//					HashTree.Fork(
		//						HashTree.Labeled(
		//							"a",
		//							HashTree.Fork(
		//								HashTree.Fork(
		//									HashTree.Labeled(
		//										"x",
		//										HashTree.Leaf("hello")
		//									),
		//									HashTree.Empty()
		//								),
		//								HashTree.Labeled(
		//									"y",
		//									HashTree.Leaf("world")
		//								)
		//							)
		//						),
		//						HashTree.Labeled(
		//							"b",
		//							HashTree.Leaf("good")
		//						)
		//					),
		//					HashTree.Fork(
		//						HashTree.Labeled(
		//							"c",
		//							HashTree.Empty()
		//						),
		//						HashTree.Labeled(
		//							"d",
		//							HashTree.Leaf("morning")
		//						)
		//					)
		//				)
		//			)
		//		}
		//	};
		//	foreach ((string cborHex, (string expectedRootHashHex, HashTree expectedTree)) in tests)
		//	{
		//		byte[] cborBytes = ByteUtil.FromHexString(cborHex);
		//		var cborReader = new CborReader(cborBytes);
		//		HashTree tree = Certificate.ReadTreeCbor(cborReader);
		//		Assert.Equal(expectedTree, tree);

		//		// TODO
		//		//byte[] rootHash = tree.BuildRootHash();
		//		//string rootHashHex = ByteUtil.ToHexString(rootHash);
		//		//Assert.Equal(expectedRootHashHex, rootHashHex);
		//	}
		//}

		[Theory]
		[InlineData("8301830182045820250f5e26868d9c1ea7ab29cbe9c15bf1c47c0d7605e803e39e375a7fe09c6ebb830183024e726571756573745f7374617475738301820458204b268227774ec77ff2b37ecb12157329d54cf376694bdd59ded7803efd82386f83025820edad510eaaa08ed2acd4781324e6446269da6753ec17760f206bbe81c465ff528301830183024b72656a6563745f636f64658203410383024e72656a6563745f6d6573736167658203584443616e69737465722069766733372d71696161612d61616161622d61616167612d63616920686173206e6f20757064617465206d6574686f64202772656769737465722783024673746174757382034872656a65637465648204582097232f31f6ab7ca4fe53eb6568fc3e02bc22fe94ab31d010e5fb3c642301f1608301820458203a48d1fc213d49307103104f7d72c2b5930edba8787b90631f343b3aa68a5f0a83024474696d65820349e2dc939091c696eb1669")]
		public void BuildRootHash(string certHex)
		{
			byte[] cborBytes = ByteUtil.FromHexString(certHex);
			var cborReader = new CborReader(cborBytes);
			HashTree tree = Certificate.ReadTreeCbor(cborReader);
			byte[] rootHash = tree.BuildRootHash();
			Assert.True(rootHash != null);
		}
	}
}
