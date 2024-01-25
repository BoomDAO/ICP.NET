using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

using System.Collections.Generic;
using Key = System.String;
using HeaderField = System.ValueTuple<System.String, System.String>;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class SetAssetPropertiesRequest
	{
		[CandidName("key")]
		public Key Key { get; set; }

		[CandidName("max_age")]
		public OptionalValue<OptionalValue<ulong>> MaxAge { get; set; }

		[CandidName("headers")]
		public OptionalValue<OptionalValue<List<(string, string)>>> Headers { get; set; }

		[CandidName("allow_raw_access")]
		public OptionalValue<OptionalValue<bool>> AllowRawAccess { get; set; }

		[CandidName("is_aliased")]
		public OptionalValue<OptionalValue<bool>> IsAliased { get; set; }

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

		public SetAssetPropertiesRequest()
		{
		}
	}
}
