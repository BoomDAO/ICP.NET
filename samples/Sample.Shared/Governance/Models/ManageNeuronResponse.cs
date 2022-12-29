using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class ManageNeuronResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("command")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Command1> Command { get; set; }
		
	}
}

