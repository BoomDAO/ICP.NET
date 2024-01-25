using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class GetChunkResult
	{
		[CandidName("content")]
		public byte[] Content { get; set; }

		public GetChunkResult(byte[] content)
		{
			this.Content = content;
		}

		public GetChunkResult()
		{
		}
	}
}
