namespace Sample.Shared.Governance.Models
{
	public class NeuronStakeTransfer
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("to_subaccount")]
		public System.Collections.Generic.List<byte> ToSubaccount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("neuron_stake_e8s")]
		public ulong NeuronStakeE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("from")]
		public EdjCase.ICP.Candid.Models.OptionalValue<EdjCase.ICP.Candid.Models.Principal> From { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("memo")]
		public ulong Memo { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("from_subaccount")]
		public System.Collections.Generic.List<byte> FromSubaccount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("transfer_timestamp")]
		public ulong TransferTimestamp { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("block_height")]
		public ulong BlockHeight { get; set; }
		
	}
}

