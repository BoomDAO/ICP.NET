using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Utilities;
using System.Collections.Generic;
using Xunit;

namespace EdjCase.ICP.Candid.Tests
{
	public class ArgTests
	{
		[Fact]
		public void RequestId_Dict()
		{
			var arg = CandidArg.FromCandid(
				CandidTypedValue.FromValueAndType(
					CandidValue.Text("https://www.theverge.com/rss/index.xml"),
					new CandidPrimitiveType(PrimitiveType.Text)
				),
				CandidTypedValue.FromValueAndType(
					new CandidRecord(new Dictionary<CandidTag, CandidValue>
					{
							{
								CandidTag.FromName("title"),
								CandidValue.Text("The Title")
							},
							{
								CandidTag.FromName("body"),
								new CandidRecord(new Dictionary<CandidTag, CandidValue>
								{
									{
										CandidTag.FromName("format"),
										new CandidOptional(CandidValue.Text("text/html"))
									},
									{
										CandidTag.FromName("value"),
										CandidValue.Text("<h1>Hello</h1>")
									}
								})
							},
							{
								CandidTag.FromName("link"),
								CandidValue.Text("https://www.theverge.com/rss/index.xml")
							},
							{
								CandidTag.FromName("authors"),
								new CandidVector(new CandidValue[]
								{
									new CandidVariant("name", CandidValue.Text("author1")),
									new CandidVariant("name", CandidValue.Text("author2"))
								})
							},
							{
								CandidTag.FromName("imageLink"),
								new CandidOptional(CandidValue.Text("https://google.com"))
							},
							{
								CandidTag.FromName("language"),
								new CandidOptional(CandidValue.Text("en-us"))
							},
							{
								CandidTag.FromName("date"),
								CandidValue.Int(0)
							}
							}),
							new CandidRecordType(new Dictionary<CandidTag, CandidType>
							{
							{
								CandidTag.FromName("title"),
								new CandidPrimitiveType(PrimitiveType.Text)
							},
							{
								CandidTag.FromName("body"),
								new CandidRecordType(new Dictionary<CandidTag, CandidType>
								{
									{
										CandidTag.FromName("format"),
										new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text))
									},
									{
										CandidTag.FromName("value"),
										new CandidPrimitiveType(PrimitiveType.Text)
									}
								})
							},
							{
								CandidTag.FromName("link"),
								new CandidPrimitiveType(PrimitiveType.Text)
							},
							{
								CandidTag.FromName("authors"),
								new CandidVectorType(new CandidVariantType(new Dictionary<CandidTag, CandidType>
								{
									{
										CandidTag.FromName("name"),
										new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text))
									},
									{
										CandidTag.FromName("identity"),
										new CandidPrimitiveType(PrimitiveType.Principal)
									},
									{
										CandidTag.FromName("handle"),
										new CandidPrimitiveType(PrimitiveType.Text)
									}
								}))
							},
							{
								CandidTag.FromName("imageLink"),
								new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text))
							},
							{
								CandidTag.FromName("language"),
								new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text))
							},
							{
								CandidTag.FromName("date"),
								new CandidPrimitiveType(PrimitiveType.Int)
							}
					})
				)
			);

			byte[] bytes = arg.Encode();
			byte[] expectedBytes = ByteUtil.FromHexString("4449444C056E716C02F1FEE18D0371B79EBAEC0F006B03CBE4FDC70400A887F6BF0B71BE8ABDC90B686D026C0798ABEC810171F5D99ECF0200A2F5ED880401AEAC8D93047CFAAFCCBD0471D880C6D00700889FC5C709030271042668747470733A2F2F7777772E74686576657267652E636F6D2F7273732F696E6465782E786D6C09546865205469746C65011268747470733A2F2F676F6F676C652E636F6D0E3C68313E48656C6C6F3C2F68313E0109746578742F68746D6C002668747470733A2F2F7777772E74686576657267652E636F6D2F7273732F696E6465782E786D6C0105656E2D7573020007617574686F72310007617574686F7232");

			Assert.Equal(expectedBytes, bytes);

			byte[] hash = arg.ComputeHash(SHA256HashFunction.Create());
			byte[] expectedHash = ByteUtil.FromHexString("A11B7C777E78241FD4EB7FE5B7CE25BAD1B2E06558100D531F73D527D55FA589");

			Assert.Equal(expectedHash, hash);
		}
	}
}
