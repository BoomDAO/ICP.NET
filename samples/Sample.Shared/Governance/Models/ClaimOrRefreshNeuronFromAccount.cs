using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshNeuronFromAccount
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("controller")]
		public EdjCase.ICP.Candid.Models.OptionalValue<EdjCase.ICP.Candid.Models.Principal> Controller { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("memo")]
		public ulong Memo { get; set; }
		
	}
}

