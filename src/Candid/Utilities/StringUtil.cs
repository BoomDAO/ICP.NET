using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Values
{
	internal static class StringUtil
	{
		public static string ToSnakeCase(string value)
		{
			IEnumerable<string> tokens = GetTokens(value);
			return string.Join("_", tokens);
		}
		public static string ToPascalCase(string value)
		{
			IEnumerable<string> tokens = GetTokens(value)
				.Select(t => char.ToUpper(t[0]) + t.Substring(1));
			return TransformFirstAlpha(string.Join("", tokens), char.ToUpper); // Handles non letter first char
		}
		public static string ToCamelCase(string value)
		{
			IEnumerable<string> tokens = GetTokens(value)
				.Select((t, i) => (i == 0 ? t[0] : char.ToUpper(t[0])) + t.Substring(1));
			return TransformFirstAlpha(string.Join("", tokens), char.ToLower); // Handles non letter first char
		}

		public static string TransformFirstAlpha(string stringValue, Func<char, char> transform)
		{
			int i = 0;
			while (i < stringValue.Length && !Char.IsLetter(stringValue[i]))
			{
				i++;
			}
			if (i == stringValue.Length)
			{
				return stringValue;
			}
			string newValue = "";
			if (i > 0)
			{
				newValue += stringValue.Substring(0, i);
			}
			newValue += transform(stringValue[i]);
			if (i + 1 < stringValue.Length)
			{
				newValue += stringValue.Substring(i + 1);
			}
			return newValue;
		}

		private static IEnumerable<string> GetTokens(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentNullException(nameof(value));
			}
			if (value.Contains('_'))
			{
				return SplitOn('_');
			}

			var tokens = new List<string>();
			int lastIndex = 0;
			int i = 0;
			for (; i < value.Length; i++)
			{
				char c = value[i];
				if (i != 0 && char.IsUpper(c))
				{
					int length = (i - lastIndex);
					string token = value.Substring(lastIndex, length);
					lastIndex = i;
					tokens.Add(token);
				}
			}
			if (lastIndex < i)
			{
				string lastToken = value.Substring(lastIndex);
				tokens.Add(lastToken);
			}
			return tokens.Select(t => t.ToLower());

			IEnumerable<string> SplitOn(char c)
			{
				bool startsWithChar = value.StartsWith(c);
				IEnumerable<string> v = value
					.Split(new[] { c }, StringSplitOptions.RemoveEmptyEntries)
					.Select(s => s.ToLower());
				if (startsWithChar)
				{
					v = v.Prepend(c.ToString());
				}
				return v;
			}
		}

	}
}