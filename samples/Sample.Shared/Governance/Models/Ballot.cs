namespace Sample.Shared.Governance.Models
{
	public class Ballot
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("vote")]
		public int Vote { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("voting_power")]
		public ulong VotingPower { get; set; }
		
	}
}

