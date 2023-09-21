using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class MakeProposalResponse
	{
		[CandidName("proposal_id")]
		public OptionalValue<NeuronId> ProposalId { get; set; }

		public MakeProposalResponse(OptionalValue<NeuronId> proposalId)
		{
			this.ProposalId = proposalId;
		}

		public MakeProposalResponse()
		{
		}
	}
}