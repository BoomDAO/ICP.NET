namespace Sample.Shared.Governance.Models
{
	public class RegisterVote
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("vote")]
		public int Vote { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("proposal")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> Proposal { get; set; }
		
	}
}

