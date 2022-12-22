using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Amount
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("e8s")]
		public ulong E8s { get; set; }
		
	}
}

