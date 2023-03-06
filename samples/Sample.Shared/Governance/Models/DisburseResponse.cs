using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class DisburseResponse
	{
		[CandidName("transfer_block_height")]
		public ulong TransferBlockHeight { get; set; }

		public DisburseResponse(ulong transferBlockHeight)
		{
			this.TransferBlockHeight = transferBlockHeight;
		}

		public DisburseResponse()
		{
		}
	}
}