namespace Sample.Shared.Governance.Models
{
	public class ListProposalInfo
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("include_reward_status")]
		public System.Collections.Generic.List<int> IncludeRewardStatus { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("before_proposal")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> BeforeProposal { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("limit")]
		public uint Limit { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("exclude_topic")]
		public System.Collections.Generic.List<int> ExcludeTopic { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("include_status")]
		public System.Collections.Generic.List<int> IncludeStatus { get; set; }
		
	}
}

