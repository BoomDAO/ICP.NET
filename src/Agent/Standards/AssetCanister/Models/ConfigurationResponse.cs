using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the response for the configuration of an asset canister.
	/// </summary>
	public class ConfigurationResponse
	{
		/// <summary>
		/// Maximum number of batches allowed for upload at one time.
		/// </summary>
		[CandidName("max_batches")]
		public OptionalValue<ulong> MaxBatches { get; set; }

		/// <summary>
		/// Maximum number of chunks across all batches being uploaded.
		/// </summary>
		[CandidName("max_chunks")]
		public OptionalValue<ulong> MaxChunks { get; set; }

		/// <summary>
		/// Maximum total size of content bytes across all chunks being uploaded.
		/// </summary>
		[CandidName("max_bytes")]
		public OptionalValue<ulong> MaxBytes { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationResponse"/> class with specified configuration values.
		/// </summary>
		/// <param name="maxBatches">Maximum number of batches allowed for upload at one time.</param>
		/// <param name="maxChunks">Maximum number of chunks across all batches being uploaded.</param>
		/// <param name="maxBytes">Maximum total size of content bytes across all chunks being uploaded.</param>
		public ConfigurationResponse(OptionalValue<ulong> maxBatches, OptionalValue<ulong> maxChunks, OptionalValue<ulong> maxBytes)
		{
			this.MaxBatches = maxBatches;
			this.MaxChunks = maxChunks;
			this.MaxBytes = maxBytes;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationResponse"/> class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ConfigurationResponse()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
