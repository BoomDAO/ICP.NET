using EdjCase.ICP.Candid.Mapping;
using ChunkId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the result of a 'create_chunk' operation on an asset canister.
	/// </summary>
	public class CreateChunkResult
	{
		/// <summary>
		/// Unique identifier for the created chunk.
		/// </summary>
		[CandidName("chunk_id")]
		public ChunkId ChunkId { get; set; }

		/// <summary>
		/// Initializes a new instance of the CreateChunkResult class with a specified chunk ID.
		/// </summary>
		/// <param name="chunkId">Unique identifier for the chunk.</param>
		public CreateChunkResult(ChunkId chunkId)
		{
			this.ChunkId = chunkId;
		}

		/// <summary>
		/// Initializes a new instance of the CreateChunkResult class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public CreateChunkResult()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
