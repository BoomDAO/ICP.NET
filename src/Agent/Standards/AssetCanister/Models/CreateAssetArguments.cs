using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Key = System.String;
using HeaderField = System.ValueTuple<System.String, System.String>;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the arguments for creating a new asset in the asset canister.
	/// </summary>
	public class CreateAssetArguments
	{
		/// <summary>
		/// Unique key identifier for the asset.
		/// </summary>
		[CandidName("key")]
		public Key Key { get; set; }

		/// <summary>
		/// Content type of the asset (e.g., 'text/plain', 'image/jpeg').
		/// </summary>
		[CandidName("content_type")]
		public string ContentType { get; set; }

		/// <summary>
		/// Optional maximum age for caching the asset.
		/// </summary>
		[CandidName("max_age")]
		public OptionalValue<ulong> MaxAge { get; set; }

		/// <summary>
		/// Optional list of additional HTTP headers for the asset.
		/// </summary>
		[CandidName("headers")]
		public OptionalValue<List<HeaderField>> Headers { get; set; }

		/// <summary>
		/// Optional flag to enable aliasing for the asset.
		/// </summary>
		[CandidName("enable_aliasing")]
		public OptionalValue<bool> EnableAliasing { get; set; }

		/// <summary>
		/// Optional flag to allow raw access to the asset.
		/// </summary>
		[CandidName("allow_raw_access")]
		public OptionalValue<bool> AllowRawAccess { get; set; }

		/// <summary>
		/// Constructor for creating asset arguments with specified properties.
		/// </summary>
		/// <param name="key">Asset key.</param>
		/// <param name="contentType">Content type of the asset.</param>
		/// <param name="maxAge">Maximum age for caching.</param>
		/// <param name="headers">Additional HTTP headers.</param>
		/// <param name="enableAliasing">Flag for enabling aliasing.</param>
		/// <param name="allowRawAccess">Flag for allowing raw access.</param>
		public CreateAssetArguments(Key key, string contentType, OptionalValue<ulong> maxAge, OptionalValue<List<HeaderField>> headers, OptionalValue<bool> enableAliasing, OptionalValue<bool> allowRawAccess)
		{
			this.Key = key;
			this.ContentType = contentType;
			this.MaxAge = maxAge;
			this.Headers = headers;
			this.EnableAliasing = enableAliasing;
			this.AllowRawAccess = allowRawAccess;
		}
	}
}
