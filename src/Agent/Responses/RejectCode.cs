namespace EdjCase.ICP.Agent.Responses
{
	/// <summary>
	/// The set of code that are possible from a 'reject' response to a request
	/// </summary>
	public enum RejectCode
	{
		/// <summary>
		/// Fatal system error, retry unlikely to be useful
		/// </summary>
		SysFatal = 1,
		/// <summary>
		/// Transient system error, retry might be possible
		/// </summary>
		SysTransient = 2,
		/// <summary>
		/// Invalid destination (e.g. canister/account does not exist)
		/// </summary>
		DestinationInvalid = 3,
		/// <summary>
		/// Explicit reject by the canister
		/// </summary>
		CanisterReject = 4,
		/// <summary>
		/// Canister error (e.g., trap, no response)
		/// </summary>
		CanisterError = 5
	}
}
