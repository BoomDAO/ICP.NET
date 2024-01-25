using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using BatchId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to commit a proposed batch of operations in the asset canister.
	/// </summary>
	public class CommitProposedBatchRequest
	{
		/// <summary>
		/// The unique identifier of the proposed batch.
		/// </summary>
		[CandidName("batch_id")]
		public BatchId BatchId { get; set; }

		/// <summary>
		/// The evidence required to commit the proposed batch.
		/// </summary>
		[CandidName("evidence")]
		public byte[] Evidence { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CommitProposedBatchRequest"/> class with specified batch ID and evidence.
		/// </summary>
		/// <param name="batchId">The unique identifier of the proposed batch.</param>
		/// <param name="evidence">The evidence required to commit the proposed batch.</param>
		public CommitProposedBatchRequest(BatchId batchId, byte[] evidence)
		{
			this.BatchId = batchId;
			this.Evidence = evidence;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CommitProposedBatchRequest"/> class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public CommitProposedBatchRequest()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
