using Dahomey.Cbor.ObjectModel;
using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Agent.Responses
{
	public class QueryResponse
	{
		public QueryResponseType Type { get; }
		private readonly object value;

		private QueryResponse(QueryResponseType type, object value)
		{
			this.Type = type;
			this.value = value;
		}

		public QueryReply AsReplied()
		{
			this.ThrowIfWrongType(QueryResponseType.Replied);
			return (QueryReply)this.value;
		}

		public (ReplicaRejectCode Code, string? Message) AsRejected()
		{
			this.ThrowIfWrongType(QueryResponseType.Rejected);
			return (ValueTuple<ReplicaRejectCode, string>)this.value;
		}

		private void ThrowIfWrongType(QueryResponseType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Unable to parse '{this.Type}' query response as type '{type}'");
			}
		}

		public QueryReply ThrowOrGetReply()
		{
			if (this.Type == QueryResponseType.Rejected)
			{
				(ReplicaRejectCode code, string? message) = this.AsRejected();
				throw new QueryRejectedException(code, message);
			}
			return this.AsReplied();
		}

		public static QueryResponse Rejected(ReplicaRejectCode code, string? message)
		{
			return new QueryResponse(QueryResponseType.Rejected, (code, message));
		}

		public static QueryResponse Replied(QueryReply reply)
		{
			return new QueryResponse(QueryResponseType.Replied, reply);
		}
	}

	public enum QueryResponseType
	{
		Replied,
		Rejected
	}

	public class QueryReply
	{
		public CandidArg Arg { get; }

		public QueryReply(CandidArg arg)
		{
			this.Arg = arg;
		}
	}

	public enum ReplicaRejectCode
	{
		SysFatal = 1,
		SysTransient = 2,
		DestinationInvalid = 3,
		CanisterReject = 4,
		CanisterError = 5
	}

	public class QueryRejectedException : Exception
	{
		public ReplicaRejectCode Code { get; }
		public string? RejectionMessage { get; }
		public QueryRejectedException(ReplicaRejectCode code, string? message)
		{
			this.Code = code;
			this.RejectionMessage = message;
		}

		public override string Message => $"Query was rejected. Code: {this.Code}, Message: {this.Message}";
	}
}