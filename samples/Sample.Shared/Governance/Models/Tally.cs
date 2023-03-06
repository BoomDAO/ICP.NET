using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class Tally
	{
		[CandidName("no")]
		public ulong No { get; set; }

		[CandidName("yes")]
		public ulong Yes { get; set; }

		[CandidName("total")]
		public ulong Total { get; set; }

		[CandidName("timestamp_seconds")]
		public ulong TimestampSeconds { get; set; }

		public Tally(ulong no, ulong yes, ulong total, ulong timestampSeconds)
		{
			this.No = no;
			this.Yes = yes;
			this.Total = total;
			this.TimestampSeconds = timestampSeconds;
		}

		public Tally()
		{
		}
	}
}