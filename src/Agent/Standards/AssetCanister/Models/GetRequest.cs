using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	internal class GetRequest
	{
		[CandidName("key")]
		public string Key { get; set; }

		[CandidName("accept_encodings")]
		public List<string> AcceptEncodings { get; set; }

		public GetRequest(string key, List<string> acceptEncodings)
		{
			this.Key = key;
			this.AcceptEncodings = acceptEncodings;
		}

		public GetRequest()
		{
		}
	}
}
