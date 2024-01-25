using EdjCase.ICP.Candid.Mapping;
using BatchId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the arguments for deleting a batch in the asset canister.
	/// </summary>
	public class DeleteBatchArguments
	{
		/// <summary>
		/// Unique identifier for the batch to be deleted.
		/// </summary>
		[CandidName("batch_id")]
		public BatchId BatchId { get; set; }

		/// <summary>
		/// Initializes a new instance of DeleteBatchArguments with a specified batch ID.
		/// </summary>
		/// <param name="batchId">Unique identifier for the batch.</param>
		public DeleteBatchArguments(BatchId batchId)
		{
			this.BatchId = batchId;
		}

		/// <summary>
		/// Initializes a new instance of DeleteBatchArguments.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public DeleteBatchArguments()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
