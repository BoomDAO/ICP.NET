using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class SetDefaultFollowees
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("default_followees")]
		public System.Collections.Generic.List<SetDefaultFollowees> DefaultFollowees { get; set; }
		
	}
}

