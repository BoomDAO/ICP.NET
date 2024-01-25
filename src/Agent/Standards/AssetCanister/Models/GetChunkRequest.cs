using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Mapping;
using ChunkId = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Key = System.String;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class GetChunkRequest
	{
		[CandidName("key")]
		public Key Key { get; set; }

		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		[CandidName("index")]
		public ChunkId Index { get; set; }

		[CandidName("sha256")]
		public OptionalValue<byte[]> Sha256 { get; set; }

		public GetChunkRequest(Key key, string contentEncoding, ChunkId index, OptionalValue<byte[]> sha256)
		{
			this.Key = key;
			this.ContentEncoding = contentEncoding;
			this.Index = index;
			this.Sha256 = sha256;
		}

		public GetChunkRequest()
		{
		}
	}
}
