using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class NeuronInFlightCommand
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("command")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Command_2> Command { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("timestamp")]
		public ulong Timestamp { get; set; }
		
	}
}

