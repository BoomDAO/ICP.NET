namespace Sample.Shared.Governance.Models
{
	public class DisburseResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("transfer_block_height")]
		public ulong TransferBlockHeight { get; set; }
		
	}
}

