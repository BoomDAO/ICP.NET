using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class BallotInfo
	{
		[CandidName("vote")]
		public int Vote { get; set; }

		[CandidName("proposal_id")]
		public OptionalValue<NeuronId> ProposalId { get; set; }

		public BallotInfo(int vote, OptionalValue<NeuronId> proposalId)
		{
			this.Vote = vote;
			this.ProposalId = proposalId;
		}

		public BallotInfo()
		{
		}
	}
}