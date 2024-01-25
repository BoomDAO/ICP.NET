using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Mapping;
using ChunkId = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Key = System.String;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to retrieve a specific chunk of an asset's content encoding from an asset canister.
	/// </summary>
	public class GetChunkRequest
	{
		/// <summary>
		/// Unique identifier for the asset.
		/// </summary>
		[CandidName("key")]
		public Key Key { get; set; }

		/// <summary>
		/// Content encoding type of the asset (e.g., 'gzip', 'identity').
		/// </summary>
		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		/// <summary>
		/// Index of the chunk within the asset's content encoding.
		/// </summary>
		[CandidName("index")]
		public ChunkId Index { get; set; }

		/// <summary>
		/// Optional SHA-256 hash of the entire asset encoding for validation.
		/// </summary>
		[CandidName("sha256")]
		public OptionalValue<byte[]> Sha256 { get; set; }

		/// <summary>
		/// Initializes a new instance of the GetChunkRequest class with specified parameters.
		/// </summary>
		/// <param name="key">Asset key.</param>
		/// <param name="contentEncoding">Content encoding type.</param>
		/// <param name="index">Chunk index.</param>
		/// <param name="sha256">Optional SHA-256 hash for validation.</param>
		public GetChunkRequest(Key key, string contentEncoding, ChunkId index, OptionalValue<byte[]> sha256)
		{
			this.Key = key;
			this.ContentEncoding = contentEncoding;
			this.Index = index;
			this.Sha256 = sha256;
		}

		/// <summary>
		/// Initializes a new instance of the GetChunkRequest class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public GetChunkRequest()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
