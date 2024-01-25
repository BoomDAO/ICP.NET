using EdjCase.ICP.Candid.Mapping;
using BatchId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the result of creating a new batch in the asset canister.
	/// </summary>
	public class CreateBatchResult
	{
		/// <summary>
		/// Unique identifier for the created batch.
		/// </summary>
		[CandidName("batch_id")]
		public BatchId BatchId { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateBatchResult"/> class with a specified batch ID.
		/// </summary>
		/// <param name="batchId">Unique identifier for the batch.</param>
		public CreateBatchResult(BatchId batchId)
		{
			this.BatchId = batchId;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateBatchResult"/> class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public CreateBatchResult()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
