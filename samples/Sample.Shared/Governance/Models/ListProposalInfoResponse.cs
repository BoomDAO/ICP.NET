using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class ListProposalInfoResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("proposal_info")]
		public System.Collections.Generic.List<ProposalInfo> ProposalInfo { get; set; }
		
	}
}

