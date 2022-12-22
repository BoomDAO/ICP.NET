using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class KnownNeuronData
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("name")]
		public string Name { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("description")]
		public EdjCase.ICP.Candid.Models.OptionalValue<string> Description { get; set; }
		
	}
}

