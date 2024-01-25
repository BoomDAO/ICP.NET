using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

using System.Collections.Generic;
using Key = System.String;
using HeaderField = System.ValueTuple<System.String, System.String>;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to set properties of an asset in the asset canister.
	/// </summary>
	public class SetAssetPropertiesRequest
	{
		/// <summary>
		/// Unique identifier for the asset.
		/// </summary>
		[CandidName("key")]
		public Key Key { get; set; }

		/// <summary>
		/// Optional. The max age (in nanoseconds) for the asset
		/// </summary>
		[CandidName("max_age")]
		public OptionalValue<OptionalValue<ulong>> MaxAge { get; set; }

		/// <summary>
		/// Optional. The headers for the asset
		/// </summary>
		[CandidName("headers")]
		public OptionalValue<OptionalValue<List<(string, string)>>> Headers { get; set; }

		/// <summary>
		/// Optional. Whether to allow raw access to the asset
		/// </summary>
		[CandidName("allow_raw_access")]
		public OptionalValue<OptionalValue<bool>> AllowRawAccess { get; set; }

		/// <summary>
		/// Optional. Whether the asset is aliased
		/// </summary>
		[CandidName("is_aliased")]
		public OptionalValue<OptionalValue<bool>> IsAliased { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SetAssetPropertiesRequest"/> class.
		/// </summary>
		/// <param name="key">The key of the asset.</param>
		/// <param name="maxAge">The maximum age of the asset.</param>
		/// <param name="headers">The headers of the asset.</param>
		/// <param name="allowRawAccess">Indicates whether raw access is allowed for the asset.</param>
		/// <param name="isAliased">Indicates whether the asset is aliased.</param>
		public SetAssetPropertiesRequest(
			Key key,
			OptionalValue<OptionalValue<ulong>> maxAge,
			OptionalValue<OptionalValue<List<(string, string)>>> headers,
			OptionalValue<OptionalValue<bool>> allowRawAccess,
			OptionalValue<OptionalValue<bool>> isAliased
		)
		{
			this.Key = key;
			this.MaxAge = maxAge;
			this.Headers = headers;
			this.AllowRawAccess = allowRawAccess;
			this.IsAliased = isAliased;
		}
	}
}
