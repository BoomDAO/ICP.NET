namespace Sample.Shared.Governance.Models
{
	public class WaitForQuietState
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("current_deadline_timestamp_seconds")]
		public ulong CurrentDeadlineTimestampSeconds { get; set; }
		
	}
}

