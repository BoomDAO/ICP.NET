using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class Committed
	{
		[CandidName("total_direct_contribution_icp_e8s")]
		public OptionalValue<ulong> TotalDirectContributionIcpE8s { get; set; }

		[CandidName("total_neurons_fund_contribution_icp_e8s")]
		public OptionalValue<ulong> TotalNeuronsFundContributionIcpE8s { get; set; }

		[CandidName("sns_governance_canister_id")]
		public OptionalValue<Principal> SnsGovernanceCanisterId { get; set; }

		public Committed(OptionalValue<ulong> totalDirectContributionIcpE8s, OptionalValue<ulong> totalNeuronsFundContributionIcpE8s, OptionalValue<Principal> snsGovernanceCanisterId)
		{
			this.TotalDirectContributionIcpE8s = totalDirectContributionIcpE8s;
			this.TotalNeuronsFundContributionIcpE8s = totalNeuronsFundContributionIcpE8s;
			this.SnsGovernanceCanisterId = snsGovernanceCanisterId;
		}

		public Committed()
		{
		}
	}
}