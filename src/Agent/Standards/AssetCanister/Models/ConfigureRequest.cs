using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to configure the asset canister with limits on batches, chunks, and bytes.
	/// </summary>
	public class ConfigureRequest
	{
		/// <summary>
		/// Optional maximum number of batches being uploaded at one time.
		/// </summary>
		[CandidName("max_batches")]
		public OptionalValue<OptionalValue<ulong>> MaxBatches { get; set; }

		/// <summary>
		/// Optional maximum number of chunks across all batches being uploaded.
		/// </summary>
		[CandidName("max_chunks")]
		public OptionalValue<OptionalValue<ulong>> MaxChunks { get; set; }

		/// <summary>
		/// Optional maximum total size of content bytes across all chunks being uploaded.
		/// </summary>
		[CandidName("max_bytes")]
		public OptionalValue<OptionalValue<ulong>> MaxBytes { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigureRequest"/> class with specified limits.
		/// </summary>
		/// <param name="maxBatches">Optional maximum number of batches.</param>
		/// <param name="maxChunks">Optional maximum number of chunks.</param>
		/// <param name="maxBytes">Optional maximum size of bytes.</param>
		public ConfigureRequest(OptionalValue<OptionalValue<ulong>> maxBatches, OptionalValue<OptionalValue<ulong>> maxChunks, OptionalValue<OptionalValue<ulong>> maxBytes)
		{
			this.MaxBatches = maxBatches;
			this.MaxChunks = maxChunks;
			this.MaxBytes = maxBytes;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigureRequest"/> class without specified limits.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ConfigureRequest()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
