using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Sample.Shared.Governance.Models;

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