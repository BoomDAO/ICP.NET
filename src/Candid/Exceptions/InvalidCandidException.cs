using System;
using System.IO;

namespace EdjCase.ICP.Candid.Exceptions
{
	/// <summary>
	/// An error that occurs if the candid models do not follow the 
	/// specification
	/// </summary>
	public class InvalidCandidException : Exception
	{
		internal string ErrorMessage { get; }
		internal string? TraceString { get; }

		internal InvalidCandidException(Exception inner, string? trace = null) : this(inner.ToString(), trace)
		{

		}
		internal InvalidCandidException(string message, string? trace = null) : base()
		{
			this.ErrorMessage = message;
			this.TraceString = trace;
		}

		/// <inheritdoc/>
		public override string Message
		{
			get
			{
				if (this.TraceString == null)
				{
					return this.ErrorMessage;
				}
				return this.ErrorMessage + "\nTrace:\n" + this.TraceString;
			}
		}
	}
}
