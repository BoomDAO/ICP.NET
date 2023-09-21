using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class GlobalTimeOfDay
	{
		[CandidName("seconds_after_utc_midnight")]
		public OptionalValue<ulong> SecondsAfterUtcMidnight { get; set; }

		public GlobalTimeOfDay(OptionalValue<ulong> secondsAfterUtcMidnight)
		{
			this.SecondsAfterUtcMidnight = secondsAfterUtcMidnight;
		}

		public GlobalTimeOfDay()
		{
		}
	}
}