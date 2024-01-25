using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;


namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class StreamingCallbackHttpResponse
	{
		[CandidName("body")]
		public List<byte> Body { get; set; }

		[CandidName("token")]
		public OptionalValue<StreamingCallbackToken> Token { get; set; }

		public StreamingCallbackHttpResponse(List<byte> body, OptionalValue<StreamingCallbackToken> token)
		{
			this.Body = body;
			this.Token = token;
		}

		public StreamingCallbackHttpResponse()
		{
		}
	}
}