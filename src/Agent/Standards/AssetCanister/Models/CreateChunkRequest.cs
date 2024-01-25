using EdjCase.ICP.Candid.Mapping;
using BatchId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class CreateChunkRequest
	{
		[CandidName("batch_id")]
		public BatchId BatchId { get; set; }

		[CandidName("content")]
		public byte[] Content { get; set; }

		public CreateChunkRequest(BatchId batchId, byte[] content)
		{
			this.BatchId = batchId;
			this.Content = content;
		}

		public CreateChunkRequest()
		{
		}
	}
}
