using EdjCase.ICP.Agent.Responses;
using System;

namespace EdjCase.ICP.Agent
{
	/// <summary>
	/// Exception to indicate that a request has been cleaned up.
	/// This is usually due to the request being too old
	/// </summary>
	public class RequestCleanedUpException : Exception
	{
		/// <summary></summary>
		public RequestCleanedUpException() : base("Request reply/rejected data has timed out and has been cleaned up")
		{

		}
	}

	/// <summary>
	/// Exception to indicate that the specified public key is invalid
	/// </summary>
	public class InvalidPublicKey : Exception
	{
		/// <summary></summary>
		public InvalidPublicKey(Exception innerEx) : base("Invalid public key value", innerEx)
		{
		}
	}

	/// <summary>
	/// Exception to indicate that the certificate is invalid
	/// </summary>
	public class InvalidCertificateException : Exception
	{
		/// <param name="message">Specific error message</param>
		public InvalidCertificateException(string message) : base(message)
		{

		}
	}


	/// <summary>
	/// Exception for when a call to a canister is rejected/has an error
	/// </summary>
	public class CallRejectedException : Exception
	{
		/// <summary>
		/// The type of rejection that occurred
		/// </summary>
		public RejectCode RejectCode { get; }

		/// <summary>
		/// The human readable message of the rejection error
		/// </summary>
		public string? RejectMessage { get; }

		/// <summary>
		/// Optional. Specific error code for differentiating specific errors
		/// </summary>
		public string? ErrorCode { get; }

		/// <param name="rejectCode">The type of rejection that occurred</param>
		/// <param name="rejectMessage">The human readable message of the rejection error</param>
		/// <param name="errorCode">Optional. Specific error code for differentiating specific errors</param>
		public CallRejectedException(RejectCode rejectCode, string? rejectMessage, string? errorCode)
		{
			this.RejectCode = rejectCode;
			this.RejectMessage = rejectMessage;
			this.ErrorCode = errorCode;
		}

		/// <inheritdoc />
		public override string Message => $"Call was rejected. Code: {this.RejectCode}, Message: {this.RejectMessage}, ErrorCode: {this.ErrorCode}";
	}

	/// <summary>
	/// Exception for when a query to a canister is rejected/has an error
	/// </summary>
	public class QueryRejectedException : Exception
	{
		/// <summary>
		/// The details of the rejection
		/// </summary>
		public QueryRejectInfo Info { get; }

		/// <param name="info">The type of rejection that occurred</param>
		internal QueryRejectedException(QueryRejectInfo info)
		{
			this.Info = info;
		}

		/// <inheritdoc />
		public override string Message => $"Query was rejected. Code: {this.Info.Code}, Message: {this.Info.Message}, ErrorCode: {this.Info.ErrorCode}";
	}
}
