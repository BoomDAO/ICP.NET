using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class ListKnownNeuronsResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("known_neurons")]
		public System.Collections.Generic.List<KnownNeuron> KnownNeurons { get; set; }
		
	}
}

