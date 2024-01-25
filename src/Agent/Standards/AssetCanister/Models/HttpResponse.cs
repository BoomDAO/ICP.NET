using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;
using HeaderField = System.ValueTuple<System.String, System.String>;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents an HTTP response from the asset canister.
	/// </summary>
	public class HttpResponse
	{
		/// <summary>
		/// HTTP status code of the response.
		/// </summary>
		[CandidName("status_code")]
		public ushort StatusCode { get; set; }

		/// <summary>
		/// List of headers included in the response.
		/// </summary>
		[CandidName("headers")]
		public List<HeaderField> Headers { get; set; }

		/// <summary>
		/// Body of the response as a list of bytes.
		/// </summary>
		[CandidName("body")]
		public List<byte> Body { get; set; }

		/// <summary>
		/// Optional streaming strategy for the response.
		/// </summary>
		[CandidName("streaming_strategy")]
		public OptionalValue<StreamingStrategy> StreamingStrategy { get; set; }

		/// <summary>
		/// Initializes a new instance of the HttpResponse class with specified parameters.
		/// </summary>
		/// <param name="statusCode">HTTP status code of the response.</param>
		/// <param name="headers">List of headers included in the response.</param>
		/// <param name="body">Body of the response as a list of bytes.</param>
		/// <param name="streamingStrategy">Optional streaming strategy for the response.</param>
		public HttpResponse(ushort statusCode, List<HeaderField> headers, List<byte> body, OptionalValue<StreamingStrategy> streamingStrategy)
		{
			this.StatusCode = statusCode;
			this.Headers = headers;
			this.Body = body;
			this.StreamingStrategy = streamingStrategy;
		}

		/// <summary>
		/// Initializes a new instance of the HttpResponse class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public HttpResponse()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
