using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Followees
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("FolloweesItem")]
		public System.Collections.Generic.List<NeuronId> FolloweesItem { get; set; }
		
	}
}

