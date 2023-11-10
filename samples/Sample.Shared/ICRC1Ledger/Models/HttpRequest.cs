using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class HttpRequest
	{
		[CandidName("url")]
		public string Url { get; set; }

		[CandidName("method")]
		public string Method { get; set; }

		[CandidName("body")]
		public byte[] Body { get; set; }

		[CandidName("headers")]
		public Dictionary<string, string> Headers { get; set; }

		public HttpRequest(string url, string method, byte[] body, Dictionary<string, string> headers)
		{
			this.Url = url;
			this.Method = method;
			this.Body = body;
			this.Headers = headers;
		}

		public HttpRequest()
		{
		}
	}
}