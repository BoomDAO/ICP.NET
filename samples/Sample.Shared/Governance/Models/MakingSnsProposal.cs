using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class MakingSnsProposal
	{
		[CandidName("proposal")]
		public OptionalValue<Proposal> Proposal { get; set; }

		[CandidName("caller")]
		public OptionalValue<Principal> Caller { get; set; }

		[CandidName("proposer_id")]
		public OptionalValue<NeuronId> ProposerId { get; set; }

		public MakingSnsProposal(OptionalValue<Proposal> proposal, OptionalValue<Principal> caller, OptionalValue<NeuronId> proposerId)
		{
			this.Proposal = proposal;
			this.Caller = caller;
			this.ProposerId = proposerId;
		}

		public MakingSnsProposal()
		{
		}
	}
}