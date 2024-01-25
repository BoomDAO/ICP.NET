using EdjCase.ICP.Candid.Mapping;
using BatchId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to create a chunk in an asset canister batch.
	/// </summary>
	public class CreateChunkRequest
	{
		/// <summary>
		/// Unique identifier for the batch to which the chunk belongs.
		/// </summary>
		[CandidName("batch_id")]
		public BatchId BatchId { get; set; }

		/// <summary>
		/// Content of the chunk as a byte array.
		/// </summary>
		[CandidName("content")]
		public byte[] Content { get; set; }

		/// <summary>
		/// Initializes a new instance of the CreateChunkRequest class with specified batch ID and content.
		/// </summary>
		/// <param name="batchId">Unique identifier for the batch.</param>
		/// <param name="content">Content of the chunk as a byte array.</param>
		public CreateChunkRequest(BatchId batchId, byte[] content)
		{
			this.BatchId = batchId;
			this.Content = content;
		}

		/// <summary>
		/// Initializes a new instance of the CreateChunkRequest class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public CreateChunkRequest()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
