using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class Migration
	{
		[CandidName("status")]
		public OptionalValue<int> Status { get; set; }

		[CandidName("failure_reason")]
		public OptionalValue<string> FailureReason { get; set; }

		[CandidName("progress")]
		public OptionalValue<Progress> Progress { get; set; }

		public Migration(OptionalValue<int> status, OptionalValue<string> failureReason, OptionalValue<Progress> progress)
		{
			this.Status = status;
			this.FailureReason = failureReason;
			this.Progress = progress;
		}

		public Migration()
		{
		}
	}
}