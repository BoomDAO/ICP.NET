using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class DisburseToNeuron
	{
		[CandidName("dissolve_delay_seconds")]
		public ulong DissolveDelaySeconds { get; set; }

		[CandidName("kyc_verified")]
		public bool KycVerified { get; set; }

		[CandidName("amount_e8s")]
		public ulong AmountE8s { get; set; }

		[CandidName("new_controller")]
		public OptionalValue<Principal> NewController { get; set; }

		[CandidName("nonce")]
		public ulong Nonce { get; set; }

		public DisburseToNeuron(ulong dissolveDelaySeconds, bool kycVerified, ulong amountE8s, OptionalValue<Principal> newController, ulong nonce)
		{
			this.DissolveDelaySeconds = dissolveDelaySeconds;
			this.KycVerified = kycVerified;
			this.AmountE8s = amountE8s;
			this.NewController = newController;
			this.Nonce = nonce;
		}

		public DisburseToNeuron()
		{
		}
	}
}