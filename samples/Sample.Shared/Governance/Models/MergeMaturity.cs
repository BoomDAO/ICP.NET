namespace Sample.Shared.Governance.Models
{
	public class MergeMaturity
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("percentage_to_merge")]
		public uint PercentageToMerge { get; set; }
		
	}
}

