using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class SetDissolveTimestamp
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolve_timestamp_seconds")]
		public ulong DissolveTimestampSeconds { get; set; }
		
	}
}

