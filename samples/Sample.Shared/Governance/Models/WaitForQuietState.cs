using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class WaitForQuietState
	{
		[CandidName("current_deadline_timestamp_seconds")]
		public ulong CurrentDeadlineTimestampSeconds { get; set; }

		public WaitForQuietState(ulong currentDeadlineTimestampSeconds)
		{
			this.CurrentDeadlineTimestampSeconds = currentDeadlineTimestampSeconds;
		}

		public WaitForQuietState()
		{
		}
	}
}