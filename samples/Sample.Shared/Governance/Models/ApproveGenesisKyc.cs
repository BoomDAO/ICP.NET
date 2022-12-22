using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class ApproveGenesisKyc
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("principals")]
		public System.Collections.Generic.List<EdjCase.ICP.Candid.Models.Principal> Principals { get; set; }
		
	}
}

