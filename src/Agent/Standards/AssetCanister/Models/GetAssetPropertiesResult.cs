using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using HeaderField = System.ValueTuple<System.String, System.String>;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class GetAssetPropertiesResult
	{
		[CandidName("max_age")]
		public OptionalValue<ulong> MaxAge { get; set; }

		[CandidName("headers")]
		public HeadersInfo Headers { get; set; }

		[CandidName("allow_raw_access")]
		public OptionalValue<bool> AllowRawAccess { get; set; }

		[CandidName("is_aliased")]
		public OptionalValue<bool> IsAliased { get; set; }

		public GetAssetPropertiesResult(OptionalValue<ulong> maxAge, HeadersInfo headers, OptionalValue<bool> allowRawAccess, OptionalValue<bool> isAliased)
		{
			this.MaxAge = maxAge;
			this.Headers = headers;
			this.AllowRawAccess = allowRawAccess;
			this.IsAliased = isAliased;
		}

		public GetAssetPropertiesResult()
		{
		}

		public class HeadersInfo : OptionalValue<HeadersInfo.HeadersInfoValue>
		{
			public HeadersInfo()
			{
			}

			public HeadersInfo(HeadersInfoValue value) : base(value)
			{
			}

			public class HeadersInfoValue : List<HeaderField>
			{
				public HeadersInfoValue()
				{
				}
			}
		}
	}
}
