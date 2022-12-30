using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class MergeMaturity
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("percentage_to_merge")]
		public uint PercentageToMerge { get; set; }
		
	}
}

