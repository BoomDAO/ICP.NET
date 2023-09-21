using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class ListProposalInfo
	{
		[CandidName("include_reward_status")]
		public List<int> IncludeRewardStatus { get; set; }

		[CandidName("omit_large_fields")]
		public OptionalValue<bool> OmitLargeFields { get; set; }

		[CandidName("before_proposal")]
		public OptionalValue<NeuronId> BeforeProposal { get; set; }

		[CandidName("limit")]
		public uint Limit { get; set; }

		[CandidName("exclude_topic")]
		public List<int> ExcludeTopic { get; set; }

		[CandidName("include_all_manage_neuron_proposals")]
		public OptionalValue<bool> IncludeAllManageNeuronProposals { get; set; }

		[CandidName("include_status")]
		public List<int> IncludeStatus { get; set; }

		public ListProposalInfo(List<int> includeRewardStatus, OptionalValue<bool> omitLargeFields, OptionalValue<NeuronId> beforeProposal, uint limit, List<int> excludeTopic, OptionalValue<bool> includeAllManageNeuronProposals, List<int> includeStatus)
		{
			this.IncludeRewardStatus = includeRewardStatus;
			this.OmitLargeFields = omitLargeFields;
			this.BeforeProposal = beforeProposal;
			this.Limit = limit;
			this.ExcludeTopic = excludeTopic;
			this.IncludeAllManageNeuronProposals = includeAllManageNeuronProposals;
			this.IncludeStatus = includeStatus;
		}

		public ListProposalInfo()
		{
		}
	}
}