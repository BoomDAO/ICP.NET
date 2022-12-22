using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class NodeProvider
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<EdjCase.ICP.Candid.Models.Principal> Id { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("reward_account")]
		public EdjCase.ICP.Candid.Models.OptionalValue<AccountIdentifier> RewardAccount { get; set; }
		
	}
}

