using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Values
{
	internal static class StringUtil
	{
		public static string ToSnakeCase(string value)
		{
			IEnumerable<string> tokens = StringUtil.GetTokens(value);
			return string.Join("_", tokens);
		}
		public static string ToPascalCase(string value)
		{
			IEnumerable<string> tokens = StringUtil.GetTokens(value)
				.Select(t => char.ToUpper(t[0]) + t.Substring(1));
			return string.Join("", tokens);
		}
		public static string ToCamelCase(string value)
		{
			IEnumerable<string> tokens = StringUtil.GetTokens(value)
				.Select((t, i) => (i == 0 ? t[0] : char.ToUpper(t[0])) + t.Substring(1));
			return string.Join("", tokens);
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
			if (value.Contains('-'))
			{
				return SplitOn('-');
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
				return value
					.Split(new[] { c }, StringSplitOptions.RemoveEmptyEntries)
					.Select(s => s.ToLower());
			}
		}

	}
}