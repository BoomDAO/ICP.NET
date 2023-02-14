using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace EdjCase.ICP.Candid.Parsers
{
	internal static class CandidServiceFileParser
	{
		public static CandidServiceDescription Parse(string text)
		{
			text = Regex.Replace(text, "//.*\r?\n", ""); // Remove comments
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
					declaredTypes.Add(CandidId.Create(name), type);
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

				return new CandidServiceDescription(service, declaredTypes, serviceReferenceId);
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

}
