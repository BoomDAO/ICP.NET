using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Followees
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("followees")]
		public System.Collections.Generic.List<NeuronId> Followees_ { get; set; }
		
	}
}

