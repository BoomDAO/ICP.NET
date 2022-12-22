using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class NeuronId
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("id")]
		public ulong Id { get; set; }
		
	}
}

