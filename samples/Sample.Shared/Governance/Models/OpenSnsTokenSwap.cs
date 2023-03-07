using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class OpenSnsTokenSwap
	{
		[CandidName("community_fund_investment_e8s")]
		public OptionalValue<ulong> CommunityFundInvestmentE8s { get; set; }

		[CandidName("target_swap_canister_id")]
		public OptionalValue<Principal> TargetSwapCanisterId { get; set; }

		[CandidName("params")]
		public OptionalValue<Params> Params { get; set; }

		public OpenSnsTokenSwap(OptionalValue<ulong> communityFundInvestmentE8s, OptionalValue<Principal> targetSwapCanisterId, OptionalValue<Params> @params)
		{
			this.CommunityFundInvestmentE8s = communityFundInvestmentE8s;
			this.TargetSwapCanisterId = targetSwapCanisterId;
			this.Params = @params;
		}

		public OpenSnsTokenSwap()
		{
		}
	}
}