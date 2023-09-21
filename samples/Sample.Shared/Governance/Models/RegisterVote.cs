using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class RegisterVote
	{
		[CandidName("vote")]
		public int Vote { get; set; }

		[CandidName("proposal")]
		public OptionalValue<NeuronId> Proposal { get; set; }

		public RegisterVote(int vote, OptionalValue<NeuronId> proposal)
		{
			this.Vote = vote;
			this.Proposal = proposal;
		}

		public RegisterVote()
		{
		}
	}
}