using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Follow
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("topic")]
		public int Topic { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("followees")]
		public System.Collections.Generic.List<NeuronId> Followees { get; set; }
		
	}
}

