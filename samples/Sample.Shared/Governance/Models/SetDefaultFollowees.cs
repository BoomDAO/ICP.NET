using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class SetDefaultFollowees
	{
		public class R0V0
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("0")]
			public int F0 { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("1")]
			public Followees F1 { get; set; }
			
		}
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("default_followees")]
		public System.Collections.Generic.List<R0V0> DefaultFollowees { get; set; }
		
	}
}

