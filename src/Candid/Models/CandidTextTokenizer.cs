using EdjCase.ICP.Candid.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models
{
	internal class CandidTextTokenHelper
	{
		public List<CandidTextToken> Tokens { get; }
		public int CurrentTokenIndex { get; private set; }

		public CandidTextToken? PreviousToken => this.Tokens.ElementAtOrDefault(this.CurrentTokenIndex - 1);
		public CandidTextToken CurrentToken => this.Tokens[this.CurrentTokenIndex];
		public CandidTextToken? NextToken => this.Tokens.ElementAtOrDefault(this.CurrentTokenIndex + 1);
		public CandidTextTokenHelper(List<CandidTextToken> tokens)
		{
			if (tokens?.Any() != true)
			{
				throw new ArgumentNullException(nameof(tokens));
			}
			this.Tokens = tokens;
			this.CurrentTokenIndex = 0;
		}

		public bool MoveNext()
		{
			if (this.CurrentTokenIndex >= this.Tokens.Count - 1)
			{
				return false;
			}
			this.CurrentTokenIndex++;
			return true;
		}
		public void MoveNextOrThrow()
		{
			if (!this.MoveNext())
			{
				throw new CandidTextParseException("Unexpected end of text");
			}
		}
	}

	internal static class CandidTextTokenizer
	{
		public static CandidTextTokenHelper Tokenize(string candidText)
		{
			ReadOnlySpan<char> textSpan = candidText.AsSpan();
			int index = 0;
			var tokens = new List<CandidTextToken>();
			while (index < textSpan.Length)
			{
				char c = textSpan[index];
				bool end = false;
				while (char.IsWhiteSpace(c))
				{
					index++;
					if (index >= textSpan.Length)
					{
						end = true;
						break;
					}
					c = textSpan[index];
				}
				if (end)
				{
					break;
				}
				CandidTextTokenType tokenType = GetType(c);
				string? text = null;
				if (tokenType == CandidTextTokenType.Text)
				{
					int startIndex = index;
					do
					{
						if (index >= textSpan.Length - 1)
						{
							index++;
							break;
						}
						c = textSpan[++index];
					}
					while (GetType(c) == CandidTextTokenType.Text && !char.IsWhiteSpace(c));


					text = textSpan
						.Slice(startIndex, index - startIndex)
						.ToString();
					if (string.IsNullOrEmpty(text))
					{
						break;
					}
					index--; // Account for the 'lost' character
				}
				tokens.Add(new CandidTextToken(tokenType, text));
				index++;
			}
			return new CandidTextTokenHelper(tokens);
		}

		private static CandidTextTokenType GetType(char c)
		{
			return c switch
			{
				'(' => CandidTextTokenType.OpenParenthesis,
				')' => CandidTextTokenType.CloseParenthesis,
				'{' => CandidTextTokenType.OpenCurlyBrace,
				'}' => CandidTextTokenType.CloseCurlyBrace,
				'[' => CandidTextTokenType.OpenBracket,
				']' => CandidTextTokenType.CloseBracket,
				':' => CandidTextTokenType.Colon,
				';' => CandidTextTokenType.SemiColon,
				'.' => CandidTextTokenType.Period,
				',' => CandidTextTokenType.Comma,
				_ => CandidTextTokenType.Text,
			};
		}
	}

	internal class CandidTextToken
	{
		public CandidTextTokenType Type { get; }
		private string? Text { get; }
		public CandidTextToken(CandidTextTokenType type, string? text)
		{
			this.Type = type;
			this.Text = text;
		}

		public string GetTextValueOrThrow()
		{
			this.ValidateType(CandidTextTokenType.Text);
			return this.Text ?? throw new CandidTextParseException("Expected text value, got null");
		}

		internal void ValidateType(CandidTextTokenType type)
		{
			if (this.Type != type)
			{
				throw new CandidTextParseException($"Expected type '{type}', got '{this.Type}'");
			}
		}
	}

	internal enum CandidTextTokenType
	{
		OpenParenthesis,
		CloseParenthesis,
		OpenCurlyBrace,
		CloseCurlyBrace,
		OpenBracket,
		CloseBracket,
		Colon,
		SemiColon,
		Text,
		Period,
		Comma
	}
}