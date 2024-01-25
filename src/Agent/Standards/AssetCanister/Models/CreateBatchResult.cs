using EdjCase.ICP.Candid.Mapping;
using BatchId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class CreateBatchResult
	{
		[CandidName("batch_id")]
		public BatchId BatchId { get; set; }

		public CreateBatchResult(BatchId batchId)
		{
			this.BatchId = batchId;
		}

		public CreateBatchResult()
		{
		}
	}
}
