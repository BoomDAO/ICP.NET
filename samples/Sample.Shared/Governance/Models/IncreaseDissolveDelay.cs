using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class IncreaseDissolveDelay
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("additional_dissolve_delay_seconds")]
		public uint AdditionalDissolveDelaySeconds { get; set; }
		
	}
}

