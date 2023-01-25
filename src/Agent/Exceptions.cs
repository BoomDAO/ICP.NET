using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Agent
{
	/// <summary>
	/// Exception to indicate that a request has been cleaned up.
	/// This is usually due to the request being too old
	/// </summary>
	public class RequestCleanedUpException : Exception
	{
		/// <summary>
		/// 
		/// </summary>
		public RequestCleanedUpException() : base("Request reply/rejected data has timed out and has been cleaned up")
		{

		}
	}

	/// <summary>
	/// Exception to indicate that the specified BLS public key is invalid
	/// </summary>
	public class InvalidPublicKey : Exception
	{
		/// <summary>
		/// 
		/// </summary>
		public InvalidPublicKey()
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



	public class CallRejectedException : Exception
	{
		public UnboundedUInt RejectCode { get; }
		public string RejectMessage { get; }
		public string? ErrorCode { get; }
		public CallRejectedException(UnboundedUInt rejectCode, string rejectMessage, string? errorCode)
		{
			this.RejectCode = rejectCode;
			this.RejectMessage = rejectMessage;
			this.ErrorCode = errorCode;
		}
	}
}
