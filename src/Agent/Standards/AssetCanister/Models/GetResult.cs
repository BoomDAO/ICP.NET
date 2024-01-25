using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Mapping;
using ChunkId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class GetResult
	{
		[CandidName("content")]
		public byte[] Content { get; set; }

		[CandidName("content_type")]
		public string ContentType { get; set; }

		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		[CandidName("sha256")]
		public OptionalValue<byte[]> Sha256 { get; set; }

		[CandidName("total_length")]
		public ChunkId TotalLength { get; set; }

		public GetResult(byte[] content, string contentType, string contentEncoding, OptionalValue<byte[]> sha256, ChunkId totalLength)
		{
			this.Content = content;
			this.ContentType = contentType;
			this.ContentEncoding = contentEncoding;
			this.Sha256 = sha256;
			this.TotalLength = totalLength;
		}

		public GetResult()
		{
		}
	}
}
