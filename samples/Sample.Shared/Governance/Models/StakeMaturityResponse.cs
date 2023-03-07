using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class StakeMaturityResponse
	{
		[CandidName("maturity_e8s")]
		public ulong MaturityE8s { get; set; }

		[CandidName("staked_maturity_e8s")]
		public ulong StakedMaturityE8s { get; set; }

		public StakeMaturityResponse(ulong maturityE8s, ulong stakedMaturityE8s)
		{
			this.MaturityE8s = maturityE8s;
			this.StakedMaturityE8s = stakedMaturityE8s;
		}

		public StakeMaturityResponse()
		{
		}
	}
}