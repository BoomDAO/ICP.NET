using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class InitialTokenDistribution
	{
		[CandidName("treasury_distribution")]
		public OptionalValue<SwapDistribution> TreasuryDistribution { get; set; }

		[CandidName("developer_distribution")]
		public OptionalValue<DeveloperDistribution> DeveloperDistribution { get; set; }

		[CandidName("swap_distribution")]
		public OptionalValue<SwapDistribution> SwapDistribution { get; set; }

		public InitialTokenDistribution(OptionalValue<SwapDistribution> treasuryDistribution, OptionalValue<DeveloperDistribution> developerDistribution, OptionalValue<SwapDistribution> swapDistribution)
		{
			this.TreasuryDistribution = treasuryDistribution;
			this.DeveloperDistribution = developerDistribution;
			this.SwapDistribution = swapDistribution;
		}

		public InitialTokenDistribution()
		{
		}
	}
}