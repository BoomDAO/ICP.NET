using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid.Parsers
{
	public static class CandidServiceFileParser
	{
		public static CandidServiceDescription Parse(string text)
		{
			using (var textReader = new StringReader(text))
			{
				var declaredTypes = new Dictionary<CandidId, CandidType>();
				string? nextWord = textReader.ReadNextWord();
				while (nextWord == "type")
				{
					string? name = textReader.ReadNextWord();
					if (name == null)
					{
						throw new EndOfStreamException();
					}
					textReader.ReadNextWord("=");
					string t = GetType(textReader);
					CandidType type = CandidTextParser.Parse(t);
					declaredTypes.Add(CandidId.Parse(name), type);
					nextWord = textReader.ReadNextWord();
				}
				if (nextWord != "service")
				{
					// TODO
					throw new Exception($"nextWord is {nextWord}");
				}
				string? seperator = textReader.ReadNextWord();
				if (seperator != ":")
				{
					// TODO this is the name, use it?
					textReader.ReadNextWord(":");
				}
				textReader.SkipWhitespace();
				char nextChar = textReader.PeekOrThrow();
				if (nextChar == '(')
				{
					textReader.Read();
					while (textReader.ReadOrThrow() != ')')
					{
						// TODO what is this part??? they are args? for a service?
					}

					textReader.ReadNextWord("->");
					textReader.SkipWhitespace();
					nextChar = textReader.PeekOrThrow();
				}
				CandidId? serviceReferenceId;
				CandidServiceType service;
				if (nextChar == '{')
				{
					string serviceBody = textReader.ReadToEnd();
					serviceReferenceId = null;
					service = CandidTextParser.Parse<CandidServiceType>("service " + serviceBody);
				}
				else
				{
					var reference = CandidTextParser.Parse<CandidReferenceType>(textReader.ReadNextWord()!);
					serviceReferenceId = reference.Id;
					service = (CandidServiceType)declaredTypes[reference.Id];
				}

				return new CandidServiceDescription(serviceReferenceId, service, declaredTypes);
			}
		}

		private static string GetType(StringReader textReader)
		{
			var stringBuilder = new StringBuilder();

			bool bracesStarted = false;
			int bracesLeft = 0;
			while (!bracesStarted || bracesLeft > 0)
			{
				char c = textReader.ReadOrThrow();
				if (c == '{')
				{
					bracesStarted = true;
					bracesLeft++;
				}
				else if (c == '}')
				{
					bracesLeft--;
				}
				// end of type alias
				else if (!bracesStarted && c == ';')
				{
					break;
				}
				stringBuilder.Append(c);
			}
			if (textReader.PeekOrDefault() == ';')
			{
				textReader.ReadOrThrow();
			}
			return stringBuilder.ToString();
		}
	}

	internal static class StringReaderExtensions
	{
		public static string? ReadNextWord(this StringReader reader, string? expectedValue = null)
		{
			reader.SkipWhitespace();
			var stringBuilder = new StringBuilder();
			char? wordChar = reader.PeekOrDefault();
			while (wordChar != null && !char.IsWhiteSpace(wordChar.Value))
			{
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
