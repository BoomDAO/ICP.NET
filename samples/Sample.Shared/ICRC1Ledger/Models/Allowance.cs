using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Timestamp = System.UInt64;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class Allowance
	{
		[CandidName("allowance")]
		public UnboundedUInt Allowance_ { get; set; }

		[CandidName("expires_at")]
		public OptionalValue<Timestamp> ExpiresAt { get; set; }

		public Allowance(UnboundedUInt allowance, OptionalValue<Timestamp> expiresAt)
		{
			this.Allowance_ = allowance;
			this.ExpiresAt = expiresAt;
		}

		public Allowance()
		{
		}
	}
}