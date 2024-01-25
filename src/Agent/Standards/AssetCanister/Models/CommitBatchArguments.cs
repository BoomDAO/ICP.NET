using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using BatchId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the arguments for committing a batch of operations in the asset canister.
	/// </summary>
	public class CommitBatchArguments
	{
		/// <summary>
		/// The unique identifier of the batch.
		/// </summary>
		[CandidName("batch_id")]
		public BatchId BatchId { get; set; }

		/// <summary>
		/// A list of operations to be committed in the batch.
		/// </summary>
		[CandidName("operations")]
		public List<BatchOperationKind> Operations { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CommitBatchArguments"/> class with specified batch ID and operations.
		/// </summary>
		/// <param name="batchId">The unique identifier of the batch.</param>
		/// <param name="operations">A list of operations to be committed in the batch.</param>
		public CommitBatchArguments(BatchId batchId, List<BatchOperationKind> operations)
		{
			this.BatchId = batchId;
			this.Operations = operations;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CommitBatchArguments"/> class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public CommitBatchArguments()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
