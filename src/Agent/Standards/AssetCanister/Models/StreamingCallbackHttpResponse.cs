using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;


namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a response from the streaming callback method
	/// </summary>
	public class StreamingCallbackHttpResponse
	{
		/// <summary>
		/// The returned bytes for the streaming callback
		/// </summary>
		[CandidName("body")]
		public byte[] Body { get; set; }

		/// <summary>
		/// Optional. The token to use for the next streaming callback
		/// </summary>
		[CandidName("token")]
		public OptionalValue<StreamingCallbackToken> Token { get; set; }

		/// <summary>
		/// Represents an HTTP response for a streaming callback.
		/// </summary>
		/// <param name="body">The body of the response.</param>
		/// <param name="token">The optional token associated with the response.</param>
		public StreamingCallbackHttpResponse(byte[] body, OptionalValue<StreamingCallbackToken> token)
		{
			this.Body = body;
			this.Token = token;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="StreamingCallbackHttpResponse"/> class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		public StreamingCallbackHttpResponse()
		{
		}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}
}
