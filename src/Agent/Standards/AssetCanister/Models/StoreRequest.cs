using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Mapping;
using Key = System.String;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to store a small asset in the asset canister.
	/// </summary>
	public class StoreRequest
	{
		/// <summary>
		/// Unique identifier for the asset.
		/// </summary>
		[CandidName("key")]
		public Key Key { get; set; }

		/// <summary>
		/// The content type of the asset (e.g text/plain)
		/// </summary>
		[CandidName("content_type")]
		public string ContentType { get; set; }

		/// <summary>
		/// The content encoding of the asset (e.g UTF-8)
		/// </summary>
		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		/// <summary>
		/// The content bytes of the asset
		/// </summary>
		[CandidName("content")]
		public byte[] Content { get; set; }

		/// <summary>
		/// Optional. The SHA256 hash of the entire content bytes, for validation of the bytes
		/// </summary>
		[CandidName("sha256")]
		public OptionalValue<byte[]> Sha256 { get; set; }

		/// <summary>
		/// Represents a request to store an asset in the canister.
		/// </summary>
		/// <param name="key">The key of the asset.</param>
		/// <param name="contentType">The content type of the asset.</param>
		/// <param name="contentEncoding">The content encoding of the asset.</param>
		/// <param name="content">The content of the asset.</param>
		/// <param name="sha256">The SHA256 hash of the asset content.</param>
		public StoreRequest(Key key, string contentType, string contentEncoding, byte[] content, OptionalValue<byte[]> sha256)
		{
			this.Key = key;
			this.ContentType = contentType;
			this.ContentEncoding = contentEncoding;
			this.Content = content;
			this.Sha256 = sha256;
		}
	}
}
