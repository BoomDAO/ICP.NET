using System;
using System.IO;

namespace EdjCase.ICP.Candid.Exceptions
{
	/// <summary>
	/// An error that occurs during the decoding of bytes to a candid structure
	/// </summary>
	public class CandidDecodingException : Exception
	{
		/// <summary>
		/// The index where the byte reader last read. Helps identitfy the source of the 
		/// decoding issue
		/// </summary>
		internal int ByteEndIndex { get; }

		/// <summary>
		/// Message about the error that occurred
		/// </summary>
		internal string ErrorMessage { get; }

		/// <param name="byteEndIndex">The index where the byte reader last read</param>
		/// <param name="message">Message about the error that occurred</param>
		internal CandidDecodingException(int byteEndIndex, string message)
		{
			this.ByteEndIndex = byteEndIndex;
			this.ErrorMessage = message;
		}

		/// <inheritdoc/>
		public override string Message
		{
			get
			{
				return $"Candid failed with decoding at byte index {this.ByteEndIndex} with message: {this.ErrorMessage}";
			}
		}

		internal static CandidDecodingException FromReader(BinaryReader reader, string message)
		{
			return new CandidDecodingException((int)reader.BaseStream.Position, message);
		}
	}
}
