using EdjCase.ICP.Candid.Mapping;
using BatchId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class DeleteBatchArguments
	{
		[CandidName("batch_id")]
		public BatchId BatchId { get; set; }

		public DeleteBatchArguments(BatchId batchId)
		{
			this.BatchId = batchId;
		}

		public DeleteBatchArguments()
		{
		}
	}
}