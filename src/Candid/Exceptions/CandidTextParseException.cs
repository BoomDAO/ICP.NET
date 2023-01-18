using System;

namespace EdjCase.ICP.Candid.Exceptions
{
	/// <summary>
	/// An error that occurs when the conversion of text to a candid model fails
	/// </summary>
	public class CandidTextParseException : Exception
	{
		internal string ParseError { get; }
		internal CandidTextParseException(string message)
		{
			this.ParseError = message;
		}

		/// <inheritdoc/>
		public override string Message
		{

			get
			{
				return "Failed to parse Candid text at . Error: " + this.ParseError;
			}

		}
	}
}
