using EdjCase.ICP.Candid.Mapping;

using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to set asset content with chunks in the asset canister.
	/// </summary>
	public class SetAssetContentArguments
	{
		/// <summary>
		/// Unique identifier for the asset.
		/// </summary>
		[CandidName("key")]
		public string Key { get; set; }

		/// <summary>
		/// The content encoding of the asset (e.g UTF-8)
		/// </summary>
		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		/// <summary>
		/// A list of chunk ids that have been uploaded, to use as the content bytes
		/// </summary>
		[CandidName("chunk_ids")]
		public List<UnboundedUInt> ChunkIds { get; set; }

		/// <summary>
		/// Optional. The SHA256 hash of the entire content bytes, for validation of the chunk bytes
		/// </summary>
		[CandidName("sha256")]
		public OptionalValue<byte[]> Sha256 { get; set; }

		/// <param name="key">Unique identifier for the asset.</param>
		/// <param name="contentEncoding">The content encoding of the asset (e.g UTF-8)</param>
		/// <param name="chunkIds">A list of chunk ids that have been uploaded, to use as the content bytes</param>
		/// <param name="sha256">Optional. The SHA256 hash of the entire content bytes, for validation of the chunk bytes</param>
		public SetAssetContentArguments(string key, string contentEncoding, List<UnboundedUInt> chunkIds, OptionalValue<byte[]> sha256)
		{
			this.Key = key;
			this.ContentEncoding = contentEncoding;
			this.ChunkIds = chunkIds;
			this.Sha256 = sha256;
		}
	}
}
