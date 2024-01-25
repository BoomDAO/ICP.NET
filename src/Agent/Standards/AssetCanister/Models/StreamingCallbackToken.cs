using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Key = System.String;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a single result of streaming an asset from the asset canister
	/// </summary>
	public class StreamingCallbackToken
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
		/// The index of the chunk to stream
		/// </summary>
		[CandidName("index")]
		public UnboundedUInt Index { get; set; }

		/// <summary>
		/// Optional. The SHA256 hash of the entire content bytes, for validation of the bytes
		/// </summary>
		[CandidName("sha256")]
		public OptionalValue<byte[]> Sha256 { get; set; }


		/// <summary>
		/// Initializes a new instance of the <see cref="StreamingCallbackToken"/> class.
		/// </summary>
		/// <param name="key">The key associated with the token.</param>
		/// <param name="contentEncoding">The content encoding of the token.</param>
		/// <param name="index">The index of the token.</param>
		/// <param name="sha256">The SHA256 value of the token.</param>
		public StreamingCallbackToken(Key key, string contentEncoding, UnboundedUInt index, OptionalValue<byte[]> sha256)
		{
			this.Key = key;
			this.ContentEncoding = contentEncoding;
			this.Index = index;
			this.Sha256 = sha256;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StreamingCallbackToken"/> class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public StreamingCallbackToken()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
