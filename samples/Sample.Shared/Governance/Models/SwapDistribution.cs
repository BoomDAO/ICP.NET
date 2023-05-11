using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class SwapDistribution
	{
		[CandidName("total")]
		public OptionalValue<Tokens> Total { get; set; }

		public SwapDistribution(OptionalValue<Tokens> total)
		{
			this.Total = total;
		}

		public SwapDistribution()
		{
		}
	}
}