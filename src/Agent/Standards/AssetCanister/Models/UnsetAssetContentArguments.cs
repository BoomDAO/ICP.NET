using EdjCase.ICP.Candid.Mapping;
using Key = System.String;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to remove the content encoding for an asset
	/// </summary>
	public class UnsetAssetContentArguments
	{
		/// <summary>
		/// Unique identifier for the asset.
		/// </summary>
		[CandidName("key")]
		public Key Key { get; set; }

		/// <summary>
		/// The content encoding of the asset (e.g UTF-8)
		/// </summary>
		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnsetAssetContentArguments"/> class.
		/// </summary>
		/// <param name="key">The key of the asset.</param>
		/// <param name="contentEncoding">The encoding of the asset content.</param>
		public UnsetAssetContentArguments(Key key, string contentEncoding)
		{
			this.Key = key;
			this.ContentEncoding = contentEncoding;
		}

	}
}
