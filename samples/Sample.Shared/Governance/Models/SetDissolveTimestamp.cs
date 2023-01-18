namespace Sample.Shared.Governance.Models
{
	public class SetDissolveTimestamp
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("dissolve_timestamp_seconds")]
		public ulong DissolveTimestampSeconds { get; set; }
		
	}
}

