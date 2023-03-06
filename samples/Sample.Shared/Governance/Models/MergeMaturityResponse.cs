using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class MergeMaturityResponse
	{
		[CandidName("merged_maturity_e8s")]
		public ulong MergedMaturityE8s { get; set; }

		[CandidName("new_stake_e8s")]
		public ulong NewStakeE8s { get; set; }

		public MergeMaturityResponse(ulong mergedMaturityE8s, ulong newStakeE8s)
		{
			this.MergedMaturityE8s = mergedMaturityE8s;
			this.NewStakeE8s = newStakeE8s;
		}

		public MergeMaturityResponse()
		{
		}
	}
}