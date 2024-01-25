using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;
using HeaderField = System.ValueTuple<System.String, System.String>;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents an HTTP request for the Asset Canister.
	/// </summary>
	public class HttpRequest
	{
		/// <summary>
		/// HTTP method (e.g., GET, POST).
		/// </summary>
		[CandidName("method")]
		public string Method { get; set; }

		/// <summary>
		/// URL of the request.
		/// </summary>
		[CandidName("url")]
		public string Url { get; set; }

		/// <summary>
		/// List of HTTP headers.
		/// </summary>
		[CandidName("headers")]
		public List<HeaderField> Headers { get; set; }

		/// <summary>
		/// Body of the request as a list of bytes.
		/// </summary>
		[CandidName("body")]
		public List<byte> Body { get; set; }

		/// <summary>
		/// Optional certificate version.
		/// </summary>
		[CandidName("certificate_version")]
		public OptionalValue<ushort> CertificateVersion { get; set; }

		/// <summary>
		/// Constructs an HttpRequest with specified parameters.
		/// </summary>
		/// <param name="method">HTTP method.</param>
		/// <param name="url">URL of the request.</param>
		/// <param name="headers">List of HTTP headers.</param>
		/// <param name="body">Body of the request as a list of bytes.</param>
		/// <param name="certificateVersion">Optional certificate version.</param>
		public HttpRequest(string method, string url, List<HeaderField> headers, List<byte> body, OptionalValue<ushort> certificateVersion)
		{
			this.Method = method;
			this.Url = url;
			this.Headers = headers;
			this.Body = body;
			this.CertificateVersion = certificateVersion;
		}

		/// <summary>
		/// Default constructor for HttpRequest.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public HttpRequest()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
