using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class RegisterVote
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("vote")]
		public int Vote { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("proposal")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> Proposal { get; set; }
		
	}
}

