using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class Canister
	{
		[CandidName("id")]
		public OptionalValue<Principal> Id { get; set; }

		public Canister(OptionalValue<Principal> id)
		{
			this.Id = id;
		}

		public Canister()
		{
		}
	}
}