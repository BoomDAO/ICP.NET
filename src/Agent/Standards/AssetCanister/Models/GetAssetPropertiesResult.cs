using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using HeaderField = System.ValueTuple<System.String, System.String>;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the result of a request to get asset properties from an asset canister.
	/// </summary>
	public class GetAssetPropertiesResult
	{
		/// <summary>
		/// Maximum age of the asset for caching purposes.
		/// </summary>
		[CandidName("max_age")]
		public OptionalValue<ulong> MaxAge { get; set; }

		/// <summary>
		/// List of additional HTTP headers to be set when serving the asset.
		/// </summary>
		[CandidName("headers")]
		public OptionalValue<List<HeaderField>> Headers { get; set; }

		/// <summary>
		/// Indicates whether the asset can be retrieved from raw.ic0.app or raw.icp0.io.
		/// </summary>
		[CandidName("allow_raw_access")]
		public OptionalValue<bool> AllowRawAccess { get; set; }

		/// <summary>
		/// Indicates if the asset's key might be an alias for another asset.
		/// </summary>
		[CandidName("is_aliased")]
		public OptionalValue<bool> IsAliased { get; set; }

		/// <summary>
		/// Constructor for initializing GetAssetPropertiesResult with specific values.
		/// </summary>
		/// <param name="maxAge">Maximum age of the asset for caching.</param>
		/// <param name="headers">List of additional HTTP headers for the asset.</param>
		/// <param name="allowRawAccess">Flag for raw access availability of the asset.</param>
		/// <param name="isAliased">Flag indicating if the asset key is an alias.</param>
		public GetAssetPropertiesResult(OptionalValue<ulong> maxAge, OptionalValue<List<HeaderField>> headers, OptionalValue<bool> allowRawAccess, OptionalValue<bool> isAliased)
		{
			this.MaxAge = maxAge;
			this.Headers = headers;
			this.AllowRawAccess = allowRawAccess;
			this.IsAliased = isAliased;
		}

		/// <summary>
		/// Default constructor for GetAssetPropertiesResult.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public GetAssetPropertiesResult()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
