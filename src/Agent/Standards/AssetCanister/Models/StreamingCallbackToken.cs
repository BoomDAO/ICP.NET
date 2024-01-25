using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Key = System.String;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class StreamingCallbackToken
	{
		[CandidName("key")]
		public Key Key { get; set; }

		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		[CandidName("index")]
		public UnboundedUInt Index { get; set; }

		[CandidName("sha256")]
		public OptionalValue<List<byte>> Sha256 { get; set; }

		public StreamingCallbackToken(Key key, string contentEncoding, UnboundedUInt index, OptionalValue<List<byte>> sha256)
		{
			this.Key = key;
			this.ContentEncoding = contentEncoding;
			this.Index = index;
			this.Sha256 = sha256;
		}

		public StreamingCallbackToken()
		{
		}
	}
}