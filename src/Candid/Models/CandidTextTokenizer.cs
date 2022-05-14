using ICP.Candid.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ICP.Candid.Models
{
    public class CandidTextTokenHelper
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

    public static class CandidTextTokenizer
    {
        public static CandidTextTokenHelper Tokenize(string candidText)
        {
            ReadOnlySpan<char> textSpan = candidText.AsSpan();
            int index = 0;
            var tokens = new List<CandidTextToken>();
            while (index < textSpan.Length)
            {
                char c = textSpan[index];
                while (char.IsWhiteSpace(c))
                {
                    c = textSpan[++index];
                }
                CandidTextTokenType tokenType = GetType(c);
                string? text = null;
                if (tokenType == CandidTextTokenType.Text)
                {
                    int startIndex = index;
                    do
                    {
                        if(index >= textSpan.Length - 1)
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
                ':' => CandidTextTokenType.Colon,
                ';' => CandidTextTokenType.SemiColon,
                '.' => CandidTextTokenType.Period,
                _ => CandidTextTokenType.Text,
            };
        }
    }

    public class CandidTextToken
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

    public enum CandidTextTokenType
    {
        OpenParenthesis,
        CloseParenthesis,
        OpenCurlyBrace,
        CloseCurlyBrace,
        Colon,
        SemiColon,
        Text,
        Period
    }
}