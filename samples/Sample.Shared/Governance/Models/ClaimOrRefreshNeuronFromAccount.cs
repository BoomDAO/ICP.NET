using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshNeuronFromAccount
	{
		public EdjCase.ICP.Candid.Models.Principal? controller { get; set; }
		
		public ulong memo { get; set; }
		
	}
}

