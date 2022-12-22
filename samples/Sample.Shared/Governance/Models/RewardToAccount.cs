using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class RewardToAccount
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("to_account")]
		public EdjCase.ICP.Candid.Models.OptionalValue<AccountIdentifier> ToAccount { get; set; }
		
	}
}

