using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class DerivedProposalInformation
	{
		[CandidName("swap_background_information")]
		public OptionalValue<SwapBackgroundInformation> SwapBackgroundInformation { get; set; }

		public DerivedProposalInformation(OptionalValue<SwapBackgroundInformation> swapBackgroundInformation)
		{
			this.SwapBackgroundInformation = swapBackgroundInformation;
		}

		public DerivedProposalInformation()
		{
		}
	}
}