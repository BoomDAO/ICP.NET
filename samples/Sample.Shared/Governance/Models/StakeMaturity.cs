using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class StakeMaturity
	{
		[CandidName("percentage_to_stake")]
		public OptionalValue<uint> PercentageToStake { get; set; }

		public StakeMaturity(OptionalValue<uint> percentageToStake)
		{
			this.PercentageToStake = percentageToStake;
		}

		public StakeMaturity()
		{
		}
	}
}