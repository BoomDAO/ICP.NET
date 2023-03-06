using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class MergeMaturity
	{
		[CandidName("percentage_to_merge")]
		public uint PercentageToMerge { get; set; }

		public MergeMaturity(uint percentageToMerge)
		{
			this.PercentageToMerge = percentageToMerge;
		}

		public MergeMaturity()
		{
		}
	}
}