using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class WaitForQuietState
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("current_deadline_timestamp_seconds")]
		public ulong CurrentDeadlineTimestampSeconds { get; set; }
		
	}
}

