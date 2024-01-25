using EdjCase.ICP.Candid.Mapping;

using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class SetAssetContentArguments
	{
		[CandidName("key")]
		public string Key { get; set; }

		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		[CandidName("chunk_ids")]
		public List<UnboundedUInt> ChunkIds { get; set; }

		[CandidName("sha256")]
		public OptionalValue<byte[]> Sha256 { get; set; }

		public SetAssetContentArguments(string key, string contentEncoding, List<UnboundedUInt> chunkIds, OptionalValue<byte[]> sha256)
		{
			this.Key = key;
			this.ContentEncoding = contentEncoding;
			this.ChunkIds = chunkIds;
			this.Sha256 = sha256;
		}

		public SetAssetContentArguments()
		{
		}
	}
}
