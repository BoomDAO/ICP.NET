using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class SetOpenTimeWindowRequest
	{
		[CandidName("open_time_window")]
		public OptionalValue<TimeWindow> OpenTimeWindow { get; set; }

		public SetOpenTimeWindowRequest(OptionalValue<TimeWindow> openTimeWindow)
		{
			this.OpenTimeWindow = openTimeWindow;
		}

		public SetOpenTimeWindowRequest()
		{
		}
	}
}