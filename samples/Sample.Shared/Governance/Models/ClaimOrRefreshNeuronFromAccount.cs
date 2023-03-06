using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshNeuronFromAccount
	{
		[CandidName("controller")]
		public OptionalValue<Principal> Controller { get; set; }

		[CandidName("memo")]
		public ulong Memo { get; set; }

		public ClaimOrRefreshNeuronFromAccount(OptionalValue<Principal> controller, ulong memo)
		{
			this.Controller = controller;
			this.Memo = memo;
		}

		public ClaimOrRefreshNeuronFromAccount()
		{
		}
	}
}