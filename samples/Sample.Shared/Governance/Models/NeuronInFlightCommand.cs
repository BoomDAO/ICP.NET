using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class NeuronInFlightCommand
	{
		public Command_2? command { get; set; }
		
		public ulong timestamp { get; set; }
		
	}
}

