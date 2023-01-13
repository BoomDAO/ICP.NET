using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Agent.Responses
{
	public class RequestStatus
	{
		public enum StatusType
		{
			Received,
			Processing,
			Replied,
			Rejected,
			Done
		}

		public StatusType Type { get; }
		private object? value;

		public RequestStatus(StatusType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		protected RequestStatus()
		{
		}

		public CandidArg AsReplied()
		{
			this.ValidateType(StatusType.Replied);
			return (CandidArg)this.value!;
		}

		public (UnboundedUInt RejectCode, string RejectMessage, string? ErrorCode) AsRejected()
		{
			this.ValidateType(StatusType.Rejected);
			return (ValueTuple<UnboundedUInt, string, string?>)this.value!;
		}

		private void ValidateType(StatusType type)
		{
			if (type != this.Type)
			{
				throw new InvalidOperationException($"Cannot cast request status type '{this.Type}' to '{type}'");
			}
		}

		public static RequestStatus Received()
		{
			return new RequestStatus(StatusType.Received, null);
		}

		public static RequestStatus Processing()
		{
			return new RequestStatus(StatusType.Processing, null);
		}

		public static RequestStatus Replied(CandidArg arg)
		{
			return new RequestStatus(StatusType.Replied, arg);
		}

		public static RequestStatus Rejected(UnboundedUInt rejectCode, string rejectMessage, string? errorcode)
		{
			return new RequestStatus(StatusType.Rejected, (rejectCode, rejectMessage, errorcode));
		}

		public static RequestStatus Done()
		{
			return new RequestStatus(StatusType.Done, null);
		}

	}
}
