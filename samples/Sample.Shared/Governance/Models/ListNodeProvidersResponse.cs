using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class ListNodeProvidersResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("node_providers")]
		public System.Collections.Generic.List<NodeProvider> NodeProviders { get; set; }
		
	}
}

