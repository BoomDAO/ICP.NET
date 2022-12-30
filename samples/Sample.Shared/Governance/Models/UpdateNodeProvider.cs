using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class UpdateNodeProvider
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("reward_account")]
		public EdjCase.ICP.Candid.Models.OptionalValue<AccountIdentifier> RewardAccount { get; set; }
		
	}
}

