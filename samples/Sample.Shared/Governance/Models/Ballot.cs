using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class Ballot
	{
		[CandidName("vote")]
		public int Vote { get; set; }

		[CandidName("voting_power")]
		public ulong VotingPower { get; set; }

		public Ballot(int vote, ulong votingPower)
		{
			this.Vote = vote;
			this.VotingPower = votingPower;
		}

		public Ballot()
		{
		}
	}
}