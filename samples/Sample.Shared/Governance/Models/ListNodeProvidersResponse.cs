namespace Sample.Shared.Governance.Models
{
	public class ListNodeProvidersResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("node_providers")]
		public System.Collections.Generic.List<NodeProvider> NodeProviders { get; set; }
		
	}
}

