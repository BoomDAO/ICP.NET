using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the result of a 'get_chunk' method call to an asset canister.
	/// </summary>
	public class GetChunkResult
	{
		/// <summary>
		/// Content of the asset chunk.
		/// </summary>
		[CandidName("content")]
		public byte[] Content { get; set; }

		/// <summary>
		/// Initializes a new instance of the GetChunkResult class with specified content.
		/// </summary>
		/// <param name="content">Byte array representing the content of the chunk.</param>
		public GetChunkResult(byte[] content)
		{
			this.Content = content;
		}

		/// <summary>
		/// Initializes a new instance of the GetChunkResult class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public GetChunkResult()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
