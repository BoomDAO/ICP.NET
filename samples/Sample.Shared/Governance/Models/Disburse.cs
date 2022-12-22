using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Disburse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("to_account")]
		public EdjCase.ICP.Candid.Models.OptionalValue<AccountIdentifier> ToAccount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("amount")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Amount> Amount { get; set; }
		
	}
}

