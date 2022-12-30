using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class MergeMaturityResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("merged_maturity_e8s")]
		public ulong MergedMaturityE8s { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("new_stake_e8s")]
		public ulong NewStakeE8s { get; set; }
		
	}
}

