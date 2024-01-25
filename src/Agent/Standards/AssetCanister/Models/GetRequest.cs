using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request for retrieving an asset from the asset canister.
	/// </summary>
	internal class GetRequest
	{
		/// <summary>
		/// The key identifying the asset to retrieve.
		/// </summary>
		[CandidName("key")]
		public string Key { get; set; }

		/// <summary>
		/// List of acceptable content encodings for the asset.
		/// </summary>
		[CandidName("accept_encodings")]
		public List<string> AcceptEncodings { get; set; }

		/// <summary>
		/// Initializes a new instance of the GetRequest class with specified key and acceptable encodings.
		/// </summary>
		/// <param name="key">The asset key.</param>
		/// <param name="acceptEncodings">List of acceptable content encodings.</param>
		public GetRequest(string key, List<string> acceptEncodings)
		{
			this.Key = key;
			this.AcceptEncodings = acceptEncodings;
		}

		/// <summary>
		/// Initializes a new instance of the GetRequest class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public GetRequest()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
