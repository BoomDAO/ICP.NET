using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Agent.Responses
{
	/// <summary>
	/// A model representing the response data in the form of a variant
	/// </summary>
	public class QueryResponse
	{
		/// <summary>
		/// The type of response returned. Can be used to call the right method
		/// to extract the variant data
		/// </summary>
		public QueryResponseType Type { get; }
		private readonly object value;

		private QueryResponse(QueryResponseType type, object value)
		{
			this.Type = type;
			this.value = value;
		}

		/// <summary>
		/// Gets the reply data IF the response type is 'replied'. Otherwise will throw exception
		/// </summary>
		/// <exception cref="InvalidOperationException">Will throw if not of type 'replied'</exception>
		/// <returns>Reply data</returns>
		public QueryReply AsReplied()
		{
			this.ThrowIfWrongType(QueryResponseType.Replied);
			return (QueryReply)this.value;
		}

		/// <summary>
		/// Gets the reply data IF the response type is 'rejected'. Otherwise will throw exception
		/// </summary>
		/// <exception cref="InvalidOperationException">Will throw if not of type 'rejected'</exception>
		/// <returns>Reject error information</returns>
		public QueryRejectInfo AsRejected()
		{
			this.ThrowIfWrongType(QueryResponseType.Rejected);
			return (QueryRejectInfo)this.value;
		}

		private void ThrowIfWrongType(QueryResponseType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Unable to parse '{this.Type}' query response as type '{type}'");
			}
		}

		/// <summary>
		/// Helper method to either get the reply data or throw an exception
		/// that formats the 'rejected' data
		/// </summary>
		/// <returns>Query reply data</returns>
		/// <exception cref="QueryRejectedException">Throws if 'rejected'</exception>
		public QueryReply ThrowOrGetReply()
		{
			if (this.Type == QueryResponseType.Rejected)
			{
				QueryRejectInfo rejectInfo = this.AsRejected();
				throw new QueryRejectedException(rejectInfo);
			}
			return this.AsReplied();
		}

		internal static QueryResponse Rejected(RejectCode code, string? message, string? errorCode)
		{
			return new QueryResponse(QueryResponseType.Rejected, new QueryRejectInfo(code, message, errorCode));
		}

		internal static QueryResponse Replied(QueryReply reply)
		{
			return new QueryResponse(QueryResponseType.Replied, reply);
		}
	}

	/// <summary>
	/// The variant options for a query response
	/// </summary>
	public enum QueryResponseType
	{
		/// <summary>
		/// When the canister replies to a query request with no errors
		/// </summary>
		Replied,
		/// <summary>
		/// When the cansiter request has errors to query request
		/// </summary>
		Rejected
	}

	/// <summary>
	/// Wrapper object around the candid arg that is returned
	/// </summary>
	public class QueryReply
	{
		/// <summary>
		/// The candid arg returned from a request
		/// </summary>
		public CandidArg Arg { get; }

		internal QueryReply(CandidArg arg)
		{
			this.Arg = arg;
		}
	}

	/// <summary>
	/// Data from a query response that has been rejected
	/// </summary>
	public class QueryRejectInfo
	{
		/// <summary>
		/// The type of query reject
		/// </summary>
		public RejectCode Code { get; }

		/// <summary>
		/// Optional. A human readable message about the rejection
		/// </summary>
		public string? Message { get; }

		/// <summary>
		/// Optional. A specific error id for the reject
		/// </summary>
		public string? ErrorCode { get; }

		internal QueryRejectInfo(RejectCode code, string? message, string? errorCode)
		{
			this.Code = code;
			this.Message = message;
			this.ErrorCode = errorCode;
		}
	}
}