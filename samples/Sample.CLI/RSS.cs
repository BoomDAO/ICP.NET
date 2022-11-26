using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System.Xml.Serialization;
using EdjCase.ICP.Agent.Requests;
using EdjCase.ICP.Candid;
using System.Net.Http.Headers;
using Path = EdjCase.ICP.Candid.Models.Path;

namespace RSSOracle
{
	internal class RSS
	{
		public async Task Get()
		{
			//var arg = CandidArg.FromBytes(new byte[] { 68, 73, 68, 76, 0, 1, 121, 54, 215, 245, 143 });
			
			
			//var callRequest = new CallRequest(canisterId, method, arg, sender, ingressExpiry);
			//byte[] encoded = callRequest.BuildHashableItem();


			HttpClient client = new HttpClient();

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(RSSOracle.feed));
			Stream stream = await client.GetStreamAsync("https://www.theverge.com/rss/index.xml");
			RSSOracle.feed feed = (RSSOracle.feed)xmlSerializer.Deserialize(stream)!;

			var identity = new AnonymousIdentity();
			EdjCase.ICP.Agent.Agents.HttpAgent httpAgent = new(identity, new Uri("http://localhost:4943"));
			var canisterId = Principal.FromText("qaa6y-5yaaa-aaaaa-aaafa-cai");

			var channelId = CandidValueWithType.FromValueAndType(
				CandidPrimitive.Text("https://www.theverge.com/rss/index.xml"),
				new CandidPrimitiveType(PrimitiveType.Text)
			);
			foreach (var entry in feed.entry)
			{

				var encodedArgument = CandidArg.FromCandid(
					channelId,
					CandidValueWithType.FromValueAndType(
						new CandidRecord(new Dictionary<CandidTag, CandidValue>
						{
						{
							CandidTag.FromName("title"),
							CandidPrimitive.Text(entry.title)
						},
						{
							CandidTag.FromName("body"),
							new CandidRecord(new Dictionary<CandidTag, CandidValue>
							{
								{
									CandidTag.FromName("format"),
									new CandidOptional(CandidPrimitive.Text(entry.content.type))
								},
								{
									CandidTag.FromName("value"),
									CandidPrimitive.Text(entry.content.Value)
								}
							})
						},
						{
							CandidTag.FromName("link"),
							CandidPrimitive.Text(entry.link.href)
						},
						{
							CandidTag.FromName("authors"),
							new CandidVector(entry.author.Select(a => new CandidVariant("name", CandidPrimitive.Text(a))).ToArray())
						},
						{
							CandidTag.FromName("imageLink"),
							new CandidOptional(null)
						},
						{
							CandidTag.FromName("language"),
							new CandidOptional(CandidPrimitive.Text(feed.lang))
						},
						{
							CandidTag.FromName("date"),
							CandidPrimitive.Nat(0)
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
							new CandidPrimitiveType(PrimitiveType.Nat)
						}
						})
					)
				);
				RequestId id = await httpAgent.CallAsync(canisterId, "push", encodedArgument);

				var pathRequestStatus = Path.FromSegments(PathSegment.FromString("request_status"), id.RawValue);
				var pathSubnet = Path.FromMultiString("subnet");
				var paths = new List<Path>
				{
					pathRequestStatus,
					//pathSubnet
				};
				while (true)
				{
					EdjCase.ICP.Agent.Responses.ReadStateResponse response = await httpAgent.ReadStateAsync(canisterId, paths);
					HashTree? requestStatus = response.Certificate.Tree.GetValue(pathRequestStatus);
					HashTree? subnet = response.Certificate.Tree.GetValue(pathSubnet);
				}
			}

		}
	}
}
