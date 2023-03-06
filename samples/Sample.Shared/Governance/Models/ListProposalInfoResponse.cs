using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ListProposalInfoResponse
	{
		[CandidName("proposal_info")]
		public List<ProposalInfo> ProposalInfo { get; set; }

		public ListProposalInfoResponse(List<ProposalInfo> proposalInfo)
		{
			this.ProposalInfo = proposalInfo;
		}

		public ListProposalInfoResponse()
		{
		}
	}
}