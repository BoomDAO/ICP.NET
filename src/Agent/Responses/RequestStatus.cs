using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Agent.Responses
{
	/// <summary>
	/// A variant type of the status of a request that has been sent to a canister
	/// </summary>
	public class RequestStatus
	{
		/// <summary>
		/// The types of statuses the request can be
		/// </summary>
		public enum StatusType
		{
			/// <summary>
			/// The request has been received by the node, but not yet being processed
			/// </summary>
			Received,
			/// <summary>
			/// The request is being processed and does not have a response yet
			/// </summary>
			Processing,
			/// <summary>
			/// The request has been processed and it has reply data
			/// </summary>
			Replied,
			/// <summary>
			/// The request has been processed and has reject data
			/// </summary>
			Rejected,
			/// <summary>
			/// The request has been processed but the response data has been removed.
			/// This usually happens after a certain amount of time to save space
			/// </summary>
			Done
		}

		/// <summary>
		/// The type of status was returned
		/// </summary>
		public StatusType Type { get; }
		private object? value;

		private RequestStatus(StatusType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		/// <summary>
		/// Returns the candid arg IF the status is 'replied', otherwise throws exception
		/// </summary>
		/// <returns>Candid arg of reply</returns>
		public CandidArg AsReplied()
		{
			this.ValidateType(StatusType.Replied);
			return (CandidArg)this.value!;
		}

		/// <summary>
		/// Returns the reject data IF the status is 'rejected', otherwise throws exception
		/// </summary>
		/// <returns>Reject error information</returns>
		public (RejectCode RejectCode, string RejectMessage, string? ErrorCode) AsRejected()
		{
			this.ValidateType(StatusType.Rejected);
			return (ValueTuple<RejectCode, string, string?>)this.value!;
		}

		private void ValidateType(StatusType type)
		{
			if (type != this.Type)
			{
				throw new InvalidOperationException($"Cannot cast request status type '{this.Type}' to '{type}'");
			}
		}

		internal static RequestStatus Received()
		{
			return new RequestStatus(StatusType.Received, null);
		}

		internal static RequestStatus Processing()
		{
			return new RequestStatus(StatusType.Processing, null);
		}

		internal static RequestStatus Replied(CandidArg arg)
		{
			return new RequestStatus(StatusType.Replied, arg);
		}

		internal static RequestStatus Rejected(RejectCode rejectCode, string rejectMessage, string? errorcode)
		{
			return new RequestStatus(StatusType.Rejected, (rejectCode, rejectMessage, errorcode));
		}

		internal static RequestStatus Done()
		{
			return new RequestStatus(StatusType.Done, null);
		}

	}
}
