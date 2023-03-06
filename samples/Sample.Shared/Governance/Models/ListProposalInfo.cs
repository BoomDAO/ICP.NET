using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class ListProposalInfo
	{
		[CandidName("include_reward_status")]
		public List<int> IncludeRewardStatus { get; set; }

		[CandidName("before_proposal")]
		public OptionalValue<NeuronId> BeforeProposal { get; set; }

		[CandidName("limit")]
		public uint Limit { get; set; }

		[CandidName("exclude_topic")]
		public List<int> ExcludeTopic { get; set; }

		[CandidName("include_status")]
		public List<int> IncludeStatus { get; set; }

		public ListProposalInfo(List<int> includeRewardStatus, OptionalValue<NeuronId> beforeProposal, uint limit, List<int> excludeTopic, List<int> includeStatus)
		{
			this.IncludeRewardStatus = includeRewardStatus;
			this.BeforeProposal = beforeProposal;
			this.Limit = limit;
			this.ExcludeTopic = excludeTopic;
			this.IncludeStatus = includeStatus;
		}

		public ListProposalInfo()
		{
		}
	}
}