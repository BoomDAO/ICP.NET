using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;
using HeaderField = System.ValueTuple<System.String, System.String>;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class HttpRequest
	{
		[CandidName("method")]
		public string Method { get; set; }

		[CandidName("url")]
		public string Url { get; set; }

		[CandidName("headers")]
		public HttpRequest.HeadersInfo Headers { get; set; }

		[CandidName("body")]
		public List<byte> Body { get; set; }

		[CandidName("certificate_version")]
		public OptionalValue<ushort> CertificateVersion { get; set; }

		public HttpRequest(string method, string url, HttpRequest.HeadersInfo headers, List<byte> body, OptionalValue<ushort> certificateVersion)
		{
			this.Method = method;
			this.Url = url;
			this.Headers = headers;
			this.Body = body;
			this.CertificateVersion = certificateVersion;
		}

		public HttpRequest()
		{
		}

		public class HeadersInfo : List<HeaderField>
		{
			public HeadersInfo()
			{
			}
		}
	}
}
