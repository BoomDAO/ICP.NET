using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class DisburseToNeuron
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolve_delay_seconds")]
		public ulong DissolveDelaySeconds { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("kyc_verified")]
		public bool KycVerified { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("amount_e8s")]
		public ulong AmountE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("new_controller")]
		public EdjCase.ICP.Candid.Models.OptionalValue<EdjCase.ICP.Candid.Models.Principal> NewController { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("nonce")]
		public ulong Nonce { get; set; }
		
	}
}

