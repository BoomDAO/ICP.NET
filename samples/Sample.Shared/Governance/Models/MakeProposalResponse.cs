namespace Sample.Shared.Governance.Models
{
	public class MakeProposalResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("proposal_id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> ProposalId { get; set; }
		
	}
}

