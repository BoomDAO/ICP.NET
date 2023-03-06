using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class GovernanceError
	{
		[CandidName("error_message")]
		public string ErrorMessage { get; set; }

		[CandidName("error_type")]
		public int ErrorType { get; set; }

		public GovernanceError(string errorMessage, int errorType)
		{
			this.ErrorMessage = errorMessage;
			this.ErrorType = errorType;
		}

		public GovernanceError()
		{
		}
	}
}