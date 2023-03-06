using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ListNodeProvidersResponse
	{
		[CandidName("node_providers")]
		public List<NodeProvider> NodeProviders { get; set; }

		public ListNodeProvidersResponse(List<NodeProvider> nodeProviders)
		{
			this.NodeProviders = nodeProviders;
		}

		public ListNodeProvidersResponse()
		{
		}
	}
}