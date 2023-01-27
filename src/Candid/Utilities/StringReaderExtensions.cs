using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EdjCase.ICP.Candid.Utilities
{
	internal static class StringReaderExtensions
	{
		public static string? ReadNextWord(this StringReader reader, string? expectedValue = null)
		{
			reader.SkipWhitespace();
			var stringBuilder = new StringBuilder();
			char? wordChar = reader.PeekOrDefault();
			while (wordChar != null && !char.IsWhiteSpace(wordChar.Value))
			{
				if (stringBuilder.Length > 0 && wordChar == ':')
				{
					// stop word if there is no space between ':' and the text
					break;
				}
				stringBuilder.Append(wordChar);
				reader.Read();
				wordChar = reader.PeekOrDefault();
			}
			reader.SkipWhitespace();
			if (stringBuilder.Length < 1)
			{
				return null;
			}
			string value = stringBuilder.ToString();
			if (expectedValue != null && value != expectedValue)
			{
				// TODO 
				throw new Exception();
			}
			return value;
		}

		public static void SkipWhitespace(this StringReader reader)
		{
			char? nextChar = reader.PeekOrDefault();
			while (nextChar != null && char.IsWhiteSpace(nextChar.Value))
			{
				reader.Read();
				nextChar = reader.PeekOrDefault();
			}
		}

		public static char? ReadOrDefault(this StringReader reader)
		{
			int c = reader.Read();
			if (c == -1)
			{
				return null;
			}
			return (char)c;
		}
		public static char ReadOrThrow(this StringReader reader, char? expectedValue = null)
		{
			int c = reader.Read();
			if (c == -1)
			{
				throw new EndOfStreamException();
			}
			if (expectedValue != null && c != expectedValue)
			{
				// TODO
				throw new Exception();
			}
			return (char)c;
		}
		public static char? PeekOrDefault(this StringReader reader)
		{
			int c = reader.Peek();
			if (c == -1)
			{
				return null;
			}
			return (char)c;
		}

		public static char PeekOrThrow(this StringReader reader)
		{
			int c = reader.Peek();
			if (c == -1)
			{
				throw new EndOfStreamException();
			}
			return (char)c;
		}
	}
}
