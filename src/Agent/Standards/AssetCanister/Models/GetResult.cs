using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Mapping;
using ChunkId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the result of a 'get' operation from the asset canister.
	/// </summary>
	public class GetResult
	{
		/// <summary>
		/// The content of the asset.
		/// </summary>
		[CandidName("content")]
		public byte[] Content { get; set; }

		/// <summary>
		/// The MIME type of the asset.
		/// </summary>
		[CandidName("content_type")]
		public string ContentType { get; set; }

		/// <summary>
		/// The encoding of the asset content.
		/// </summary>
		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		/// <summary>
		/// The SHA-256 hash of the asset, if available.
		/// </summary>
		[CandidName("sha256")]
		public OptionalValue<byte[]> Sha256 { get; set; }

		/// <summary>
		/// The total length of the asset in chunks.
		/// </summary>
		[CandidName("total_length")]
		public ChunkId TotalLength { get; set; }

		/// <summary>
		/// Initializes a new instance of the GetResult class with specified details.
		/// </summary>
		/// <param name="content">The content of the asset.</param>
		/// <param name="contentType">The MIME type of the asset.</param>
		/// <param name="contentEncoding">The encoding of the asset content.</param>
		/// <param name="sha256">The SHA-256 hash of the asset, if available.</param>
		/// <param name="totalLength">The total length of the asset in chunks.</param>
		public GetResult(byte[] content, string contentType, string contentEncoding, OptionalValue<byte[]> sha256, ChunkId totalLength)
		{
			this.Content = content;
			this.ContentType = contentType;
			this.ContentEncoding = contentEncoding;
			this.Sha256 = sha256;
			this.TotalLength = totalLength;
		}

		/// <summary>
		/// Initializes a new instance of the GetResult class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public GetResult()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
