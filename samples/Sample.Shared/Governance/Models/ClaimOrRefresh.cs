using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefresh
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("by")]
		public EdjCase.ICP.Candid.Models.OptionalValue<By> By { get; set; }
		
	}
}

