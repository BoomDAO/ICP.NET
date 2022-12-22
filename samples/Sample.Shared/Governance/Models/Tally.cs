using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Tally
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("no")]
		public ulong No { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("yes")]
		public ulong Yes { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("total")]
		public ulong Total { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("timestamp_seconds")]
		public ulong TimestampSeconds { get; set; }
		
	}
}

