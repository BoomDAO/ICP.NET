using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class RewardToNeuron
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolve_delay_seconds")]
		public ulong DissolveDelaySeconds { get; set; }
		
	}
}

