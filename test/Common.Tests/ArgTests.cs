using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ICP.Candid.Tests
{
	public class ArgTests
	{
		[Fact]
		public void RequestId_Dict()
		{
			var arg = CandidArg.FromCandid(
				CandidValueWithType.FromValueAndType(
					CandidPrimitive.Text("https://www.theverge.com/rss/index.xml"),
					new CandidPrimitiveType(PrimitiveType.Text)
				),
				CandidValueWithType.FromValueAndType(
					new CandidRecord(new Dictionary<CandidTag, CandidValue>
					{
							{
								CandidTag.FromName("title"),
								CandidPrimitive.Text("The Title")
							},
							{
								CandidTag.FromName("body"),
								new CandidRecord(new Dictionary<CandidTag, CandidValue>
								{
									{
										CandidTag.FromName("format"),
										new CandidOptional(CandidPrimitive.Text("text/html"))
									},
									{
										CandidTag.FromName("value"),
										CandidPrimitive.Text("<h1>Hello</h1>")
									}
								})
							},
							{
								CandidTag.FromName("link"),
								CandidPrimitive.Text("https://www.theverge.com/rss/index.xml")
							},
							{
								CandidTag.FromName("authors"),
								new CandidVector(new CandidValue[]
								{
									new CandidVariant("name", CandidPrimitive.Text("author1")),
									new CandidVariant("name", CandidPrimitive.Text("author2"))
								})
							},
							{
								CandidTag.FromName("imageLink"),
								new CandidOptional(CandidPrimitive.Text("https://google.com"))
							},
							{
								CandidTag.FromName("language"),
								new CandidOptional(CandidPrimitive.Text("en-us"))
							},
							{
								CandidTag.FromName("date"),
								CandidPrimitive.Int(0)
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
			byte[] expectedBytes = ByteUtil.FromHexString("4449444c056e716c02f1fee18d0371b79ebaec0f006b03cbe4fdc70471a887f6bf0b71be8abdc90b686d026c0798abec810171f5d99ecf0200a2f5ed880401aeac8d93047cfaafccbd0471d880c6d00700889fc5c709030271042668747470733a2f2f7777772e74686576657267652e636f6d2f7273732f696e6465782e786d6c09546865205469746c65011268747470733a2f2f676f6f676c652e636f6d0e3c68313e48656c6c6f3c2f68313e0109746578742f68746d6c002668747470733a2f2f7777772e74686576657267652e636f6d2f7273732f696e6465782e786d6c0105656e2d7573020007617574686f72310007617574686f7232");

			Assert.Equal(expectedBytes, bytes);

			byte[] hash = arg.ComputeHash(SHA256HashFunction.Create());
			byte[] expectedHash = new byte[] { };

			Assert.Equal(expectedHash, hash);
		}
	}
}
