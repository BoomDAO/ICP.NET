using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Split
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("amount_e8s")]
		public ulong AmountE8s { get; set; }
		
	}
}

