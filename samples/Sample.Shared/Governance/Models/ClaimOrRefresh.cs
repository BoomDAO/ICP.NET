using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefresh
	{
		[CandidName("by")]
		public OptionalValue<By> By { get; set; }

		public ClaimOrRefresh(OptionalValue<By> by)
		{
			this.By = by;
		}

		public ClaimOrRefresh()
		{
		}
	}
}