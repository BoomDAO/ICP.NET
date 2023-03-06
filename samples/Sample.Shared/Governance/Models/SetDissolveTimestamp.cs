using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class SetDissolveTimestamp
	{
		[CandidName("dissolve_timestamp_seconds")]
		public ulong DissolveTimestampSeconds { get; set; }

		public SetDissolveTimestamp(ulong dissolveTimestampSeconds)
		{
			this.DissolveTimestampSeconds = dissolveTimestampSeconds;
		}

		public SetDissolveTimestamp()
		{
		}
	}
}