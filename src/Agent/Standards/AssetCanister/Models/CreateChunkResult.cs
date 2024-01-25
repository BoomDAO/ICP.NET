using EdjCase.ICP.Candid.Mapping;
using ChunkId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
		public class CreateChunkResult
		{
			[CandidName("chunk_id")]
			public ChunkId ChunkId { get; set; }

			public CreateChunkResult(ChunkId chunkId)
			{
				this.ChunkId = chunkId;
			}

			public CreateChunkResult()
			{
			}
		}
}
