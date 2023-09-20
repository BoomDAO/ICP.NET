using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

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