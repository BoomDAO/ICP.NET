namespace Sample.Shared.Governance.Models
{
	public class NeuronStakeTransfer
	{
		public List<byte> ToSubaccount { get; set; }
		
		public ulong NeuronStakeE8s { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal? From { get; set; }
		
		public ulong Memo { get; set; }
		
		public List<byte> FromSubaccount { get; set; }
		
		public ulong TransferTimestamp { get; set; }
		
		public ulong BlockHeight { get; set; }
		
	}
}
