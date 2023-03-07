using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class Committed
	{
		[CandidName("sns_governance_canister_id")]
		public OptionalValue<Principal> SnsGovernanceCanisterId { get; set; }

		public Committed(OptionalValue<Principal> snsGovernanceCanisterId)
		{
			this.SnsGovernanceCanisterId = snsGovernanceCanisterId;
		}

		public Committed()
		{
		}
	}
}